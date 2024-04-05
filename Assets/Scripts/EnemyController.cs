using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    ClearSystem _clearSystem;
    [SerializeField] int _hp;
    [SerializeField] Slider _hpSlider;
    private void Start()
    {
        _hpSlider.maxValue = _hp;
    }
    private void Update()
    {
        if (_hp <= 0 && _hpSlider.value <= 0)
        {
            _clearSystem = GameObject.FindObjectOfType<ClearSystem>();
            _clearSystem.AddScore(1);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            _hp -= 1;
            _hpSlider.DOValue(_hp,0.5f);
        }
    }
}
