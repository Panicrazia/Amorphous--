using Godot;
using System;
using System.Collections.Generic;
using static SpawnWeights;

public static class SlimeSpawner
{
    public static PackedScene GetSlimeToSpawn(HashSet<BiomeEnum> biomes)//TODO: add biome param so it chooses from biome specific weights
    {
        List<WeightedSlimeType> weights = new List<WeightedSlimeType>();
        float totalWeight = 0f;

        //go through biomes, add all weights to the weights list
        foreach (BiomeEnum biome in biomes)
        {
            weights.AddRange(biome.GetBiomeSpawnWeights());
        }
        foreach (var weight in weights)
        {
            totalWeight += weight.GetWeight();
        }

        float randomValue = GD.Randf() * totalWeight;
        double cumulativeWeight = 0.0f;

        foreach (var weight in weights)
        {
            cumulativeWeight += weight.GetWeight();
            if (cumulativeWeight > randomValue)
            {
                return weight.GetSlime();
            }
        }
        GD.Print("nothing selected randomly, cumWeight was " + cumulativeWeight +", there were " + weights.Count + " entries in the weights list");
        return null;

        //for each entry in biome list, add to total weight
        //second thought, just have total weight be a value automatically genned in the biome list? 
        //nope, weather will be more difficult to add with that being a thing

        return SlimeType.Green.GetSmallSlime();
    }
}
