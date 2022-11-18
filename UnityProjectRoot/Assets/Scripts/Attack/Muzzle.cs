using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Muzzle : MonoBehaviour
{
    [SerializeField] VisualEffect _effect;
    BoxCollider _collider;
    float _time;
    float _effectTime = 1.0f;

    void Start()
    {
        _collider = _effect.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (InputUtility.GetDownFire)
        {
            OnAttack();
        }
        
        if(_time >= _effectTime)
        {
            StopAttack();
        }
    }

    public void OnAttack()
    {
        _effect.enabled = true;
        _collider.enabled = true;
        _time = 0;
    }

    public void StopAttack()
    {
        _effect.enabled = false;
        _collider.enabled = false;
    }
}
