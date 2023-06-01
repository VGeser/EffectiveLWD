namespace SimulatorSubsystem;

public class SimulationSubsystem
{
    private RuleTable _table;
    private TimeData _timeData;

    public SimulationSubsystem(RuleTable ruleTable, TimeData timeData)
    {
        _timeData = timeData;
        _table = ruleTable;
    }

    public DecodedMessageData Simulate(String filename)
    {
        TableController tableController = new TableController(filename, _table);
        EncodingController encodingController = new EncodingController(tableController, new TimeCalculator(_timeData));
        
        Decoder decoder = new Decoder(_table);
        DecodingController decodingController = new DecodingController(decoder);

        return decodingController.Decode(encodingController.EncodeAll());
    }
}