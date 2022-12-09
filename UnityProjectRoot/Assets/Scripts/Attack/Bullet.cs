using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _lifeTime = 100f;
    [SerializeField] int _damageValue = 1;
    [SerializeField, Range(1f, 2f)] float _scale = 1f;
    [SerializeField] LayerMask _enemyLayer;

    Vector3 _localScale;
    float _lifeTimer = 0;
    Rigidbody _rb;
    Collider _col;
    Transform _thisTransform;
    VisualEffect _effect;
    bool _isActive = false;

    const float _scaleDuration = 1f;

    public bool IsActive => _isActive;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<SphereCollider>();
        _effect = GetComponent<VisualEffect>();
        _thisTransform = this.transform;
    }
    private void Start()
    {
        _localScale = new Vector3(_scale, _scale, _scale);
    }

    private void Update()
    {
        if (!_isActive) return;

        _lifeTimer += Time.deltaTime;

        if (_lifeTimer > _lifeTime)
        {
            this.Destroy();
        }

        var dir = _thisTransform.forward * _speed;
        _rb.velocity = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _enemyLayer)
        {
            var e = other.GetComponent<MosquitoHealth>();
            e.TakeDamage(_damageValue);
        }
        else
        {
            if (other.CompareTag("Player")) return;

            this.Destroy();
        }
    }
    public void DisactiveForInstantiate()
    {
        _effect.SendEvent("StopPlay");
        _isActive = false;
        _col.enabled = false;
        _thisTransform.localScale = Vector3.zero;
    }

    public void Create()
    {
        _effect.SendEvent("OnPlay");
        _lifeTimer = 0;
        _isActive = true;
        _col.enabled = true;
        _rb.isKinematic = false;
        _thisTransform.DOScale(_localScale, _scaleDuration);
    }

    public void Destroy()
    {
        _effect.SendEvent("StopPlay");
        _isActive = false;
        _col.enabled = false;
        _rb.isKinematic = true;
        _thisTransform.localScale = Vector3.zero;
    }
}
