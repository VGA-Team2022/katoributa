using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] Item _item;
    [SerializeField] List<GameObject> _spawnPosition;
    private void Start()
    {
        for (int i = 0; i < _spawnPosition.Count; i++)
        {
            Instantiate(_item, _spawnPosition[i].transform);
            Debug.Log("アイテム出した");
        }
    }
}
