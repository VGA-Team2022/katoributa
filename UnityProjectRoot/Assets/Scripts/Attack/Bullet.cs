using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _Speed;
    float _sponeTime = 0;
    [SerializeField] float _LifeTime;
    void Update()
    {
        transform.position += transform.forward * _Speed * Time.deltaTime;
        _sponeTime += Time.deltaTime;
        if (_sponeTime > _LifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
