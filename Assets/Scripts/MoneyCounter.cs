using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] Text _moneyText = default; 
    public int _money { get;private set; }
    private void Start()
    {
        _moneyText.text = _money.ToString();
    }

    public void AddMoney(int AddValue)
    {
        _money += AddValue;
        _moneyText.text = _money.ToString();
    }
}
