using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class SortHelper
    {

        public static byte Kth_element(Byte[] Array, int T)
        {

            int k = 0;
            int counter = T;
            byte sum = 0;
            byte max;
            byte min;
            int max_Index;
            int min_Index;
            while (counter != 0)
            {
                max = 0;
                min = 255;
                max_Index = -1;
                min_Index = -1;
                for (int i = 0; i < Array.Length - 1; i++)
                {
                    if (Array[i] > max)
                    {
                        max = Array[i];
                        max_Index = i;
                    }
                    else if (Array[i] < min)
                    {
                        min = Array[i];
                        min_Index = i;
                    }
                    List<byte> list = new List<byte>(Array);
                    if (max_Index != -1)

                        list.RemoveAt(max_Index);

                    if (min_Index != -1)

                        list.RemoveAt(min_Index);

                    Array = list.ToArray();

                }
                counter--;

            }
            for (int i = 0; i < Array.Length - 1; i++)
            {
                k = k + 1;


                sum = (byte)(sum + Array[i]);

            }

            if (k == 0) {return Array[0]; }
            return (byte)(sum / k);
             //throw new NotImplementedException();
        }

        public static Byte[] CountingSort(Byte[] Array)
        {
            // TODO: Implement the Counting Sort alogrithm on the input array
            int[] collect = new int[256];
            for (int i = 0; i < Array.Length; i++)
            {
                int value = Array[i];
                collect[value]++;
            }

            byte[] result = new byte[Array.Length];
            int j = 0;

            for (int i = 0; i < collect.Length; i++)
            {
                while (collect[i] > 0)
                {
                    result[j] = (byte)i;
                    collect[i]--;
                    j++;
                }


            }
            return result;
            // Remove the next line
            //throw new NotImplementedException();
        }

        public static byte[] QuickSort(Byte[] Array, int leftIndex, int rightIndex)
        {
            // TODO: Implement the Quick Sort alogrithm on the input array
            var i = leftIndex;
            var j = rightIndex;
            var pivot = Array[leftIndex];
            while (i <= j)
            {
                while (Array[i] < pivot)
                {
                    i++;
                }

                while (Array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    byte temp = Array[i];
                    Array[i] = Array[j];
                    Array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                QuickSort(Array, leftIndex, j);
            if (i < rightIndex)
                QuickSort(Array, i, rightIndex);
            // Remove the next line
            // throw new NotImplementedException();
            return Array;
        }
    }
}