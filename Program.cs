using System;
using System.Linq;
using System.Text;

namespace GADemoFromZhihu
{
    internal static class Program
    {
        //染色体数量
        private const int ChromosomeQuantity = 300;
        //染色体长度
        private const int ChromosomeLength = 30;
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

        private const int SubValueQuantity = 2;

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        private static void Main()
        {
            var population = new Population(RetainRate, SelectionRate, MutationRate, ChromosomeLength,
                ChromosomeQuantity, SubValueQuantity);
            var builder = new StringBuilder();


            //指定测试函数的解空间上下界
            TestFunction.SetBound(0, 10000);
            //随机生成染色体
            population.RandomGenerateChromosome();

            #region 针对分支函数随机生成测试数据

            //            var rnd = new Random();
            //
            //            for (int i = 0; i < GenerationQuantity * ChromosomeQuantity; i++)
            //            {
            //                int dice = rnd.Next(0, 10001);
            //
            //                builder.Clear();
            //                builder.Append(i + 1).Append(": ");
            //                builder.Append(dice);
            //                builder.Append(" ");
            //                builder.Append($"{TestFunction.BranchTest1(dice)} ");
            //
            //
            //                Console.WriteLine(builder);
            //
            //                if (TestFunction.BranchTest1(dice) == "#ab")
            //                {
            //                    break;
            //                }
            //
            //            }

            #endregion

            #region 针对于一代中所有染色体的测试


            //                foreach (var c in population.Chromosomes)
            //                {
            //                    builder.Clear();
            //                    builder.Append($"{OutputHelper.DisplayChromosomeBinaryValue(c)} ");
            //
            //                    //所有映射到解空间的值（若有级联）
            ////                    foreach (var value in c.SubValues)
            ////                    {
            ////                        builder.Append($"{c.GetDecodedValue(value)} ");
            ////                    }
            //
            //                    int x = (int) c.GetDecodedValue(c.SubValues[0]);
            //                    int y = (int) c.GetDecodedValue(c.SubValues[1]);
            //
            //
            //                    builder.Append(TestFunction.BranchTest1(x, y));
            //                    builder.Append(TestFunction.StubbedBranchTest1(x, y));
            //
            //                    Console.WriteLine(builder);
            //                }

            #endregion

            #region 进化 N 代

            for (var i = 0; i < GenerationQuantity; i++)
            {
                population.Envolve();

                //找出拥有每一代最高 Fitnetss 值的那个实际的解
                var maxFitness = population.Chromosomes.Max(n => n.Fitness);

                var mostFittest = (from c in population.Chromosomes
                                   where Equals(c.Fitness, maxFitness)
                                   select c).First();

                int x = (int)mostFittest.GetDecodedValue(mostFittest.SubValues[0]);
                int y = (int)mostFittest.GetDecodedValue(mostFittest.SubValues[1]);

                builder.Clear();
                builder.Append($"after {i + 1:000} envolve(s): ");
                //染色体二进制表示
                builder.Append($"{OutputHelper.DisplayChromosomeBinaryValue(mostFittest)} ");

                //所有映射到解空间的值（若有级联）

                foreach (var subValue in mostFittest.SubValues)
                {
                    builder.Append(mostFittest.GetDecodedValue(subValue)).Append(" ");
                }


                builder.Append(TestFunction.BranchTest1(x, y));
                builder.Append(TestFunction.StubbedBranchTest1(x, y));
                Console.WriteLine(builder);
            }

            #endregion



            Console.ReadKey();
        }
    }
}