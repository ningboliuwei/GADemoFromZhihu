using System;
using System.Linq;

namespace GADemoFromZhihu
{
    internal static class Program
    {
        //染色体数量
        private const int ChromosomeQuantity = 300;
        //染色体长度
        private const int ChromosomeLength = 17;
        //存活率
        private const double RetainRate = 0.2;
        //变异率
        private const double MutationRate = 0.3;
        //随机选择率
        private const double SelectionRate = 0.01;
        //精确度
        private const double Precision = 0.00000000000001;
        //进化代数
        private const int GenerationQuantity = 200;

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        private static void Main()
        {
            var population = new Population
            {
                RetainRate = RetainRate,
                SelectionRate = SelectionRate,
                MutationRate = MutationRate,
                ChromosomeLength = ChromosomeLength
            };

            //随机生成染色体
            population.RandomGenerate(ChromosomeQuantity);

//            foreach (var p in population.Chromosomes.OrderByDescending(c => c.Fitnetss))
//                Console.WriteLine(p.Output());

            for (var i = 0; i < GenerationQuantity; i++)
            {
                population.Envolve();

                //找出拥有每一代最高 Fitnetss 值的那个实际的解
                var maxFitnetss = population.Chromosomes.Max(n => n.Fitnetss);
                var result = from c in population.Chromosomes
                    where Math.Abs(c.Fitnetss - population.Chromosomes.Max(n => n.Fitnetss)) < Precision
                    select c.DecodedValue;

                Console.WriteLine($@"after {i + 1:000} envolve(s): {result.ToList()[0]}, {maxFitnetss}" +
                                  Environment.NewLine);
            }

            Console.ReadLine();
        }
    }
}