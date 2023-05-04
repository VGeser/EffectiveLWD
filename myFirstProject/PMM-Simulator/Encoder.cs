using System.Globalization;

namespace SimulatorSubsystem;

public class Encoder
{
    public string Encode(Entry.Parameters parameters, Dictionary<String, Double?> slice)
    {
        string message = parameters.Mark;
        foreach (var param in parameters.EncodedParameters)
        {
            double? value = slice[param.Mnemonic];
            if (value != null)
            {
                int intVal = param.ToRepresentation(value);
                string hexPart = intVal.ToString(CultureInfo.CurrentCulture);
                hexPart = DeletePunctuationAndTrailing(hexPart);
                if (hexPart.Length < intVal)
                {
                    hexPart=hexPart.PadRight(intVal, '0');
                }
                hexPart = hexPart.Substring(0, intVal);
                message += hexPart;
            }
            else
            {
                return "";
            }
        }

        return message;
    }

    private String DeletePunctuationAndTrailing(string s)
    {
        string res = s;
        res=res.Replace(",", "");
        res=res.Replace("-", "");
        res.TrimStart('0');
        return res;
    }
}