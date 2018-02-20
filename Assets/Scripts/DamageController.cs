using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    [SerializeField]
    [Range(0, 100)]
    int damage;

    public int DealDamage()
    {
        return damage;
    }
}
