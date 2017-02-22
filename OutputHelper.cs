using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADemoFromZhihu
{
    class OutputHelper
    {
        public const int TotalWidth = 30;

        public  static string DisplayChromosomeInfo(Chromosome chromosome)
        {
            return Convert.ToString(chromosome.Value, 2)
                       .PadLeft(chromosome.Population.ChromosomeLength, '0')
                       .PadRight(TotalWidth) + chromosome.GetDecodedValue(chromosome.Value);
        }
    }
}
