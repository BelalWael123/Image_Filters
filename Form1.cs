using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace ImageFilters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[,] ImageMatrix;
        int Wmax = 1;
        int T = 1;
        int SelectedFilterID = 0;
        int UsedAlgorithm = 0;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
        }

        private void btnZGraph_Click(object sender, EventArgs e)
        {

            int start = 3;
            int wsize = Wmax;
            double[] x_values = new double[((wsize - start) / 2) + 1];
            if (SelectedFilterID == 0)
            {
                double[] y_values_counting = new double[((wsize - start) / 2) + 1];
                double[] y_values_kth = new double[((wsize - start) / 2) + 1];

                for (int i = 0; start < wsize; i++)
                {
                    x_values[i] = start;
                    double T1 = Environment.TickCount;
                    AlphaTrimFilter.ApplyFilter(ImageMatrix, start, 0, T);
                    double T2 = Environment.TickCount;
                    y_values_counting[i] = T2 - T1;
                    double T3 = Environment.TickCount;
                    AlphaTrimFilter.ApplyFilter(ImageMatrix, start, 1, T);
                    double T4 = Environment.TickCount;
                    y_values_kth[i] = T4 - T3;
                    start += 2; 
                }

                //Create a graph and add two curves to it
                ZGraphForm ZGF = new ZGraphForm("Alpha Trim Filter", "Window Size", "Time");
                ZGF.add_curve("using Counting Sort", x_values, y_values_counting, Color.Red);
                ZGF.add_curve("using Kth Element Mean", x_values, y_values_kth, Color.Blue);
                ZGF.Show();
            }
            else
            {
                double[] y_values_counting = new double[((wsize - start) / 2) + 1];
                double[] y_values_Quick = new double[((wsize - start) / 2) + 1];

                for (int i = 0; start < wsize; i++)
                {
                    x_values[i] = start;
                    double T1 = Environment.TickCount;
                    AdaptiveMedianFilter.ApplyFilter(ImageMatrix, start, 1);
                    double T2 = Environment.TickCount;
                    y_values_counting[i] = T2 - T1;
                    double T3 = Environment.TickCount;
                    AdaptiveMedianFilter.ApplyFilter(ImageMatrix, start, 0);
                    double T4 = Environment.TickCount;
                    y_values_Quick[i] = T4 - T3;
                    start += 2;
                }

                //Create a graph and add two curves to it
                ZGraphForm ZGF = new ZGraphForm("Adaptive Median Filter", "Window Size", "Time");
                ZGF.add_curve("using Counting Sort", x_values, y_values_counting, Color.Red);
                ZGF.add_curve("using Quik Sort", x_values, y_values_Quick, Color.Blue);
                ZGF.Show();
            }
            
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            if (SelectedFilterID == 0)
            {
                ImageOperations.DisplayImage(AlphaTrimFilter.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm, T), pictureBox2);
            }
            else
            {
                ImageOperations.DisplayImage(AdaptiveMedianFilter.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm), pictureBox2);
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAlgorithm.Visible = true;
            lbl_algorithm.Visible = true;
            if (cbFilter.SelectedIndex == 0)
            {
                label1.Visible = true;
                maxWindowSize.Visible = true;
                label2.Visible = true;
                trimmingValue.Visible = true;
                SelectedFilterID = 0;

                cbAlgorithm.Items.Clear();

                cbAlgorithm.Items.Add("Counting Sort");
                cbAlgorithm.Items.Add("Kth Smallest/Largest");
            }
            else
            {
                label1.Visible = true;
                maxWindowSize.Visible = true;
                label2.Visible = false;
                trimmingValue.Visible = false;
                SelectedFilterID = 1;

                cbAlgorithm.Items.Clear();

                cbAlgorithm.Items.Add("Quick Sort");
                cbAlgorithm.Items.Add("Counting Sort");
            }
        }

        private void cbAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsedAlgorithm = cbAlgorithm.SelectedIndex;
        }

        private void maxWindowSize_ValueChanged(object sender, EventArgs e)
        {
            Wmax = (int)maxWindowSize.Value;
        }

        private void trimmingValue_ValueChanged(object sender, EventArgs e)
        {
            T = (int)trimmingValue.Value;
        }
    }
}