using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Tooltip("���W�b�h�{�f�B�R���|�[�l���g")]
    Rigidbody _rb;

    Animator _anim;

    [Tooltip("�����Ă��邩�ǂ���")]
    bool _isMove;

    [Tooltip("�n�ʂɂ���Ƃ��̏d��"), SerializeField]
    float _groundDrag = 0f;

    [Tooltip("�󒆂ɂ���Ƃ��̏d��"), SerializeField]
    float _airDrag = 0f;

    [Tooltip("�v���C���[�̒��S�_"), SerializeField]
    Vector3 _playerCentor;

    [Tooltip("�n�ʂ̃��C���["), SerializeField]
    LayerMask[] _groundLayers;

    [Tooltip("���ꂼ��̒��n��"), SerializeField]
    int[] _landingSoundIds;

    [Tooltip("����̃v���C���[�̃��[�h")]
    int _currentPlayerMode;

    [Tooltip("���S")]
    Vector3 _centor;

    [Tooltip("�ݒu������������邩�ǂ���")]
    bool _isGroundDebug = true;

    [Tooltip("�ݒu����̃T�C�Y"), SerializeField]
    Vector3 _groundCollisionSize;

    [Tooltip("�T�E���h�v���C���[�R���|�[�l���g")]
    SoundPlayer _soundPlayer;

    bool _isLanding;

    float _playerSpeedSqrMagnitude;

    [SerializeField] float _multiplierGravity = 1.0f;

    public bool IsMove => _isMove;

    public float PlayerSpeedSqrMagnitude => _playerSpeedSqrMagnitude;
    private void Start()
    {
        SetUp();
    }

    void Update()
    {
        if (GameManager.Instance.GameState == GameState.GameFinish) return;
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
    /// Update�ŉ�Player��State
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
    /// PowerUp�{�^�����������R���g���[���[
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
            _rb.AddForce((_multiplierGravity - 1f) * Physics.gravity, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// �ݒu����
    /// </summary>
    /// <returns>�ݒu���Ă��邩�ǂ���</returns>
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
    /// Layer�����Gizmo�\��
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
