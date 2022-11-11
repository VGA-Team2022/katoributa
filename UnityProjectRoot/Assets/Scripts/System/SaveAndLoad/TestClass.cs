using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    public int _num;
    public string _str;
    public Vector3 _vec;

    public void SetValue(TestSaveData saveData)
    {
        //値TestSaveDataからセット
        _num = saveData._num;
        _str = saveData._str;
        _vec = saveData._vec;
    }

    public static TestClass I { get; private set; }
    private void Awake()
    {
        I = this;
    }
}

