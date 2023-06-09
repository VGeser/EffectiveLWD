using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Model
{
    public  class ProtocolSelectCondition
    {
        public bool isRotor { get; set; }
        public bool isStat { get; set; }
        public bool isTfgFlag { get; set; }

        public int Frequency { get; set; }

        public int InitialPasses { get; set; }
    }
}
