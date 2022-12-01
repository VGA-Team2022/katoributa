using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBullet : MonoBehaviour
{
    [SerializeField, Tooltip("’e‘¬")]
    float _speed;
    
    /// <summary>’e‚ª‘¶İ‚·‚éŠÔ‚ğŒv‘ª‚·‚é•Ï”</summary>
    float _sponeTime = 0;

    [SerializeField, Tooltip("’e‚ª‘¶İ‚·‚éŠÔ")]
    float _LifeTime;

    [SerializeField, Tooltip("—^‚¦‚éƒ_ƒ[ƒW—Ê")]
    int _damage;

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        _sponeTime += Time.deltaTime;
        if (_sponeTime > _LifeTime)
        {
            _sponeTime = 0;
            gameObject.SetActive(false);
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
