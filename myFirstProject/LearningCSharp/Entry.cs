namespace LearningCSharp
{
    public class Entry
    {
        public int Id { get; }
        public ChoiceCondition Choice { get; }

        public Parameters Param { get; }

        public readonly struct ChoiceCondition
        {
            private sealed class ChoiceConditionEqualityComparer : IEqualityComparer<ChoiceCondition>
            {
                public bool Equals(ChoiceCondition x, ChoiceCondition y)
                {
                    return x.Mode == y.Mode &&
                           x.IsStatic == y.IsStatic &&
                           x.TfFlag == y.TfFlag &&
                           x._frequency == y._frequency &&
                           x._initialPasses == y._initialPasses;
                }

                public int GetHashCode(ChoiceCondition obj)
                {
                    return HashCode.Combine(obj.Mode,
                        obj.IsStatic,
                        obj.TfFlag,
                        obj._frequency,
                        obj._initialPasses);
                }
            }

            public static IEqualityComparer<ChoiceCondition> ChoiceConditionComparer 
            { get; } = new ChoiceConditionEqualityComparer();

            public bool Mode { get; }

            public bool IsStatic { get; }

            public bool TfFlag { get; }

            private readonly int _frequency;
            public int Frequency => _frequency;

            private readonly int _initialPasses;
            public int InitialPasses => _initialPasses;

            public ChoiceCondition(bool mode, bool isStatic, bool tfFlag,
                int frequency, int initialPasses)
            {
                Mode = mode;
                IsStatic = isStatic;
                TfFlag = tfFlag;
                _frequency = frequency;
                _initialPasses = initialPasses;
            }
        }

        public class Parameters
        {
            //TODO: find specification
        }

        public Entry(ChoiceCondition choiceCondition,
            Parameters parameters,
            int id)
        {
            Choice = choiceCondition;
            Param = parameters;
            Id = id;
        }
    }
}