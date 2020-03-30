using OsuRec.Data.mock;
using OsuRec.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OsuRec.ViewModels
{
    public class RelatedBeatmapsListViewModel
    {
        public List<MockRelatedBeatmap> RelatedBeatmaps { get; set; } = GetRelatedBeatmaps();

        //private List<string> cbl_items = new List<string>();

        private static RelationProcess _relationProcess = new RelationProcess();

        
        
        
        public static List<MockRelatedBeatmap> GetRelatedBeatmaps()
        {
            _relationProcess.ShowStats("D4NES1337");
            var relatedMapsByMainMod = _relationProcess.RelatedBeatmapsByMainMod;
            return relatedMapsByMainMod;
        }




        




        //empty substring checker
        //Why? IDK
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            const int kNotFound = -1;
            strStart = "title";
            strEnd = "hit_length";
            var startIdx = strSource.IndexOf(strStart);
            if (startIdx != kNotFound)
            {
                startIdx += strStart.Length;
                var endIdx = strSource.IndexOf(strEnd, startIdx);
                if (endIdx > startIdx)
                {
                    return strSource.Substring(startIdx, endIdx - startIdx);
                }
            }
            return String.Empty;
        }
    }
}

