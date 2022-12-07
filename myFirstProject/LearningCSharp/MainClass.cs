namespace LearningCSharp;

public class MainClass
{
    public static void Main()
    {
        Entry.ChoiceCondition truthy = new Entry.ChoiceCondition(true, true, true);
        Table table = new Table();
        table.AddRule(new Entry(
            truthy,
            new Entry.ChoiceBoundaries(1, 0),
            new Entry.Parameters(),
            0));
        table.AddRule(new Entry(
            truthy,
            new Entry.ChoiceBoundaries(1, 0),
            new Entry.Parameters(),
            1));
        table.AddRule(new Entry(
            truthy,
            new Entry.ChoiceBoundaries(1, 2),
            new Entry.Parameters(),
            2));
        Console.Write(table.GetAndSetNextRule(truthy).Id);
        Console.Write(table.GetAndSetNextRule(truthy).Id);
        Console.Write(table.GetAndSetNextRule(truthy).Id);
        Console.Write(table.GetAndSetNextRule(truthy).Id);
        Console.Write(table.GetAndSetNextRule(truthy).Id);
        Console.Write(table.GetAndSetNextRule(truthy).Id);
        Console.Write(table.GetAndSetNextRule(truthy).Id);
        Console.Write(table.GetAndSetNextRule(truthy).Id);
        Console.Write(table.GetAndSetNextRule(truthy).Id);
    }
}