using System;
using System.Collections.Generic;

namespace GADemoFromZhihu
{
    public class Chromosome
    {
        public Population Population { get; set; }
        public int Value { get; set; }

        public List<int> SubValues
        {
            get
            {
                //根据染色体得到级联的多参数的各个参数的值（解码前）
                var mask = 0;
                var singleChromosomeLength = Convert.ToInt32(Population.ChromosomeLength / Population.SubValueQuantity);
                var subValues = new List<int>();

                //生成 singleChromosomeLength 个 1 的掩码
                for (var i = 0; i < singleChromosomeLength; i++)
                    mask += 1 << i;

                //获取每个片段的值
                for (var i = 0; i < Population.SubValueQuantity; i++)
                {
                    var space = (Population.SubValueQuantity - i - 1) * singleChromosomeLength;
                    var subValue = (Value & (mask << space)) >> space;
                    subValues.Add(subValue);
                }

                return subValues;
            }
        }

        public double Fitness
        {
            get
            {
                //更换测试函数，就改这里
                int x = (int) GetDecodedValue(SubValues[0]);
                int y = (int) GetDecodedValue(SubValues[1]);

                return TestFunction.StubbedBranchTest1(x, y);
            }

            private set { }
        }

        //得到将染色体值转换为在解空间对应的值
        public double GetDecodedValue(double value)
        {
            return TestFunction.LowerBound + value * (TestFunction.UpperBound - TestFunction.LowerBound) /
                   (Math.Pow(2,Convert.ToInt32(Population.ChromosomeLength / Population.SubValueQuantity)) - 1);
        }
    }
}