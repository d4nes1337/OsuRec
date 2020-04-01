using OsuRec.Data;
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
        public string username { get; set; }
        public List<RelatedBeatmap> RelatedBeatmapsByMainMod { get; set; }
        public List<RelatedBeatmap> RelatedBeatmapsBySecondMod { get; set; }

        //private List<string> cbl_items = new List<string>();

        private static RelationProcess _relationProcess = new RelationProcess();


        public RelatedBeatmapsListViewModel(string username)
        {
            this.username = username;
            RelatedBeatmapsByMainMod = GetRelatedBeatmapsByMainMod(username);
            RelatedBeatmapsBySecondMod = GetRelatedBeatmapsBySecondMod(username);
        }

        public static List<RelatedBeatmap> GetRelatedBeatmapsByMainMod(string username)
        {
            _relationProcess.ShowStats(username);
            var relatedMapsByMainMod = _relationProcess.RelatedBeatmapsByMainMod;
            return relatedMapsByMainMod;
        }
        public static List<RelatedBeatmap> GetRelatedBeatmapsBySecondMod(string username)
        {
            var relatedMapsBySecondMod = _relationProcess.RelatedBeatmapsBySecondMod;
            return relatedMapsBySecondMod;
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

