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
        
        
         private BigInteger gcd(BigInteger a, BigInteger b)
        {
            //Essentially A will equal B and the new value of B shall be A mod B until B is 0
            if (a == 0) { return b; }
            return gcd(b % a, a);
        }
         private BigInteger ModInverse(BigInteger e, BigInteger phi) 
        {
            //Keep increasing d for as long as it's under phi, return d if the remainder of the product of e and d with phi is one
            for (BigInteger d = 2; d < phi; d++) 
            {
            if((e*d)%phi == 1) { return d;}
            }
            return -1;
        
        }

        public List<BigInteger> GenerateKeys()
        {
            //Normal RSA encryption algorithm, idk why the maths work, not a number theorist
            BigInteger p = 7919;
            BigInteger q = 1009;
            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger e;
            for (e = 2; e < phi; e++)
            {
                if (gcd(e, phi) == 1) { break; }
            }
            BigInteger d = ModInverse(e, phi);

            return [e, d, n];
        }
        

        public BigInteger Encrypt_text(BigInteger m, BigInteger e, BigInteger n)
        {

            return BigInteger.ModPow(m, e, n);
        }

        public int decrypt_text(BigInteger m, BigInteger d, BigInteger n)
        {
            return (int)BigInteger.ModPow(m,d,n);
        }


    }
}
