using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]float _interval = 10.0f;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _spawnPos;
    [SerializeField] int _enemyLimit = 10;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemyLimit > 0 && timer > _interval)
        {
            Instantiate(_enemy, _spawnPos.transform);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
