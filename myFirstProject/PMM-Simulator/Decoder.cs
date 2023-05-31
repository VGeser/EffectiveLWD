namespace SimulatorSubsystem;

public class Decoder
{
    private readonly RuleTable _table;

    public Decoder(RuleTable table)
    {
        _table = table;
    }

    public Dictionary<String, Double> Decode(String message)
    {
        Dictionary<String, Double> result = new Dictionary<string, double>();
        message = message.Substring(1);
        //TODO make length customizable through config or smthng
        String ruleMarker = message[0].ToString();
        message = message.Substring(1);
        Entry entry = GetTableEntryByMarker(ruleMarker);
        List<Entry.EncodedParameter> parameters = entry.Param.EncodedParameters;
        foreach (var parameter in parameters)
        {
            int len = parameter.Symbols;
            string fromMessage = message.Substring(0, len);
            message = message.Substring(len);
            result.Add(parameter.Mnemonic, parameter.Decode(fromMessage));
        }

        return result;
    }

    private Entry GetTableEntryByMarker(String marker)
    {
        return _table.GetRuleByIndex(int.Parse(marker));
    }
}