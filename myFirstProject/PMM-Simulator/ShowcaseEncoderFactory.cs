namespace LearningCSharp;

public class ShowcaseEncoderFactory : AbstractEncoderFactory
{
    public Decoder GetDecoder()
    {
        return new ShowcaseDecoder();
    }
    
    public Encoder GetEncoder()
    {
        return new ShowcaseEncoder();
    }
}