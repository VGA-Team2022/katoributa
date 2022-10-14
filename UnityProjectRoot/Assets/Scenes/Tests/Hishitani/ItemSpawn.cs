using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject SpawnPos;
    [SerializeField] GameObject Item;
    [SerializeField] float SpawnTime = 10.0f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Item && timer > SpawnTime)
        //対象のアイテムがとられていて、TimerがSpawnTimeより大きい場合、アイテムを生成
        //Timerのリセットはアイテム側でやってもらう予定
        {
            Instantiate(Item,SpawnPos.transform);
        }
        timer += Time.deltaTime;
    }
}
