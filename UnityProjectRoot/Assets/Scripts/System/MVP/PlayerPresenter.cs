using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour
{
    [Header("設定")]
    [SerializeField, Tooltip("線香を設定")] MVPMosquitCoil _senkouCoil;
    [SerializeField, Tooltip("HPのテキストを設定")] MVPText _hpText;

    private void Start()
    {
        if(_senkouCoil)
        {
            var senkou = GameObject.FindGameObjectWithTag("Player").GetComponent<SenkouHealth>();

            if(!senkou)
            {
                Debug.LogError("SenkouHealthが見つかりませんでした");
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
            Debug.LogError("SenkouTextが設定されていません");
        }

        if(_hpText)
        {
            var hp = GameObject.FindGameObjectWithTag("Player").GetComponent<Durability>();

            if(!hp)
            {
                Debug.LogError("Durabilityが見つかりませんでした");
                return;
            }

            hp.HP.Subscribe(x =>
            {
                _hpText.SetText($"蚊取り豚の耐久力:{x:00}");
            }).AddTo(this);
        }
        else
        {
            Debug.LogError("HpTextが設定されていません");
        }
    }
}
