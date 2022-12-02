using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Muzzle : MonoBehaviour
{
    [SerializeField] Bullet _bullet;
    [SerializeField] Transform _muzzuleTransform;

    GameObject _root;

    private void Awake()
    {
        if(!_muzzuleTransform)
        {
            _muzzuleTransform = this.transform;
        }
    }

    private void Start()
    {
        _root = new GameObject("BulletRoot");
    }

    private void Update()
    {
        if(InputUtility.GetDownFire)
        {
            OnAttack();
        }
    }

    void OnAttack()
    {
        var bullet = Instantiate(_bullet, _muzzuleTransform.position, Quaternion.identity, _root.transform);
        bullet.transform.LookAt(_muzzuleTransform.forward);
    }
}
