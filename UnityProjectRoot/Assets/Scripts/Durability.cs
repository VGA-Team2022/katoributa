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
    [Tooltip("衝撃")] float _impulse = 0;
    [SerializeField ,Tooltip("無敵時間")] float _invisibleTime = 1f;
    [Tooltip("無敵フラグ")] bool _isInvisible;

    [Header("落下時の挙動")]
    [SerializeField, Tooltip("Raycastの位置")] Transform _rayPos;
    [SerializeField, Tooltip("どの位の高さでダメージを受けるか")] float _damageDistance = 100f;
    [Tooltip("Rayの距離")] float _rayRange = 0.5f;
    [Tooltip("空中にいるかどうかのフラグ")] bool _isFall;
    [Tooltip("落ちた場所")] float _fallenPosition;
    [Tooltip("落下してから地面に落ちるまでの距離")] float _fallenDistance;
    

    [Header("無敵フラグ")]
    [SerializeField] bool _godMode;
    
    PlayerState _playerState;
    Rigidbody _rb;

    public IReactiveProperty<int> HP => _hp;

    private void Start()
    {
        _playerState = GetComponent<PlayerState>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckHeight();

        CheckVelocity();
    }

    private void CheckHeight()
    {
        Debug.DrawLine(_rayPos.position, this.transform.position + Vector3.down * _rayRange, Color.blue);

        if (_isFall)
        {
            _fallenPosition = Mathf.Max(_fallenPosition, transform.position.y);
            if (Physics.Linecast(_rayPos.position, this.transform.position + Vector3.down * _rayRange, LayerMask.GetMask("Ground")))
            {
                _fallenDistance = _fallenPosition - transform.position.y;
                if (_fallenDistance >= _damageDistance)
                {
                    TakeDamage(1);
                    Debug.Log("高いところから落ちた");
                }
                _isFall = false;
            }
        }
        else
        {
            if (!Physics.Linecast(this.transform.position, this.transform.position + Vector3.down * _rayRange, LayerMask.GetMask("Ground")))
            {
                _fallenPosition = transform.position.y;
                _fallenDistance = 0;
                _isFall = true;
            }
        }
    }

    /// <summary>
    /// 何かに当たった際に衝撃を計算、衝撃が指定した数値以上の場合にダメージを受ける関数
    /// </summary>
    private void CheckVelocity()
    {
        if(_isInvisible == true)
        {
            return;
        }

        Debug.DrawLine(_rayPos.position, _rayPos.position + Vector3.right * _rayRange, Color.blue);
        Debug.DrawLine(_rayPos.position, _rayPos.position + Vector3.left * _rayRange, Color.blue);
        Debug.DrawLine(_rayPos.position, _rayPos.position + Vector3.forward * _rayRange, Color.blue);
        Debug.DrawLine(_rayPos.position, _rayPos.position + Vector3.back * _rayRange, Color.blue);

        if (Physics.Linecast(_rayPos.position, _rayPos.position + Vector3.right * _rayRange, LayerMask.GetMask("Ground")))
        {
            _impulse = _rb.velocity.magnitude / 5f;

            if (_impulse > _damageSpeed)
            {
                Debug.Log("豚がぶつかった");

                TakeDamage(_damage);
                StartCoroutine(Invisible());

                if (_hp.Value <= 0)
                {
                    OnDead();
                }
            }
        }

        if (Physics.Linecast(_rayPos.position, _rayPos.position + Vector3.left * _rayRange, LayerMask.GetMask("Ground")))
        {
            _impulse = _rb.velocity.magnitude / 5f;

            if (_impulse > _damageSpeed)
            {
                Debug.Log("豚がぶつかった");

                TakeDamage(_damage);
                StartCoroutine(Invisible());

                if (_hp.Value <= 0)
                {
                    OnDead();
                }
            }
        }

        if (Physics.Linecast(_rayPos.position, _rayPos.position + Vector3.forward * _rayRange, LayerMask.GetMask("Ground")))
        {
            _impulse = _rb.velocity.magnitude / 5f;

            if (_impulse > _damageSpeed)
            {
                Debug.Log("豚がぶつかった");

                TakeDamage(_damage);
                StartCoroutine(Invisible());

                if (_hp.Value <= 0)
                {
                    OnDead();
                }
            }
        }

        if (Physics.Linecast(_rayPos.position, _rayPos.position + Vector3.back * _rayRange, LayerMask.GetMask("Ground")))
        {
            _impulse = _rb.velocity.magnitude / 5f;

            if (_impulse > _damageSpeed)
            {
                Debug.Log("豚がぶつかった");

                TakeDamage(_damage);
                StartCoroutine(Invisible());

                if (_hp.Value <= 0)
                {
                    OnDead();
                }
            }
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

    /// <summary>
    /// 無敵時間に使うコルーチン関数
    /// </summary>
    /// <returns></returns>
    IEnumerator Invisible()
    {
        _isInvisible = true;
        yield return new WaitForSeconds(_invisibleTime);
        _isInvisible = false;
    }
}
