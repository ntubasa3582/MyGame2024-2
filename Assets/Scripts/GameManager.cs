using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    static GameManager _instance;
    public event Action<bool> OnPauseResume; //escキーでPause画面を呼び出す
    UiManager _uiManager;
    public bool _gameStop { get; private set; } = false;        //ゲームが終わったときのフラグ
    bool _gamePause;        //ゲームを中断するフラグ
    void Awake()
    {
        if (_instance == null)
        {
            _instance = GetComponent<GameManager>();
            _instance = this;
        }
        _uiManager = GameObject.FindObjectOfType<UiManager>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _gamePause = !_gamePause;
            OnPauseResume(_gamePause);
        }
    }
}
