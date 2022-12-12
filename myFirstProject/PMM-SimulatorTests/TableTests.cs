using LearningCSharp;

namespace MMM_SimulatorTests;

public class TableTests
{
    private Table _table;

    public TableTests()
    {
        this._table = new Table();
    }
    
    [SetUp]
    public void Setup()
    {
        _table = new Table();
        
        Entry.ChoiceCondition truthy = new Entry.ChoiceCondition(true, true, true);
        
        _table.AddRule(new Entry(
            truthy,
            new Entry.ChoiceBoundaries(1, 0),
            new Entry.Parameters("S0", new List<(string, int)>()),
            0));
        _table.AddRule(new Entry(
            truthy,
            new Entry.ChoiceBoundaries(1, 0),
            new Entry.Parameters("S1", new List<(string, int)>()),
            1));
        _table.AddRule(new Entry(
            truthy,
            new Entry.ChoiceBoundaries(1, 1),
            new Entry.Parameters("S2", new List<(string, int)>()),
            2));
        _table.AddRule(new Entry(
            truthy,
            new Entry.ChoiceBoundaries(2, 0),
            new Entry.Parameters("S3", new List<(string, int)>()),
            3));
        _table.AddRule(new Entry(
            new Entry.ChoiceCondition(true, false, true),
            new Entry.ChoiceBoundaries(1, 0),
            new Entry.Parameters("S4", new List<(string, int)>()),
            4));
    }

    [Test]
    public void TestDefaultValue()
    {
        Entry firstRule = _table.GetCurrentRule();
        Assert.AreEqual(0, firstRule.Id);
    }

    [Test]
    public void TestFirstIteration()
    {
        Entry? nextRule = _table.GetAndSetNextRule(new Entry.ChoiceCondition(true, true, true));
        if (nextRule != null)
        {
            Assert.AreEqual(1, nextRule.Id);
        }
        else throw new Exception();
    }
    
    [Test]
    public void TestInitialPassesBoundary()
    {
        Entry? checkedRule = null;
        for (int i = 0; i < 2; i++)
        {
            checkedRule = _table.GetAndSetNextRule(new Entry.ChoiceCondition(true, true, true));
        }
        if (checkedRule != null)
        {
            Assert.AreEqual(0, checkedRule.Id);
        }
        else throw new Exception();
        
        for (int i = 0; i < 2; i++)
        {
            checkedRule = _table.GetAndSetNextRule(new Entry.ChoiceCondition(true, true, true));
        }
        if (checkedRule != null)
        {
            Assert.AreEqual(2, checkedRule.Id);
        }
        else throw new Exception();
    }
    
    [Test]
    public void TestFrequencyBoundary()
    {
        Entry? checkedRule = null;
        
        //just skipping property with initialPasses
        for (int i = 0; i < 2; i++)
        {
            checkedRule = _table.GetAndSetNextRule(new Entry.ChoiceCondition(true, true, true));
        }
        //Now ID is 0, round is 2, so in 3 next-s we should get the rule with frequency=2
        for (int i = 0; i < 3; i++)
        {
            checkedRule = _table.GetAndSetNextRule(new Entry.ChoiceCondition(true, true, true));
        }
        if (checkedRule != null)
        {
            Assert.AreEqual(3, checkedRule.Id);
        }
        //Now, after 4 passes, we should return to 0th rule, because round is not divisible by 2
        for (int i = 0; i < 4; i++)
        {
            checkedRule = _table.GetAndSetNextRule(new Entry.ChoiceCondition(true, true, true));
        }
        if (checkedRule != null)
        {
            Assert.AreEqual(0, checkedRule.Id);
        }
        //And back to 3rd rule after next 3 next-s
        for (int i = 0; i < 3; i++)
        {
            checkedRule = _table.GetAndSetNextRule(new Entry.ChoiceCondition(true, true, true));
        }
        if (checkedRule != null)
        {
            Assert.AreEqual(3, checkedRule.Id);
        }
    }

    [Test]
    public void TestChoiceConditions()
    {
        Entry? checkedRule = _table.GetAndSetNextRule(new Entry.ChoiceCondition(true, false, true));
        if (checkedRule != null)
        {
            Assert.AreEqual(4, checkedRule.Id);
        }
        else throw new Exception();
        Assert.Null(_table.GetAndSetNextRule(new Entry.ChoiceCondition(true, false, true)));
    }
}