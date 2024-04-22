using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] _pausePanel = default;
    [SerializeField] GameObject[] _nextBuyText = default;
    PauseGame _pauseGame;
    void Awake()
    {
        _pauseGame = FindAnyObjectByType<PauseGame>();
        for (int i = 0; i < _pausePanel.Length; i++)
        {
            _pausePanel[i].SetActive(false);
        }
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
        //オブジェクトのセットアクティブを切り替える
        if (isPause)
        {
            for (int i = 0; i < _pausePanel.Length; i++)
            {
                _pausePanel[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < _pausePanel.Length; i++)
            {
                _pausePanel[i].SetActive(false);
            }
        }
    }
}
