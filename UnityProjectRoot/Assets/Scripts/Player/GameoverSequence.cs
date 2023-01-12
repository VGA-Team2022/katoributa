using TK.Rendering.PostFX;
using UnityEngine;
using UnityEngine.Rendering;

public class GameoverSequence : PopUpUIBase
{
    [SerializeField, Tooltip("スロー演出の速度倍率")]
    float _slowTimeScale = 0.2f;

    /// <summary>元のTimeScaleを保持する変数</summary>
    float _timeScaleCash = 1f;

    [SerializeField, Tooltip("いじりたいPostProcessing")]
    Volume _post = null;

    /// <summary>色味反転PostProcessing機能</summary>
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

    /// <summary>画面がネガポジ反転してゆっくりになるシーケンス</summary>
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
