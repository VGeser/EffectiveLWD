namespace MainRestore;

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
                string hexPart = BitConverter.DoubleToInt64Bits(value.Value).ToString("X");
                message += hexPart;
            }
            else
            {
                return "";
            }
        }

        return message;
    }
}