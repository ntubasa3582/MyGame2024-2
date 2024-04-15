using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    //Moneyを管理するスクリプト
    [SerializeField] Text _moneyText = default; 
    public int _money { get;private set; }
    private void Start()
    {
        //ゲーム開始時に_moneyをテキストに反映させる
        _moneyText.text = _money.ToString();
    }

    public void MoneyValueChange(int Value)
    {
        //_moneyの値を変えてテキストに反映させる
        _money += Value;
        _moneyText.text = _money.ToString();
    }
}
