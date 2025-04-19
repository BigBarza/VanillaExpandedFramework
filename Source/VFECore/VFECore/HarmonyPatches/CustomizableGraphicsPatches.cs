﻿using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace VFECore;

public static class CustomizableGraphicsPatches
{
    [HarmonyPatch]
    private static class InjectImpliedDefComps
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.DeclaredMethod(typeof(ThingDefGenerator_Buildings), "NewBlueprintDef_Thing");
            yield return AccessTools.DeclaredMethod(typeof(ThingDefGenerator_Buildings), "NewFrameDef_Thing");
        }

        private static void Postfix(ThingDef def, ThingDef __result)
        {
            // If the original def has the customizable graphics, insert it into blueprint
            // and frame so they get the gizmo and default graphic as well.
            var props = def.GetCompProperties<CompProperties_CustomizableGraphic>();
            if (props != null)
                __result.comps.Add(props);
        }
    }

    [HarmonyPatch(typeof(Blueprint_Build), "MakeSolidThing")]
    private static class PreserveBlueprintOverride
    {
        private static void Postfix(Blueprint_Build __instance, Thing __result)
        {
            // If working with customizable graphic, preserve the
            // graphic override index when replacing it with a frame.
            if (__instance.HasComp<CompCustomizableGraphic>())
                __result.overrideGraphicIndex = __instance.overrideGraphicIndex;
        }
    }

    [HarmonyPatch(typeof(GhostUtility), nameof(GhostUtility.GhostGraphicFor))]
    private static class UseUiIconForCustomizableGraphicGhosts
    {
        // The ghost after selecting a building is forced to use the same graphic class as the building itself.
        // We can't easily force it to draw the one we want without this patch. This patch causes the building
        // to use a single graphic using the UI icon path for the graphic, same as linked buildings and single-tile doors.
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var targetMethod = AccessTools.DeclaredPropertyGetter(typeof(GraphicData), nameof(GraphicData.Linked));
            var methodToInsert = AccessTools.DeclaredMethod(typeof(UseUiIconForCustomizableGraphicGhosts), nameof(IsCustomizableGraphic));
            var totalPatched = 0;

            foreach (var instr in instructions)
            {
                yield return instr;

                if (instr.Calls(targetMethod))
                {
                    // Load the arg with index 1 (ThingDef for the building)
                    yield return CodeInstruction.LoadArgument(1);
                    // Call our method to check if the building has the customizable graphic comp
                    yield return new CodeInstruction(OpCodes.Call, methodToInsert);
                    // Logical or the 2 bools. Both will be executed (which is not ideal), but since the code will
                    // be called at most once per graphic/thing/color/stuff combination
                    yield return new CodeInstruction(OpCodes.Or);

                    totalPatched++;
                }
            }

            const int expectedPatched = 1;
            if (totalPatched != expectedPatched)
                Log.Error($"Patched incorrect amount of instructions for {nameof(GhostUtility)}.{nameof(GhostUtility.GhostGraphicFor)}. Expected: {expectedPatched}, patched: {totalPatched}.");
        }

        private static bool IsCustomizableGraphic(ThingDef def) => def.GetCompProperties<CompProperties_CustomizableGraphic>() != null;
    }

    [HarmonyPatch(typeof(GraphicUtility), nameof(GraphicUtility.ExtractInnerGraphicFor))]
    private static class UseCorrectGraphicForMinifiedThing
    {
        private static bool Prefix(Graphic outerGraphic, Thing thing, ref int? indexOverride, ref Graphic __result)
        {
            // GraphicUtility.ExtractInnerGraphicFor does not support Graphic_Indexed
            // subtypes, so we need to add support for it ourselves.

            // If the graphic is not Graphic_Indexed, or there's no comp, let vanilla handle it
            if (outerGraphic is not Graphic_Indexed indexed || !thing.HasComp<CompCustomizableGraphic>())
                return true;

            // If index is provided, use it
            if (indexOverride != null)
                __result = indexed.SubGraphicAtIndex(indexOverride.Value);
            // If no index and thing is not null, use it
            else if (thing != null)
                __result = indexed.SubGraphicFor(thing);
            // Otherwise, let vanilla handle the graphics here
            else
                return true;

            // If we replaced the result, prevent vanilla code from running
            return false;
        }
    }
}