namespace SimulatorSubsystem;

public class Encoder
{
    public string Encode(Entry entry, Dictionary<String, Double?> slice)
    {
        //TODO customizable markers??
        string res = "S" + entry.Id;
        foreach (Entry.EncodedParameter encoded in entry.Param.EncodedParameters)
        {
            List<Int32> codeAsInts = encoded.Lookup(encoded.ToRepresentation(slice[encoded.Mnemonic]));
            foreach (Int32 symbAsInt in codeAsInts)
            {
                res += Transcribe(symbAsInt);
            }
        }

        return res;
    }

    private String Transcribe(Int32 num)
    {
        if (num is >= 0 and <= 9)
        {
            return num.ToString();
        }

        char c = 'a';
        c += (char)(num - 10);
        char[] chars = { c };
        return new string(chars);
    }
}