using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    PauseGame _pauseGame;
    [SerializeField] GameObject _enemy = default;
    [SerializeField] float _spawnInterval = 0;
    bool _timerStop = false;
    float _timer = 0;
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
            _timer += Time.deltaTime;
            if (_timer > _spawnInterval)
            {
                _timer = 0;
                Instantiate(_enemy, transform.position, Quaternion.identity);
            }
        }
    }

    public void intervalSet(float interval)
    {
        _spawnInterval = interval;
    }
}
