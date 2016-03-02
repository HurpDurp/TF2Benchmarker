using System.Collections.Generic;

namespace TF2_Benchmarker
{
    /// <summary>
    /// Struct containing a name and value, for storing TF2 console cvars.
    /// 
    /// It may contain a list of other cvars inside of it, for use in multiline commands.
    /// </summary>
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
