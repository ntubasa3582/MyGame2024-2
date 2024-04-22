using DG.Tweening;
using System.Collections;
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
        _player = GameObject.FindGameObjectWithTag("Point");
        _hpSlider.maxValue = _hp;
        _hpSlider.value = _hp;
    }
    private void Start()
    {
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
            FPSController fPSController = default;
            fPSController = GameObject.FindAnyObjectByType<FPSController>();
            _moneyCounter = GameObject.FindAnyObjectByType<MoneyCounter>();
            fPSController.KillCountUp(1);
            switch (fPSController._killCount)
            {
                case <0:
                    _moneyCounter.MoneyValueChange(_deathGetMoney);
                    break;
                case <10:
                    _moneyCounter.MoneyValueChange(_deathGetMoney*1.1f);
                    break;
                case <50:
                    _moneyCounter.MoneyValueChange(_deathGetMoney*1.5f);
                    break;
                case <100:
                    _moneyCounter.MoneyValueChange(_deathGetMoney*2f);
                    break;
                case <500:
                    _moneyCounter.MoneyValueChange(_deathGetMoney*3f);
                    break;

            }
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
            BulletController _bullet = default;
            _bullet = other.gameObject.GetComponent<BulletController>();
            _hp -= _bullet._enemyDamage;
            _hpSlider.DOValue(_hp, 0.1f);
        }
        if (other.gameObject.tag == "Player")
        {
            FPSController _fpscon = default;
            _fpscon = other.gameObject.GetComponent<FPSController>();
            _fpscon.Damage();
            Destroy(gameObject);
        }
    }
}
