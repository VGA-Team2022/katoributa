using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("移動パターンの制御 Velocityはかくかく,AddForceはスーッと"), SerializeField] 
    PlayerMovePatternTest _movePattern;

    [Tooltip("プレイヤーのスピード"), SerializeField]
    float _playerSpeed = 5.0f;

    [Tooltip("プレイヤーの移動入力に対する追従度、PlayerSpeedに準拠 AddForceのみ"), SerializeField]
    float _playerSpeedMultiply = 5.0f;

    [Tooltip("ジャンプ力"), SerializeField]
    float _playerJumpSpeed = 3.0f;

    [Tooltip("地面のレイヤー"), SerializeField]
    LayerMask _groundLayer;

    [Tooltip("設置判定のサイズ"), SerializeField]
    Vector3 _groundCollisionSize;

    [Tooltip("プレイヤーの中心点"), SerializeField]
    Vector3 _playerCentor;

    [Tooltip("地面にいるときの重力"), SerializeField]
    float _groundDrag = 0f;

    [Tooltip("空中にいるときの重力"), SerializeField]
    float _airDrag = 0f;

    Vector3 _centor;
    Rigidbody _rb;

    void Start()
    {
        SetUp();
    }

    /// <summary>
    /// Startで行う初期セットアップ
    /// </summary>
    void SetUp()
    {
        if(!TryGetComponent(out _rb))
        {
            _rb =  gameObject.AddComponent<Rigidbody>();
        }

        //ContainsのRotationX,Zを固定
        _rb.constraints = (RigidbodyConstraints)80;
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
        switch (_movePattern)
        {
            case PlayerMovePatternTest.Velocity:
                VelocityMove();
                break;
            case PlayerMovePatternTest.AddForce:
                AddForceMove();
                break;
            default:
                Debug.LogError("移動パターンを指定してください");
                break;
        }
    }

    /// <summary>
    /// Rigidbodyのvelocityを使って移動をする。
    /// かくかくした動き
    /// </summary>
    void VelocityMove()
    {
        Vector3 dir = PlayerVec(InputUtility.GetDirectionMove);
        _rb.velocity = dir;
    }

    /// <summary>
    /// RigidbodyのAddForceを使い移動する。
    /// スーッと動いて、スーッと止まる。追従度設定可能。
    /// </summary>
    void AddForceMove()
    {
        Vector3 dir = PlayerVec(InputUtility.GetDirectionMove);
        _rb.AddForce(_playerSpeedMultiply * (dir - _rb.velocity));
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
    /// 
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
    /// </summary>
    void PlayerJump()
    {
        if(InputUtility.GetDownJump)
        {
            _rb.AddForce(Vector3.up * _playerJumpSpeed, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Updateで回すPlayerのState
    /// </summary>
    void PlayerState()
    {
        _centor = transform.position + _playerCentor;
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
    /// プランナーさんにテストしていただくための列挙
    /// </summary>
    enum PlayerMovePatternTest
    {
        Velocity,
        AddForce,
    }
}
