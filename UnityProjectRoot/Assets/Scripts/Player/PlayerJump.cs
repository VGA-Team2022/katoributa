using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("�ʏ펞")]
    [Tooltip("�W�����v��"), SerializeField]
    float _playerJumpSpeed = 8.0f;

    [Header("����������")]
    [SerializeField]
    float _merameraJumpSpeed = 8f;

    [Header("�T�E���h")]
    [SerializeField]
    int _jumpSoundId = 1;

    [Tooltip("�v���C���[�X�e�[�g�R���|�[�l���g")]
    PlayerState _playerState;

    [Tooltip("���W�b�h�{�f�B�R���|�[�l���g")]
    Rigidbody _rb;

    [Tooltip("�v���C���[�̉��R���|�[�l���g")]
    SoundPlayer _soundPlayer;

    float _currentJumpSpeed;

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
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPowerUpEvent -= PlayerJumpPowerUp;
        GameManager.Instance.OnPowerDownEvent -= PlayerJumpPowerDown;
    }

    void Update()
    {
        PlayerJumpMethod();
    }

    /// <summary>
    /// �v���C���[���W�����v����@�\
    /// ���Ǘ\��
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
}
