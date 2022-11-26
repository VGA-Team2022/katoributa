using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("プレイヤーのスピード"), SerializeField]
    float _playerSpeed = 5.0f;

    [Tooltip("プレイヤーの移動入力に対する追従度、PlayerSpeedに準拠"), SerializeField]
    float _playerSpeedMultiply = 5.0f;

    [Tooltip("スピードの上限"), SerializeField]
    float _maximizePlayerSpeed = 5.0f;

    [Tooltip("リジッドボディ")]
    Rigidbody _rb;

    [Tooltip("現在の最大スピード")]
    float _currentMaximizeSpeed;

    [Tooltip("現在のスピード")]
    float _currentSpeed;

    [Tooltip("サウンドプレイヤーコンポーネント")]
    SoundPlayer _soundPlayer;

    [Tooltip("メラメラ時のスピード"), SerializeField]
    float _merameraPlayerSpeed = 10.0f;

    [Tooltip("メラメラ時の最大スピード"), SerializeField]
    float _merameraPlayerMaximizeSpeed = 10.0f;

    [Tooltip("プレイヤーステートコンポーネント")]
    PlayerState _playerState;

    void Start()
    {
        SetUp();
    }

    void FixedUpdate()
    {
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
        _soundPlayer = GetComponent<SoundPlayer>();
        _playerState = GetComponent<PlayerState>();
        PlayerPowerDown();
        GameManager.Instance.OnPowerUpEvent += PlayerPowerUp;
        GameManager.Instance.OnPowerDownEvent += PlayerPowerDown;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnPowerUpEvent -= PlayerPowerUp;
        GameManager.Instance.OnPowerDownEvent -= PlayerPowerDown;
    }

    /// <summary>
    /// Playerの移動方法を決定するステート
    /// </summary>
    void PlayerMoveMethod()
    {
        if (_rb.velocity.magnitude <= _currentMaximizeSpeed)
        {
            Vector3 dir = PlayerVec(InputUtility.GetDirectionMove);
            _rb.AddForce(_playerSpeedMultiply * (dir - _rb.velocity));
        }

        if (_playerState.IsMove)
        {
            _soundPlayer.PlaySound("SE_walk wood 3");
        }
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

    void PlayerPowerUp()
    {
        _currentSpeed = _merameraPlayerSpeed;
        _currentMaximizeSpeed = _merameraPlayerMaximizeSpeed;
    }

    void PlayerPowerDown()
    {
        _currentSpeed = _playerSpeed;
        _currentMaximizeSpeed = _maximizePlayerSpeed;
    }
}
