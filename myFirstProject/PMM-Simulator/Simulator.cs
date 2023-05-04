namespace SimulatorSubsystem;

public class Simulator
{
    private readonly RuleTable _ruleTable;
    private readonly Slicer _slicer;

    // public Simulator(RuleTable ruleTable, Dictionary<String, Double?[]> data)
    // {
    //     _ruleTable = ruleTable;
    //     _slicer = new Slicer(data);
    // }
    //
    // public (int, int, int, string)[] Simulate((int, int, int) times, (bool, bool, bool)[] states,
    //     int fileInterval, int messageInterval)
    // {
    //     Entry current = _ruleTable.GetCurrentRule();
    //     Encoder encoder = new Encoder();
    //     
    //     int slice = 0;
    //     long timer = 0;
    //
    //     (int, int, int, string)[] res = new (int, int, int, string)[states.Length];
    //     int i = 0;
    //     
    //     foreach((bool, bool, bool) state in states)
    //     {
    //         (int, int, int, string) stepRes = (-1,-1,-1,"");
    //         stepRes.Item1 = slice;
    //         String message = encoder.Encode(current.Param, _slicer.GetSlice(slice * (messageInterval / fileInterval)));
    //         stepRes.Item4 = message;
    //
    //         stepRes.Item2 = (int)timer;
    //         stepRes.Item3 = (int)(timer += CountTime(message, times));
    //         current = _ruleTable.GetAndSetNextRule(
    //                       new Entry.ChoiceCondition(state.Item1, state.Item2, state.Item3)) 
    //                   ?? throw new InvalidOperationException();
    //
    //         if (timer < (slice + 1) * messageInterval)
    //         {
    //             timer = (slice + 1) * messageInterval;
    //             slice++;
    //         }
    //         else
    //         {
    //             slice = (int)(timer / messageInterval);
    //         }
    //
    //         res[i] = stepRes;
    //         i++;
    //     }
    //
    //     return res;
    // }
    //
    // private static int CountTime(string message, (int, int, int) times)
    // {
    //     int res = times.Item1;
    //     foreach (char c in message.Substring(1))
    //     {
    //         res += times.Item2;
    //         res += (c - '0')*times.Item3;
    //     }
    //
    //     return res;
    // }
}