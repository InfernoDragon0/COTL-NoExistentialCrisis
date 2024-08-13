using HarmonyLib;
using Lamb.UI;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NoExistentialCrisis.Patches
{
    [HarmonyPatch]
    internal class CrisisAvertedPatch
    {
        [HarmonyPatch(typeof(RitualRessurect), nameof(RitualRessurect.DoRessurectRoutine), MethodType.Enumerator)]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> RitualRessurect_DoRessurectRoutine(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ldloc_S),
                    new CodeMatch(OpCodes.Ldc_R4),
                    new CodeMatch(OpCodes.Bge_Un)
                )
                .Advance(1)
                .SetOperandAndAdvance(Plugin.cursedChance.Value)
                .MatchForward(false,
                    new CodeMatch(OpCodes.Ldloc_S),
                    new CodeMatch(OpCodes.Ldc_R4),
                    new CodeMatch(OpCodes.Bge_Un)
                )
                .Advance(1)
                .SetOperandAndAdvance(Plugin.crisisChance.Value)
                .InstructionEnumeration();
        }



        
    }
}
