using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Muzzle : MonoBehaviour
{
    [SerializeField] Bullet _bullet;
    [SerializeField] Transform _muzzuleTransform;
    [SerializeField] int _limit = 20;
    [SerializeField] GameObject a;

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

        a.transform.position = Camera.main.WorldToScreenPoint (pos);

        var root = new GameObject("BulletRoot").transform;

        _pool.SetBaseObj(_bullet, root);
        _pool.SetCapacity(_limit);
    }

    private void Update()
    {
        if(InputUtility.GetDownFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        var bullet = _pool.Instantiate();
    }
}
