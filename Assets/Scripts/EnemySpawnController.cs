using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    //エネミーの生成を管理するスクリプト
    PauseGame _pauseGame;
    [SerializeField] GameObject _enemy = default;
    [SerializeField] Vector2 _xpos;
    [SerializeField] Vector2 _ypos;
    Vector3 _randomPos = default;
    //[SerializeField] float _spawnInterval = 0;
    bool _timerStop = false;
    //float _timer = 0;
    bool _spawnSwitch = true;
    void Awake()
    {
        _pauseGame = FindAnyObjectByType<PauseGame>();
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
        if (!_timerStop)
        {
            if (_spawnSwitch)
            {
                //_timer = 0;
                RandomPos();
                Instantiate(_enemy, transform.position, Quaternion.identity);
                _spawnSwitch = false;
                //_timer += Time.deltaTime;
                //if (_timer > _spawnInterval)
                //{
                //}
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
 