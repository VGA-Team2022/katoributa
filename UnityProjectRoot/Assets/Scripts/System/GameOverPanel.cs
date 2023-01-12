using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : PopUpUIBase
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameOverEvent += PopUpAnimation;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameOverEvent -= PopUpAnimation;
    }
}
