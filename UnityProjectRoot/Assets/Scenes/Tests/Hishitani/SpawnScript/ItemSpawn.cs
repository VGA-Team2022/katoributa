using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] _spawnPosition;//アイテムの位置
    [SerializeField] Item _itemOdject;
    public int _getCount = 0;
    Item[] _items;
    bool _pauseSwich = false;

    [SerializeField] SoundPlayer _soundPlayer;
    [SerializeField] string _soundName = "SE_item get mono";

    private void Start()
    {
        _soundPlayer = GetComponent<SoundPlayer>();

        _items = new Item[_spawnPosition.Length];
        for (int i = 0; i < 2;)
        {
            int x = Random.Range(0, 2);
            if(!_items[x])
            {
                _items[x] = Instantiate(_itemOdject, _spawnPosition[i].transform);
                _items[x]._objectCount = x;
                i++;
            }
            
        }
    }
    public void Spawn(int offNumber)
    {
        _soundPlayer.PlaySound(_soundName);

        if (_getCount < 5)
        {
            for(int i = 0; i < 3; i++)
            {
                if(i != offNumber)
                {
                    if(!_items[i])
                    {
                        _items[i] = Instantiate(_itemOdject, _spawnPosition[i].transform);
                        _items[i]._objectCount = i;
                        break;
                    }
                }
            }
        }
        else if(_getCount < 10)
        {
            for (int i = 3; i < 6; i++)
            {
                if (i != offNumber)
                {
                    if (!_items[i])
                    {
                        _items[i] = Instantiate(_itemOdject, _spawnPosition[i].transform);
                        _items[i]._objectCount = i;
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = 6; i < 9; i++)
            {
                if (i != offNumber)
                {
                    if (!_items[i])
                    {
                        _items[i] = Instantiate(_itemOdject, _spawnPosition[i].transform);
                        _items[i]._objectCount = i;
                        break;
                    }
                }
            }
        }
    }
    void Pause()
    {
        _pauseSwich = true;
    }
    void Resume()
    {
        _pauseSwich = false;
    }
}
