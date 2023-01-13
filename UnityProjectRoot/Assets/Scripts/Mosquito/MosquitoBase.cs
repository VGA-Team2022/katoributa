using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(MosquitoMove))]
[RequireComponent(typeof(MosquitoHealth))]
[RequireComponent(typeof(Rigidbody))]
public class MosquitoBase : MonoBehaviour, IObjectPool
{
    MosquitoMove _move;
    MosquitoHealth _health;
    Rigidbody _rb;
    Collider _col;
    VisualEffect _model;
    bool _isActive;

    public bool IsActive => _isActive;
    public MosquitoMove Move => _move;
    public MosquitoHealth Health => _health;

    bool _isPause;

    private void Awake()
    {
        _move = GetComponent<MosquitoMove>();
        _health = GetComponent<MosquitoHealth>();
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<SphereCollider>();
        _model = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        if (_isPause) return;

        if (_health.IsDead)
        {
            var fall = _health.Falling();

            Destroy();
        }
        else
        {
            _move.Move();
        }
    }

    public void Create()
    {
        _rb.isKinematic = false;
        _col.enabled = true;
        _model.enabled = true;
        _health.IsDead = false;
        _isActive = true;
        _health.IsDead = false;
    }

    public void Destroy()
    {
        _rb.isKinematic = true;
        _col.enabled = false;
        _model.enabled = false;
        _isActive = false;
        _health.IsDead = true;
        _move.SoundPlayer.StopSound();
        _health.OnDestroy?.Invoke();
    }

    public void DisactiveForInstantiate()
    {
        _rb.isKinematic = true;
        _col.enabled = false;
        _model.enabled = false;
        _isActive = false;
        _health.IsDead = true;
    }
    void Pause()
    {
        _isPause = true;
    }
    void Resume()
    {
        _isPause = false;
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
