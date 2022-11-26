using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Tooltip("ジャンプ力"), SerializeField]
    float _playerJumpSpeed = 3.0f;

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
            _soundPlayer.PlaySound("SE_jump1");
        }
    }
}
