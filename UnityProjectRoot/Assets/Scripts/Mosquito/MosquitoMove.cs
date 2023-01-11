using UnityEngine;

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
    [SerializeField ,Tooltip("蚊の巡回地点")] Vector3[] _wayPoints;
    [SerializeField, Tooltip("移動する速さ(Xが最低値・Yが最大値)")] Vector2 _moveSpeed = Vector2.one;
    [SerializeField, Tooltip("巡回地点を変更するまでの距離")] float _moveNextDistance = 0.2f;
    [SerializeField, Tooltip("巡回地点に着いてから次に動き出すまでの時間")] float _stopTime = 0.5f;
    [Header("サウンド")]
    [SerializeField, Tooltip("蚊の移動音(ID)")] int _cueId = 3;

    int _random;
    int _currentIndex;
    float _currentMoveSpeed;

    float _stopTimer;

    Rigidbody _rb;
    Transform _thisTransform;
    SoundPlayer _sound;

    public SoundPlayer SoundPlayer => _sound;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _thisTransform = this.transform;
        _sound = GetComponent<SoundPlayer>();
    }

    private void Start()
    {
        if (_rb)
        {
            _rb.useGravity = false;
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    public void Move()
    {
        if (_rb is null) return;

        if (Vector3.Distance(_thisTransform.position, _wayPoints[_currentIndex]) < _moveNextDistance)
        {
            _stopTimer += Time.deltaTime;

            if (_stopTimer > _stopTime)
            {
                _stopTimer = 0;
                MoveNext();
            }

            _rb.velocity = Vector3.zero;

            return;
        }

        var dir = _wayPoints[_currentIndex] - _thisTransform.position;
        dir.Normalize();

        _rb.velocity = dir * Mathf.PerlinNoise(Time.time, _random) * _currentMoveSpeed;
    }

    public void Init(Vector3[] points)
    {
        _wayPoints = null;
        _wayPoints = points;

        _currentMoveSpeed = Random.Range(_moveSpeed.x, _moveSpeed.y);

        _random = Random.Range(0, 10);

        if (_sound)
        {
            _sound.PlaySound(_cueId);
        }
    }

    /// <summary>
    /// 目的地を更新させる
    /// </summary>
    void MoveNext()
    {
        _currentIndex = (_currentIndex + 1) % _wayPoints.Length;

        if (_wayPoints[_currentIndex] == null)
        {
            MoveNext();
        }
    }
    void Pause()
    {
        _sound.PauseSound(true);
        _rb.isKinematic = true;
    }
    void Resume()
    {
        _sound.PauseSound(false);
        _rb.isKinematic = false;
    }
    private void OnEnable()
    {
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnResume += Resume;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnPause -= Pause;
        GameManager.Instance.OnResume -= Resume;
    }
}
