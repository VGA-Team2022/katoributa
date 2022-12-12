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

    [Header("���x�ɉ����ă_���[�W���󂯂鏈��")]
    [SerializeField, Tooltip("�_���[�W���󂯂鑬�x�̉���")] float _damageSpeed = 5f;
    [SerializeField, Tooltip("�_���[�W")] int _damage = 1;
    [SerializeField] Breaker _breaker;
    [SerializeField] Fracture _fracture;
    [Tooltip("�Ռ�")] float _impulse = 0;
    [SerializeField ,Tooltip("���G����")] float _invisibleTime = 1f;
    [Tooltip("���G�t���O")] bool _isInvisible;

    [Header("�������̋���")]
    [SerializeField, Tooltip("Raycast�̈ʒu")] Transform _rayPos;
    [SerializeField, Tooltip("�ǂ̈ʂ̍����Ń_���[�W���󂯂邩")] float _damageDistance = 100f;
    [Tooltip("Ray�̋���")] float _rayRange = 0.5f;
    [Tooltip("�󒆂ɂ��邩�ǂ����̃t���O")] bool _isFall;
    [Tooltip("�������ꏊ")] float _fallenPosition;
    [Tooltip("�������Ă���n�ʂɗ�����܂ł̋���")] float _fallenDistance;
    

    [Header("���G�t���O")]
    [SerializeField] bool _godMode;
    
    PlayerState _playerState;
    Rigidbody _rb;

    public IReactiveProperty<int> HP => _hp;

    private void Start()
    {
        if(_rayPos == null)
        {
            Debug.LogWarning("RaycastPos�I�u�W�F�N�g��Player�����ɔz�u���ăC���X�y�N�^�[����A�T�C�����ĉ�����");
        }
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
                    Debug.Log("�����Ƃ��납�痎����");
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
    /// �����ɓ��������ۂɏՌ����v�Z�A�Ռ����w�肵�����l�ȏ�̏ꍇ�Ƀ_���[�W���󂯂�֐�
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
                Debug.Log("�؂��Ԃ�����");

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
                Debug.Log("�؂��Ԃ�����");

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
                Debug.Log("�؂��Ԃ�����");

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
                Debug.Log("�؂��Ԃ�����");

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
    /// �_���[�W���󂯂鏈��
    /// </summary>
    /// <param name="damage">�󂯂�_���[�W</param>
    public void TakeDamage(int damage)
    {
        if (!_godMode)
            _hp.Value -= damage;
            
        Debug.Log($"�_���[�W���󂯂� : HP = {_hp} : Damage = {damage}");
    }

    void OnDead()
    {
        Debug.Log("����");
        _breaker.Break(_fracture, Vector3.zero);
        GameManager.Instance.OnGameOver();
    }

    /// <summary>
    /// ���G���ԂɎg���R���[�`���֐�
    /// </summary>
    /// <returns></returns>
    IEnumerator Invisible()
    {
        _isInvisible = true;
        yield return new WaitForSeconds(_invisibleTime);
        _isInvisible = false;
    }
}
