using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class CaesarCipher
    {

        private static char ShiftLetter(char c, int ShiftAmount, int Modulus=27) 
        {

            if(Char.IsWhiteSpace(c)) return c;
            ShiftAmount%=Modulus-1; 
            int ShiftedCharAsInt = (Convert.ToInt32(c)+ShiftAmount);
           
            
            return Convert.ToChar(ShiftedCharAsInt);
        }

        public static string ShiftString(string words, int ShiftAmount, int Modulus = 27) 
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in words) {
                sb.Append(ShiftLetter(c, ShiftAmount));
            }

            return sb.ToString();
        }


    }
}
