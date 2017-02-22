using System;
using System.Linq;

namespace GADemoFromZhihu
{
    internal static class Program
    {
        //染色体数量
        private const int ChromosomeQuantity = 300;
        //染色体长度
        private const int ChromosomeLength = 11;
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

        private const int SubValueQuantity = 1;

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        private static void Main()
        {
            var population = new Population(RetainRate, SelectionRate, MutationRate, ChromosomeLength,
                ChromosomeQuantity, SubValueQuantity);

            //
            //            //随机生成染色体
            population.RandomGenerateChromosome();

            //随机生成用于 2 个值的染色体（）
            //            population.RandomGenerateMultipleParametersChromosome(ChromosomeQuantity, 2);
            //
            //            foreach (var c in population.Chromosomes)
            //            {
            //                var values = c.GetPartlyValues(2);
            //
            //                Console.WriteLine($@" {Convert.ToString(c.Value, 2).PadLeft(ChromosomeLength, '0').PadRight(30) }{values[0]}, {values[1]}, {TestFunction.BranchTest(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]))}, {100000 - c.Fitness}");
            //                //                Console.WriteLine($@"after {i + 1:000} envolve(s): {Convert.ToString(result.v, 2).PadLeft(ChromosomeLength, '0').PadRight(30) }{result.paras[0]}, {result.paras[1]}, {maxFitnetss} {TestFunction.BranchTest(Convert.ToInt32(result.paras[0]), Convert.ToInt32(result.paras[1]))}");
            //            }


            //            foreach (var p in population.Chromosomes.OrderByDescending(c => c.Fitness))
            //            {
            //                Console.WriteLine(p.Output());
            //            }
            //            foreach (var p in population.Chromosomes.OrderByDescending(c => c.Fitness))
            //            {
            //                Console.Write(Convert.ToString(p.Value, 2).PadLeft(ChromosomeLength, '0') + " ");
            //
            //                var values = p.GetPartlyValues(3);
            //
            //                foreach (var value in values)
            //                {
            //                    Console.Write(value + " ");
            //                }
            //
            //                Console.WriteLine();
            //            }
            //
            for (var i = 0; i < GenerationQuantity; i++)
            {
                population.Envolve();

                //找出拥有每一代最高 Fitnetss 值的那个实际的解
                var maxFitnetss = population.Chromosomes.Max(n => n.Fitness);
                //                                var result = from c in population.Chromosomes
                //                                    where Math.Abs(c.Fitness - population.Chromosomes.Max(n => n.Fitness)) < Precision
                //                                    select c.GetDecodedValue(c.Value);
                var result = (from c in population.Chromosomes
                    where c.Fitness == maxFitnetss
                    select c).First();
                //
                Console.WriteLine($@"after {i + 1:000} envolve(s):{OutputHelper.DisplayChromosomeInfo(result)}");




                //            Console.WriteLine("0 —— 非三角形， 1 —— 等边三角形，2 —— 等腰三角形，3 —— 一般三角形");
                //
                //            for (int i = 0; i < 100; i++)
                //            {
                //                Console.WriteLine("Input x y z");
                //                var values = (from s in Console.ReadLine().Split(' ') select Convert.ToInt32(s)).ToList();
                //                Console.WriteLine(TestFunction.TriangleType(values[0], values[1], values[2]));
                //            }
               
            }
            Console.ReadKey();
        }
    }
}