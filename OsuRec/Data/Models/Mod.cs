using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OsuRec.Data.Models
{
    public class Mod
    {
        public ushort ModID { get; set; }
        public string ModName { get; set; }
        public string ModIMG { get; set; }
        private List<Mod> Mods = new List<Mod>();

        public List<Mod> ModCombo = new List<Mod>();


        private const string pathToJson = "../../OsuRec/OsuRec/Data/Models/mods.json";

        public void GetModIMGbyID(string modID)
        {
            ushort intmod = Convert.ToUInt16(modID);
            DecodeMods(intmod);
        }

        public void DecodeMods(ushort intmod)
        {
            Mods = SetMods();
        CycleAgain:
            ushort predmod = 0;
            ushort pastmod = 0;

            foreach (var item in Mods)
            {
                pastmod = item.ModID;
                if (intmod > predmod && intmod < pastmod)
                {
                    Mod mod = new Mod();
                    mod = Mods.Where(x => x.ModID == predmod).First();
                    ModCombo.Add(mod);
                    var tintmod = intmod - predmod;
                    intmod = Convert.ToUInt16(tintmod);
                    goto CycleAgain;
                }
                else if (intmod == item.ModID)
                {
                    Mod mod = new Mod();
                    mod = Mods.Where(x => x.ModID == item.ModID).First();
                    ModCombo.Add(mod);
                    break;
                }
                predmod = item.ModID;
            }
        }

        public static List<Mod> SetMods()
        {
            List<Mod> Mods = new List<Mod>();
            using (StreamReader r = new StreamReader(pathToJson))
            {
                string json = r.ReadToEnd();
                Mods = JsonConvert.DeserializeObject<List<Mod>>(json);
            }
            return Mods;
        }
    }
}
