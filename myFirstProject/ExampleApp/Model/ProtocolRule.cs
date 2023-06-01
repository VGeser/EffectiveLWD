using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Model
{
    public class ProtocolRule
    {
        public IList<ProtocolSelectCondition> SelectCondition { get; set; } = new List<ProtocolSelectCondition>();
        public IList<ProtocolParameter> Parameters { get; set; } = new List<ProtocolParameter>();
        public string Name { get; set; }
    }
}
