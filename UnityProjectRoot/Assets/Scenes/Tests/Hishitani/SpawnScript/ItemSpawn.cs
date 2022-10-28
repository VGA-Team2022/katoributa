using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] Item _item;//アイテムのオブジェクト
    [SerializeField] List<GameObject> _spawnPosition;//アイテムの出現位置
    private void Start()
    {
        for (int i = 0; i < _spawnPosition.Count; i++)
        {
            //アイテムの生成
            Instantiate(_item, _spawnPosition[i].transform);
            Debug.Log("アイテム出した");
        }
    }
}
