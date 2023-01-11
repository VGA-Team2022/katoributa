using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVPMosquitCoil : MonoBehaviour
{
    Material _coilMaterial;
    float _changeValue;
    const float _sliderValue = 1.07f;

    private void Awake()
    {
        _coilMaterial = GetComponent<Renderer>().material;
    }

    public void OnStart(float time)
    {
        _changeValue = _sliderValue / time;
    }

    public void SetValue(float value)
    {
        if(_coilMaterial)
            //var currentValue = _coilMaterial.GetFloat("_disssolveamount");
            _coilMaterial.SetFloat("_disssolveamount", (_changeValue * value) - 0.97f);
    }
}
