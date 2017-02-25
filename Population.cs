using System;
using System.Collections.Generic;
using System.Linq;

namespace GADemoFromZhihu
{
    public class Population
    {
        public enum SelectType
        {
            Elite,
            Roulette,
            Hybrid
        }

        public Population(double retainRate, double selectionRate, double mutationRate, int chromosomeLength,
            int chromosomeQuantity, int subValueQuantity)
        {
            RetainRate = retainRate;
            SelectionRate = selectionRate;
            MutationRate = mutationRate;
            ChromosomeLength = chromosomeLength;
            SubValueQuantity = subValueQuantity;
            ChromosomeQuantity = chromosomeQuantity;
        }

        //染色体集合
        public List<Chromosome> Chromosomes { get; private set; } = new List<Chromosome>();
        //（需要的）染色体个数
        public int ChromosomeQuantity { get; set; }
        //存活率
        public double RetainRate { get; set; }
        //（随机）选择率
        public double SelectionRate { get; set; }
        //变异率
        public double MutationRate { get; set; }
        //子值数量（多参数级联情况下）
        public int SubValueQuantity { get; }
        //染色体长度（总长度）
        public int ChromosomeLength { get; }

        //随机生成若干染色体
        public void RandomGenerateChromosome()
        {
            var rnd = new Random();

            for (var i = 0; i < ChromosomeQuantity; i++)
            {
                var chromosome = new Chromosome {Population = this};

                #region 暂时废弃 

                //先随机生成 N 个片段，再合并
                for (var j = 0; j < SubValueQuantity; j++)
                {
                    //随机生成一个长度为 ChromosomeLength 的 1 / parameterQuantity 的染色体，每位(基因)是 1 或 0
                    var segment = 0;
                    var singleChromosomeLength = Convert.ToInt32(ChromosomeLength / SubValueQuantity);

                    //生成其中的
                    for (var k = 0; k < singleChromosomeLength; k++)
                        segment += Convert.ToInt32(1 << k) * Convert.ToInt32(rnd.Next(0, 2));

                    chromosome.Value += segment << ((SubValueQuantity - j - 1) * singleChromosomeLength);
                }

                #endregion

                //随机生成 ChromosomeLength 个 0 或 1，并拼接起来
                //                for (var k = 0; k < ChromosomeLength; k++)
                //                    chromosome.Value += (1 << k) * Convert.ToInt32(rnd.Next(0, 2));

                Chromosomes.Add(chromosome);
            }
        }

        //返回一个选择后的种群（精英选择法）
        public Population EliteSelect(Population population)
        {
            var sortedChromosomes = population.Chromosomes.OrderByDescending(c => c.Fitness).ToList();

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

        //轮盘赌选择法
        private Population RouletteSelect(Population population)
        {
            var sortedChromosomes = population.Chromosomes.OrderByDescending(c => c.Fitness).ToList();
            var totalFitness = sortedChromosomes.Sum(c => c.Fitness);
            var selectedChromosomes = new List<Chromosome>();

            //所有染色体的选择概率
            var selectionRateList = (from c in sortedChromosomes
                select c.Fitness / totalFitness).ToList();
            //所有染色体的累积选择概率
            var sumedSelectionRateList = new List<double>();

            for (var i = 0; i < selectionRateList.Count; i++)
            {
                double sum = 0;
                for (var j = 0; j <= i; j++)
                    sum += selectionRateList[j];
                sumedSelectionRateList.Add(sum);
            }

            //根据生成的 0~1 之间的随机数，根据累积概率决定哪些染色体存活下来（在该染色体的累积概率区间内）。
            var rnd = new Random();
            var dice = rnd.NextDouble();

            if (dice <= sumedSelectionRateList[0])
                selectedChromosomes.Add(sortedChromosomes[0]);

            for (var i = 1; i < sortedChromosomes.Count; i++)
                if (dice > sumedSelectionRateList[i - 1] && dice <= sumedSelectionRateList[i])
                    selectedChromosomes.Add(sortedChromosomes[i]);

            var result = Clone();
            result.Chromosomes = selectedChromosomes;
            return result;
        }

        private Population HybridSelect(Population population)
        {
            var sortedChromosomes = population.Chromosomes.OrderByDescending(c => c.Fitness).ToList();
            var retainQuantity = Convert.ToInt32(sortedChromosomes.Count * RetainRate);

            var elitePopulation = Clone();
            elitePopulation.Chromosomes = sortedChromosomes.Take(retainQuantity).ToList();

            var roulettePopulation = Clone();
            roulettePopulation.Chromosomes = sortedChromosomes.Skip(retainQuantity).ToList();

            var selectedPopulation = RouletteSelect(roulettePopulation);
            elitePopulation.Chromosomes.AddRange(selectedPopulation.Chromosomes);

            return elitePopulation;
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
                    //交叉点索引，孩子取父亲交叉点之前（不含交叉点，即 crossPos）及母亲交叉点之后（含交叉点）的基因
                    var crossPos = rnd.Next(1, ChromosomeLength);
                    var span = ChromosomeLength - crossPos;

                    var fatherMask = 0;
                    for (var j = 0; j < crossPos; j++)
                        fatherMask += 1 << j;
                    fatherMask = fatherMask << span;

                    var motherMask = 0;
                    for (var j = 0; j < span; j++)
                        motherMask += 1 << j;

                    var childValue = (father.Value & fatherMask) | (mother.Value & motherMask);

                    children.Add(new Chromosome {Population = this, Value = childValue});
                }
            }

            var result = Clone();
            result.Chromosomes = children;
            return result;
        }

        //当前种群进行变异
        public void Mutate()
        {
            var rnd = new Random();
            //将现有的染色体集合复制到 result 集合中去

            foreach (var p in Chromosomes)
                if (rnd.NextDouble() < MutationRate)
                {
                    var mutationPos = rnd.Next(0, ChromosomeLength);

                    p.Value = p.Value ^ (1 << (ChromosomeLength - mutationPos - 1));
                }
        }

        //返回一个与当前种群参数相同，但不包含任何染色体实例的种群（即返回具有相同参数的空种群）
        public Population Clone()
        {
            var copy = MemberwiseClone() as Population;

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
            var copy = MemberwiseClone() as Population;

            if (copy != null)
            {
                copy.Chromosomes = new List<Chromosome>();
                foreach (var c in Chromosomes)
                    copy.Chromosomes.Add(c);

                return copy;
            }

            return null;
        }

        //当前种群进化
        public void Envolve(SelectType selectType)
        {
            Population parents = null;

            if (selectType == SelectType.Elite)
            {
                //精英选择
                parents = EliteSelect(this);
            }
            else if (selectType == SelectType.Roulette)
            {
                //轮盘赌选择
                parents = RouletteSelect(this);
            }
            else if (selectType == SelectType.Hybrid)
            {
                //精英与轮盘赌混合选择
                parents = HybridSelect(this);
            }

            //crossover 得到子女种群
            var children = Crossover(Chromosomes.Count - parents.Chromosomes.Count);
            Chromosomes.Clear();
            //将选择出来的所有父母染色体，及他们 crossover 得到的子女，作为新的种群
            Chromosomes.AddRange(parents.Chromosomes);
            Chromosomes.AddRange(children.Chromosomes);
            //变异
            Mutate();
        }
    }
}