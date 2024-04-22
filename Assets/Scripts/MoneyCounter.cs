using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    //Moneyを管理するスクリプト
    [SerializeField] Text _moneyText = default; 
    public float _money { get;private set; } = 1000000;
    private void Start()
    {
        //ゲーム開始時に_moneyをテキストに反映させる
        _moneyText.text = _money.ToString();
    }

    public void MoneyValueChange(float Value)
    {
        //_moneyの値を変えてテキストに反映させる
        _money += Value;

        _moneyText.text = _money.ToString("f0");
    }
}
