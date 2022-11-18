using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _Speed;
    float _sponeTime = 0;
    [SerializeField] float _LifeTime;
    [SerializeField] int _damage;

    void Update()
    {
        transform.position += transform.forward * _Speed * Time.deltaTime;
        _sponeTime += Time.deltaTime;
        if (_sponeTime > _LifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log("‰á‚ª’e‚É“–‚½‚Á‚½");
            var obj = other.GetComponent<MosquitoHealth>();
            obj.TakeDamage(_damage);
        }
    }
}
