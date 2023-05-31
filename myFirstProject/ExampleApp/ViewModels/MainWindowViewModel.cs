using ExampleApp.Commands;
using ExampleApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExampleApp.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public ObservableCollection<Rule> Rules { get; }

        private Rule _SelectedRule;

        public Rule SelectedRule
        {
            get => _SelectedRule;
            set => Set(ref _SelectedRule, value);
        }
        public ICommand CreateRuleCommand { get; }

        private bool CanCreateRuleCommandExecute(object p) => true;

        private void OnCreateRuleCommandExecuted(object p)
        {
            var rule_max_index = Rules.Count + 1;
            var parameters = Enumerable.Range(1, 1).Select(i => new Parameter
            {
                isRotor = false,
                isStat = true,
                isTfgFlag = false,
                Frequency = 10,
                InitialPasses = 5
            });
            var new_rule = new Rule()
            {
                Name = $"Правило {rule_max_index}",
                Parameters = new ObservableCollection<Parameter>(parameters)
            };

            Rules.Add(new_rule);
        }


        public ICommand DeleteRuleCommand { get; }

        private bool CanDeleteRuleCommandExecuted(object p) => p is Rule rule && Rules.Contains(rule);

        private void OnDeleteRuleCommandExecuted(object p)
        {
            if (!(p is Rule rule)) return;

            Rules.Remove(rule);
        }

        public MainWindowViewModel()
        {
            CreateRuleCommand = new LambdaCommand(OnCreateRuleCommandExecuted, CanCreateRuleCommandExecute);
            DeleteRuleCommand = new LambdaCommand(OnDeleteRuleCommandExecuted, CanDeleteRuleCommandExecuted);

            //var parameters_index = 1;
            var parameters = Enumerable.Range(1, 1).Select(i => new Parameter
            {
                isRotor = false,
                isStat = true,
                isTfgFlag = false,
                Frequency = 10,
                InitialPasses = 5
            });
            var rules = Enumerable.Range(1, 5).Select(i => new Rule
            {
                Name = $"Правило {i}",
                Parameters = new ObservableCollection<Parameter>(parameters)
            });
            Rules = new ObservableCollection<Rule>(rules);
        }

    }
}
   
