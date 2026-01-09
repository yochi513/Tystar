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
}