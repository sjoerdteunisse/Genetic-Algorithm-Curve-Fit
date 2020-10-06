using System;

namespace GA_CSharp_Curve_Fit.GA
{
    public class Organism : IComparable <Organism>
    {
        private int _geneCount;
        private double _fitLevel;
        private Genome[] _genomes;
        
        readonly Random _rnd = new Random();
        private readonly double _maxGeneValue;
        private readonly double _minGeneValue;

        public Organism(int geneCount, double maxGeneStartValue, double minGeneStartValue)
        {
            _maxGeneValue = maxGeneStartValue;
            _minGeneValue = minGeneStartValue;
            _geneCount = geneCount;
            _genomes = new Genome[geneCount];
        }

        public void SetInitialValues()
        {
            for (int i = 0; i < _geneCount; i++)
            {
                _genomes[i] = new Genome() { Value = _rnd.NextDouble() * _maxGeneValue - _minGeneValue };
            }
        }

        public void Mutate(float learnRate)
        {
            var geneModificationCount = _rnd.Next(0, 6);

            for (int i = 0; i < 6; i++)
            {
                _genomes[i].Value += (_rnd.NextDouble() * 2 - 1) * learnRate;
            }
        }

        public int CompareTo(Organism other)
        {
            return FitLevel.CompareTo(other.FitLevel);
        }

        public Genome[] Genomes { get => _genomes; set => _genomes = value; }
        public int GeneCount { get => _geneCount; set => _geneCount = value; }
        public double FitLevel { get => _fitLevel; set => _fitLevel = value; }
    }
}
