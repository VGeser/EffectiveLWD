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

        public ObservableCollection<ProtocolRule> Rules { get; set; }

        private ProtocolRule _SelectedRule;


        public ProtocolRule SelectedRule
        {
            get => _SelectedRule;
            set => Set(ref _SelectedRule, value);
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
            var rule_max_index = Rules.Count + 1;
            var conditions = Enumerable.Range(1, 1).Select(i => new ProtocolSelectCondition
            {
                isRotor = false,
                isStat = true,
                isTfgFlag = false,
                Frequency = 10,
                InitialPasses = 5,
            });

            var parameters = Enumerable.Range(1, 1).Select(i => new ProtocolParameter
            {
                Name = "Name",
                RangeFrom = 1,
                RangeTo = 10,
                CenterBinStart = 5,
                Step = 1,
                Symbols = "a"
            });

            var new_rule = new ProtocolRule()
            {
                Name = $"Правило {rule_max_index}",
                SelectCondition = new List<ProtocolSelectCondition>(conditions),

                Parameters = new List<ProtocolParameter> (parameters)
            };

            Rules.Add(new_rule);
        }


        public ICommand DeleteRuleCommand { get; }

        private bool CanDeleteRuleCommandExecuted(object p) => p is ProtocolRule rule && Rules.Contains(rule);

        private void OnDeleteRuleCommandExecuted(object p)
        {
            if (!(p is ProtocolRule rule)) return;

            Rules.Remove(rule);
        }

        public MainWindowViewModel()
        {
            CreateRuleCommand = new LambdaCommand(OnCreateRuleCommandExecuted, CanCreateRuleCommandExecute);
            DeleteRuleCommand = new LambdaCommand(OnDeleteRuleCommandExecuted, CanDeleteRuleCommandExecuted);


            var conditions = Enumerable.Range(1, 1).Select(i => new ProtocolSelectCondition
            {
                isRotor = false,
                isStat = true,
                isTfgFlag = false,
                Frequency = 10,
                InitialPasses = 5,
            });

            var parameters = Enumerable.Range(1, 1).Select(i => new ProtocolParameter
            {
                Name = "Name",
                RangeFrom = 1,
                RangeTo = 10,
                CenterBinStart = 5,
                Step = 1,
                Symbols = "a"
            });

            var rules = Enumerable.Range(1, 5).Select(i => new ProtocolRule
            {
                Name = $"Правило {i}",
                SelectCondition = new List<ProtocolSelectCondition>(conditions),

                Parameters = new List<ProtocolParameter> (parameters)
            });

            Rules = new ObservableCollection<ProtocolRule>(rules);
        }

    }
}

