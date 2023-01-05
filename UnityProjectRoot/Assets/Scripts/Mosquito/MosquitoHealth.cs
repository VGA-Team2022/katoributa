using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// 蚊の体力、煙に当たった時の挙動
/// </summary>
public class MosquitoHealth : MonoBehaviour
{
    [Header("蚊の体力")]
    [SerializeField, Tooltip("蚊の体力")] int _health = 3;
    [Header("落下速度")]
    [SerializeField] float _fallingSpeed = 5f;
    [Header("追加するスコア")]
    [SerializeField, Min(0)] int _score = 100;
    [SerializeField] SoundPlayer _sound;
    [SerializeField] int _cueId = 9;

    VisualEffect _model;
    Transform _thisTransform;
    Vector3 _landingPosition;
    Rigidbody _rb;

    float _elapsed;
    int _currentHealth;
    float _turbulenceIntensity;
    float _turbulenceDrag;

    const float _fallDuration = 2f;
    const float _rayDistance = 100f;
    const float _destroyDistance = 1f;

    public delegate void DestroyDelegate();
    public DestroyDelegate OnDestroy;

    bool _isDead;

    public bool IsDead { get => _isDead; set => _isDead = value; }

    private void Awake()
    {
        _model = GetComponent<VisualEffect>();
        _rb = GetComponent<Rigidbody>();
        _thisTransform = this.transform;
        _turbulenceIntensity = GetProperty("Turbulence Intensity");
        _turbulenceDrag = GetProperty("Turbulence Drag");
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log($"蚊がダメージを受けた(受けたダメージ：{damage})");

        if (_currentHealth >= 0)
        {
            Debug.Log($"蚊が倒された");
            _isDead = true;
            _landingPosition = PositionCalculation();
            SetProperty("Turbulence Intensity", 0);
            SetProperty("Turbulence Drag", 0);
            GameManager.Instance.AddScore(_score);
        }
    }

    public void  Init()
    {
        _elapsed = 0;
        _currentHealth = _health;
        SetProperty("Turbulence Intensity", _turbulenceIntensity);
        SetProperty("Turbulence Drag", _turbulenceDrag);
    }

    public bool Falling()
    {
        if(Vector3.Distance(_thisTransform.position, _landingPosition) < _destroyDistance)
        {
            return false;
        }

        _elapsed += Time.deltaTime;
        float speed = 0;

        if (_elapsed < _fallDuration)
        {
            speed = Mathf.Lerp(0, _fallingSpeed, _elapsed / _fallDuration);
        }

        var dir = _landingPosition - _thisTransform.position;
        dir.Normalize();
        _rb.velocity = dir * speed;

        return true;
    }

    Vector3 PositionCalculation()
    {
        if (_sound)
        {
            _sound.PlaySound(_cueId);
        }

        RaycastHit info = default;
        var hit = Physics.Raycast(_thisTransform.position, Vector3.down, out info, _rayDistance);
        return hit ? info.point : _thisTransform.position;
    }

    void SetProperty(string name, float value)
    {
        _model.SetFloat(name, value);
    }
    float GetProperty(string name)
    {
        return _model.GetFloat(name);
    }
}
