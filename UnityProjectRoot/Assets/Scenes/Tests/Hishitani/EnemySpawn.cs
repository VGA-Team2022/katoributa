using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]float Interval = 10.0f;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject SpawnPos;
    [SerializeField] int EnemyLimit = 10;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyLimit > 0 && timer > Interval)
        {
            Instantiate(Enemy, SpawnPos.transform);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
