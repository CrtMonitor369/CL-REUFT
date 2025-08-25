using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class MaxSort
    {
        /*
         * Create a new list, while the og list is not empty add the max of it to the start of the new list and remove this element from the original list
         */
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

        static public List<int> minSort(List<int> list)
        {
            List<int> new_list = new List<int>();
            while (list.Count > 0)
            {
                new_list.Add(list.Min());
                list.RemoveAt(list.IndexOf(list.Min()));
            }
            return new_list;
        }

    }



}
