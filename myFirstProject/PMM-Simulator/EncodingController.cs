namespace SimulatorSubsystem;

public class EncodingController
{
    private TableController _tableController;
    private TimeCalculator _timeCalculator;
    private int _currentTime;
    private Entry.ChoiceCondition _conditionSupplier = new(true,true,true);
    
    public EncodingController(TableController tableController, TimeCalculator timeCalculator)
    {
        _tableController = tableController;
        _timeCalculator = timeCalculator;
        _currentTime = 0;
    }

    public EncodedMessageData EncodeAll()
    {
        EncodedMessageData data = new EncodedMessageData();
        String? message = "";
        while (message != null)
        {
            int currentSlice = _currentTime / 250;
            message = _tableController.CreateMessageFromSlice(currentSlice, _conditionSupplier);
            int elapsed = _timeCalculator.CalculateTime(message ?? "");
            if(message != null)
                data.AddMessage(message,_currentTime);
            _currentTime += elapsed;
        }

        return data;
    }
}