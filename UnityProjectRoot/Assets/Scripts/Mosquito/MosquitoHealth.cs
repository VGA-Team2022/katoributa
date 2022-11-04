using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 蚊の体力、煙に当たった時の挙動
/// </summary>
public class MosquitoHealth : MonoBehaviour
{
    [Header("蚊の体力")]
    [SerializeField, Tooltip("蚊の体力")] int _health = 3;

    public delegate void DestroyDelegate();
    public DestroyDelegate OnDestroy;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"蚊がダメージを受けた(受けたダメージ：{damage})");

        if(_health >= 0)
        {
            Debug.Log($"蚊が倒された");
            OnDestroy?.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == 8)
        {
            TakeDamage(1);
            Debug.Log("蚊が煙に当たった");
        }
    }
}
