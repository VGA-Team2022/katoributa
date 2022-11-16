using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MosquitoHealth))]
/// <summary>
/// 蚊の動きを制御するコンポーネント
/// </summary>
public class MosquitoMove : MonoBehaviour
{
    /*MEMO
    蚊が移動するポイントはランダムな位置
    等速的な動きではなく、緩急のあるふわふわした動き
    回転に関しては別クラスで行っている
     */

    [Header("蚊の巡回地点")]
    [Tooltip("蚊の巡回地点")] Vector3?[] _wayPoints;
    [SerializeField, Tooltip("移動する速さ(Xが最低値・Yが最大値)")] Vector2 _moveSpeed = Vector2.one;
    [SerializeField, Tooltip("巡回地点を変更するまでの距離")] float _moveNextDistance = 0.2f;
    [SerializeField, Tooltip("巡回地点に着いてから次に動き出すまでの時間")] float _stopTime = 0.5f;

    int _random;
    int _currentIndex;
    float _currentMoveSpeed;

    float _stopTimer;

    Rigidbody _rb;
    Transform _thisTransform;

    private void Awake()
    {

        //キャッシュ
        _rb = GetComponent<Rigidbody>();
        _thisTransform = this.transform;

        //ランダムな速さにする
        _currentMoveSpeed = Random.Range(_moveSpeed.x, _moveSpeed.y);

        if (_rb is null) return;

        //重量を無効化
        _rb.useGravity = false;
    }

    private void Start()
    {   
        //パーリンノイズで使用するYの値
        _random = Random.Range(0, 10);
    }

    private void Update()
    {
        //移動処理
        if(_rb is null) return;

        //向かっている巡回地点との距離が一定以下になったら目的地の更新
        if (Vector3.Distance(_thisTransform.position, _wayPoints[_currentIndex].Value) < _moveNextDistance)
        {
            //一定時間止まってから
            _stopTimer += Time.deltaTime;

            //更新する
            if (_stopTimer > _stopTime)
            {
                _stopTimer = 0;
                MoveNext();
            }

            _rb.velocity = Vector3.zero;

            return;
        }

        var dir = _wayPoints[_currentIndex].Value - _thisTransform.position;
        dir.Normalize();

        //ベクトル更新
        //動きに統一性を持たせないようにノイズをかける（経過時間×ランダムな値）
        _rb.velocity = dir * Mathf.PerlinNoise(Time.time, _random) * _currentMoveSpeed;
    }

    /// <summary>
    /// 生成時にマップ内のランダムな座標を設定する
    /// </summary>
    /// <param name="points"></param>
    public void Init(Vector3?[] points)
    {
        //一旦空にしてから追加
        _wayPoints = null;
        _wayPoints = points;
    }

    /// <summary>
    /// 目的地を更新させる
    /// </summary>
    void MoveNext()
    {
        //ループさせる為
        _currentIndex = (_currentIndex + 1) % _wayPoints.Length;

        //指定された要素の座標をNullだったらもう一度
        if (_wayPoints[_currentIndex] is null)
        {
            MoveNext();
        }
    }
}
