namespace LearningCSharp;

public interface AbstractEncoderFactory
{
    public Encoder GetEncoder();
    public Decoder GetDecoder();
}