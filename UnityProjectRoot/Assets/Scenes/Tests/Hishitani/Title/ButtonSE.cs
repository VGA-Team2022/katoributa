using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSE : MonoBehaviour
{
    [SerializeField] string _seName;
    [SerializeField] SoundPlayer _soundPlayer;
    public void SePlay()
    {
        _soundPlayer.PlaySound(_seName);
    }
}
