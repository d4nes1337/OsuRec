﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OsuRec.Data.Models.OsuAPI
{
    public class DataProcessing
    {
        private const string base_url = "https://osu.ppy.sh";
        //"../../OsuRec/OsuRec/Data/Models/OsuAPI/Key.txt"
        //"h:/root/home/d4nes1337-001/www/osurec/Key.txt"
        private string Key = GetKey("h:/root/home/d4nes1337-001/www/osurec/Key.txt");
        private string ValidSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz1234567890";


        protected string ProfileName;
        protected Int64 ProfileID;
        public dynamic userScores { get; protected set; }

        //private const osu.Game.Rulesets.Osu.OsuRuleset Ruleset = new osu.Game.Rulesets.Osu.OsuRuleset(0);


        public dynamic GetUser(string Nickname)
        {
            ProfileName = Nickname; //Console.ReadLine();
            dynamic userData = getJsonFromApi($"get_user?k={Key}&u={ProfileName}")[0];
            /*Console.WriteLine($"User:  {userData.username} " +
                $"\nPP rank:  {userData.pp_rank}");*/
            return userData; //
        }
        public void GetTopScores()
        {
            dynamic userTopScores = getJsonFromApi($"get_user_best?k={Key}&u={ProfileName}&m=0&limit=30");
            userScores = userTopScores;
        }

        public dynamic GetBeatmapScores(string beatmapID, string mod)
        {
            dynamic beatmapScores = getJsonFromApi($"get_scores?k={Key}&b={beatmapID}&mods={mod}&limit=5");
            return beatmapScores;
        }

        public dynamic GetUserScores(string username)
        {
            dynamic userTopScores = getJsonFromApi($"get_user_best?k={Key}&u={username}&limit=100");
            return userTopScores;
        }



        #region Helpers
        private dynamic getJsonFromApi(string request)
        {
            WebClient client = new WebClient();
            string strPageCode = client.DownloadString($"{base_url}/api/{request}");
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(strPageCode);
            return dobj;
        }

        public static bool UsernameIsValid(string username) //False if username is invalid, if false => write: use userID
        {
            return !string.IsNullOrEmpty(username) && !Regex.IsMatch(username, @"[^a-zA-z\d_]");
        }
        private static string GetKey(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            return streamReader.ReadToEnd();
        }
        #endregion
    }
}