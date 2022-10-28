using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// 線香豚の耐久力（高いところから落ちるとHPが減る）
/// </summary>
public class Durability : MonoBehaviour
{
    [Header("耐久力")]
    [SerializeField, Tooltip("耐久力")] int _hp = 5;

    [Header("耐久力を表示するText")]
    [SerializeField, Tooltip("表示させるText")] TMP_Text _hpText;

    [Header("速度に応じてダメージを受ける処理")]
    [SerializeField, Tooltip("ダメージを受ける速度の下限")] float _damageSpeed = 5f;
    [SerializeField, Tooltip("ダメージ")] int _damage = 1;


    private void Start()
    {
        if (_hpText == null)
        {
            Debug.LogError("HPテキストが設定されていません");
        }

        _hpText.text = "蚊取り豚の耐久力：" + _hp.ToString();
    }

    private void CheckVelocity(Collision collision)
    {
        // 衝撃を5で割る（計算を楽にするため）
        float impulse = collision.impulse.magnitude / 5f;

        Debug.Log(impulse);

        if(impulse > _damageSpeed)
        {
            TakeDamage(_damage);
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != 10)
        {
            CheckVelocity(collision);
        }
        else
        {
            Debug.Log("クッションに衝突");
        }
    }
}
