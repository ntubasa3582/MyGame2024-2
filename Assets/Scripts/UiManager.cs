using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance => _instance;
    static UiManager _instance;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timeText;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = GetComponent<UiManager>();
            _instance = this;
        }
    }

    void Start()
    {
        ScoreTextUpdate();
    }

    /// <summary>スコアを表示するテキスト</summary>
    public void ScoreTextUpdate()
    {
        _scoreText.text = ScoreManager.Instance.Score.ToString();
    }
}
