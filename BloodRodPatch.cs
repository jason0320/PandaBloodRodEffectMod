using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Mod_PandaBloodRodEffectMod
{
    [HarmonyPatch]
    internal class BloodRodPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ActMelee), nameof(ActMelee.Attack))]
        internal static void BloodRodPrefix(ActMelee __instance)
        {
            Chara attacker = Act.CC;
            if (attacker == null) return;

            Thing equippedWeapon = null;
            foreach (BodySlot slot in attacker.body.slots)
            {
                if (slot.thing != null && slot.thing.id == "staff_blood")
                {
                    equippedWeapon = slot.thing;
                    break;
                }
            }
            if (equippedWeapon == null) return;

            int currentMP = attacker.mana.value;
            int maxMP = attacker.mana.max;

            if (currentMP <= 20) return;

            int mpCost = Mathf.FloorToInt(maxMP * 0.05f);
            int bonusDice = Mathf.Min(200, Mathf.FloorToInt(maxMP * 0.005f));

            attacker.mana.value -= mpCost;

            if (AttackProcess.Current != null)
            {
                var field = AccessTools.Field(typeof(AttackProcess), "diceNum");
                if (field != null)
                {
                    int original = (int)field.GetValue(AttackProcess.Current);
                    field.SetValue(AttackProcess.Current, original + bonusDice);
                }
            }
        }
    }
}
