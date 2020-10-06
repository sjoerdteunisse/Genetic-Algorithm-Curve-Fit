using System;

namespace GA_CSharp_Curve_Fit.GA
{
    public class Organism : IComparable <Organism>
    {
        private int _geneCount;
        private double _fitLevel;
        private Genome[] genomes;
        Random rnd = new Random();

        private double _maxGeneValue;
        private double _minGeneValue;

        public Organism(int geneCount, double maxGeneStartValue, double minGeneStartValue)
        {
            _maxGeneValue = maxGeneStartValue;
            _minGeneValue = minGeneStartValue;
            _geneCount = geneCount;
            genomes = new Genome[geneCount];
        }

        public void SetInitialValues()
        {
            for (int i = 0; i < _geneCount; i++)
            {
                genomes[i] = new Genome() { Value = rnd.NextDouble() * _maxGeneValue - _minGeneValue };
            }
        }

        public void Mutate(float learnRate)
        {
            for (int i = 0; i < genomes.Length; i++)
            {
                genomes[i].Value = (rnd.NextDouble() * 2 - 1) * learnRate;
            }
        }

        public int CompareTo(Organism other)
        {
            return this.FitLevel.CompareTo(other.FitLevel);
        }

        public Genome[] Genomes { get => genomes; set => genomes = value; }
        public int GeneCount { get => _geneCount; set => _geneCount = value; }
        public double FitLevel { get => _fitLevel; set => _fitLevel = value; }
    }
}
