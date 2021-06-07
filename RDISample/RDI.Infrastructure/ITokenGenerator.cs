using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Infrastructure
{
    public interface ITokenGenerator
    {
        string GenerateToken(long cardNumber, int CVV);
    }
}
