﻿
using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;


namespace VanillaGenesExpanded
{

    public static class StaticCollectionsClass
    {

        //This static class stores lists of pawns for different things.


        // A list of pawns containing blood changing genes
        public static IDictionary<Thing, ThingDef> bloodtype_gene_pawns = new Dictionary<Thing, ThingDef>();
        // A list of pawns with custom blood icons
        public static IDictionary<Thing, string> bloodIcon_gene_pawns = new Dictionary<Thing, string>();
        // A list of pawns with custom blood effects
        public static IDictionary<Thing, EffecterDef> bloodEffect_gene_pawns = new Dictionary<Thing, EffecterDef>();
        // A list of pawns with custom wounds
        public static IDictionary<Thing, FleshTypeDef> woundsFromFleshtype_gene_pawns = new Dictionary<Thing, FleshTypeDef>();


        public static void AddBloodtypeGenePawnToList(Thing thing, ThingDef thingDef)
        {

            if (!bloodtype_gene_pawns.ContainsKey(thing))
            {
                bloodtype_gene_pawns[thing] = thingDef;
            }
        }

        public static void RemoveBloodtypeGenePawnFromList(Thing thing)
        {
            if (bloodtype_gene_pawns.ContainsKey(thing))
            {
                bloodtype_gene_pawns.Remove(thing);
            }

        }

        public static void AddBloodIconGenePawnToList(Thing thing, string icon)
        {

            if (!bloodIcon_gene_pawns.ContainsKey(thing))
            {
                bloodIcon_gene_pawns[thing] = icon;
            }
        }

        public static void RemoveBloodIconGenePawnFromList(Thing thing)
        {
            if (bloodIcon_gene_pawns.ContainsKey(thing))
            {
                bloodIcon_gene_pawns.Remove(thing);
            }

        }

        public static void AddBloodEffectGenePawnToList(Thing thing, EffecterDef effect)
        {

            if (!bloodEffect_gene_pawns.ContainsKey(thing))
            {
                bloodEffect_gene_pawns[thing] = effect;
            }
        }

        public static void RemoveBloodEffectGenePawnFromList(Thing thing)
        {
            if (bloodEffect_gene_pawns.ContainsKey(thing))
            {
                bloodEffect_gene_pawns.Remove(thing);
            }

        }

        public static void AddWoundsFromFleshtypeGenePawnToList(Thing thing, FleshTypeDef fleshtype)
        {

            if (!woundsFromFleshtype_gene_pawns.ContainsKey(thing))
            {
                woundsFromFleshtype_gene_pawns[thing] = fleshtype;
            }
        }

        public static void RemoveWoundsFromFleshtypeGenePawnFromList(Thing thing)
        {
            if (woundsFromFleshtype_gene_pawns.ContainsKey(thing))
            {
                woundsFromFleshtype_gene_pawns.Remove(thing);
            }

        }

    }
}
