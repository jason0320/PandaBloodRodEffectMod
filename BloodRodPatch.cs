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

            int currentMP = _CC.mana.value;

            int A = Mathf.Clamp(currentMP / 200, 1, 200);

            int B = Mathf.Clamp(currentMP / 400, 1, 100);

            __instance.dDim += A;
            __instance.dBonus += B;

            int cost = currentMP / 20;
            _CC.mana.value = currentMP - cost;
        }
    }
}
