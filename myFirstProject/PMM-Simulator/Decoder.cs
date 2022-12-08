namespace LearningCSharp;

public interface Decoder
{
    public Dictionary<String, Double> Decode(String message);
}