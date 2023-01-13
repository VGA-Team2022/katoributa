using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertController : MonoBehaviour
{
    /// <summary>�����̎c�ʊǗ��R���|�[�l���g</summary>
    SenkouHealth _senkouHealth = null;

    [SerializeField, Tooltip("BGM�Ƃ��ďo���A���[�g�pSoundPlayer")]
    SoundPlayer _bgmSpeaker = null;

    [SerializeField, Tooltip("�����������[�h�̎��ɗ����T�E���h��ID")]
    int _soundIdMeramera = 4;

    [SerializeField, Tooltip("���������Ȃ����ɗ����T�E���h��ID")]
    int _soundIdSenkoAlert = 5;

    /// <summary>true : �����������[�h��</summary>
    bool _isPowerUp = false;

    /// <summary>�ڐG����R���|�[�l���g</summary>
    Durability _durability = null;

    [Space]
    [SerializeField, Tooltip("SE�Ƃ��ďo���A���[�g�pSoundPlayer")]
    SoundPlayer _seSpeaker = null;

    [SerializeField, Tooltip("���C�t�����Ȃ����ɗ����T�E���h��ID")]
    int _soundIdLifeAlert = 27;

    // Start is called before the first frame update
    void Start()
    {
        _durability = FindObjectOfType<Durability>();
        _senkouHealth = FindObjectOfType<SenkouHealth>();

        _bgmSpeaker?.StopSound();
        _seSpeaker?.StopSound();
    }

    // Update is called once per frame
    void Update()
    {
        if(_senkouHealth && _senkouHealth.Health.Value < 10f)
        {
            _bgmSpeaker?.PlaySound(_soundIdSenkoAlert);
        }
        else if (_isPowerUp)
        {
            _bgmSpeaker?.PlaySound(_soundIdMeramera);
        }
        else
        {
            _bgmSpeaker?.StopSound();
        }

        if(_durability && _durability.HP.Value < 2)
        {
            _seSpeaker?.PlaySound(_soundIdLifeAlert);
        }
        else
        {
            _seSpeaker?.StopSound();
        }
    }

    private void EnabledPowerUp()
    {
        _isPowerUp = true;
    }

    private void DisabledPowerUp()
    {
        _isPowerUp = false;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPowerUpEvent += EnabledPowerUp;
        GameManager.Instance.OnPowerDownEvent += DisabledPowerUp;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPowerUpEvent -= EnabledPowerUp;
        GameManager.Instance.OnPowerDownEvent -= DisabledPowerUp;
    }
}
