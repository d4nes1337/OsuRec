using OsuRec.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsuRec.Data.mock
{
    public class MockScore : IBeatmap
    {
        public string beatmap_ID { get; set; }
        public string enabled_mods { get; set; }
        public string user_id { get; set; }
        public string pp { get; set; }
    }
}

