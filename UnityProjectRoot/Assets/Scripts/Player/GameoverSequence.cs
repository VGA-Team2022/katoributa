using TK.Rendering.PostFX;
using UnityEngine;
using UnityEngine.Rendering;

public class GameoverSequence : PopUpUIBase
{
    [SerializeField, Tooltip("�X���[���o�̑��x�{��")]
    float _slowTimeScale = 0.2f;

    /// <summary>����TimeScale��ێ�����ϐ�</summary>
    float _timeScaleCash = 1f;

    [SerializeField, Tooltip("�����肽��PostProcessing")]
    Volume _post = null;

    /// <summary>�F�����]PostProcessing�@�\</summary>
    ReverseColor _rc = null;


    // Start is called before the first frame update
    void Start()
    {
        _post?.profile.TryGet(out _rc);
    }

    void OnEnable()
    {
        GameManager.Instance.OnGameOverEvent += SlowAndInvertion;
        GameManager.Instance.OnGameOverEvent += PopUpAnimation;
    }
    void OnDisable()
    {
        GameManager.Instance.OnGameOverEvent -= SlowAndInvertion;
        GameManager.Instance.OnGameOverEvent -= PopUpAnimation;
    }

    /// <summary>��ʂ��l�K�|�W���]���Ă������ɂȂ�V�[�P���X</summary>
    void SlowAndInvertion()
    {
        _rc?.isActivation.Override(true);
        _timeScaleCash = Time.timeScale;
        Time.timeScale = _slowTimeScale;
    }

    void OnDestroy()
    {
        Time.timeScale = _timeScaleCash;
    }
}
