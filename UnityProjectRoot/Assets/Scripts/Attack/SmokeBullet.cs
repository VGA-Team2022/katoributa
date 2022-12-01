using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBullet : MonoBehaviour
{
    [SerializeField, Tooltip("弾速")]
    float _speed;
    
    /// <summary>弾が存在する時間を計測する変数</summary>
    float _sponeTime = 0;

    [SerializeField, Tooltip("弾が存在する時間")]
    float _LifeTime;

    [SerializeField, Tooltip("与えるダメージ量")]
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
            Debug.Log("蚊が弾に当たった");
            var obj = other.GetComponent<MosquitoHealth>();
            obj.TakeDamage(_damage);
        }
    }
}
