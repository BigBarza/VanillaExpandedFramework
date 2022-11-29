﻿using RimWorld;
using System.Collections.Generic;
using Verse;
using UnityEngine;

namespace VanillaGenesExpanded
{

    public class GeneExtension : DefModExtension
    {

        //Custom gene backgrounds
        public string backgroundPathEndogenes;
        public string backgroundPathXenogenes;

        //Applies hediffs to existing body parts when the gene is acquired
        public List<HediffToBodyparts> hediffsToBodyParts;
        //Applies a hediff to the whole body when the gene is acquired
        public HediffDef hediffToWholeBody;

        //Makes "fur" associated with this gene use the body's colour, instead of the hair
        public bool useSkinColorForFur = false;
        //Keeps "fur" untinted
        public bool dontColourFur = false;
        //Switches "fur" shader to be CutoutComplex rather than skin shader.
        public bool useMaskForFur = false;

        //Reference to a thingsetmaker that makes pawns with this gene start play with some things (only on game start)
        public ThingSetMakerDef thingSetMaker = null;

        //Gender bender
        public bool forceMale = false;
        public bool forceFemale = false;

        //Custom blood thingDef, custom blood icon, custom flood splash effecter, custom wounds from a fleshtype
        public ThingDef customBloodThingDef = null;
        public string customBloodIcon = "";
        public EffecterDef customBloodEffect = null;
        public FleshTypeDef customWoundsFromFleshtype = null;

        //Custom vomit thingDef, custom vomit effecter
        public ThingDef customVomitThingDef = null;    
        public EffecterDef customVomitEffect = null;

        //Disease progression factor. Diseases will advance (when not immunized) by this factor
        public float diseaseProgressionFactor = 1f;

        //Caravan carrying factor. Pawns will have their caravan carrying capacity multiplied by this factor
        public float caravanCarryingFactor = 1f;

        //Hide gene from appearing on the xenotype creation screen. Useful for special genes that only appear as rewards during gameplay.
        public bool hideGene = false;

        //Makes this gene cause no skill loss for a given skill.
        public SkillDef noSkillLoss = null;

        //Makes genes scale body and head
        public Vector2 bodyScaleFactor = new Vector2(1f, 1f);
        public Vector2 headScaleFactor = new Vector2(1f, 1f);

        public BodyTypeDef forcedBodyType;
        public string bodyNakedGraphicPath;
        public string bodyDessicatedGraphicPath;
        public string headDessicatedGraphicPath;
        public string skullGraphicPath;
    }

}
