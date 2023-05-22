using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RimWorld;
using Verse;

namespace VFEMedieval
{
    public class TerrainDiggableByBiome
    {
        public BiomeDef biome;
        public List<TerrainDiggable> terrainPairs;
    }
    public class TerrainDiggable
    {
        public TerrainDef from;

        public TerrainDef to;
    }
    public class TerrainListDef : Def
    {
        public List<TerrainDiggable> terrainPairsDefault;
        public List<TerrainDiggableByBiome> terrainPairsByBiomes;

        public bool HasMatchingTerrain(TerrainDef terrainDef, Map map, bool from)
        {
            if (terrainPairsByBiomes != null)
            {
                foreach (var entry in terrainPairsByBiomes)
                {
                    if (entry.biome == map.Biome)
                    {
                        if (from)
                        {
                            return entry.terrainPairs.Any(x => x.from == terrainDef);
                        }
                        else
                        {
                            return entry.terrainPairs.Any(x => x.to == terrainDef);
                        }
                    }
                }
            }

            foreach (var entry in terrainPairsDefault)
            {
                if (from)
                {
                    if (entry.from == terrainDef)
                    {
                        return true;
                    }
                }
                else
                {
                    if (entry.to == terrainDef)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
