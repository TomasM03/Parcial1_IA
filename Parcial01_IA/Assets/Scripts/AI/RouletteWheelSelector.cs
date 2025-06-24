using System.Collections.Generic;
using UnityEngine;

public static class RouletteWheelSelector
{
    public static T Select<T>(List<(T option, float weight)> items)
    {
        float totalWeight = 0f;
        foreach (var item in items)
        {
            totalWeight += item.weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var item in items)
        {
            cumulativeWeight += item.weight;
            if (randomValue <= cumulativeWeight)
            {
                return item.option;
            }
        }

        // En caso de errores o suma de pesos 0, devolver el primer item por defecto
        return items[0].option;
    }
}
