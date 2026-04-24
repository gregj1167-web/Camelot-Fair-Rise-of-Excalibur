using UnityEngine;

public enum EggRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

public class RaritySystem
{
    public static EggRarity GetRandomRarity()
    {
        int roll = Random.Range(1, 101);

        if (roll > 95)
            return EggRarity.Legendary;
        if (roll > 80)
            return EggRarity.Epic;
        if (roll > 50)
            return EggRarity.Rare;

        return EggRarity.Common;
    }
}
