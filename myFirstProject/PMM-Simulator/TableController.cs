using Ipgg.LasParser;

namespace SimulatorSubsystem;

public class TableController
{
    private readonly Slicer _slicer;
    private readonly RuleTable _table;
    private readonly Encoder _encoder;

    public TableController(String filename, RuleTable table)
    {
        LLasParserVlasov parser = new LLasParserVlasov();
        parser.ReadFile(filename, "utf-8");
        _slicer = new Slicer(parser.Data);
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