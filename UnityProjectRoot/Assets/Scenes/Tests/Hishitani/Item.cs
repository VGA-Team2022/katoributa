using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemSpawn _targetItem;
    [SerializeField] SenkouHealth _target;
    [SerializeField] float _heal = 10;
    private void OnTriggerEnter(Collider other)
    {
        _target.GetHeal(_heal);
        _targetItem.TimerLiset();
        gameObject.active = false;
    }
}
