using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Muzzle : MonoBehaviour
{
    [Header("ê›íË")]
    [SerializeField] Bullet _bullet;
    [SerializeField] Transform _muzzuleTransform;
    [Space(5)]
    [SerializeField, Range(0.5f,1.5f)] float _reloadTime = 0.5f;
    [Header("ÉåÉeÉBÉNÉãUI")]
    [SerializeField] Image _reticle;
    [SerializeField] SoundPlayer _soundPlayer;
    [SerializeField] int _shotSoundId;
    [SerializeField] int _merameraShotSoundId;
    bool _isPowerUp;

    float _reloadTimer;

    const int _limit = 20;

    ObjectPool<Bullet> _pool = new ObjectPool<Bullet> ();

    private void Awake()
    {
        if(!_muzzuleTransform)
        {
            _muzzuleTransform = this.transform;
        }
    }

    private void Start()
    {
        var pos = _muzzuleTransform.position;
        pos.z += 100;

        if (_reticle)
        {
            _reticle.transform.position = Camera.main.WorldToScreenPoint(pos);
        }

        var root = new GameObject("BulletRoot").transform;

        _pool.SetBaseObj(_bullet, root);
        _pool.SetCapacity(_limit);
    }

    private void Update()
    {
        if (GameManager.Instance.GameState == GameState.GameFinish) return;
        if (_reloadTimer <= _reloadTime)
        {
            _reloadTimer += Time.deltaTime;
            return;
        }

        if(InputUtility.GetDownFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        if(_isPowerUp)
        {
            _soundPlayer.PlaySound(_merameraShotSoundId);
        }
        else
        {
            _soundPlayer.PlaySound(_shotSoundId);
        }
       
        var bullet = _pool.Instantiate();
        bullet.transform.position = _muzzuleTransform.position;
        bullet.transform.rotation = _muzzuleTransform.rotation;

        _reloadTimer = 0;
    }
}
