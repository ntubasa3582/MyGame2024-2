using System;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public event Action<bool> OnPauseResume;
    bool _pauseFlg = false; //trueの時にゲームを停止する
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))//escキーが押されたらゲームを停止する
        {
            _pauseFlg = !_pauseFlg;
            OnPauseResume(_pauseFlg);
        }
    }
}
