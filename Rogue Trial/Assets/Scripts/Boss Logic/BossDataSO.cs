using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "bossData", menuName = "CustomSO/Boss", order = 0)]
public class BossDataSO : ScriptableObject
{

    public Health health;

    public readonly float SLAM_ATTCD=5.0f;

    public readonly float TREMOR_ATTCD=10.0f;

    public readonly float ROCK_DMG = 1.0f;

    public readonly float SLAM_DMG = 2.0f;

    //public Sprite rock;



}
