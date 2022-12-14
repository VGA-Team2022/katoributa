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
    [Tooltip("衝突したかどうかのフラグ")] bool _isCollision = false;

    [Header("ダメージ･破壊時の処理")]
    [SerializeField, Tooltip("ダメージ")] int _damage = 1;
    [SerializeField] Breaker _breaker;
    [SerializeField] Fracture _fracture;

    [Header("無敵フラグ")]
    [SerializeField] bool _godMode;

    Rigidbody _rb;

    public IReactiveProperty<int> HP => _hp;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
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
        var x = ((int)_prevVelocity.x);
        var y = ((int)_prevVelocity.y);
        var z = ((int)_prevVelocity.z);

        _velocity = _rb.velocity;

        if (_isCollision)
        {
            Vector3 xz = new Vector3(x, 0, z);
            var xzMag = Mathf.Abs((int)xz.magnitude);

            Debug.Log($"{y}, {xzMag}");
            if(xzMag > _damageSpeed || Mathf.Abs(y) > _damageHeight)
            {
                TakeDamage(_damage);
            }

            _isCollision = false;
        }

        _prevVelocity = _velocity;
        //Debug.Log($"velocity = {_velocity}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isCollision = true;
        }
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    /// <param name="damage">受けるダメージ</param>
    public void TakeDamage(int damage)
    {
        if (!_godMode)
            _hp.Value -= damage;

        Debug.Log($"ダメージを受けた : HP = {_hp} : Damage = {damage}");
    }

    void OnDead()
    {
        Debug.Log("死んだ");
        _breaker.Break(_fracture, Vector3.zero);
        GameManager.Instance.OnGameOver();
    }
}
