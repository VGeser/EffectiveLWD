using Ipgg.LasParser;


public class LasReader
{
    private LLasParserVlasov parser = new LLasParserVlasov();

    public string saveDict(string namefile, string charset)
    {
        parser.ReadFile(namefile, charset);
        return ((string.Join(Environment.NewLine, (parser.Data).Select(a => $"{a.Key}: {a.Value}"))));
    }
}