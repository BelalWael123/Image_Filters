using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class AlphaTrimFilter
    {
        public static Byte[,] ApplyFilter(Byte[,] ImageMatrix, int MaxWindowSize, int UsedAlgorithm, int TrimValue)
        {
            byte[,] result = new byte[ImageOperations.GetHeight(ImageMatrix), ImageOperations.GetWidth(ImageMatrix)];
            byte[] window = new byte[MaxWindowSize * MaxWindowSize];
            int limit = MaxWindowSize / 2;
            for (int i = 0; i < ImageOperations.GetHeight(ImageMatrix); i++)
            {
                for (int j = 0; j < ImageOperations.GetWidth(ImageMatrix); j++)
                {
                    int window_index = 0;

                    for (int z = i - limit; z <= i + limit; z++)
                    {

                        if (z < 0 || z >= ImageOperations.GetHeight(ImageMatrix))
                        {

                            continue;
                        }

                        for (int x = j - limit; x <= j + limit; x++)
                        {
                            if (x < 0 || x >= ImageOperations.GetWidth(ImageMatrix))
                            {

                                continue;
                            }

                            window[window_index] = ImageMatrix[z, x];
                            window_index++;
                        }
                    }
                    if (UsedAlgorithm == 0)
                    {
                        window = SortHelper.CountingSort(window);
                        int sum = 0;
                        for (int x = TrimValue; x < window.Length - TrimValue; x++)
                        {
                            sum += window[x];
                        }
                        byte avg = (byte)(sum / (window.Length - (TrimValue * 2)));
                        result[i, j] = avg;
                    }
                    else if (UsedAlgorithm == 1)
                    {
                        result[i, j] = SortHelper.Kth_element(window, TrimValue);
                    }
                }

            }
            return result;
        }
    }
}
