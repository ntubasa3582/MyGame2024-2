using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    //エネミーの生成を管理するスクリプト
    MoneyTrade _moneyTrade = null;
    PauseGame _pauseGame;
    [SerializeField] GameObject _enemy = default;
    [SerializeField] List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField] Vector2 _xpos;
    [SerializeField] Vector2 _ypos;
    Vector3 _randomPos = default;
    [SerializeField] float _spawnInterval = 5;
    bool _timerStop = false;
    float _timer = 0;
    bool _spawnSwitch = true;
    void Awake()
    {
        _pauseGame = FindAnyObjectByType<PauseGame>();
        _moneyTrade = FindAnyObjectByType<MoneyTrade>();
        _timer = 5;
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
        //タイマーのオンオフを切り替える
        if (isPause)
        {
            _timerStop = true;
        }
        else
        {
            _timerStop = false;
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (!_timerStop)
        {
            if (_timer > _spawnInterval)
            {
                Instantiate(_enemyList[_moneyTrade._consumeMoney[1]], transform.position, Quaternion.identity);
                _timer = 0;
                RandomPos();
            }
        }
    }

    public void SpawnSwitchChange()
    {
        _spawnSwitch = true;
    }

    public void RandomPos()
    {
        _randomPos.x = Random.Range(_xpos.x, _xpos.y);
        _randomPos.y = Random.Range(_ypos.x, _ypos.y);
        _randomPos = new Vector3(_randomPos.x,_randomPos.y,16.6f);
    }
}
 