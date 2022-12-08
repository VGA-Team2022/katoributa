using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BreakObject;
using UniRx;

/// <summary>
/// 線香豚の耐久力（高いところから落ちるとHPが減る）
/// </summary>
public class Durability : MonoBehaviour
{
    [Header("耐久力")]
    [SerializeField, Tooltip("耐久力")] IntReactiveProperty _hp = new IntReactiveProperty(5);

    [Header("速度に応じてダメージを受ける処理")]
    [SerializeField, Tooltip("ダメージを受ける速度の下限")] float _damageSpeed = 5f;
    [SerializeField, Tooltip("ダメージ")] int _damage = 1;
    [SerializeField] Breaker _breaker;
    [SerializeField] Fracture _fracture;
    [Header("無敵フラグ")]
    [SerializeField] bool _godMode;

    public IReactiveProperty<int> HP => _hp;

    private void CheckVelocity(Collision collision)
    {
        // 衝撃を5で割る（計算を楽にするため）
        float impulse = collision.impulse.magnitude / 5f;

        Debug.Log(impulse);

        if(impulse > _damageSpeed)
        {
            TakeDamage(_damage);
            if(_hp.Value <= 0)
            {
                OnDead();
            }
        }
    }

    /// <summary>
    /// ダメージを受ける処理（落下距離によって）
    /// </summary>
    /// <param name="damage">受けるダメージ</param>
    public void TakeDamage(int damage)
    {
        if(!_godMode)
            _hp.Value -= damage;

        Debug.Log($"ダメージを受けた : HP = {_hp} : Damage = {damage}");
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

    void OnDead()
    {
        Debug.Log("死んだ");
        _breaker.Break(_fracture, Vector3.zero);
        GameManager.Instance.OnGameOver();
    }
}
