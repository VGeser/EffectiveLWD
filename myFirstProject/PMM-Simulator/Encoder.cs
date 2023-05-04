namespace SimulatorSubsystem;

public class Encoder
{
    public string Encode(Entry entry, Dictionary<String, Double?> slice)
    {
        string res = "{S" + entry.Id;
        foreach (Entry.EncodedParameter encoded in entry.Param.EncodedParameters)
        {
            List<Int32> codeAsInts = encoded.Lookup(encoded.ToRepresentation(slice[encoded.Mnemonic]));
            foreach (Int32 symbAsInt in codeAsInts)
            {
                res += Transcribe(symbAsInt);
            }
        }

        res += "}";
        
        return res;
    }

    private String Transcribe(Int32 num)
    {
        if (num is >= 0 and <= 9)
        {
            return num.ToString();
        }

        switch (num)
        {
            case 10: return "a";
            case 11: return "b";
            case 12: return "c";
            case 13: return "d";
            case 14: return "e";
            case 15: return "f";
            default: return "Ъ";
        }
    }
}