using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CL_REUFT
{
    internal class BucketSort
    {
        public static List<int> bucketsort(List<int> thelist) 
        {
            int max_val = thelist.Max();
            int size = max_val / thelist.Count;

            List<List<int>> buckets = new List<List<int>>();
            for (int i = 0; i < max_val; i++) { buckets.Add(new List<int>()); }
            foreach (int num in thelist) 
            {
                int index = Convert.ToInt16(num / size);
                buckets[index].Add(num);
            }
            List<int> SortedList = new List<int>();
            foreach(List<int> bucket in buckets) 
            {
            foreach(int num in CL_REUFT.MaxSort.minSort(bucket)) 
                {
                SortedList.Add(num);
                }
            }
            return SortedList;


        }


    }
}
