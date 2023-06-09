using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExampleApp.Model;

public class SimulationTimes : INotifyPropertyChanged
{
    private int _synchro = 1000;
    private int _start = 400;
    private int _tick = 100;

    public int Synchro
    {
        get => _synchro;
        set
        {
            if (value != _synchro)
            {
                _synchro = value;
                OnPropertyChanged();
            }
        }
    }
    
    public int Start
    {
        get => _start;
        set
        {
            if (value != _start)
            {
                _start = value;
                OnPropertyChanged();
            }
        }
    }
    
    public int Tick
    {
        get => _tick;
        set
        {
            if (value != _tick)
            {
                _tick = value;
                OnPropertyChanged();
            }
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}