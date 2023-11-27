using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionScroll", menuName = "PotionScroll", order = 1)]
public class PotionScroll : ScriptableObject
{
    [SerializeField]
    private List<Potions> potions;
}
