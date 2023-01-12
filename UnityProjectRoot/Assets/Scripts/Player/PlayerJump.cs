using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("通常時")]
    [Tooltip("ジャンプ力"), SerializeField]
    float _playerJumpSpeed = 8.0f;

    [Header("メラメラ時")]
    [SerializeField]
    float _merameraJumpSpeed = 8f;

    [Header("サウンド")]
    [SerializeField]
    int _jumpSoundId = 1;

    [Tooltip("プレイヤーステートコンポーネント")]
    PlayerState _playerState;

    [Tooltip("リジッドボディコンポーネント")]
    Rigidbody _rb;

    [Tooltip("プレイヤーの音コンポーネント")]
    SoundPlayer _soundPlayer;

    float _currentJumpSpeed;

    bool _isPause;

    void Start()
    {
        _playerState = GetComponent<PlayerState>();
        _rb = GetComponent<Rigidbody>();
        _soundPlayer = GetComponent<SoundPlayer>();
        PlayerJumpPowerDown();
    }

    void OnEnable()
    {
        GameManager.Instance.OnPowerUpEvent += PlayerJumpPowerUp;
        GameManager.Instance.OnPowerDownEvent += PlayerJumpPowerDown;
        GameManager.Instance.OnPause += PlayerJumpPause;
        GameManager.Instance.OnResume += PlayerJumpResume;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPowerUpEvent -= PlayerJumpPowerUp;
        GameManager.Instance.OnPowerDownEvent -= PlayerJumpPowerDown;
        GameManager.Instance.OnPause -= PlayerJumpPause;
        GameManager.Instance.OnResume -= PlayerJumpResume;
    }

    void Update()
    {
        if (GameManager.Instance.GameState != GameState.InGame || _isPause) return;
        PlayerJumpMethod();
    }

    /// <summary>
    /// プレイヤーがジャンプする機能
    /// 改良予定
    /// </summary>
    void PlayerJumpMethod()
    {
        if (InputUtility.GetDownJump && _playerState.IsGround())
        {
            _rb.AddForce(Vector3.up * _currentJumpSpeed, ForceMode.Impulse);
            _soundPlayer.PlaySound(_jumpSoundId);
        }
    }

    void PlayerJumpPowerUp()
    {
        _currentJumpSpeed = _merameraJumpSpeed;
    }

    void PlayerJumpPowerDown()
    {
        _currentJumpSpeed = _playerJumpSpeed;
    }

    void PlayerJumpPause()
    {
        _rb.isKinematic = true;
        _soundPlayer.PauseSound(true);
        _isPause = true;
    }

    void PlayerJumpResume()
    {
        _rb.isKinematic = false;
        _soundPlayer.PauseSound(false);
        _isPause = false;
    }
}
