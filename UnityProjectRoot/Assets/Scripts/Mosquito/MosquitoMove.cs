using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

/// <summary>
/// 蚊の動きを制御するコンポーネント
/// </summary>
public class MosquitoMove : MonoBehaviour
{
    [Header("蚊の巡回地点")]
    [SerializeField, Tooltip("蚊の巡回地点")] GameObject[] _wayPoints;
    [SerializeField, Tooltip("何秒かけて移動するか")] float _moveTime;
    [SerializeField, Tooltip("各巡回地点へ対しての動き方")] PathType _pathType;

    private void Start()
    {
        transform.DOPath
            (
            _wayPoints.Select(wayPoints => wayPoints.transform.position).ToArray(),
            _moveTime,
            _pathType
            )
            .SetLookAt(0.01f) // 前を向くようにする
            .SetLoops(-1, LoopType.Yoyo);
    }
}
