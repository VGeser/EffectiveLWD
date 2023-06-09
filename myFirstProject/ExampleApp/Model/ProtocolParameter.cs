using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Model
{
    public class ProtocolParameter
    {
        public string Name { get; set; }

        public int RangeFrom { get; set; }

        public int RangeTo { get; set; }

        public int CenterBinStart { get; set; }

        public double Step { get; set; }

        public string Symbols { get; set; }
    }
}
