using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAttachment : MonoBehaviour
{
    #region 変数
    [SerializeField] float _comboTime = 3f;
    [SerializeField] float _multiplicationLimit = 2f;
    [Space(10)]
    [SerializeField] Image _countDownImage;
    [SerializeField] Sprite[] _countSprites;
    [Space(10)]
    [SerializeField] SoundPlayer _sound;
    [SerializeField] int _soundCue = 35;

    #endregion

    #region プロパティ
    public float ComboTime => _comboTime;
    public float MultiplicationLimit => _multiplicationLimit;
    public Image CountImage => _countDownImage;
    public Sprite[] CountSprites => _countSprites;
    #endregion

    #region デリゲート
    public delegate void MonoEvent();
    MonoEvent _updateEvent;
    #endregion

    private void Awake()
    {
        GameManager.Instance.SetupUpdateCallback(this);
        GameManager.Instance.OnSetup(this);
    }
    private void Update()
    {
        _updateEvent?.Invoke();
    }

    /// <summary>
    /// Updateで呼びたい処理を登録しておく
    /// </summary>
    public void SetupCallBack(MonoEvent e)
    {
        _updateEvent = e;
    }
    private void OnEnable()
    {
        GameManager.Instance.OnGameEndEvent += OnTimeUp;
        GameManager.Instance.OnGameOverEvent += OnTimeUp;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameEndEvent -= OnTimeUp;
        GameManager.Instance.OnGameOverEvent -= OnTimeUp;
    }
    void OnTimeUp()
    {
        if(_sound)
        {
            _sound.PlaySound(_soundCue);
        }
    }
}
