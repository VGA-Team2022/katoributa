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

    [Header("�_���[�W����")]
    [SerializeField, Tooltip("�ǂ̈ʂ̑��x����_���[�W���󂯂邩")] float _damageSpeed = 5f;
    [SerializeField, Tooltip("�ǂ̈ʂ̍�������_���[�W���󂯂邩")] float _damageHeight = 5f;

    [Tooltip("1�t���[���O��Velocity")] Vector3 _prevVelocity;
    [Tooltip("���݂�Velocity")] Vector3 _velocity;

    [Header("�_���[�W��j�󎞂̏���")]
    [SerializeField, Tooltip("�_���[�W")] int _damage = 1;
    [SerializeField] Breaker _breaker;
    [SerializeField] Fracture _fracture;

    [Header("���G�t���O")]
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
    /// �Փ˃_���[�W����
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

        if(diffXZ > Mathf.Pow(_damageSpeed, 2))
        {
            TakeDamage(_damage);
        }
        else if (diffY > _damageHeight && y < 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position + Vector3.up * 0.01f, Vector3.down, out hit, 0.15f, LayerMask.GetMask("Ground")))
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
        if (!_godMode)
        {
            _hp.Value -= damage;

            if(_hp.Value < 1)
            {
                OnDead();
            }
        }
            
        Debug.Log($"�_���[�W���󂯂� : HP = {_hp} : Damage = {damage}");
    }

    void OnDead()
    {
        Debug.Log("����");
        _breaker.Break(_fracture, Vector3.zero);
        GameManager.Instance.OnGameOver();
    }
}
