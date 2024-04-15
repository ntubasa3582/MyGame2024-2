using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Slider _hpSlider;      //エネミーのHpを表示するスライダー
    [SerializeField] float _hp;             //エネミーのHpの値
    [SerializeField] float _moveSpeed;      //エネミーの移動スピード
    [SerializeField] int _deathGetMoney = 0;//エネミー死亡時に取得できる金の数
    PauseGame _pauseGame;
    MoneyCounter _moneyCounter;
    GameObject _player;
    Animator _animator;
    bool _enemyMoveStop = false;
    private void Awake()
    {
        _pauseGame = FindAnyObjectByType<PauseGame>();
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        _hpSlider.maxValue = _hp;
        _player = GameObject.FindGameObjectWithTag("Player");
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
            _enemyMoveStop = true;
            _animator.speed = 0;
        }
        else
        {
            _enemyMoveStop = false;
            _animator.speed = 1;
        }
    }

    private void Update()
    {
        if (_hp <= 0 && _hpSlider.value <= 0)
        {
            _moneyCounter = GameObject.FindAnyObjectByType<MoneyCounter>();
            _moneyCounter.MoneyValueChange(_deathGetMoney);
            Destroy(gameObject);
        }
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
            ProjectilesController projectiles = default;
            projectiles = other.gameObject.GetComponent<ProjectilesController>();
            _hp -= projectiles._enemyDamage;
            _hpSlider.DOValue(_hp,0.2f);
        }
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
