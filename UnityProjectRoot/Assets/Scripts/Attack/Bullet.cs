using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _lifeTime = 3f;
    [SerializeField] int _damage = 1;
    [SerializeField] LayerMask _enemyLayer;

    float _lifeTimer = 0;
    Rigidbody _rb;
    Collider _col;
    VisualEffect _effect;
    bool _isActive = false;

    public bool IsActive => _isActive;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<SphereCollider>();
        _effect = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        if (!_isActive) return;

        _lifeTimer += Time.deltaTime;

        if (_lifeTimer > _lifeTime)
        {
            this.Destroy();
        }

        var dir = Vector3.forward * _speed;
        _rb.velocity = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _enemyLayer)
        {
            var e = other.GetComponent<MosquitoHealth>();
            e.TakeDamage(_damage);
        }
    }
    public void DisactiveForInstantiate()
    {
        _effect.SendEvent("StopPlay");
        _isActive = false;
        _col.enabled = false;
    }

    public void Create()
    {
        _effect.SendEvent("OnPlay");
        _lifeTimer = 0;
        _isActive = true;
        _col.enabled = true;
    }

    public void Destroy()
    {
        _effect.SendEvent("StopPlay");
        _isActive = false;
        _col.enabled = false;
    }
}
