using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;    //�����Y��Ȃ��悤��

public class VFX_test : MonoBehaviour
{
    [SerializeField] VisualEffect effect;
    //Hierarchy�ɂ���Visual Effect�̃I�u�W�F�N�g���Q�Ƃ���(VFXGraph���̂��̂ł͂Ȃ�)

    void Update()
    {
        //�����L�[��Œ�~�A�����L�[���ōĐ�
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            effect.SendEvent("StopPlay");
            //StopPlay��Event Name�Ŏw�肵���C�ӂ̖���
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            effect.SendEvent("OnPlay");
            //OnPlay��Event Name�Ŏw�肵���C�ӂ̖���
        }
    }
}