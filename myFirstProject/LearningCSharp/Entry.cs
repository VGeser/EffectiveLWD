namespace LearningCSharp
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
            private readonly int _frequency;
            public int Frequency => _frequency;

            private readonly int _initialPasses;
            public int InitialPasses => _initialPasses;

            public ChoiceBoundaries(int frequency, int initialPasses)
            {
                this._frequency = frequency;
                this._initialPasses = initialPasses;
            }
        }

        public class Parameters
        {
            //TODO: find specification
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