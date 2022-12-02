using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Tooltip("リジッドボディコンポーネント")]
    Rigidbody _rb;

    Animator _anim;

    [Tooltip("動いているかどうか")]
    bool _isMove;

    [Tooltip("地面にいるときの重力"), SerializeField]
    float _groundDrag = 0f;

    [Tooltip("空中にいるときの重力"), SerializeField]
    float _airDrag = 0f;

    [Tooltip("プレイヤーの中心点"), SerializeField]
    Vector3 _playerCentor;

    [Tooltip("地面のレイヤー"), SerializeField]
    LayerMask[] _groundLayers;

    [Tooltip("それぞれの着地音")]
    int[] _landingSoundIds;

    [Tooltip("現状のプレイヤーのモード")]
    int _currentPlayerMode;

    [Tooltip("中心")]
    Vector3 _centor;

    [Tooltip("設置判定を可視化するかどうか")]
    bool _isGroundDebug = true;

    [Tooltip("設置判定のサイズ"), SerializeField]
    Vector3 _groundCollisionSize;

    [Tooltip("サウンドプレイヤーコンポーネント")]
    SoundPlayer _soundPlayer;

    bool _isLanding;

    float _playerSpeedSqrMagnitude;

    public bool IsMove => _isMove;

    public float PlayerSpeedSqrMagnitude => _playerSpeedSqrMagnitude;
    private void Start()
    {
        SetUp();
    }

    void Update()
    {
        PlayerStateMethod();
        ControlDrag();
        PlayerPowerUpControll();
    }

    void SetUp()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _soundPlayer = GetComponent<SoundPlayer>();
    }

    /// <summary>
    /// Updateで回すPlayerのState
    /// </summary>
    void PlayerStateMethod()
    {
        _centor = transform.position + _playerCentor;
        _playerSpeedSqrMagnitude = Vector3.ProjectOnPlane(_rb.velocity, Vector3.up).sqrMagnitude;

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
    /// PowerUpボタンを押されるコントローラー
    /// </summary>
    void PlayerPowerUpControll()
    {
        if (InputUtility.GetDownActionSwitch)
        {
            _currentPlayerMode++;
            _currentPlayerMode %= 2;
            GameManager.Instance.PlayerModeChange((PlayerMode)_currentPlayerMode);
        }
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
    /// 設置判定
    /// </summary>
    /// <returns>設置しているかどうか</returns>
    public bool IsGround()
    {
        bool hit = false;

        for(int i = 0; i < _groundLayers.Length; i++)
        {
            Collider[] collision = Physics.OverlapBox(_centor, _groundCollisionSize, Quaternion.identity, _groundLayers[i]);
            if (collision.Length != 0)
            {
                hit = true;
                if(!_isLanding)
                {
                    _isLanding = true;
                    _soundPlayer.PlaySound(_landingSoundIds[i]);
                }
            }
        }

        if (!hit)
        {
            _isLanding = false;
        }
        _anim.SetBool("IsGround", hit);
        return hit;
    }


    /// <summary>
    /// Layer判定のGizmo表示
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isGroundDebug)
        {
            Gizmos.DrawCube(_centor, _groundCollisionSize);
        }
    }
}
