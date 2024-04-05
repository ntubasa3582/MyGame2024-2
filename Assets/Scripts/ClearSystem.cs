using System.Collections.Generic;
using UnityEngine;

public class ClearSystem : MonoBehaviour
{
    int _score = 0;
    int _enemyDeathCount = 0;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy"); //シーン内にあるEnemyタグが付いているオブジェクトを全て取得配列に入れる
        foreach (GameObject obj in objs)
        {
        }
    }

    public void AddScore(int score)
    {
        _score += score;
    }
}
