using UnityEngine;

/// <summary>
/// �v���C���[�𓮂����R���|�[�l���g
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Tooltip("�v���C���[�̃X�s�[�h"), SerializeField]
    float _playerSpeed = 5.0f;

    [Tooltip("�v���C���[�̈ړ����͂ɑ΂���Ǐ]�x�APlayerSpeed�ɏ���"), SerializeField]
    float _playerSpeedMultiply = 5.0f;

    [Tooltip("�X�s�[�h�̏��"), SerializeField]
    float _maximizePlayerSpeed = 5.0f;

    [Tooltip("�W�����v��"), SerializeField]
    float _playerJumpSpeed = 3.0f;

    [Tooltip("�n�ʂ̃��C���["), SerializeField]
    LayerMask _groundLayer;

    [Tooltip("�ݒu������������邩�ǂ���")]
    bool _isGroundDebug = true;

    [Tooltip("�ݒu����̃T�C�Y"), SerializeField]
    Vector3 _groundCollisionSize;

    [Tooltip("�v���C���[�̒��S�_"), SerializeField]
    Vector3 _playerCentor;

    [Tooltip("�n�ʂɂ���Ƃ��̏d��"), SerializeField]
    float _groundDrag = 0f;

    [Tooltip("�󒆂ɂ���Ƃ��̏d��"), SerializeField]
    float _airDrag = 0f;

    bool _isMove;

    Vector3 _centor;

    Rigidbody _rb;

    SoundPlayer _soundPlayer;

    [SerializeField]
    float _merameraPlayerSpeed = 10.0f;

    [SerializeField]
    float _merameraPlayerMaximizeSpeed = 10.0f;

    float _currentSpeed;

    float _currentMaximizeSpeed;

    int _currentPlayerMode;

    //�ꎞ�I�ɒǉ�
    float _timer;

    void Start()
    {
        SetUp();
    }

    /// <summary>
    /// Start�ōs�������Z�b�g�A�b�v
    /// </summary>
    void SetUp()
    {
        if (!TryGetComponent(out _rb))
        {
            _rb = gameObject.AddComponent<Rigidbody>();
        }
        _soundPlayer = GetComponent<SoundPlayer>();
        PlayerPowerDown();
        GameManager.Instance.OnPowerUpEvent += PlayerPowerUp;
        GameManager.Instance.OnPowerDownEvent += PlayerPowerDown;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnPowerUpEvent -= PlayerPowerUp;
        GameManager.Instance.OnPowerDownEvent -= PlayerPowerDown;
    }

    void Update()
    {
        PlayerState();
        ControlDrag();
        PlayerJump();
        PlayerPowerUpControll();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    /// <summary>
    /// Player�̈ړ����@�����肷��X�e�[�g
    /// </summary>
    void PlayerMove()
    {
        if (_rb.velocity.magnitude <= _currentMaximizeSpeed)
        {
            Vector3 dir = PlayerVec(InputUtility.GetDirectionMove);
            _rb.AddForce(_playerSpeedMultiply * (dir - _rb.velocity));
        }

        if (_isMove)
        {
            _timer += Time.fixedDeltaTime;

            if (_timer >= 0.4f)
            {
                _timer = 0;
                _soundPlayer.PlaySound("SE_walk wood 3");
            }
        }
    }

    /// <summary>
    /// �i�s���������肷��֐�
    /// </summary>
    /// <param name="inputVec">���͕�����Vector2��</param>
    /// <returns>�i�s����</returns>
    Vector3 PlayerVec(Vector2 inputVec)
    {
        Vector3 vec = new Vector3(inputVec.x, 0, inputVec.y);
        vec.Normalize();
        vec *= _currentSpeed;
        vec.y = _rb.velocity.y;
        vec = transform.TransformDirection(vec);
        return vec;
    }

    /// <summary>
    /// �d�͊Ǘ�
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
    /// �v���C���[���W�����v����@�\
    /// ���Ǘ\��
    /// </summary>
    void PlayerJump()
    {
        if (InputUtility.GetDownJump && IsGround())
        {
            _rb.AddForce(Vector3.up * _playerJumpSpeed, ForceMode.Impulse);
            _soundPlayer.PlaySound("SE_jump1");
        }
    }

    void PlayerPowerUpControll()
    {
        if(InputUtility.GetDownActionSwitch)
        {
            _currentPlayerMode = _currentPlayerMode++ % 2;
            GameManager.Instance.PlayerModeChange((PlayerMode)_currentPlayerMode);
        }
    }

    /// <summary>
    /// Update�ŉ�Player��State
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
    /// �ݒu����
    /// </summary>
    /// <returns>�ݒu���Ă��邩�ǂ���</returns>
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
    /// Layer�����Gizmo�\��
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(_isGroundDebug)
        {
            Gizmos.DrawCube(_centor, _groundCollisionSize);
        }
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
