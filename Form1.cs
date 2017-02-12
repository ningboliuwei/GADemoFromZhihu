using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GADemoFromZhihu
{
    public partial class Form1 : Form
    {
        //种群中的染色体数量
        private const int ChromosomeCount = 10;
        //染色体长度
        private const int ChromosomeLength = 17;

        private const double RetainRate = 0.2;
        //变异率
//        private double mutationRate = 0.01;
//
        //随机选择率
        private const double RandomSelectRate = 0.5;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var population = new Population(ChromosomeLength, ChromosomeCount)
            {
                RetainRate = RetainRate,
                RandomSelectRate = RandomSelectRate
            };

            textBox1.Text = "";

            foreach (var p in population.Chromosomes.OrderByDescending(c=>c.Fitnetss))
                textBox1.Text += Convert.ToString(p.Value, 2).PadLeft(ChromosomeLength, '0').PadRight(20,' ') +  p.DecodedValue.ToString().PadRight(20, ' ') + p.Fitnetss.ToString().PadRight(20, ' ') + Environment.NewLine;

            textBox1.Text += @"-------------------------" + Environment.NewLine;

            foreach (var s in population.Select())
            {
                textBox1.Text += Convert.ToString(s.Value, 2).PadLeft(ChromosomeLength, '0').PadRight(20, ' ') + s.DecodedValue.ToString().PadRight(20, ' ') + s.Fitnetss.ToString().PadRight(20, ' ') + Environment.NewLine;
            }
        }
    }
}