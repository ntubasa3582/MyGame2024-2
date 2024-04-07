using System;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public event Action<bool> OnPauseResume;
    bool _pauseFlg = false; //true‚Ì‚ÉƒQ[ƒ€‚ğ’â~‚·‚é
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))//escƒL[‚ª‰Ÿ‚³‚ê‚½‚çƒQ[ƒ€‚ğ’â~‚·‚é
        {
            _pauseFlg = !_pauseFlg;
            OnPauseResume(_pauseFlg);
        }
    }
}
