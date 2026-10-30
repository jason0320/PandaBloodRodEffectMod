using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Mod_PandaBloodRodEffectMod
{
    [HarmonyPatch(typeof(AttackProcess), nameof(AttackProcess.Prepare))]
    internal class StaffBloodPatch
    {
        [HarmonyPostfix]
        static void PreparePostfix(AttackProcess __instance, Chara _CC, Thing _weapon)
        {
            if (_CC == null || _weapon == null) return;

            if (_weapon.id != "staff_blood") return;

            int maxMP = _CC.mana.max;
            int currentMP = _CC.mana.value;

            if (currentMP <= 20) return;
            if (maxMP <= 0) return;

            int mpCost = Mathf.FloorToInt(maxMP * 0.05f);
            int bonusDice = Mathf.Min(200, Mathf.FloorToInt(maxMP * 0.005f));

            if (mpCost <= 0 && bonusDice <= 0) return;

            _CC.mana.Mod(-mpCost);

            __instance.dNum += bonusDice;

            Msg.SayRaw($"[staff_blood] MP {currentMP} -> {_CC.mana.value} | dNum {__instance.dNum} (+{bonusDice})");
        }
    }
}
