using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]float _interval = 10.0f;//新しく蚊を出すまでの間隔(秒)
    [SerializeField] GameObject _enemy;//蚊のオブジェクト
    [SerializeField] List<GameObject>  _spawnPos;//出現させる位置
    [SerializeField] int _enemyLimit = 10;//何体まで出すか
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
            //蚊の生成　位置は複数に場所からランダムに選択される
            Instantiate(_enemy, _spawnPos[Random.Range(0,_spawnPos.Count)].transform);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
