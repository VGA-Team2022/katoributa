using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BreakObject;
using UniRx;

/// <summary>
/// �����؂̑ϋv�́i�����Ƃ��납�痎�����HP������j
/// </summary>
public class Durability : MonoBehaviour
{
    [Header("�ϋv��")]
    [SerializeField, Tooltip("�ϋv��")] IntReactiveProperty _hp = new IntReactiveProperty(5);
    [Tooltip("�ϋv�͂̍ő�l")] int _maxHp;

    [Header("�_���[�W����")]
    [SerializeField, Tooltip("�ǂ̈ʂ̑��x����_���[�W���󂯂邩")] float _damageSpeed = 5f;
    [SerializeField, Tooltip("�ǂ̈ʂ̍�������_���[�W���󂯂邩")] float _damageHeight = 5f;

    [Tooltip("1�t���[���O��Velocity")] Vector3 _prevVelocity;
    [Tooltip("���݂�Velocity")] Vector3 _velocity;

    [Header("�_���[�W��j�󎞂̏���")]
    [SerializeField, Tooltip("�_���[�W")] int _damage = 1;
    [SerializeField] Breaker _breaker;
    [SerializeField] Fracture _fracture;

    [Header("�T�E���h")]
    [SerializeField, Tooltip("���ڐG��")] int _tatamiSEID;
    [SerializeField, Tooltip("�؍ސڐG��")] int _woodSEID;
    [SerializeField, Tooltip("�N�b�V�����ڐG��")] int _cushionSEID;
    [SerializeField, Tooltip("�S�ڐG��")] int _metalSEID;
    [SerializeField, Tooltip("���ڐG��")] int _paperSEID;
    [SerializeField, Tooltip("�q�r���鎞")] int _damageSEID;
    [SerializeField, Tooltip("���鎞")] int _breakSEID;

    [Header("���G�t���O")]
    [SerializeField] bool _godMode;
    [Tooltip("�|�[�Y�t���O")] bool _isPaused = false;

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
        Debug.Log($"�ϋv�͂̍ő�l�� {_maxHp} �ł�");
    }

    private void Update()
    {
        CheckVelocity();
    }

    /// <summary>
    /// �Փ˃_���[�W����
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
    /// �_���[�W���󂯂鏈��
    /// </summary>
    /// <param name="damage">�󂯂�_���[�W</param>
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
        
        Debug.Log($"�_���[�W���󂯂� : HP = {_hp} : Damage = {damage}");
    }

    /// <summary>
    /// �ϋv�͂̉�
    /// </summary>
    /// <param name="value">�񕜂���l</param>
    public void DuraHeal(int value)
    {
        if (_isPaused)
        {
            return;
        }
        if (_hp.Value >= _maxHp)
        {
            Debug.Log("�ϋv�͂͊��ɍő�̂��߉񕜂��Ȃ�����");
            return;
        }

        _hp.Value += value;
        Debug.Log($"�ϋv�͂� {value} �񕜂���");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPaused)
        {
            return;
        }

        //�ڐG����SE��炷
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
        Debug.Log("����");
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
