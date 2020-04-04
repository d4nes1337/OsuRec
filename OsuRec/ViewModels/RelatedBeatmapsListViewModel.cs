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

        private RelationProcess _relationProcess { get; set; }


        public RelatedBeatmapsListViewModel(string username)
        {
            this.username = username;
            RelationProcess _relationProcess = new RelationProcess();
            _relationProcess.ShowStats(username);
            RelatedBeatmapsByMainMod = _relationProcess.RelatedBeatmapsByMainMod;
            RelatedBeatmapsBySecondMod = _relationProcess.RelatedBeatmapsBySecondMod;
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

