using Commons;

namespace SimulatorSubsystem;

public class MainClass
{
    public static void Main()
    {
        TableDataHolder _holder = new TableDataHolder();
        _holder.Frequency = 1;
        _holder.Name = "PRES";
        _holder.InitialPasses = 0;
        _holder.Center = 70;
        _holder.RuleMask = new[] { true, true, true };
        _holder.Step = 1;
        _holder.Symbols = 3;
        _holder.X1 = 0;
        _holder.X2 = 100;
        int binSize = 5;
        int radix = 16;
        RuleTable _ruleTable = new RuleTable();
        int id = 0;
        Entry.ChoiceCondition condition = new Entry.ChoiceCondition(_holder.RuleMask[0],_holder.RuleMask[1],_holder.RuleMask[2]);
        Entry.ChoiceBoundaries boundaries = new Entry.ChoiceBoundaries(_holder.Frequency, _holder.InitialPasses);
        List<Entry.EncodedParameter> encoded = new List<Entry.EncodedParameter>();
        EncodingHistogramCreatorWithNormal creatorWithNormal = new EncodingHistogramCreatorWithNormal();
        EncodingHistogram histogram = creatorWithNormal.Create(binSize, _holder.Step, _holder.X1, _holder.X2, _holder.Center, radix, _holder.Symbols);
        encoded.Add(new Entry.SimpleEncodedParameter(_holder.Name, radix, _holder.Symbols, histogram));
        Entry.Parameters parameters = new Entry.Parameters(encoded);
        Entry entry = new Entry(condition, boundaries, parameters,id);
        _ruleTable.AddRule(entry);
        
        Console.WriteLine("settings passed");
        
        Decoder decoder = new Decoder(_ruleTable);
        Console.WriteLine("RES: " + decoder.Decode("S0202")["PRES"]);
    }
}