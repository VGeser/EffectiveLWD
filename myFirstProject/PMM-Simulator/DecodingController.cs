namespace SimulatorSubsystem;

public class DecodingController
{
    private readonly Decoder _decoder;

    public DecodingController(Decoder decoder)
    {
        _decoder = decoder;
    }

    public DecodedMessageData Decode(EncodedMessageData encoded)
    {
        DecodedMessageData result = new DecodedMessageData(encoded.GetSize());
        for (int i = 0; i < encoded.GetSize(); i++)
        {
            Dictionary<String, Double> decodedMessage = _decoder.Decode(encoded.GetMessageByIndex(i));
            result.AddMessage(decodedMessage, encoded.GetTimeByIndex(i));
        }

        return result;
    }
}