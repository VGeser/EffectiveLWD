using LearningCSharp;

namespace MMM_SimulatorTests;

public class SlicerTests
{
    private readonly Slicer _slicer;
    private readonly Dictionary<String, Double?[]> _dictionary;

    public SlicerTests()
    {
        LasReader reader = new LasReader();
        _dictionary = reader.SaveDict(@"..\..\..\testdata\2022_09_21_21_18_13_277.gti.las", "utf-8");
        this._slicer = new Slicer(_dictionary);
    }

    [Test]
    public void TestSlicing()
    {
        Dictionary<String, Double?> dictionary = _slicer.GetSlice(0);
        foreach (KeyValuePair<String, Double?> pair in dictionary)
        {
            Assert.AreEqual(pair.Value, _dictionary[pair.Key][0]);
        }
    }
}