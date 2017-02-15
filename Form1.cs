using System;
using System.Windows.Forms;

namespace GADemoFromZhihu
{
    public partial class Form1 : Form
    {
        //种群中的染色体数量
        private const int ChromosomeCount = 10;
        //染色体长度
        private const int ChromosomeLength = 10;

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
//            var population = new Population(ChromosomeLength, ChromosomeCount)
//            {
//                RetainRate = RetainRate,
//                SelectionRate = RandomSelectRate
//            };
//
//            textBox1.Text = "";
//
//            foreach (var p in population.Chromosomes.OrderByDescending(c => c.Fitnetss))
//                textBox1.Text += p.Output();
//
//            textBox1.Text += @"-------------------------" + Environment.NewLine;
//
//            var parents = population.Select();
//            foreach (var s in parents)
//            {
//                textBox1.Text += s.Output();
//            }
//
//            textBox1.Text += @"-------------------------" + Environment.NewLine;
//
//            var children = population.Crossover(parents);
//            foreach (var c in children)
//            {
//                textBox1.Text += c.Output();
//            }
//
//            Console.WriteLine("hello world");
        }
    }
}