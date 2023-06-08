using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExampleApp.Model;

public class SimulationStatistics : INotifyPropertyChanged
{
    private int _totalMessages;
    private int _fileLength;
    
    public int TotalMessages { 
        get=>_totalMessages;
        set
        {
            if (value != _totalMessages)
            {
                _totalMessages = value;
                OnPropertyChanged();
            }
        } 
    }
    
    public int FileLength { 
        get=>_fileLength;
        set
        {
            if (value != _fileLength)
            {
                _fileLength = value;
                OnPropertyChanged();
            }
        } 
    }

    public SimulationStatistics()
    {
        _totalMessages = 0;
        _fileLength = 0;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}