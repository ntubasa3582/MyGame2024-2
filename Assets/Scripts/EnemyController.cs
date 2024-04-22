using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Slider _hpSlider;      //エネミーのHpを表示するスライダー
    [SerializeField] float _hp;             //エネミーのHpの値
    [SerializeField] float _moveSpeed;      //エネミーの移動スピード
    [SerializeField] int _deathGetMoney = 0;//エネミー死亡時に取得できる金の数
    PauseGame _pauseGame;
    EnemySpawnController _enemySpawnController;
    MoneyCounter _moneyCounter;
    GameObject _player;
    Animator _animator;
    bool _enemyMoveStop = false;
    private void Awake()
    {
        _pauseGame = FindAnyObjectByType<PauseGame>();
        _animator = GetComponent<Animator>();
        _enemySpawnController = GameObject.FindAnyObjectByType<EnemySpawnController>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        _hpSlider.maxValue = _hp;
    }

    private void OnEnable()
    {
        //デリゲート登録
        _pauseGame.OnPauseResume += PauseResume;
    }

    private void OnDisable()
    {
        //デリゲート解除
        _pauseGame.OnPauseResume -= PauseResume;
    }

    void PauseResume(bool isPause)
    {
        //エネミーの動きを止める
        if (isPause)
        {
            _animator.speed = 0;
            _enemyMoveStop = true;
        }
        else
        {
            _animator.speed = 1;
            _enemyMoveStop = false;
        }
    }

    private void Update()
    {
        if (_hp <= 0 && _hpSlider.value <= 0)
        {
            _moneyCounter = FindAnyObjectByType<MoneyCounter>();
            _moneyCounter.MoneyValueChange(_deathGetMoney);
            //_enemySpawnController.SpawnSwitchChange();
            Destroy(gameObject);
        }
        transform.LookAt(_player.transform.position);
        if (!_enemyMoveStop)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed);
        transform.LookAt(_player.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            _enemyMoveStop = true;
            BulletController _bullet = default;
            _bullet = other.gameObject.GetComponent<BulletController>();
            _hp -= _bullet._enemyDamage;
            _hpSlider.DOValue(_hp, 0.1f).OnComplete(()=> _enemyMoveStop = false);
        }
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
