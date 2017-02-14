using System;

namespace GADemoFromZhihu
{
    internal class Chromosome
    {
        public int Length { get; set; }
        public int Value { get; set; }

        public double Fitnetss
        {
            get
            {
                var x = DecodedValue;

                return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
            }
        }

        public double DecodedValue => 0 + Value * (9.0 - 0) / (Math.Pow(2, Length) - 1);

        public string Output()
        {
            return Convert.ToString(Value, 2).PadLeft(Length, '0').PadRight(20, ' ') + DecodedValue.ToString().PadRight(20, ' ') + Fitnetss.ToString().PadRight(20, ' ') + Environment.NewLine;
        }
    }
}