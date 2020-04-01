using OsuRec.Data.Models.OsuAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OsuRec.Data.Models
{
    public class PlayStyleInfo
    {

        private List<string> ModsInTopScores = new List<string>();
        private Dictionary<string, string> BeatmapIDsToMods = new Dictionary<string, string>(); //(beatmap_id : mod)//
        private DataProcessing dataProcessing = new DataProcessing();
        private List<string> UserPP = new List<string>();

        public List<string> BeatmapsByMainMod { get; private set; } = new List<string>();
        public List<string> BeatmapsBySecondMod { get; private set; } = new List<string>();

        public string MainMod { get; private set; }
        public string SecondMod { get; private set; } = "none";
        public decimal PPRangeFrom { get; private set; }
        public decimal PPRangeTo { get; private set; }
        private CultureInfo culture = new CultureInfo("en-US");



        #region Entered user data processing mthods
        public void ModDistribution()
        {
            var distribution = new Dictionary<string, int>();
            foreach (var item in ModsInTopScores)
            {
                if (distribution.ContainsKey(item))
                {
                    distribution[item]++;
                }
                else
                {
                    distribution.Add(item, 1);
                }
            }
            var mainModCount = distribution.Values.Max();
            MainMod = distribution.FirstOrDefault(x => x.Value == mainModCount).Key;
            distribution.Remove(MainMod);
            if (distribution.Keys.Count != 0)
            {
                var secondModCount = distribution.Values.Max();
                SecondMod = distribution.FirstOrDefault(x => x.Value == secondModCount).Key;
            }
        }

        public void GetBeatmapsByMods()
        {
            foreach (var item in BeatmapIDsToMods)
            {
                if (item.Value == MainMod)
                {
                    BeatmapsByMainMod.Add(item.Key);
                }
                else if (SecondMod != "none" && item.Value == SecondMod)
                {
                    BeatmapsBySecondMod.Add(item.Key);
                }
            }
            SortListsOfBeatmaps();
        }


        public void SortListsOfBeatmaps()
        {
            int n = BeatmapsByMainMod.Count();
            if (n > 10)
            {
                for (int i = 10; i < n; n--)
                {
                    BeatmapsByMainMod.RemoveAt(n - 1);
                }
            }
            int k = BeatmapsBySecondMod.Count();
            if (k > 4)
            {
                for (int i = 4; i < k; k--)
                {
                    BeatmapsBySecondMod.RemoveAt(k - 1);
                }
            }
        }


        // TODO: Make persentage instead of this simple summ algorythm
        // TODO: Distribution chack
        private void SetPPRange()
        {
            PPRangeFrom = Convert.ToDecimal(UserPP[4], culture);
            var ppRangeTo = Convert.ToDecimal(UserPP[0], culture);
            if (ppRangeTo - PPRangeFrom <= 20)
            { PPRangeTo = ppRangeTo + (ppRangeTo - PPRangeFrom) * 2; }
            else
            { PPRangeTo = ppRangeTo + (ppRangeTo - PPRangeFrom); }
        }
        #endregion

        #region Others
        public dynamic GetInfo(string Nickname)
        {
            dynamic userData = dataProcessing.GetUser(Nickname);
            dataProcessing.GetTopScores();
            foreach (var item in dataProcessing.userScores)
            {
                ModsInTopScores.Add(item.enabled_mods.ToString());
                BeatmapIDsToMods.Add(item.beatmap_id.ToString(), item.enabled_mods.ToString());
                UserPP.Add(item.pp.ToString());
            }
            SetPPRange();
            return userData; //
        }
        #endregion
    }
}
