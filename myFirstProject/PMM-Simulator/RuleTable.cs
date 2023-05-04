namespace SimulatorSubsystem;

public class RuleTable
{
    private readonly List<Entry> _table;
    //Default int value is 0
    private int _currentRule;
    private int _currentRound = 1;
    public RuleTable()
    {
        _table = new List<Entry>();
    }
    
    public RuleTable(List<Entry> tableInit)
    {
        _table = tableInit;
    }

    public void AddRule(Entry entry)
    {
        _table.Add(entry);
        _table.Sort(CompareEntriesById);
    }

    private int CompareEntriesById(Entry a, Entry b)
    {
        return a.Id - b.Id;
    }

    public Entry? GetAndSetNextRule(Entry.ChoiceCondition condition)
    {
        for (int i = _currentRule+1; i < _table.Count; i++)
        {
            if (_table[i].Choice.Equals(condition)
                && _table[i].Boundaries.InitialPasses < _currentRound
                && _currentRound % _table[i].Boundaries.Frequency == 0)
            {
                _currentRule = i;
                return _table[i];
            }
        }
        _currentRound++;
        for (int i = 0; i <= _currentRule; i++)
        {
            if (_table[i].Choice.Equals(condition)
                && _table[i].Boundaries.InitialPasses < _currentRound
                && _currentRound % _table[i].Boundaries.Frequency == 0)
            {
                _currentRule = i;
                return _table[i];
            }
        }

        return null;
    }

    public Entry GetCurrentRule()
    {
        return _table[_currentRule];
    }
}