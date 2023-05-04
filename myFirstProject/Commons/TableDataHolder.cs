namespace ExampleApp;

public class TableDataHolder
{
    public int Frequency;
    public int InitialPasses;
    public int Symbols;
    public string Name = "";
    public int X1;
    public int X2;
    public int Step;
    public int Center;

    public override string ToString()
    {
        return "Freq " + Frequency + "\n" +
               "Init " + InitialPasses + "\n" +
               "Symb " + Symbols + "\n" +
               "Name " + Name + "\n" +
               "Rng1 " + X1 + "\n" +
               "Rng2 " + X2 + "\n" +
               "Step " + Step + "\n" +
               "Cent " + Center + "\n";
    }
}