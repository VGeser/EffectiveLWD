using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Model
{
    internal class ProtocolData
    {
        public ObservableCollection<ProtocolRule> ProtocolRules { get; set; } = new ObservableCollection<ProtocolRule>();
    }
}
