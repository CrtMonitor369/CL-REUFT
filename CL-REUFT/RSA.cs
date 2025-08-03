using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class RSA
    {
        static private int gcd(int a, int b)
        {
            //Essentially A will equal B and the new value of B shall be A mod B until B is 0
            if (b == 0) { return a; }
            return gcd(b, a % b);
        }

        public (string, int, int) encrypt_text(string text, List<int> primes)
        {

            //User supplied two primes
            StringBuilder encrypted_message = new StringBuilder();
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
                encrypted_message.Append((char)((Math.Pow((int)character, e) % n))); //Characters limited to the ones c# can display;
            }

            return (encrypted_message.ToString(), d, n);
        }

        public string decrypt_text(string encrypted_text, int private_key, int product_of_primes)
        {
            StringBuilder decrypted_text= new StringBuilder();
            foreach (char character in encrypted_text)
            {
                Console.WriteLine(character);
                Console.WriteLine((Math.Pow(Convert.ToUInt16(character), private_key) % product_of_primes));
                decrypted_text.Append ( (char) (Math.Pow( (int) (character), private_key) % product_of_primes) );
            }
            return decrypted_text.ToString();
        }


    }
}
