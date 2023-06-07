using Ipgg.LasParser;

namespace SimulatorSubsystem;

public class TableController
{
    private readonly Slicer _slicer;
    private readonly RuleTable _table;
    private readonly Encoder _encoder;

    public TableController(Slicer slicer, RuleTable table)
    {
        _slicer = slicer;
        _table = table;
        _encoder = new Encoder();
    }

    public string? CreateMessageFromSlice(int sliceNumber, Entry.ChoiceCondition choiceCondition)
    {
        if (sliceNumber >= _slicer.GetSize())
        {
            return null;
        }

        Entry rule = _table.GetAndSetNextRule(choiceCondition) ?? throw new ArgumentException("No entries satisfy such conditions; try specifying something else");
        return _encoder.Encode(rule,_slicer.GetSlice(sliceNumber));
    }
}