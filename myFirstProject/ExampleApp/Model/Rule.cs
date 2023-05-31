using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Model
{
    internal class Rule
    {
        public string Name { get; set; }

        public IList<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}
