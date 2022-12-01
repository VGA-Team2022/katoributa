using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField, Tooltip("生成する弾のプレハブをアタッチ")]
    GameObject _bulletPref = null;

    /// <summary> 発射した弾 </summary>
    GameObject[] _bullets = new GameObject[30];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _bullets.Length; i++)
        {
            _bullets[i] = Instantiate(_bulletPref);
            _bullets[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_bulletPref && InputUtility.GetDownFire)
        {
            foreach(GameObject bullet in _bullets)
            {
                if (!bullet.activeSelf)
                {
                    Debug.Log($"{bullet.name}");
                    bullet.SetActive(true);
                    bullet.transform.position = transform.position;
                    bullet.transform.forward = transform.forward;
                    break;
                }
            }
        }
    }
}
