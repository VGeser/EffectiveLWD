namespace SimulatorSubsystem
{
    public class Entry
    {
        public int Id { get; }
        public ChoiceCondition Choice { get; }
        public ChoiceBoundaries Boundaries { get; }
        public Parameters Param { get; }

        public readonly struct ChoiceCondition
        {
            private sealed class ChoiceConditionEqualityComparer : IEqualityComparer<ChoiceCondition>
            {
                public bool Equals(ChoiceCondition x, ChoiceCondition y)
                {
                    return x.Mode == y.Mode &&
                           x.IsStatic == y.IsStatic &&
                           x.TfFlag == y.TfFlag;
                }

                public int GetHashCode(ChoiceCondition obj)
                {
                    return HashCode.Combine(obj.Mode,
                        obj.IsStatic,
                        obj.TfFlag);
                }
            }

            public static IEqualityComparer<ChoiceCondition> ChoiceConditionComparer 
            { get; } = new ChoiceConditionEqualityComparer();

            public bool Mode { get; }

            public bool IsStatic { get; }

            public bool TfFlag { get; }

            public ChoiceCondition(bool mode, bool isStatic, bool tfFlag)
            {
                Mode = mode;
                IsStatic = isStatic;
                TfFlag = tfFlag;
            }
        }

        public readonly struct ChoiceBoundaries
        {
            public int Frequency { get; }

            public int InitialPasses { get; }

            public ChoiceBoundaries(int frequency, int initialPasses)
            {
                this.Frequency = frequency;
                this.InitialPasses = initialPasses;
            }
        }

        public class Parameters
        {
            public String Mark { get; }

            //public List<(string,int)> EncodedCurvesWithPrecision { get; }
            public List<EncodedParameter> EncodedParameters { get; set; }

            public Parameters(string mark)
            {
                Mark = mark;
            }
        }

        public abstract class EncodedParameter
        {
            private Tuple<double> _range;
            private double _step;
            private int _radix;
            private int _symbols;
            private EncodingHistogram _histogram;
            public string Mnemonic;

            protected EncodedParameter(string mnemonic, Tuple<double> range, double step, int radix, int symbols, EncodingHistogram histogram)
            {
                _histogram = histogram;
                Mnemonic = mnemonic;
                _range = range;
                _step = step;
                _symbols = symbols;
                _radix = radix;
            }

            public abstract int ToRepresentation(double? value);

            public List<Int32> Lookup(Int32 repres)
            {
                return _histogram.Lookup(repres, _radix, _symbols);
            }
        }

        class SimpleEncodedParameter : EncodedParameter
        {
            public override int ToRepresentation(double? value)
            {
                
                if(value is not null)
                {
                    return (int) Math.Round(value.Value); 
                    // есть еще Floor, который округлит 3.9 -> 3
                }
                throw new  ArgumentNullException();
            }

            public SimpleEncodedParameter(string mnemonic, Tuple<double> range, double step, int radix, int symbols, EncodingHistogram histogram) : 
                base(mnemonic, range, step, radix, symbols, histogram)
            {
                
            }
        }

        class StepChangingEncodedParameter : EncodedParameter
        {
            public StepChangingEncodedParameter(string mnemonic, Tuple<double> range, double step, int radix, int symbols, EncodingHistogram histogram, double requiredStep) : 
                base(mnemonic, range, step, radix, symbols, histogram)
            {
                RequiredStep = requiredStep;
            }

            double RequiredStep { get; set; }
            public override int ToRepresentation(double? value)
            {
                if (value is not null)
                {
                    double res = (value.Value / RequiredStep);
                    int intRes = (int)Math.Round(res);
                    return (int) (intRes * RequiredStep);
                }
                throw new  ArgumentNullException();
            }
        }

        public Entry(ChoiceCondition choiceCondition,
            ChoiceBoundaries boundaries,
            Parameters parameters,
            int id)
        {
            Choice = choiceCondition;
            Boundaries = boundaries;
            Param = parameters;
            Id = id;
        }
    }
}