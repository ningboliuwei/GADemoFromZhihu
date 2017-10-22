using System;
using System.Linq;
using System.Text;

namespace GADemoFromZhihu
{
    internal static class Program
    {
        //染色体数量
        private const int ChromosomeQuantity = 100;

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
            var builder = new StringBuilder();

            //指定测试函数的解空间上下界
            TestFunction.SetBound(0, 9);
            //随机生成染色体
            population.RandomGenerateChromosome();

            #region 随机生成三角形测试数据

            //            var rnd = new Random();
            //
            //            for (int i = 0; i < 1000; i++)
            //            {
            //                var x = rnd.Next(1, 10);
            //                var y = rnd.Next(1, 10);
            //                var z = rnd.Next(1, 10);
            //
            //                Console.WriteLine($"{x},{y},{z} {TestFunction.TriangleTypeTest(x,y,z)}");
            //            }

            #endregion

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

            #region 针对日期函数随机生成测试数据

            //            var rnd = new Random();
            //
            //            for (int i = 0; i < 10000; i++)
            //            {
            //                int year = rnd.Next(1950, 2050);
            //                int month = rnd.Next(1, 12);
            //                int day = rnd.Next(1, 31);
            //
            //                builder.Clear();
            //                builder.Append(i + 1).Append(": ");
            //                builder.Append($"{year}/{month}/{day} ");
            //                builder.Append($"{TestFunction.NextDate(year, month, day)} ");
            //
            //                Console.WriteLine(builder);
            //
            //                if (DateTime.IsLeapYear(year) && month == 2 && day == 29)
            //                {
            //                    break;
            //                }
            //            }

            #endregion


            #region 进化 N 代

//
            for (var i = 0; i < GenerationQuantity; i++)
            {
//                foreach (var c in population.Chromosomes)
//                {
//                    builder.Clear();
//                    builder.Append($"{OutputHelper.DisplayChromosomeBinaryValue(c)} ");
//
//                    //                            所有映射到解空间的值（若有级联）
//                    //                    foreach (var value in d.SubValues)
//                    //                    {
//                    //                        builder.Append($"{d.GetDecodedValue(value)} ");
//                    //                    }
//
//                    var x = c.GetDecodedValue(c.SubValues[0]);
//                    builder.Append($"{x}");
//                    builder.Append(" | fitness: ");
//                    builder.Append(c.Fitness);
//
//                    //                    Console.WriteLine(builder);
//                }

                //找出拥有每一代最高 Fitnetss 值的那个实际的解
                var maxFitness = population.Chromosomes.Max(n => n.Fitness);
                var mostFittest = population.Chromosomes.First(e => Equals(e.Fitness, maxFitness));

                builder.Clear();
                builder.Append($"after {i + 1:000} envolve(s): ");
              

                //所有映射到解空间的值（若有级联）
                var decodedSubValuesString = string.Join(" ", mostFittest.SubValues.Select(v =>
                    mostFittest.GetDecodedValue(v).ToString()).ToArray());

                //染色体二进制表示 | 所有子值（即多输入参数拼接） | 适应度
                builder.Append(
                    $"{OutputHelper.DisplayChromosomeBinaryValue(mostFittest)} | {decodedSubValuesString} | fitness: {mostFittest.Fitness}");
                // builder.Append ($"fitness: {TestFunction.StubbedTriangleTypeTestPathCoverage(a, b, c)} ");
                // builder.Append ($"result: {TestFunction.TriangleTypeTest(a, b, c)} ");
                // builder.Append ($"path: {TestFunction.TriangleTypeTestPathCoverage(a, b, c)}");
                Console.WriteLine(builder);

                //进化过程中不同的选择策略
                population.Envolve(Population.SelectType.Hybrid);
            }

//            for (var i = 0; i < 100; i++)
//            {
//                var s = Console.ReadLine();
//                var year = Convert.ToInt32(s.Split(' ')[0]);
//                var month = Convert.ToInt32(s.Split(' ')[1]);
//                var day = Convert.ToInt32(s.Split(' ')[2]);
//
//                Console.WriteLine(TestFunction.NextDate(year, month, day));
//            }

            #endregion

            Console.ReadKey();
        }
    }
}