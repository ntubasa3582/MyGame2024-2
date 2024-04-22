using System.Collections.Generic;
using UnityEngine;

public class MoneyTrade : MonoBehaviour
{
    MoneyCounter _moneyCounter;
    FPSController _fpsController;
    [SerializeField] List<int> _bulletBuyPrice = new List<int>();   //弾を交換する時に必要な金の量を入れる変数
    [SerializeField] List<int> _enemyBuyPrice = new List<int>();    //エネミーを交換する時必要な金の量を入れる変数


    public int[] _consumeMoney {get;private set; } = new int[5];  //何番目と取引しているかを記録するカウント
    //0 _bulletBuyPriceのカウント
    void Awake()
    {
        _moneyCounter = GameObject.FindAnyObjectByType<MoneyCounter>();
        _fpsController = GameObject.FindAnyObjectByType<FPSController>();
        foreach (int item in _consumeMoney)
        {
            _consumeMoney[item] = 0;
        }
    }

    public void BulletUpGrade() //プレイヤーが撃つ弾のグレードを上げるための処理を書くメソッド
    {
        if (_moneyCounter._money >= _bulletBuyPrice[_consumeMoney[0]])
        {
            _moneyCounter.MoneyValueChange(-_bulletBuyPrice[_consumeMoney[0]]); //_moneyを_bulletBuyPrice分マイナスする
            _consumeMoney[0] += 1;  //カウントを1増やす
        }
    }
    public void EnemyUpGrade()
    {
        if (_moneyCounter._money >= _enemyBuyPrice[_consumeMoney[1]])
        {
            _moneyCounter.MoneyValueChange(-_enemyBuyPrice[_consumeMoney[1]]); //_moneyを_bulletBuyPrice分マイナスする
            _consumeMoney[1] += 1;  //カウントを1増やす
        }
    }
}
