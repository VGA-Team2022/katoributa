using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _lifeTime = 3f;
    [SerializeField] int _damage = 1;
    [SerializeField] LayerMask _enemyLayer;
    
    float _lifeTimer = 0;
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _lifeTimer += Time.deltaTime;

        if(_lifeTimer > _lifeTime)
        {
            Destroy(this.gameObject);
        }

        var dir = Vector3.forward * _speed;
        _rb.velocity = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _enemyLayer)
        {
            var e = other.GetComponent<MosquitoHealth>();
            e.TakeDamage(_damage);
        }
    }
}
