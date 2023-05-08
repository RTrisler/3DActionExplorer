using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackSO", menuName = "Attacks/Normal Attack", order = 0)]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public int damage;
}

