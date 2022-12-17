using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMove : MonoBehaviour
{
    [Header("�ʏ펞")]
    [Tooltip("�v���C���[�̃X�s�[�h"), SerializeField]
    float _playerSpeed = 20.0f;

    [Tooltip("�v���C���[�̈ړ����͂ɑ΂���Ǐ]�x�APlayerSpeed�ɏ���"), SerializeField]
    float _playerSpeedMultiply = 1.0f;

    [Tooltip("�X�s�[�h�̏��"), SerializeField]
    float _maximizePlayerSpeed = 5.0f;

    [Tooltip("���W�b�h�{�f�B")]
    Rigidbody _rb;

    Animator _anim;

    [Tooltip("���݂̍ő�X�s�[�h")]
    float _currentMaximizeSpeed;

    [Tooltip("���݂̃X�s�[�h")]
    float _currentSpeed;

    [Tooltip("�T�E���h�v���C���[�R���|�[�l���g")]
    SoundPlayer _soundPlayer;

    [Header("����������")]
    [Tooltip("�����������̃X�s�[�h"), SerializeField]
    float _merameraPlayerSpeed = 20.0f;

    [Tooltip("�����������̍ő�X�s�[�h"), SerializeField]
    float _merameraPlayerMaximizeSpeed = 5.0f;

    [Tooltip("�v���C���[�X�e�[�g�R���|�[�l���g")]
    PlayerState _playerState;

    [Header("�T�E���h")]
    [SerializeField]
    int _walkSoundId = 2;

    [Header("�}�e���A��")]
    [SerializeField]
    Material _normalMaterial;

    [SerializeField]
    Material _merameraMaterial;

    [Space(5)]
    [SerializeField]
    SkinnedMeshRenderer _skinnedMesh;

    [SerializeField] VisualEffect _effect;

    bool _isPowerUp;

    void Start()
    {
        SetUp();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameState.GameFinish) return;
        PlayerMoveMethod();
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

        _anim = GetComponent<Animator>();
        _soundPlayer = GetComponent<SoundPlayer>();
        _playerState = GetComponent<PlayerState>();
        PlayerMovePowerDown();
    }

    void OnEnable()
    {
        GameManager.Instance.OnPowerUpEvent += PlayerMovePowerUp;
        GameManager.Instance.OnPowerDownEvent += PlayerMovePowerDown;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPowerUpEvent -= PlayerMovePowerUp;
        GameManager.Instance.OnPowerDownEvent -= PlayerMovePowerDown;
    }

    /// <summary>
    /// Player�̈ړ����@�����肷��X�e�[�g
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

        //�A�j���[�V�����̃p�����[�^�ɒl��ݒ�
        _anim.SetFloat("Speed", _playerState.PlayerSpeedSqrMagnitude);
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
    /// �������̉����Đ�����
    /// </summary>
    public void EmitWalkSound()
    {
        if (_playerState.IsMove)
        {
            _soundPlayer.PlaySound(_walkSoundId);
        }
    }
}
