using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsuRec.Data.mock
{
    public class MockRelatedBeatmap : Interfaces.IBeatmap
    {
        public string beatmap_ID { get; set; }
        public string pp { get; set; }
        public string enabled_mods { get; set; }
        public string rank { get; set; }
        public string beatmapLink { get; set; }
    }
}
