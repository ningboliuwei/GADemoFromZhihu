using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADemoFromZhihu
{
    class OutputHelper
    {
        public static string DisplayChromosomeBinaryValue(Chromosome chromosome)
        {
            return Convert.ToString(chromosome.Value, 2)
                .PadLeft(chromosome.Population.ChromosomeLength, '0');
        }

        public static string DisplayChromosomeSubValues(Chromosome chromosome)
        {
            string s = "";

            foreach (var value in chromosome.SubValues)
            {
                s += value + " ";
            }

            return s;
        }
    }
}