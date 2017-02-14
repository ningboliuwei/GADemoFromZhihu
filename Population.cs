using System;
using System.Collections.Generic;
using System.Linq;

namespace GADemoFromZhihu
{
    internal class Population
    {
        public List<Chromosome> Chromosomes { get; set; } = new List<Chromosome>();
        public double RetainRate { get; set; }
        public double SelectionRate { get; set; }
        public double MutationRate { get; set; }
        public int ChromosomeLength { get; set; }

        //随机生成数量为 chromosomeQuantity，每个染色体长度为 ChromosomeLength 的染色体集合
        public void RandomGenerate(int chromosomeQuantity)
        {
            var rnd = new Random();

            for (var i = 0; i < chromosomeQuantity; i++)
            {
                //随机生成一个长度为 length 的染色体，每位(基因)是 1 或 0
                var chromosome = new Chromosome();

                for (var j = 0; j < ChromosomeLength; j++)
                {
                    chromosome.Value += (1 << j) * Convert.ToInt32(rnd.Next(0, 2));
                    chromosome.Length = ChromosomeLength;
                }

                Chromosomes.Add(chromosome);
            }
        }

        //返回一个选择后的种群
        public Population Select()
        {
            var sortedChromosomes = Chromosomes.OrderByDescending(c => c.Fitnetss).ToList();

            var retainQuantity = Convert.ToInt32(sortedChromosomes.Count * RetainRate);
            var selectedChromosomes = sortedChromosomes.Take(retainQuantity).ToList();

            var rnd = new Random();
            for (var i = retainQuantity; i < sortedChromosomes.Count(); i++)
                if (rnd.NextDouble() < SelectionRate)
                    selectedChromosomes.Add(sortedChromosomes[i]);

            var result = Clone();
            result.Chromosomes = selectedChromosomes;
           return result;
        }

        //返回由当前种群进行 crossover 后所得到的孩子集合
        public Population Crossover(int childQuantity)
        {
            var children = new List<Chromosome>();
            var rnd = new Random();

            while (children.Count < childQuantity)
            {
                var father = Chromosomes[rnd.Next(0, Chromosomes.Count)];
                var mother = Chromosomes[rnd.Next(0, Chromosomes.Count)];

                if (father != mother)
                {
                    //交叉点索引，孩子取父亲交叉点之前（不含交叉点，即 crossPos 个）及母亲交叉点之后（含交叉点）的基因
                    var crossPos = rnd.Next(1, ChromosomeLength);
                    var span = ChromosomeLength - crossPos;

                    var fatherMask = 0;
                    for (var j = 0; j < crossPos; j++)
                        fatherMask += 1 << j;
                    fatherMask = fatherMask << span;

                    var motherMask = 0;
                    for (var j = 0; j < span; j++)
                        motherMask += 1 << j;

                    var child = father.Value & fatherMask | mother.Value & motherMask;

                    children.Add(new Chromosome { Length = ChromosomeLength, Value = child });
                    //
                    //                    var s8 = crossPos.ToString();
                    //                    var s6 = Convert.ToString(father.Value, 2).PadLeft(Length, '0').PadRight(20);
                    //                    var s7 = Convert.ToString(mother.Value, 2).PadLeft(Length, '0').PadRight(20);
                    //                    var s1 = Convert.ToString(fatherMask, 2).PadLeft(Length, '0').PadRight(20);
                    //                    var s2 = Convert.ToString(motherMask, 2).PadLeft(Length, '0').PadRight(20);
                    //                    var s3 = Convert.ToString(fatherMasked, 2).PadLeft(Length, '0').PadRight(20);
                    //                    var s4 = Convert.ToString(motherMasked, 2).PadLeft(Length, '0').PadRight(20);
                    //                    var s5 = Convert.ToString(child, 2).PadLeft(Length, '0').PadRight(20);
                    //
                    //                    Console.WriteLine($"{s8}\n{s6}{s1}{s3}\n{s7}{s2}{s4}\n{s5}");
                }
            }

            var result = Clone();
            result.Chromosomes = children;
            return result;
        }

        //当前种群进行变异
        public void Mutate()
        {
            Random rnd = new Random();
            //将现有的染色体集合复制到 result 集合中去

            foreach (var p in Chromosomes)
            {
                if (rnd.NextDouble() < MutationRate)
                {
                    int mutationPos = rnd.Next(0, ChromosomeLength);

                    p.Value = p.Value ^ (1 << (ChromosomeLength - mutationPos - 1));
                    //                    Console.WriteLine(" " + mutationPos + " " + Convert.ToString(n, 2).PadLeft(Length, '0') + " " + Convert.ToString(p.Value, 2).PadLeft(Length, '0') + "\n");
                }
            }
        }

        //返回一个与当前种群参数相同，但不包含任何染色体实例的种群（即返回具有相同参数的空种群）
        public Population Clone()
        {
            Population copy = MemberwiseClone() as Population;

            if (copy != null)
            {
                copy.Chromosomes = new List<Chromosome>();

                return copy;
            }
            return null;
        }

        //返回一个与当前种群参数相同，且具有完全一样的染色体集合的种群
        public Population Copy()
        {
            Population copy = MemberwiseClone() as Population;

            if (copy != null)
            {
                copy.Chromosomes = new List<Chromosome>();
                foreach (var c in Chromosomes)
                {
                    copy.Chromosomes.Add(c);
                }

                return copy;
            }

            return null;
        }


        public void Envolve()
        {
            var parents = Select();
            var children = Crossover(Chromosomes.Count - parents.Chromosomes.Count);
            Chromosomes.Clear();
            Chromosomes.AddRange(parents.Chromosomes);
            Chromosomes.AddRange(children.Chromosomes);
            Mutate();
        }
    }
}