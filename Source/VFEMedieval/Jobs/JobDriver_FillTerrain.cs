using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace VFEMedieval
{
    public class JobDriver_FillTerrain : JobDriver_AffectFloor
    {
		protected override int BaseWorkAmount
		{
			get
			{
				return 7000;
			}
		}

		protected override DesignationDef DesDef
		{
			get
			{
				return VFEM_DefOf.VFEM_FillTerrain;
			}
		}

		protected override StatDef SpeedStat
		{
			get
			{
				return StatDefOf.GeneralLaborSpeed;
			}
		}

		public JobDriver_FillTerrain()
		{
			this.clearSnow = true;
		}

		protected override void DoEffect(IntVec3 c)
		{
			TerrainDef newTerrain = GetNewTerrain(base.TargetLocA, base.Map);
			base.Map.terrainGrid.SetTerrain(base.TargetLocA, newTerrain);
			FilthMaker.RemoveAllFilth(base.TargetLocA, base.Map);
		}

        public static TerrainDef GetNewTerrain(IntVec3 cell, Map map)
        {
            TerrainDef localTerrain = cell.GetTerrain(map);
            if (VFEM_DefOf.VFEM_MoatableTerrain.terrainPairsByBiomes != null)
            {
                foreach (var entry in VFEM_DefOf.VFEM_MoatableTerrain.terrainPairsByBiomes)
                {
                    if (entry.biome == map.Biome)
                    {
                        foreach (var terrainPair in entry.terrainPairs)
                        {
                            if (terrainPair.to == localTerrain)
                            {
                                return terrainPair.from;
                            }
                        }
                    }
                }
            }

            foreach (var terrainPair in VFEM_DefOf.VFEM_MoatableTerrain.terrainPairsDefault)
            {
                if (terrainPair.to == localTerrain)
                {
                    return terrainPair.from;
                }
            }

            throw new Exception("No matching terrain found to create from " + localTerrain);
        }
    }
}
