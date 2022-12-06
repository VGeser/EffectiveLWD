using Ipgg.LasParser;

public class LasReader
{
    public string saveDict(string namefile, string charset)
    {
        var parser = new LLasParserVlasov();
        parser.ReadFile(namefile, charset);
        return ((string.Join(Environment.NewLine, (parser.Data).Select(a => $"{a.Key}: {a.Value}"))));

    }
}

