using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RDI.Entities;
using RDI.Infrastructure;
using RDI.Models.RequestModels;
using RDI.Models.ResponseModels;
using RDI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        public CardController(CardContext cardContext, ITokenGenerator tokenGenerator, ILogger logger)
        {
            CardContext = cardContext;
            TokenGenerator = tokenGenerator;
            Logger = logger;
        }

        public CardContext CardContext { get; }
        public ITokenGenerator TokenGenerator { get; }
        public ILogger Logger { get; }

        // GET: api/<CardController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets the card info based on input cardid
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cardId"></param>
        /// <param name="token"></param>
        /// <param name="cvv"></param>
        /// <returns></returns>
        [HttpPost("api/card/validate")]
        public async Task<IActionResult> validate(ValidateRequestModel validateRequestModel)
        {
            try
            {
                if (CardContext.FindCard(validateRequestModel.CardId))
                    return NotFound();
                var card = CardContext.GetCard(validateRequestModel.CardId);
                if (DateTime.UtcNow.Subtract(card.CreatedDate).TotalMinutes > 30)
                    return BadRequest();
                if (!card.CustomerId.Equals(validateRequestModel.CustomerId))
                    return BadRequest();
                if (!validateRequestModel.TokenId.Equals(TokenGenerator.GenerateToken(card.CardNumber, card.CVV)))
                    return BadRequest();
                return Ok(true);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception.Message);
                return StatusCode(500, new object[] { "Error occured while validating the card information." });
            }
        }

        // POST api/<CardController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] SaveRequestModel cardRequestModel)
        {
            try
            {
                var token =  TokenGenerator.GenerateToken(cardRequestModel.CardNumber, cardRequestModel.CVV);
                var cardId = Guid.NewGuid();

                CardContext.AddCard(new Card()
                {
                    CreatedDate = DateTime.Now,
                    CardId = cardId,
                    CardNumber = cardRequestModel.CardNumber,
                    CustomerId = cardRequestModel.CustomerId,
                    CVV = cardRequestModel.CVV,
                    TokenId = token
                });

                var response = new SaveResponseModel();
                response.CardId = cardId;
                response.TokenId = token;
                response.CreationDate = DateTime.UtcNow;
                return Ok(response);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception.Message);
                return StatusCode(500, new object[] { "Error occured while saving the card." });
            }
        }
    }
}
