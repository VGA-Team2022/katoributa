using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertController : MonoBehaviour
{
    /// <summary>線香の残量管理コンポーネント</summary>
    SenkouHealth _senkouHealth = null;

    [SerializeField, Tooltip("BGMとして出すアラート用SoundPlayer")]
    SoundPlayer _bgmSpeaker = null;

    [SerializeField, Tooltip("メラメラモードの時に流すサウンドのID")]
    int _soundIdMeramera = 4;

    [SerializeField, Tooltip("線香が少ない時に流すサウンドのID")]
    int _soundIdSenkoAlert = 5;

    /// <summary>true : メラメラモード中</summary>
    bool _isPowerUp = false;

    /// <summary>接触判定コンポーネント</summary>
    Durability _durability = null;

    [Space]
    [SerializeField, Tooltip("SEとして出すアラート用SoundPlayer")]
    SoundPlayer _seSpeaker = null;

    [SerializeField, Tooltip("ライフが少ない時に流すサウンドのID")]
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
