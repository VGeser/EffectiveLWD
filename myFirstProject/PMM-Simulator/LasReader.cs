namespace LearningCSharp;

using Ipgg.LasParser;

public class LasReader
{
    private readonly LLasParserVlasov _parser = new LLasParserVlasov();

    public Dictionary<String, Double?[]> SaveDict(string namefile, string charset)
    {
        _parser.ReadFile(namefile, charset);
        return _parser.Data;
    }
}