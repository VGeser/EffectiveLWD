using System.Collections.Generic;
using ExampleApp.Model;
using SimulatorSubsystem;

namespace ExampleApp;

public class Simulator
{
    private ProtocolData _protocol;

    public Simulator(ProtocolData protocol)
    {
        _protocol = protocol;
    }
    
    public DecodedMessageData Simulate(int radix, int binSize, string filename, TimeData timeData)
    {
        RuleTable table = new RuleTable();
        int id = 0;
        foreach (ProtocolRule rule in _protocol.ProtocolRules)
        {
            id++;
            Entry.ChoiceCondition condition = new Entry.ChoiceCondition(rule.SelectCondition[0].isRotor, 
                rule.SelectCondition[0].isStat, rule.SelectCondition[0].isTfgFlag);
            Entry.ChoiceBoundaries boundaries = new Entry.ChoiceBoundaries(rule.SelectCondition[0].Frequency,
                rule.SelectCondition[0].InitialPasses);
            List<Entry.EncodedParameter> encodedParameters = new List<Entry.EncodedParameter>();
            foreach(ProtocolParameter parameter in rule.Parameters)
            {
                EncodingHistogram histogram = EncodingHistogramCreatorWithNormal.Create(binSize, parameter.Step,
                    parameter.RangeFrom, parameter.RangeTo, parameter.CenterBinStart, radix, int.Parse(parameter.Symbols));
                encodedParameters.Add(new Entry.SimpleEncodedParameter(parameter.Name, radix, int.Parse(parameter.Symbols), histogram));
            }
            table.AddRule(new Entry(condition, boundaries, new Entry.Parameters(encodedParameters), id));
        }

        TableController tableController = new TableController(filename, table);
        TimeCalculator timeCalculator = new TimeCalculator(timeData);
        Encoder encoder = new Encoder();
        EncodingController encodingController = new EncodingController(tableController, timeCalculator);

        Decoder decoder = new Decoder(table);
        DecodingController decodingController = new DecodingController(decoder);

        return decodingController.Decode(encodingController.EncodeAll());
    }
}