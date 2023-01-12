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
    [Tooltip("耐久力の最大値")] int _maxHp;

    [Header("ダメージ処理")]
    [SerializeField, Tooltip("どの位の速度からダメージを受けるか")] float _damageSpeed = 5f;
    [SerializeField, Tooltip("どの位の高さからダメージを受けるか")] float _damageHeight = 5f;

    [Tooltip("1フレーム前のVelocity")] Vector3 _prevVelocity;
    [Tooltip("現在のVelocity")] Vector3 _velocity;

    [Header("ダメージ･破壊時の処理")]
    [SerializeField, Tooltip("ダメージ")] int _damage = 1;
    [SerializeField] Breaker _breaker;
    [SerializeField] Fracture _fracture;

    [Header("サウンド")]
    [SerializeField, Tooltip("畳接触時")] int _tatamiSEID;
    [SerializeField, Tooltip("木材接触時")] int _woodSEID;
    [SerializeField, Tooltip("クッション接触時")] int _cushionSEID;
    [SerializeField, Tooltip("鉄接触時")] int _metalSEID;
    [SerializeField, Tooltip("紙接触時")] int _paperSEID;
    [SerializeField, Tooltip("ヒビ入る時")] int _damageSEID;
    [SerializeField, Tooltip("壊れる時")] int _breakSEID;

    [Header("無敵フラグ")]
    [SerializeField] bool _godMode;
    [Tooltip("ポーズフラグ")] bool _isPaused = false;

    Rigidbody _rb;
    SphereCollider _sCol;
    ShakeCamera _sc;
    SoundPlayer _soundPlayer;

    public IReactiveProperty<int> HP => _hp;

    private void Start()
    {
        _soundPlayer = GetComponent<SoundPlayer>();
        _rb = GetComponent<Rigidbody>();
        _sCol = GetComponent<SphereCollider>();
        _sc = FindObjectOfType<ShakeCamera>();

        _maxHp = _hp.Value;
        Debug.Log($"耐久力の最大値は {_maxHp} です");
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
        if (_isPaused)
        {
            return;
        }
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
            if (Physics.OverlapSphere(transform.position + _sCol.center, _sCol.radius * 1.5f, LayerMask.GetMask("Cushion")).Length < 1)
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
        if (!_godMode || !_isPaused)
        {
            _hp.Value -= damage;

            if (_hp.Value < 1)
            {
                OnDead();
            }
            else
            {
                _soundPlayer.PlaySound(_damageSEID);
            }
        }

        _sc?.ShakeMethod();
        
        Debug.Log($"ダメージを受けた : HP = {_hp} : Damage = {damage}");
    }

    /// <summary>
    /// 耐久力の回復
    /// </summary>
    /// <param name="value">回復する値</param>
    public void DuraHeal(int value)
    {
        if (_isPaused)
        {
            return;
        }
        if (_hp.Value >= _maxHp)
        {
            Debug.Log("耐久力は既に最大のため回復しなかった");
            return;
        }

        _hp.Value += value;
        Debug.Log($"耐久力が {value} 回復した");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPaused)
        {
            return;
        }

        //接触時のSEを鳴らす
        if (collision.gameObject.CompareTag("Tatami"))
        {
            _soundPlayer.PlaySound(_tatamiSEID);
        }
        else if (collision.gameObject.CompareTag("Wood"))
        {
            _soundPlayer.PlaySound(_woodSEID);
        }
        else if (collision.gameObject.CompareTag("Cushion"))
        {
            _soundPlayer.PlaySound(_cushionSEID);
        }
        else if (collision.gameObject.CompareTag("Metal"))
        {
            _soundPlayer.PlaySound(_metalSEID);
        }
        else if (collision.gameObject.CompareTag("Paper"))
        {
            _soundPlayer.PlaySound(_paperSEID);
        }
        else
        {
            _soundPlayer.PlaySound(_woodSEID);
        }
    }

    void OnDead()
    {
        Debug.Log("死んだ");
        _soundPlayer.PlaySound(_breakSEID);
        _breaker.Break(_fracture, Vector3.zero);
        GameManager.Instance.OnGameOver();
    }

    void Pause()
    {
        _isPaused = true;
    }

    void Resume()
    {
        _isPaused = false;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnResume += Resume;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPause -= Pause;
        GameManager.Instance.OnResume -= Resume;
    }
}
