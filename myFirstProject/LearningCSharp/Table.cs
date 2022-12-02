namespace LearningCSharp;

public class Table
{
    private readonly List<Entry> _table;

    public Table()
    {
        _table = new List<Entry>();
    }
    
    public Table(List<Entry> tableInit)
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

    public Entry? GetNextRule(Entry.ChoiceCondition condition)
    {
        foreach (Entry e  in _table)
        {
            if (e.Choice.Equals(condition))
            {
                return e;
            }
        }

        return null;
    }
}