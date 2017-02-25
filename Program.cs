using System;
using System.Linq;
using System.Text;

namespace GADemoFromZhihu
{
    internal static class Program
    {
        //染色体数量
        private const int ChromosomeQuantity = 3000;
        //染色体长度
        private const int ChromosomeLength = 33;
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

        private const int SubValueQuantity = 3;

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        private static void Main()
        {
            var population = new Population(RetainRate, SelectionRate, MutationRate, ChromosomeLength,
                ChromosomeQuantity, SubValueQuantity);
            var builder = new StringBuilder();


            //指定测试函数的解空间上下界
            TestFunction.SetBound(1, 1000);
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

            #region 针对于一代中所有染色体的测试

            //foreach (var c in population.Chromosomes)
            //{
            //    builder.Clear();
            //                            builder.Append($"{OutputHelper.DisplayChromosomeBinaryValue(c)} ");

            //            所有映射到解空间的值（若有级联）
            //                                foreach (var value in c.SubValues)
            //                                {
            //                                    builder.Append($"{c.GetDecodedValue(value)} ");
            //                                }

            //    var x = c.GetDecodedValue(c.SubValues[0]);
            //    var y = c.GetDecodedValue(c.SubValues[1]);
            //    var z = c.GetDecodedValue(c.SubValues[2]);

            //    builder.Append($"{(int)x}, {(int)y}, {(int)z} ");
            //    builder.Append(TestFunction.TriangleTypeTest(x, y, z));
            //    builder.Append(" | fitness: ");
            //    builder.Append(TestFunction.StubbedBranchTest1(x, y));

            //    Console.WriteLine(builder);
            //}

            #endregion

            #region 进化 N 代

            for (var i = 0; i < GenerationQuantity; i++)
            {
                foreach (var d in population.Chromosomes)
                {
                    builder.Clear();
//                    builder.Append($"{OutputHelper.DisplayChromosomeBinaryValue(d)} ");

//                            所有映射到解空间的值（若有级联）
//                    foreach (var value in d.SubValues)
//                    {
//                        builder.Append($"{d.GetDecodedValue(value)} ");
//                    }

                    var x = (int) d.GetDecodedValue(d.SubValues[0]);
                    var y = (int) d.GetDecodedValue(d.SubValues[1]);
                    var z = (int) d.GetDecodedValue(d.SubValues[2]);

                    builder.Append($"{x}, {y}, {z} ");
                    builder.Append(TestFunction.TriangleTypeTest(x, y, z));
                    builder.Append(" | ");
                    builder.Append(TestFunction.StubbedTriangleTypeTest(x, y, z));

//                    Console.WriteLine(builder);
                }


                //找出拥有每一代最高 Fitnetss 值的那个实际的解
                var maxFitness = population.Chromosomes.Max(n => n.Fitness);

                var mostFittest = (from e in population.Chromosomes
                    where Equals(e.Fitness, maxFitness)
                    select e).First();

                var a = (int) mostFittest.GetDecodedValue(mostFittest.SubValues[0]);
                var b = (int) mostFittest.GetDecodedValue(mostFittest.SubValues[1]);
                var c = (int) mostFittest.GetDecodedValue(mostFittest.SubValues[2]);

                builder.Clear();
                builder.Append($"after {i + 1:000} envolve(s): ");
                //染色体二进制表示
                //                builder.Append($"{OutputHelper.DisplayChromosomeBinaryValue(mostFittest)} ");

                //所有映射到解空间的值（若有级联）

//                foreach (var subValue in mostFittest.SubValues)
//                {
//                    builder.Append(mostFittest.GetDecodedValue(subValue)).Append(" ");
//                }

                builder.Append($"{a},{b},{c} ");
                builder.Append($"fitness: {TestFunction.StubbedTriangleTypeTest(a, b, c)} ");
                builder.Append($"result: {TestFunction.TriangleTypeTest(a, b, c)} ");
                Console.WriteLine(builder);

                //进化过程中不同的选择策略
                population.Envolve(Population.SelectType.Roulette);
//                Console.ReadKey();
            }

            #endregion

            Console.ReadKey();
        }
    }
}