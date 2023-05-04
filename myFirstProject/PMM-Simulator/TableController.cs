using Commons;

namespace SimulatorSubsystem;

public class TableController
{
    private readonly TableDataHolder _holder;
    private readonly RuleTable _ruleTable = new();
    public TableController(TableDataHolder holder)
    {
        _holder = holder;
    }

    public RuleTable Populate(int radix, int binSize)
    {
        int id = 0;
        Entry.ChoiceCondition condition = new Entry.ChoiceCondition(_holder.RuleMask[0],_holder.RuleMask[1],_holder.RuleMask[2]);
        Entry.ChoiceBoundaries boundaries = new Entry.ChoiceBoundaries(_holder.Frequency, _holder.InitialPasses);
        List<Entry.EncodedParameter> encoded = new List<Entry.EncodedParameter>();
        EncodingHistogramCreatorWithNormal creatorWithNormal = new EncodingHistogramCreatorWithNormal();
        EncodingHistogram histogram = creatorWithNormal.Create(binSize, _holder.Step, _holder.X1, _holder.X2, _holder.Center, radix, _holder.Symbols);
        encoded.Add(new Entry.SimpleEncodedParameter(_holder.Name, radix, _holder.Symbols, histogram));
        Entry.Parameters parameters = new Entry.Parameters(encoded);
        Entry entry = new Entry(condition, boundaries, parameters,id);
        _ruleTable.AddRule(entry);
        return _ruleTable;
    }
}