using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndPanel : PopUpUIBase
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameEndEvent += PopUpAnimation;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameEndEvent -= PopUpAnimation;
    }
}
