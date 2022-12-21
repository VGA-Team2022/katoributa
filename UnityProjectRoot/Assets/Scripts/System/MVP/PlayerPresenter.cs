using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour
{
    [Header("�ݒ�")]
    [SerializeField, Tooltip("�����̃e�L�X�g��ݒ�")] MVPText _senkouText;
    [SerializeField, Tooltip("HP�̃e�L�X�g��ݒ�")] MVPText _hpText;

    private void Start()
    {
        if (_senkouText)
        {
            var senkou = GameObject.FindGameObjectWithTag("Player").GetComponent<SenkouHealth>();

            if (!senkou)
            {
                Debug.LogError("SenkouHealth��������܂���ł���");
                return;
            }

            senkou.Health.Subscribe(x =>
            {
                var m = (int)(x / 60);
                var s = (int)(x - (60 * m));

                _senkouText.SetText($"�^�C���F[{m:00}:{s:00}]");
            }).AddTo(this);
        }
        else
        {
            Debug.LogError("SenkouText���ݒ肳��Ă��܂���");
        }

        if (_hpText)
        {
            var hp = GameObject.FindGameObjectWithTag("Player").GetComponent<Durability>();

            if(!hp)
            {
                Debug.LogError("Durability��������܂���ł���");
                return;
            }

            hp.HP.Subscribe(x =>
            {
                _hpText.SetText($"���C�t �~ {x}");
            }).AddTo(this);
        }
        else
        {
            Debug.LogError("HpText���ݒ肳��Ă��܂���");
        }
    }
}
