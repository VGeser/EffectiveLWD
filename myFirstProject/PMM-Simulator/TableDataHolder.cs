namespace LearningCSharp;

public class TableDataHolder
{
    public int Frequency;
    public int InitialPasses;
    public int Symbols;
    public string Name;
    public int x1;
    public int x2;
    public int Step;
    public int Center;

    public override string ToString()
    {
        return "Freq " + Frequency + "\n" +
               "Init " + InitialPasses + "\n" +
               "Symb " + Symbols + "\n" +
               "Name " + Name + "\n" +
               "Rng1 " + x1 + "\n" +
               "Rng2 " + x2 + "\n" +
               "Step " + Step + "\n" +
               "Cent " + Center + "\n";
    }
}