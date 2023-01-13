using UnityEngine;
using UnityEngine.VFX;

public class PlayerMove : MonoBehaviour
{
    [Header("通常時")]
    [Tooltip("プレイヤーのスピード"), SerializeField]
    float _playerSpeed = 20.0f;

    [Tooltip("プレイヤーの移動入力に対する追従度、PlayerSpeedに準拠"), SerializeField]
    float _playerSpeedMultiply = 1.0f;

    [Tooltip("スピードの上限"), SerializeField]
    float _maximizePlayerSpeed = 5.0f;

    [Tooltip("リジッドボディ")]
    Rigidbody _rb;

    Animator _anim;

    [Tooltip("現在の最大スピード")]
    float _currentMaximizeSpeed;

    [Tooltip("現在のスピード")]
    float _currentSpeed;

    [Tooltip("サウンドプレイヤーコンポーネント")]
    SoundPlayer _soundPlayer;

    [Header("メラメラ時")]
    [Tooltip("メラメラ時のスピード"), SerializeField]
    float _merameraPlayerSpeed = 20.0f;

    [Tooltip("メラメラ時の最大スピード"), SerializeField]
    float _merameraPlayerMaximizeSpeed = 5.0f;

    [Tooltip("プレイヤーステートコンポーネント")]
    PlayerState _playerState;

    [Header("サウンド")]
    [SerializeField]
    int _walkSoundId = 2;

    [Header("マテリアル")]
    [SerializeField]
    Material _normalMaterial;

    [SerializeField]
    Material _merameraMaterial;

    [Space(5)]
    [SerializeField]
    SkinnedMeshRenderer _skinnedMesh;

    [SerializeField] VisualEffect _effect;

    bool _isPowerUp;

    bool _isPause;

    void Start()
    {
        SetUp();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GameState != GameState.InGame || _isPause) return;
        PlayerMoveMethod();
    }

    /// <summary>
    /// Startで行う初期セットアップ
    /// </summary>
    void SetUp()
    {
        if (!TryGetComponent(out _rb))
        {
            _rb = gameObject.AddComponent<Rigidbody>();
        }

        _anim = GetComponent<Animator>();
        _soundPlayer = GetComponent<SoundPlayer>();
        _playerState = GetComponent<PlayerState>();
        PlayerMovePowerDown();
    }

    void OnEnable()
    {
        GameManager.Instance.OnPowerUpEvent += PlayerMovePowerUp;
        GameManager.Instance.OnPowerDownEvent += PlayerMovePowerDown;
        GameManager.Instance.OnPause += PlayerMovePause;
        GameManager.Instance.OnResume += PlayerMoveResume;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPowerUpEvent -= PlayerMovePowerUp;
        GameManager.Instance.OnPowerDownEvent -= PlayerMovePowerDown;
        GameManager.Instance.OnPause -= PlayerMovePause;
        GameManager.Instance.OnResume -= PlayerMoveResume;
    }

    /// <summary>
    /// Playerの移動方法を決定するステート
    /// </summary>
    void PlayerMoveMethod()
    {
        if (!_isPowerUp && !_playerState.IsGround())
            return;

        if (_playerState.PlayerSpeedSqrMagnitude <= _currentMaximizeSpeed * _currentMaximizeSpeed)
        {
            Vector3 dir = PlayerVec(InputUtility.GetDirectionMove);
            _rb.AddForce(_playerSpeedMultiply * (dir - _rb.velocity));
        }

        //アニメーションのパラメータに値を設定
        _anim.SetFloat("Speed", _playerState.PlayerSpeedSqrMagnitude);
    }

    /// <summary>
    /// 進行方向を決定する関数
    /// </summary>
    /// <param name="inputVec">入力方向をVector2で</param>
    /// <returns>進行方向</returns>
    Vector3 PlayerVec(Vector2 inputVec)
    {
        Vector3 vec = new Vector3(inputVec.x, 0, inputVec.y);
        vec.Normalize();
        vec *= _currentSpeed;
        vec.y = _rb.velocity.y;
        vec = transform.TransformDirection(vec);
        return vec;
    }

    void PlayerMovePowerUp()
    {
        _currentSpeed = _merameraPlayerSpeed;
        _currentMaximizeSpeed = _merameraPlayerMaximizeSpeed;
        _isPowerUp = true;
        _effect.SendEvent("OnPlay");

        if (_skinnedMesh && _merameraMaterial)
            _skinnedMesh.material = _merameraMaterial;
    }

    void PlayerMovePowerDown()
    {
        _currentSpeed = _playerSpeed;
        _currentMaximizeSpeed = _maximizePlayerSpeed;
        _isPowerUp = false;
        _effect.SendEvent("StopPlay");

        if (_skinnedMesh && _normalMaterial)
            _skinnedMesh.material = _normalMaterial;
    }

    /// <summary>
    /// 歩く時の音を再生する
    /// </summary>
    public void EmitWalkSound()
    {
        if (_playerState.IsMove)
        {
            _soundPlayer.PlaySound(_walkSoundId);
        }
    }

    void PlayerMovePause()
    {
        _rb.isKinematic = true;
        _soundPlayer.PauseSound(true);
        _isPause = true;
    }

    void PlayerMoveResume()
    {
        _rb.isKinematic = false;
        _soundPlayer.PauseSound(false);
        _isPause = false;
    }
}
