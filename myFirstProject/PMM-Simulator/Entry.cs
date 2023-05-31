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
            //public List<(string,int)> EncodedCurvesWithPrecision { get; }
            public List<EncodedParameter> EncodedParameters { get; set; }

            public Parameters(List<EncodedParameter> encoded)
            {
                EncodedParameters = encoded;
            }
        }

        public abstract class EncodedParameter
        {
            protected readonly int _radix;
            public int Symbols { get; }
            protected readonly EncodingHistogram _histogram;
            public readonly string Mnemonic;

            protected EncodedParameter(string mnemonic, int radix, int symbols, EncodingHistogram histogram)
            {
                _histogram = histogram;
                Mnemonic = mnemonic;
                Symbols = symbols;
                _radix = radix;
            }

            public abstract int ToRepresentation(double? value);

            //TODO decoding for every subtype!
            public virtual double Decode(String message)
            {
                return 0;
            }

            public List<Int32> Lookup(Int32 repres)
            {
                return _histogram.Lookup(repres, _radix, Symbols);
            }
        }

        public class SimpleEncodedParameter : EncodedParameter
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

            public override double Decode(String message)
            {
                List<int> encoded = new List<int>();
                int sum = 0;
                for (int i = 0; i < message.Length; i++)
                {
                    int num = int.Parse(message.Substring(i, 1));
                    sum += num;
                    encoded.Add(num);
                }

                int index = -1;
                List<List<int>> possible = Util.GetEncodings(new List<int>(), Symbols, sum, _radix);
                foreach (var VARIABLE in possible)
                {
                    int equal = 0;
                    for (int i = 0; i < Symbols; i++)
                    {
                        if (encoded[i] == VARIABLE[i]) equal++;
                    }

                    if (equal == Symbols)
                    {
                        index = possible.IndexOf(VARIABLE);
                        break;
                    }
                }
                return (double)_histogram.getRepresByIndex(sum, index);
            }

            public SimpleEncodedParameter(string mnemonic, int radix, int symbols, EncodingHistogram histogram) : 
                base(mnemonic, radix, symbols, histogram)
            {
                
            }
        }

        public class StepChangingEncodedParameter : EncodedParameter
        {
            public StepChangingEncodedParameter(string mnemonic, int radix, int symbols, EncodingHistogram histogram, double requiredStep) : 
                base(mnemonic, radix, symbols, histogram)
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