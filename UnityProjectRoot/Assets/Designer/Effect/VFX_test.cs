using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;    //ここ忘れないように

public class VFX_test : MonoBehaviour
{
    [SerializeField] VisualEffect effect;
    //HierarchyにあるVisual Effectのオブジェクトを参照する(VFXGraphそのものではない)

    void Update()
    {
        //方向キー上で停止、方向キー下で再生
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            effect.SendEvent("StopPlay");
            //StopPlayはEvent Nameで指定した任意の名称
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            effect.SendEvent("OnPlay");
            //OnPlayはEvent Nameで指定した任意の名称
        }
    }
}