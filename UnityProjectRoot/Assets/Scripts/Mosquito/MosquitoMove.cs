using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 蚊の動きを制御するコンポーネント
/// </summary>
public class MosquitoMove : MonoBehaviour
{
    [Header("蚊の巡回地点")]
    [SerializeField, Tooltip("蚊の巡回地点")] Vector3[] _wayPoints;

    private void Start()
    {
        transform.DOPath(_wayPoints, 10f).SetLookAt(0.01f)
                                         .SetLoops(-1); ;
    }
}
