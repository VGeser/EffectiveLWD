using System.Globalization;

namespace LearningCSharp;

public class Encoder
{
    public string Encode(Entry.Parameters parameters, Dictionary<String, Double?> slice)
    {
        string message = parameters.Mark;
        foreach (var curve in parameters.EncodedCurvesWithPrecision)
        {
            double? value = slice[curve.Item1];
            if (value != null)
            {
                value = Math.Round(value.Value, curve.Item2);
                string hexPart = value.Value.ToString(CultureInfo.CurrentCulture);
                hexPart = DeletePunctuationAndTrailing(hexPart);
                if (hexPart.Length < curve.Item2)
                {
                    hexPart=hexPart.PadRight(curve.Item2, '0');
                }
                hexPart = hexPart.Substring(0, curve.Item2);
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