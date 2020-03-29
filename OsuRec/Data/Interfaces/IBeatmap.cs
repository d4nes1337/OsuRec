using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsuRec.Data.Interfaces
{
    interface IBeatmap
    {
        string beatmap_ID { get; set; }
        string enabled_mods { get; set; }
        string pp { get; set; }
    }
}
