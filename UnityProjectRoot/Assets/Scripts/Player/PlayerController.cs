using UnityEngine;

/// <summary>
/// プレイヤーを動かすコンポーネント
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Tooltip("プレイヤーのスピード"), SerializeField]
    float _playerSpeed = 5.0f;

    [Tooltip("プレイヤーの移動入力に対する追従度、PlayerSpeedに準拠"), SerializeField]
    float _playerSpeedMultiply = 5.0f;

    [Tooltip("スピードの上限"), SerializeField]
    float _maximizePlayerSpeed = 5.0f;

    [Tooltip("ジャンプ力"), SerializeField]
    float _playerJumpSpeed = 3.0f;

    [Tooltip("地面のレイヤー"), SerializeField]
    LayerMask _groundLayer;

    [Tooltip("設置判定を可視化するかどうか")]
    bool _isGroundDebug = true;

    [Tooltip("設置判定のサイズ"), SerializeField]
    Vector3 _groundCollisionSize;

    [Tooltip("プレイヤーの中心点"), SerializeField]
    Vector3 _playerCentor;

    [Tooltip("地面にいるときの重力"), SerializeField]
    float _groundDrag = 0f;

    [Tooltip("空中にいるときの重力"), SerializeField]
    float _airDrag = 0f;

    bool _isMove;

    Vector3 _centor;

    Rigidbody _rb;

    SoundPlayer _soundPlayer;

    void Start()
    {
        SetUp();
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
    }

    void Update()
    {
        PlayerState();
        ControlDrag();
        PlayerJump();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    /// <summary>
    /// Playerの移動方法を決定するステート
    /// </summary>
    void PlayerMove()
    {
        if (_rb.velocity.magnitude <= _maximizePlayerSpeed)
        {
            Vector3 dir = PlayerVec(InputUtility.GetDirectionMove);
            _rb.AddForce(_playerSpeedMultiply * (dir - _rb.velocity));
        }

        if (_isMove)
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
        vec *= _playerSpeed;
        vec.y = _rb.velocity.y;
        vec = transform.TransformDirection(vec);
        return vec;
    }

    /// <summary>
    /// 重力管理
    /// </summary>
    void ControlDrag()
    {
        if (IsGround())
        {
            _rb.drag = _groundDrag;
        }
        else
        {
            _rb.drag = _airDrag;
        }
    }

    /// <summary>
    /// プレイヤーがジャンプする機能
    /// 改良予定
    /// </summary>
    void PlayerJump()
    {
        if (InputUtility.GetDownJump)
        {
            _rb.AddForce(Vector3.up * _playerJumpSpeed, ForceMode.Impulse);
            _soundPlayer.PlaySound("SE_jump1");
        }
    }

    /// <summary>
    /// Updateで回すPlayerのState
    /// </summary>
    void PlayerState()
    {
        _centor = transform.position + _playerCentor;

        if (InputUtility.GetDirectionMove == Vector2.zero || !IsGround())
        {
            _isMove = false;
        }
        else
        {
            _isMove = true;
        }
    }

    /// <summary>
    /// 設置判定
    /// </summary>
    /// <returns>設置しているかどうか</returns>
    bool IsGround()
    {
        Collider[] collision = Physics.OverlapBox(_centor, _groundCollisionSize, Quaternion.identity, _groundLayer);
        if (collision.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Layer判定のGizmo表示
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(_isGroundDebug)
        {
            Gizmos.DrawCube(_centor, _groundCollisionSize);
        }
    }
}
