using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ulit : MonoBehaviour
{
    public static bool Randomizer(float successProbability)
    {
        var a= Random.Range(0, 1000);
        return a < 1000 * successProbability;
    }
}