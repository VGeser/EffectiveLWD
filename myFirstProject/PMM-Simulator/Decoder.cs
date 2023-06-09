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
            List<int> encoded = new List<int>();
            for (int i = 0; i < fromMessage.Length; i++)
            {
                int num = Transcribe(fromMessage.Substring(i, 1));
                encoded.Add(num);
            }
            result.Add(parameter.Mnemonic, parameter.Decode(encoded));
            message = message.Substring(len);
        }

        return result;
    }

    private Entry GetTableEntryByMarker(String marker)
    {
        return _table.GetRuleByIndex(int.Parse(marker));
    }
    
    private int Transcribe(string s)
    {
        char c = s[0];
        if (c is >= '0' and <= '9')
        {
            return c - '0';
        }

        return c - 'a' + 10;
    }
}