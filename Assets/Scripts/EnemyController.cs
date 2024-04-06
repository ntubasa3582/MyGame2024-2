using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    ClearSystem _clearSystem;
    [SerializeField] float _hp;
    [SerializeField] float _moveSpeed;
    [SerializeField] Slider _hpSlider;
    GameObject _player;
    private void Start()
    {
        _hpSlider.maxValue = _hp;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (_hp <= 0 && _hpSlider.value <= 0)
        {
            _clearSystem = GameObject.FindObjectOfType<ClearSystem>();
            _clearSystem.AddScore(1);
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed);
        transform.LookAt(_player.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            ProjectilesController projectiles = default;
            projectiles = other.gameObject.GetComponent<ProjectilesController>();
            _hp -= projectiles._enemyDamage;
            _hpSlider.DOValue(_hp,0.2f);
        }
    }
}
