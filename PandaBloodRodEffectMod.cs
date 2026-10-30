using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;

namespace PandaBloodRodEffectMod
{
    [BepInPlugin("panda.bloodrodeffect.mod", "Panda's Elonaplus Blood Rod Effect Mod", "1.0.0.0")]

    public class PandaBloodRodEffectMod : BaseUnityPlugin
    {
        private void OnStartCore()
        {
            var harmony = new Harmony("Panda's Elonaplus Blood Rod Effect Mod");
            harmony.PatchAll();
        }
    }
}
