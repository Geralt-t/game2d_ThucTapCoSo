using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]
public class D_RangeAttackData : ScriptableObject
{
    public GameObject spell;
    public float spellDamage=10f;
    public float damageRadius;
    public float timeActivated=1.5f;
}
