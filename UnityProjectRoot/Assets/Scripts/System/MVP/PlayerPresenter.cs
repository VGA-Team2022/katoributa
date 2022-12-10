using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour
{
    [Header("�ݒ�")]
    [SerializeField, Tooltip("������ݒ�")] MVPMosquitCoil _senkouCoil;
    [SerializeField, Tooltip("HP�̃e�L�X�g��ݒ�")] MVPText _hpText;

    private void Start()
    {
        if(_senkouCoil)
        {
            var senkou = GameObject.FindGameObjectWithTag("Player").GetComponent<SenkouHealth>();

            if(!senkou)
            {
                Debug.LogError("SenkouHealth��������܂���ł���");
                return;
            }

            var value = 0.1f;

            senkou.Health.Subscribe(x =>
            {
                value -= 0.001f;
                _senkouCoil.SetValue(value);
            }).AddTo(this);
        }
        else
        {
            Debug.LogError("SenkouText���ݒ肳��Ă��܂���");
        }

        if(_hpText)
        {
            var hp = GameObject.FindGameObjectWithTag("Player").GetComponent<Durability>();

            if(!hp)
            {
                Debug.LogError("Durability��������܂���ł���");
                return;
            }

            hp.HP.Subscribe(x =>
            {
                _hpText.SetText($"����؂̑ϋv��:{x:00}");
            }).AddTo(this);
        }
        else
        {
            Debug.LogError("HpText���ݒ肳��Ă��܂���");
        }
    }
}
