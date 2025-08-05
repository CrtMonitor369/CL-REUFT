using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class GeneralUtilityFunctions
    {
       static public string EscapeIt(string value)
        {
            var builder = new StringBuilder();
            foreach (var cur in value)
            {
                switch (cur)
                {
                    case '\t':
                        builder.Append(@"\t");
                        break;
                    case '\r':
                        builder.Append(@"\r");
                        break;
                    case '\n':
                        builder.Append(@"\n");
                        break;
                    // etc ...
                    default:
                        builder.Append(cur);
                        break;
                }
            }
            return builder.ToString();
        }
        static public long CalculatePower(long number, long powerOf)
        {
            long result = number;
            for (int i = 2; i <= powerOf; i++)
                result *= number;
            return result;
        }
    }
}
