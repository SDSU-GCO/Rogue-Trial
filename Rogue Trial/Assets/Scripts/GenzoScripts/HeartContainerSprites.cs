using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CreateAssetMenu(fileName = "HealthContainerImages", menuName = "Health/HeartContainerImages", order = 1)]
public class HeartContainerSprites : ScriptableObject {
    public List<Sprite> Sprites;
    public bool ContainsEmptySprite;
}