using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Muzzle : MonoBehaviour
{
    [SerializeField]GameObject bulletObject;

    private void Update()
    {
        if (InputUtility.GetDownFire)
        {
            OnAttack();
        }
    }

    public void OnAttack()
    {
        var bullet = Instantiate(bulletObject, gameObject.transform.position, Quaternion.identity);
        bullet.transform.rotation = this.transform.rotation;
    }
}
