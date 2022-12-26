using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class AdaptiveMedianFilter
    {

        public static Byte[,] ApplyFilter(Byte[,] ImageMatrix, int MaxWindowSize, int UsedAlgorithm)
        {
            byte[] a;
            byte zmed, zmin, zmax, A1, A2, B1, B2;
            int windowsize;
            for (int i = 0; i < ImageOperations.GetHeight(ImageMatrix); i++)
            {
                for (int j = 0; j < ImageOperations.GetWidth(ImageMatrix); j++)
                {
                    windowsize = 3;
                    while (true)
                    {
                        var arr = new ArrayList();
                        for (int x = i - (windowsize / 2); x <= i + (windowsize / 2); x++)
                        {

                            if (x < 0 || x >= ImageOperations.GetHeight(ImageMatrix))
                            {

                                continue;
                            }

                            for (int y = j - (windowsize / 2); y <= j + (windowsize / 2); y++)
                            {
                                if (y < 0 || y >= ImageOperations.GetWidth(ImageMatrix))
                                {

                                    continue;
                                }

                                arr.Add(ImageMatrix[x, y]);

                            }

                        }
                        a = new byte[arr.Count];
                        for (int K = 0; K < arr.Count; K++)
                        {
                            a[K] = (byte)arr[K];
                        }
                        if (UsedAlgorithm == 0)
                        {
                            a = SortHelper.QuickSort(a, 0, a.Length - 1);

                        }
                        else if (UsedAlgorithm == 1)
                        {
                            a = SortHelper.CountingSort(a);
                        }
                        if (a.Length % 2 == 0)
                        {
                            int x = (a[(a.Length / 2) - 1] + a[(a.Length / 2)]) / 2;
                            zmed = (byte)x;
                        }
                        else
                        {
                            zmed = a[(a.Length / 2)];
                        }
                        zmin = a[0];
                        zmax = a[a.Length - 1];
                        A1 = (byte)(zmed - zmin);
                        A2 = (byte)(zmax - zmed);
                        if (A1 > 0 && A2 > 0)
                        {
                            windowsize += 2;
                            if (windowsize > MaxWindowSize)
                            {
                                ImageMatrix[i, j] = zmed;
                                break;
                            }
                            else { continue; }
                        }
                        else
                        {
                            B1 = (byte)(ImageMatrix[i, j] - zmin);
                            B2 = (byte)(zmax - ImageMatrix[i, j]);
                            if (B1 <= 0 || B2 <= 0)
                            {
                                ImageMatrix[i, j] = zmed;
                            }
                            break;

                        }
                    }
                }



                //TODO: Implement adaptive median filter
                // For each pixel in the image:
                // 0) Start by window size 3×3
                // 1) Chose a non-noise median value (true median)
                // 2) Replace the center with the median value if not noise, or leave it and move to the next pixel
                // 3) Repeat the process for the next pixel starting from step 0 again

                // Remove the next line
                //throw new NotImplementedException;

            }
            return ImageMatrix;
        }
    }
}

