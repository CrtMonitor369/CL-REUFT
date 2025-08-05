using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class RSA
    {
        static private long gcd(long a, long b)
        {
            //Essentially A will equal B and the new value of B shall be A mod B until B is 0
            if (b == 0) { return a; }
            return gcd(b, a % b);
        }
        public static long ModInverse(long e, long phi)
        {
            long t = 0, newT = 1;
            long r = phi, newR = e;

            while (newR != 0)
            {
                long quotient = r / newR;

                long tempT = t;
                t = newT;
                newT = tempT - quotient * newT;

                long tempR = r;
                r = newR;
                newR = tempR - quotient * newR;
            }

            if (r > 1) throw new ArgumentException("e has no modular inverse modulo phi.");
            if (t < 0) t += phi;

            return t;
        }

        public (List<long>, long, long) Encrypt_text(string text, List<long> primes)
        {
            List<long> StringAsInt = new List<long>();
            foreach (char character in text) {
                StringAsInt.Add(Convert.ToInt32(character));
                
            }
          
            long Modulus =primes[0]*primes[1];
            long Totient = (primes[0]-1) * (primes[1]-1);
            long Public_Key_Exponent = long.MaxValue;
            for (int i = 2; i< Totient; i++) 
            {
            if((gcd(i, Totient) == 1)) 
                {
                    Public_Key_Exponent = i;
                    break;
                }
            }

            long Private_Key_Exponent = 0;
            long Temporary_Variable = 0;

            //TODO : Working Congurence algorithm
            Private_Key_Exponent = ModInverse(Public_Key_Exponent, Totient);
            //Insert Congurence Algorithm here

            List<long> Encrypted_numbers = new List<long>();
         
            foreach (long number in StringAsInt) 
            {
                Console.WriteLine(number);
            Encrypted_numbers.Add(GeneralUtilityFunctions.CalculatePower(number, Public_Key_Exponent)%Modulus);
            }
            return (Encrypted_numbers, Private_Key_Exponent, Modulus);

            //User supplied two primes
            /*StringBuilder encrypted_message = new StringBuilder();
   
            int p = primes[0];
            int q = primes[1];


            int n = p * q;
            int e=int.MaxValue;
            int t = (p - 1) * (q - 1);

         

            for (int i = 2; i < t; i++)
            {
                if (gcd(i, t) == 1)
                {
                    e = i;
                    break;
                }
            }
            int j = 0;
            int d = 0;

            while (true)
            {
                if ((j * e) % t == 1)
                {
                    d = j;
                    break;
                }
                j++;
            }
            
          
            foreach (char character in text)
            {
                encrypted_message.Append((char)((Math.Pow((int)character, e) % n)));
            }
            var encrypted_msg_str = encrypted_message.ToString();
            return (encrypted_msg_str, d, n);*/



        }

        public string decrypt_text(List<long> Encrypted_message, long private_key, long product_of_primes)
        {
            string Decrypted_Message = "";
           foreach(long number in Encrypted_message) 
            {
            Decrypted_Message = Decrypted_Message+(char)(GeneralUtilityFunctions.CalculatePower(number, private_key)%product_of_primes);
            }
            return Decrypted_Message;
        }


    }
}
