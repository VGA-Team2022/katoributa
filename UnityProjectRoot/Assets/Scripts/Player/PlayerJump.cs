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

    void Start()
    {
        _playerState = GetComponent<PlayerState>();
        _rb = GetComponent<Rigidbody>();
        _soundPlayer = GetComponent<SoundPlayer>();
    }

    void Update()
    {
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
            _rb.AddForce(Vector3.up * _playerJumpSpeed, ForceMode.Impulse);
            _soundPlayer.PlaySound(_jumpSoundId);
        }
    }
}
