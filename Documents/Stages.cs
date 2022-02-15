using System;
using System.Collections.Generic;
using System.Text;

namespace Documents
{
    internal class Stages
    {
        public string Name { get; set; }
        public string Performer { get; set; }
    }
    internal class StagesList
    {
        public List<Stages> Stages { get; set; }
    }
}
