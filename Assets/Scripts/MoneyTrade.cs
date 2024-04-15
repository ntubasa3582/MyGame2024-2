using System.Collections.Generic;
using UnityEngine;

public class MoneyTrade : MonoBehaviour
{
    MoneyCounter _moneyCounter;
    List<int> _consumeMoney = new List<int>();  //何番目と取引しているかを記録するカウント
    void Awake()
    {
        _moneyCounter = GameObject.FindAnyObjectByType<MoneyCounter>();
    }
    void Update()
    {
        
    }
}
