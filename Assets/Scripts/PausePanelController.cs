using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel = default;
    PauseGame _pauseGame;
    void Awake()
    {
        _pauseGame = FindAnyObjectByType<PauseGame>();
        _pausePanel.SetActive(false);
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
            _pausePanel.SetActive(true);
        }
        else
        {
            _pausePanel.SetActive(false);
        }
    }
}
