using Godot;
using System;

public enum BiomeEnum //includes weathers
{
    Town,
    Plains,
    Forest,
    Beach,
    Rain,
    Thunder
}
static class SpawnWeights
{
    static WeightedSlimeType[] PlainsWeightedSpawns = new WeightedSlimeType[]
    {
        //new WeightedSlimeType(SlimeType.Green.GetSmallSlime(), 30f),
        new WeightedSlimeType(SlimeType.Yellow.GetSmallSlime(), 10f)
    };
    static WeightedSlimeType[] ForestWeightedSpawns = new WeightedSlimeType[]
    {
        new WeightedSlimeType(SlimeType.Green.GetSmallSlime(), 30f)//,
        //new WeightedSlimeType(SlimeType.Yellow.GetSmallSlime(), 10f)
    };
    static WeightedSlimeType[] BeachWeightedSpawns = new WeightedSlimeType[]
    {
        new WeightedSlimeType(SlimeType.Green.GetSmallSlime(), 10f),
        new WeightedSlimeType(SlimeType.Yellow.GetSmallSlime(), 30f)
    };
    static WeightedSlimeType[] RainWeightedSpawns = new WeightedSlimeType[]
    {
        //new WeightedSlimeType(SlimeType.Green.GetSmallSlime(), 10f),
        //new WeightedSlimeType(SlimeType.Yellow.GetSmallSlime(), 30f)
    };
    static WeightedSlimeType[] ThunderWeightedSpawns = new WeightedSlimeType[]
    {
        //new WeightedSlimeType(SlimeType.Green.GetSmallSlime(), 10f),
        //new WeightedSlimeType(SlimeType.Yellow.GetSmallSlime(), 30f)
    };

    public static WeightedSlimeType[] GetBiomeSpawnWeights(this BiomeEnum biome)
    {
        switch (biome)
        {
            case BiomeEnum.Plains:
                return PlainsWeightedSpawns;
            case BiomeEnum.Forest:
                return ForestWeightedSpawns;
            case BiomeEnum.Beach:
                return BeachWeightedSpawns;
            case BiomeEnum.Rain:
                return RainWeightedSpawns;
            case BiomeEnum.Thunder:
                return ThunderWeightedSpawns;
            default:
                return new WeightedSlimeType[0];
        }
    }

    public class WeightedSlimeType
    {
        private PackedScene SlimeType;
        private float Weight;

        public WeightedSlimeType(PackedScene slime, float weight)
        {
            SlimeType = slime;
            Weight = weight;
        }

        public PackedScene GetSlime()
        {
            return SlimeType;
        }

        public float GetWeight()
        {
            return Weight;
        }
    }
}