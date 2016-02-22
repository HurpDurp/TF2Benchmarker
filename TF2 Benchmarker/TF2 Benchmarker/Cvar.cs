using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2_Benchmarker
{
    struct Cvar
    {
        public readonly string Command, Value;
        public readonly List<Cvar> MultiLineCommand;

        public Cvar(string c, string v)
        {
            Command = c;
            Value = v;
            MultiLineCommand = null;
        }

        public Cvar(string c, List<Cvar> m)
        {
            Command = c;
            Value = null;
            MultiLineCommand = new List<Cvar>(m);
        }

        public string Print()
        {
            if (Value != null)
                return Command + " " + Value;
            else
                return Command;
        }
    }
}
