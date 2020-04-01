using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OsuRec.Data.Models
{
    public class RelationProcess
    {
        //TODO: Make acc comparison
        
        
        public PlayStyleInfo playStyle = new PlayStyleInfo();
        private OsuAPI.DataProcessing dataProcessing = new OsuAPI.DataProcessing();

        public List<string> MainModBeatmaps = new List<string>();
        public List<string> SecondModBeatmaps = new List<string>();

        private List<Score> MainModScores = new List<Score>();
        private List<Score> SecondModScores = new List<Score>();

        public List<RelatedBeatmap> RelatedBeatmapsByMainMod = new List<RelatedBeatmap>();
        public List<RelatedBeatmap> RelatedBeatmapsBySecondMod = new List<RelatedBeatmap>();

        public string MainMod;
        public string SecondMod = "none";
        public decimal PPRangeFrom;
        public decimal PPRangeTo;
        private CultureInfo culture = new CultureInfo("en-US");
        private const string LinkBase = "https://osu.ppy.sh/b/";

        private dynamic SetPlayStyle(string Nickname)
        {
            dynamic userData = playStyle.GetInfo(Nickname);
            playStyle.ModDistribution();
            playStyle.GetBeatmapsByMods();
            MainModBeatmaps = playStyle.BeatmapsByMainMod;
            SecondModBeatmaps = playStyle.BeatmapsBySecondMod;
            PPRangeFrom = playStyle.PPRangeFrom;
            PPRangeTo = playStyle.PPRangeTo;
            MainMod = playStyle.MainMod;
            SecondMod = playStyle.SecondMod;
            return userData;
        }

        private void GetScoresByBeatmaps()
        {
            MainModScores = IfScoreIsInPPRange(MainModBeatmaps, MainMod);
            if (SecondMod != "none")
            {
                SecondModScores = IfScoreIsInPPRange(SecondModBeatmaps, SecondMod);
            }
        }


        private List<Score> IfScoreIsInPPRange(List<string> ModBeatmaps, string Mod)
        {
            List<Score> beatmapScores = new List<Score>();
            foreach (var item in ModBeatmaps)
            {
                int i = 0;
                dynamic scores = dataProcessing.GetBeatmapScores(item, Mod);
                foreach (var ss in scores)
                {
                    if (Convert.ToDecimal(ss.pp, culture) >= PPRangeFrom && Convert.ToDecimal(ss.pp, culture) <= PPRangeTo)
                    {
                        Score score = new Score();
                        score.beatmap_ID = item;
                        score.enabled_mods = Mod;
                        score.user_id = ss.user_id;
                        score.pp = ss.pp;
                        beatmapScores.Add(score);
                        i++;
                    }
                    if (i == 3)
                    {
                        break;
                    }
                }
            }
            return beatmapScores;
        }


        private void GetRelatedUsersScores(List<Score> scores)
        {
            foreach (var item in scores)
            {
                dynamic scoresOfUser = dataProcessing.GetUserScores(item.user_id);
                foreach (var score in scoresOfUser)
                {
                    SortScore(score);
                }
            }
        }

        private void SortScore(dynamic score)
        {
            if (Convert.ToDecimal(score.pp, culture) >= PPRangeFrom && Convert.ToDecimal(score.pp, culture) <= PPRangeTo)
            {
                if (score.enabled_mods == MainMod)
                {
                    RelatedBeatmap relatedBeatmap = AssembleRelatedBeatmap(score, MainMod);
                    RelatedBeatmapsByMainMod.Add(relatedBeatmap);
                }
                else if (score.enabled_mods == SecondMod)
                {
                    RelatedBeatmap relatedBeatmap = AssembleRelatedBeatmap(score, SecondMod);
                    RelatedBeatmapsBySecondMod.Add(relatedBeatmap);
                }
            }
        }


        private RelatedBeatmap AssembleRelatedBeatmap(dynamic score, string mod)
        {
            RelatedBeatmap relatedBeatmap = new RelatedBeatmap();
            relatedBeatmap.beatmap_ID = score.beatmap_id;
            relatedBeatmap.pp = score.pp;
            relatedBeatmap.enabled_mods = mod;
            relatedBeatmap.rank = score.rank;
            return relatedBeatmap;
        }


        #region Related beatmap list sortng methods
        private List<RelatedBeatmap> SortSimilarToTopScores(List<RelatedBeatmap> relatedBeatmaps, List<string> modBeatmapIDs)
        {
            return relatedBeatmaps.Where(x => !(modBeatmapIDs.Any(y => y == x.beatmap_ID))).Distinct().ToList();

        }
        private List<RelatedBeatmap> AddLinks(List<RelatedBeatmap> relatedBeatmaps)
        {
            foreach (var item in relatedBeatmaps)
            {
                item.beatmapLink = LinkBase + item.beatmap_ID;
            }
            return relatedBeatmaps;
        }

        //private List<mock.MockRelatedBeatmap> FillBeatmapInfo(List<mock.MockRelatedBeatmap> relatedBeatmaps)
        //{
        //    foreach (var item in relatedBeatmaps)
        //    {
        //        SetBeatmapInfo(item);
        //    }
        //    return relatedBeatmaps;
        //}
        #endregion


        #region Setting methods
        public dynamic ShowStats(string Nickname)
        {
            dynamic userData = SetPlayStyle(Nickname);
            GetScoresByBeatmaps();
            GetRelatedUsersScores(MainModScores);
            //console for test purposes
            //Console.WriteLine($"Main mod: {playStyle.MainMod}, second mod {playStyle.SecondMod}");
            //Console.WriteLine($"1: {MainModBeatmaps[0]}, 10 :{MainModBeatmaps[9]}");
            //Console.WriteLine($"PP from: {PPRangeFrom}, to :{PPRangeTo}");
            RelatedBeatmapsByMainMod = SortSimilarToTopScores(RelatedBeatmapsByMainMod, MainModBeatmaps);
            RelatedBeatmapsBySecondMod = SortSimilarToTopScores(RelatedBeatmapsBySecondMod, SecondModBeatmaps);
            RelatedBeatmapsByMainMod = AddLinks(RelatedBeatmapsByMainMod);
            RelatedBeatmapsBySecondMod = AddLinks(RelatedBeatmapsBySecondMod);
            return userData;
        }
        #endregion
    }
}
