namespace LearningCSharp;

using Ipgg.LasParser;

public class LasReader
{
    private readonly LLasParserVlasov _parser = new LLasParserVlasov();

    public string SaveDict(string namefile, string charset)
    {
        _parser.ReadFile(namefile, charset);
        return ((string.Join(Environment.NewLine, (_parser.Data).Select(a => $"{a.Key}: {a.Value}"))));
    }
}