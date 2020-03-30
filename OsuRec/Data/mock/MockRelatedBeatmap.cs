using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public string title { get; set; }
        public string artist { get; set; }
        public string coverLink { get; set; }

        public MockRelatedBeatmap SetBeatmapInfo(MockRelatedBeatmap relatedBeatmap)
        {
            string webpage;
            using (WebClient client = new WebClient())
            {
                webpage = client.DownloadString(relatedBeatmap.beatmapLink);
            }

            string beatmapset_id = "";
            int Start, End;

            //betmapset_id get+set
            if (webpage.Contains("\"beatmapset_id\":") && webpage.Contains(",\"mode\":\"osu\""))
            {
                Start = webpage.IndexOf("\"beatmapset_id\":", 0) + "\"beatmapset_id\":".Length;
                End = webpage.IndexOf(",\"mode\":\"osu\"", Start);
                beatmapset_id = webpage.Substring(Start, End - Start);
            }
            relatedBeatmap.coverLink = "https://assets.ppy.sh/beatmaps/" + beatmapset_id + "/covers/cover.jpg";
            //artist get+set
            if (webpage.Contains("\"artist\":") && webpage.Contains(",\"play_count\":"))
            {
                Start = webpage.IndexOf("\"artist\":", 0) + "\"artist\":".Length;
                End = webpage.IndexOf(",\"play_count\":", Start);
                relatedBeatmap.artist = webpage.Substring(Start + 1, End - Start - 2);
            }
            //title get+set
            if (webpage.Contains("\"title\":") && webpage.Contains(",\"artist\":"))
            {
                Start = webpage.IndexOf("\"title\":", 0) + "\"title\":".Length;
                End = webpage.IndexOf(",\"artist\":", Start);
                relatedBeatmap.title = webpage.Substring(Start + 1, End - Start - 2);
            }
            return relatedBeatmap;
        }

    }
}
