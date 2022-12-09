using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVPMosquitCoil : MonoBehaviour
{
    Material _coilMaterial;

    private void Awake()
    {
        _coilMaterial = GetComponent<Renderer>().material;
    }

    public void SetValue(float value)
    {
        if(_coilMaterial)
        {
            _coilMaterial.SetFloat("_disssolveamount", value);
        }
    }
}
