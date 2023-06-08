using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ExampleApp.Commands;
using ExampleApp.Model;
using ExampleApp.ViewModels.Base;
using Ipgg.LasParser;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using SimulatorSubsystem;

namespace ExampleApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        
        public ObservableCollection<ProtocolRule> Rules { get; set; }
        public ObservableCollection<PlotWithName> Charts { get; set; }

        private ProtocolRule _selectedRule;

        private PlotWithName _series;

        private SimulationStatistics _statistics;

        private SimulationTimes _times;

        public ProtocolRule SelectedRule
        {
            get => _selectedRule;
            set => Set(ref _selectedRule, value);
        }
        
        public PlotWithName Series
        {
            get => _series;
            set => Set(ref _series, value);
        }
        
        public SimulationStatistics Statistics
        {
            get => _statistics;
            set => Set(ref _statistics, value);
        }

        public SimulationTimes Times
        {
            get => _times;
            set => Set(ref _times, value);
        }
        /* #region Заголовок окна

         private string _Title = "Редактирование протокола передачи";

         /// <summary> Заголовок окна </summary>
         public string Title
         {
             get => _Title;
             set => Set(ref _Title, value);
         }
         #endregion
 */
        public ICommand CreateRuleCommand { get; }

        private bool CanCreateRuleCommandExecute(object p) => true;

        private void OnCreateRuleCommandExecuted(object p)
        {
            var ruleMaxIndex = Rules.Count + 1;
            var conditions = Enumerable.Range(1, 1).Select(_ => new ProtocolSelectCondition
            {
                isRotor = false,
                isStat = true,
                isTfgFlag = false,
                Frequency = 10,
                InitialPasses = 5,
            });

            var parameters = Enumerable.Range(1, 1).Select(_ => new ProtocolParameter
            {
                Name = "Name",
                RangeFrom = 1,
                RangeTo = 10,
                CenterBinStart = 5,
                Step = 1,
                Symbols = "a"
            });

            var newRule = new ProtocolRule()
            {
                Name = $"Правило {ruleMaxIndex}",
                SelectCondition = new List<ProtocolSelectCondition>(conditions),

                Parameters = new List<ProtocolParameter> (parameters)
            };

            Rules.Add(newRule);
        }


        public ICommand DeleteRuleCommand { get; }

        private bool CanDeleteRuleCommandExecuted(object p) => p is ProtocolRule rule && Rules.Contains(rule);

        private void OnDeleteRuleCommandExecuted(object p)
        {
            if (!(p is ProtocolRule rule)) return;

            Rules.Remove(rule);
        }
        
        
        public ICommand StartSimulationCommand { get; }
        
        private bool CanStartSimulationCommandExecuted(object p) => true;

        private void OnStartSimulationCommandExecuted(object p)
        {
            Charts.Clear();
            
            LLasParserVlasov parserVlasov = new LLasParserVlasov();
            parserVlasov.ReadFile(Download.f_name, "utf-8");
            Slicer slicer = new Slicer(parserVlasov.Data);
            if (slicer.ContainsNull("PRES") != -1)
            {
                throw new Exception(slicer.ContainsNull("PRES").ToString());
            }
            ProtocolData protocol = new ProtocolData
            {
                ProtocolRules = Rules
            };
            Simulator simulator = new Simulator(protocol);
            DecodedMessageData decodedMessageData = simulator.Simulate(16, 5, slicer, new TimeData(Times.Synchro, Times.Tick, Times.Start));

            Statistics.TotalMessages = decodedMessageData.GetSize();
            Statistics.FileLength = slicer.GetSize();
            
            Dictionary<string, List<ObservablePoint>> messagePlots = new Dictionary<string, List<ObservablePoint>>();
            Dictionary<string, List<ObservablePoint>> filePlots = new Dictionary<string, List<ObservablePoint>>();

            foreach (string s in slicer.GetSlice(0).Keys)
            {
                messagePlots.Add(s, new List<ObservablePoint>());
                filePlots.Add(s, new List<ObservablePoint>());
            }

            for (int i = 0; i < slicer.GetSize(); i++)
            {
                Dictionary<String, Double?> slice = slicer.GetSlice(i);
                foreach (var variable in slice.Keys)
                {
                    filePlots[variable].Add(new ObservablePoint(i * 250, slice[variable]));
                }
            }

            List<int> times = decodedMessageData.GetTimes();
            List<Dictionary<String, Double>> messages = decodedMessageData.GetMessages();

            for(int i = 0; i < decodedMessageData.GetSize(); i++)
            {
                int time = times[i];
                Dictionary<String, double> values = messages[i];

                foreach (var variable in values.Keys)
                {
                    messagePlots[variable].Add(new ObservablePoint(time, values[variable]));
                }
            }

            int totalTime = slicer.GetSize() * 250;
            foreach (string s in messagePlots.Keys)
            {
                Statistics.ParameterStats.Remove(s);
                Statistics.ParameterStats.Add(s, new ParameterStatistics((double)messagePlots[s].Count / totalTime));
                
                Charts.Add(new PlotWithName
                {
                    Name = s,
                    Plot = new List<ISeries>
                    {
                        new LineSeries<ObservablePoint>
                        {
                            Values = messagePlots[s],
                            LineSmoothness = 0,
                            GeometryFill = null,
                            GeometryStroke = null
                        },
                        new LineSeries<ObservablePoint>
                        {
                            Values = filePlots[s],
                            LineSmoothness = 0,
                            GeometryFill = null,
                            GeometryStroke = null
                        }
                    }
                });
            }

            Series = Charts[0];
        }
        
        public MainWindowViewModel()
        {
            CreateRuleCommand = new LambdaCommand(OnCreateRuleCommandExecuted, CanCreateRuleCommandExecute);
            DeleteRuleCommand = new LambdaCommand(OnDeleteRuleCommandExecuted, CanDeleteRuleCommandExecuted);
            StartSimulationCommand = new LambdaCommand(OnStartSimulationCommandExecuted, CanStartSimulationCommandExecuted);


            var conditions = Enumerable.Range(1, 1).Select(_ => new ProtocolSelectCondition
            {
                isRotor = true,
                isStat = true,
                isTfgFlag = true,
                Frequency = 1,
                InitialPasses = 0,
            });

            var parameters = Enumerable.Range(1, 1).Select(_ => new ProtocolParameter
            {
                Name = "PRES",
                RangeFrom = 0,
                RangeTo = 100,
                CenterBinStart = 70,
                Step = 1,
                Symbols = "4"
            });

            var rules = Enumerable.Range(1, 1).Select(i => new ProtocolRule
            {
                Name = $"Правило {i}",
                SelectCondition = new List<ProtocolSelectCondition>(conditions),

                Parameters = new List<ProtocolParameter> (parameters)
            });

            Rules = new ObservableCollection<ProtocolRule>(rules);

            var plots = new List<PlotWithName>();
            Charts = new ObservableCollection<PlotWithName>(plots);
            _statistics = new SimulationStatistics();
            _times = new SimulationTimes
            {
                Synchro = 1000,
                Start = 100,
                Tick = 400
            };
        }
    }
}

