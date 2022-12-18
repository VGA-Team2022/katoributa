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

    [Header("ダメージ処理")]
    [SerializeField, Tooltip("どの位の速度からダメージを受けるか")] float _damageSpeed = 5f;
    [SerializeField, Tooltip("どの位の高さからダメージを受けるか")] float _damageHeight = 5f;

    [Tooltip("1フレーム前のVelocity")] Vector3 _prevVelocity;
    [Tooltip("現在のVelocity")] Vector3 _velocity;

    [Header("ダメージ･破壊時の処理")]
    [SerializeField, Tooltip("ダメージ")] int _damage = 1;
    [SerializeField] Breaker _breaker;
    [SerializeField] Fracture _fracture;

    [Header("無敵フラグ")]
    [SerializeField] bool _godMode;

    Rigidbody _rb;
    SphereCollider _sCol;
    ShakeCamera _sc;

    public IReactiveProperty<int> HP => _hp;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _sCol = GetComponent<SphereCollider>();
        _sc = FindObjectOfType<ShakeCamera>();
    }

    private void Update()
    {
        CheckVelocity();
    }

    /// <summary>
    /// 衝突ダメージ処理
    /// </summary>
    private void CheckVelocity()
    {
        var x = _prevVelocity.x;
        var y = _prevVelocity.y;
        var z = _prevVelocity.z;

        _velocity = _rb.velocity;

        Vector3 prevXZ = new Vector3(x, 0, z);
        Vector3 currentXZ = new Vector3(_velocity.x, 0, _velocity.z);
        var currentXZVelocity = currentXZ.sqrMagnitude;
        var prevXZVelocity = prevXZ.sqrMagnitude;

        var diffXZ = prevXZVelocity - currentXZVelocity;
        var diffY = _velocity.y - y;

        if ((diffY > _damageHeight && y < 0) || diffXZ > Mathf.Pow(_damageSpeed, 2))
        {
            if(Physics.OverlapSphere(transform.position + _sCol.center, _sCol.radius * 1.3f, LayerMask.GetMask("Cushion")).Length < 1)
            {
                TakeDamage(_damage);
            }
        }

        _prevVelocity = _velocity;
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage">受けるダメージ</param>
    public void TakeDamage(int damage)
    {
        if (!_godMode)
        {
            _hp.Value -= damage;

            if(_hp.Value < 1)
            {
                OnDead();
            }
        }

        _sc?.ShakeMethod();
            
        Debug.Log($"ダメージを受けた : HP = {_hp} : Damage = {damage}");
    }

    void OnDead()
    {
        Debug.Log("死んだ");
        _breaker.Break(_fracture, Vector3.zero);
        GameManager.Instance.OnGameOver();
    }
}
