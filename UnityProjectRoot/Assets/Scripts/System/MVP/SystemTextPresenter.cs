using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SystemTextPresenter : MonoBehaviour
{
    [Header("�ݒ�")]
    [SerializeField, Tooltip("�������Ԃ̃e�L�X�g��ݒ�")] MVPText _timerText;
    [SerializeField, Tooltip("�|�������̃e�L�X�g��ݒ�")] MVPText _countText;

    private void Start()
    {
        if(_timerText)
        {
            GameManager.Instance.GameTime.Subscribe(x =>
            {
                var m = (int)(x / 60);
                var s = (int)(x - (60 * m));
                _timerText.SetText($"�o�ߎ��� [{m:00} : {s:00}]");
            }).AddTo(this);
        }
        if(_countText)
        {
            GameManager.Instance.Score.Subscribe(x =>
            {
                _countText.SetText($"���_ [{x:00000} �_]");
            }).AddTo(this);
        }
    }
}
