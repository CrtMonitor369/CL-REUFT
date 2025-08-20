using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class MaxSort
    {
        static public List<int> maxSort(List<int> list) 
        {
        List<int> new_list = new List<int>();
            while (list.Count > 0) 
            {
            new_list.Add(list.Max());
            list.RemoveAt(list.IndexOf(list.Max()));
            }
        return new_list;
        }
    }
}
