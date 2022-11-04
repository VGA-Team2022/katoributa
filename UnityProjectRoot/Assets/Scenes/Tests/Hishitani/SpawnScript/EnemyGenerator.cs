using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("蚊のプレハブ")] MosquitoMove _enemy;//蚊のオブジェクト
    [SerializeField, Tooltip("巡回する座標")] Transform[] _wayPoints;
    [SerializeField, Tooltip("巡回する座標の数")] int _wayPointsLimit = 4;
    [SerializeField, Tooltip("マップ内に何体出現していいか")] int _limit = 10;
    
    void Start()
    {
        //wayPointsLimitが座標よりも大きかった時に丸める
        _wayPointsLimit = Mathf.Clamp(_wayPointsLimit, 0, _wayPoints.Length);

        //最初にマップ内に存在する敵を生成
        for(int i = 0; i < _limit; i++)
        {
            Spawn();
        }
    }

    /// <summary>
    /// 敵を生成する
    /// </summary>
    public void Spawn()
    {
        var quotaCount = GameManager.Instance.Quota.Value;
        var defeatCount = GameManager.Instance.Score.Value;

        //（ノルマ - 倒した数）が０以下であれば何もしない
        if (quotaCount - defeatCount <= 0) return;

        var points = new Transform[_wayPointsLimit];

        for(int i = 0; i < _wayPointsLimit; i++)
        {
            //ランダムな位置に設定
            var r = Random.Range(0, _wayPoints.Length);
            points[i] = _wayPoints[r];

            //後で被らないようにするかも
        }

        //敵を作成
        var enemy = Instantiate(_enemy, points[0].position, Quaternion.identity);
        //敵の巡回位置を設定
        enemy.Init(points);

        //MosquitoHealthを取得
        var health = enemy.GetComponent<MosquitoHealth>();
        //敵が死んだときに呼ばれるデリデートに生成の関数を登録
        health.OnDestroy = Spawn;
    }
}
