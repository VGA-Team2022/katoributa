using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Muzzle : MonoBehaviour
{
    [SerializeField] VisualEffect _effect;
    BoxCollider _collider;

    void Start()
    {
        _collider = _effect.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (InputUtility.GetDownFire)
        {
            OnAttack();
        }
        else
        {
            StopAttack();
        }
    }

    public void OnAttack()
    {
        _effect.initialEventName = "OnPlay";
        _collider.enabled = true;
    }

    public void StopAttack()
    {
        _effect.initialEventName = "OnStop";
        _collider.enabled = false;
    }
}
