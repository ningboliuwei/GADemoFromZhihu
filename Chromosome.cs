using System;
using System.Collections.Generic;

namespace GADemoFromZhihu
{
    public class Chromosome
    {
        private const int OutputWidth = 300;
        public int Length { get; set; }
        public int Value { get; set; }

        public double Fitness
        {
            get
            {
                //                var decodedValue = GetDecodedValue(Value);
                var values = GetPartlyValues(2);

                return 100000 - TestFunction.BranchTestFitness(Convert.ToInt32(values[0]));
            }
        }

        public double GetDecodedValue(double value)
        {
            return TestFunction.LowerBound + value * (TestFunction.UpperBound - TestFunction.LowerBound) /
                   (Math.Pow(2, Length) - 1);
        }

        //根据染色体得到级联的多参数的各个参数的值（解码前）
        public List<double> GetPartlyValues(int valueQuantity)
        {
            var mask = 0;
            var singleChromosomeLength = Convert.ToInt32(Length / valueQuantity);
            var values = new List<double>();

            //生成 singleChromosomeLength 个 1 的掩码
            for (var i = 0; i < singleChromosomeLength; i++)
                mask += 1 << i;

            //获取每个片段的值
            for (var i = 0; i < valueQuantity; i++)
            {
                var space = (valueQuantity - i - 1) * singleChromosomeLength;
                var value = (Value & (mask << space)) >> space;
                values.Add(value);
            }

            return values;
        }

        public string Output()
        {
            var decodeValue = GetDecodedValue(Value);

            return Convert.ToString(Value, 2).PadLeft(Length, '0').PadRight(OutputWidth, ' ') +
                   decodeValue.ToString().PadRight(OutputWidth, ' ') + Fitness.ToString().PadRight(OutputWidth, ' ') +
                   Environment.NewLine;
        }
    }
}