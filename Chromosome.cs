using System;
using System.Collections.Generic;

namespace GADemoFromZhihu
{
    public class Chromosome
    {
        private const int OutputWidth = 30;
        public int Length { get; set; }
        public int Value { get; set; }


        public double Fitness
        {
            get
            {
                var x = DecodedValue;

                return TestFunction.Function2(x);
            }
        }

        public double DecodedValue
            => TestFunction.LowerBound + Value * (TestFunction.UpperBound - TestFunction.LowerBound) /
               (Math.Pow(2, Length) - 1);

        //根据染色体得到级联的多参数的各个参数的值（解码前）
        public List<double> GetPartlyValues(int valueQuantity)
        {
            var mask = 0;
            var singleChromosomeLength = Convert.ToInt32(Length / valueQuantity);
            var values = new List<double>();

            for (var i = 0; i < singleChromosomeLength; i++)
                mask += 1 << i;

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
            var decodeValue = DecodedValue;

            return Convert.ToString(Value, 2).PadLeft(Length, '0').PadRight(OutputWidth, ' ') +
                   decodeValue.ToString().PadRight(OutputWidth, ' ') + Fitness.ToString().PadRight(OutputWidth, ' ') +
                   Environment.NewLine;
        }
    }
}