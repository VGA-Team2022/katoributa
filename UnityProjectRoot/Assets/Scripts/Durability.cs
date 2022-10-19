using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 線香豚の耐久力（高いところから落ちるとHPが減る）
/// </summary>
public class Durability : MonoBehaviour
{
    [Header("耐久力")]
    [SerializeField, Tooltip("耐久力")] int _hp = 5;

    [Header("耐久力を表示するText")]
    [SerializeField, Tooltip("表示させるText")] Text _hpText;

    [Header("落下関係")]
    [SerializeField, Tooltip("Rayを飛ばす場所")] Transform _rayPos;
    [SerializeField, Tooltip("Rayの距離")] float _rayRange = 0.5f;
    [SerializeField, Tooltip("落下時のダメージ")] int _fallDamage = 1;
    [SerializeField, Tooltip("ダメージを受ける際の距離")] float _damageDistance = 2f;

    [Tooltip("落ちているかどうかのフラグ")] bool _isFall = false;
    [Tooltip("落ちた時の場所")] float _fallPos = 0f;
    [Tooltip("落下した距離")] float _fallDistance = 0f;


    private void Start()
    {
        if (_hpText == null)
        {
            Debug.LogError("HPテキストが設定されていません");
        }
        if ( _rayPos == null)
        {
            Debug.LogError("Rayポジションが設定されていません");
        }

        _fallPos = transform.position.y;
        _hpText.text = "蚊取り豚の耐久力：" + _hp.ToString();
    }

    private void Update()
    {
        Debug.DrawLine(_rayPos.position, _rayPos.position + Vector3.down * _rayRange, Color.red);

        CheckFalling();
    }

    private void CheckFalling()
    {
        //　落ちている状態
        if (_isFall)
        {

            //　落下地点と現在地の距離を計算（ジャンプ等で上に飛んで落下した場合を考慮する為の処理）
            _fallPos = Mathf.Max(_fallPos, transform.position.y);

            //　地面にレイが届いていたら
            if (Physics.Linecast(_rayPos.position, _rayPos.position + Vector3.down * _rayRange, LayerMask.GetMask("Ground")))
            {
                //　落下距離を計算
                _fallDistance = _fallPos - transform.position.y;
                //　落下によるダメージが発生する距離を超える場合ダメージを与える
                if (_fallDistance >= _damageDistance)
                {
                    TakeDamage((int)(_fallDamage));
                    Debug.Log("ダメージを受けた");
                }
                _isFall = false;
            }
        }
        else
        {
            //　地面にレイが届いていなければ落下地点を設定
            if (!Physics.Linecast(_rayPos.position, _rayPos.position + Vector3.down * _rayRange, LayerMask.GetMask("Field", "Block")))
            {
                //　最初の落下地点を設定
                _fallPos = transform.position.y;
                _fallDistance = 0;
                _isFall = true;
            }
        }
    }

    /// <summary>
    /// ダメージを受ける処理（落下距離によって）
    /// </summary>
    /// <param name="damage">受けるダメージ</param>
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        _hpText.text = "蚊取り豚の耐久力：" + _hp.ToString();
    }
}
