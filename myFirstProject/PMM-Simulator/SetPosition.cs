namespace LearningCSharp;

public class SetPosition
{
    public Int32 Sum { get; set; }
    public Int32 IndexFrom{ get; set; }
    public Int32 Count{ get; set; }

    public SetPosition(Int32 count, Int32 sum, Int32 indexFrom)
    {
        Sum = sum;
        IndexFrom = indexFrom;
        Count = count;
    }

    public override string ToString()
    {
        return "[" + Sum + " " + IndexFrom + " " + Count + "]";
    }
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}