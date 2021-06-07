using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDI.Infrastructure
{
    public class TokenGenerator : ITokenGenerator
    {
        public string GenerateToken(long cardNumber, int CVV)
        {
            string fourdigits = Convert.ToString(cardNumber);
            int[] digit = fourdigits.Substring(fourdigits.Length - 4, 4).Select(s=>Convert.ToInt32(s)).ToArray();
            for(int i=0; i< (int)(Math.Log10(CVV) + 1);i++)
            {
                int temp;
                for (int j = 0; j < digit.Length - 1; j++)
                {
                    temp = digit[0];
                    digit[0] = digit[j + 1];
                    digit[j + 1] = temp;
                }
            }

            return digit.Select(s => s.ToString()).ToString();
        }
    }
}
