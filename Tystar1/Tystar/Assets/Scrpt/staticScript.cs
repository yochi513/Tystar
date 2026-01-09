using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticScript : MonoBehaviour
{
    public static string LastSceneName;
    public static int SaveScore;
    public static int SavePlayerHP;
    public static bool ReturnedFromBoss = false;
    public static int SaveKillCount;
    public static int SaveMaxGino;
    public static float SaveCh;

    // ← これを追加
    public static bool IsGoingToBoss = false;  // true: ボス戦前, false: ボス戦後

    //ボスのHP
    public static float BossHP = 1500f;
    public static float BossMaxHP = 1500f;
}