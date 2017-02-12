using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADemoFromZhihu
{
    class Population
    {
        public List<Chromosome> Chromosomes { get; } = new List<Chromosome>();
        public double RetainRate { get; set; }
        public double RandomSelectRate { get; set; }

        //随机生成一个数量为 count，每个染色体长度为 length 的种群
        public Population(int chromosomeLength, int chromosomeCount)
        {
            var rnd = new Random();

            for (var i = 0; i < chromosomeCount; i++)
            {
                //随机生成一个长度为 length 的染色体，每位(基因)是 1 或 0
                var chromosome = new Chromosome();

                for (var j = 0; j < chromosomeLength; j++)
                {
                    chromosome.Value += (1 << j) * Convert.ToInt32(rnd.Next(0, 2));
                    chromosome.Length = chromosomeLength;
                }

                Chromosomes.Add(chromosome);
            }
        }

        public List<Chromosome> Select()
        {
            var sortedChromosomes = Chromosomes.OrderByDescending(c => c.Fitnetss).ToList();

            int retainQuantity = Convert.ToInt32(sortedChromosomes.Count * RetainRate);
            var selectedChromosomes = sortedChromosomes.Take(retainQuantity).ToList();

            Random rnd = new Random();
            for (int i = retainQuantity; i < sortedChromosomes.Count(); i++)
            {
                if (rnd.NextDouble() < RandomSelectRate)
                {
                    selectedChromosomes.Add(sortedChromosomes[i]);
                }
            }

            return selectedChromosomes;
        }

    }
}
