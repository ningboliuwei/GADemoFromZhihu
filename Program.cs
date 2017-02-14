using System;
using System.Collections.Generic;
using System.Linq;

namespace GADemoFromZhihu
{
    static class Program
    {
        private const int ChromosomeQuantity = 300;
        //染色体长度
        private const int ChromosomeLength = 17;

        //存活率
        private const double RetainRate = 0.2;
        //变异率
        private const double MutationRate = 0.3;
        //随机选择率
        private const double SelectionRate = 0.01;

        private const double Precision = 0.00000000000001;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //种群中的染色体数量


            var population = new Population()
            {
                RetainRate = RetainRate,
                SelectionRate = SelectionRate,
                MutationRate = MutationRate,
                ChromosomeLength = ChromosomeLength,
            };

           population.RandomGenerate(ChromosomeQuantity);

//            foreach (var p in population.Chromosomes.OrderByDescending(c => c.Fitnetss))
//                Console.WriteLine(p.Output());

            for (int i = 0; i < 200; i++)
            {
                population.Envolve();

                var result = from c in population.Chromosomes
                             where Math.Abs(c.Fitnetss - population.Chromosomes.Max(n=>n.Fitnetss)) < Precision
                             select c.DecodedValue;

                Console.WriteLine($@"after {i + 1} envolve(s): {result.ToList()[0]}"  +Environment.NewLine);
               
            }

            Console.ReadLine();
        }
    }
}
