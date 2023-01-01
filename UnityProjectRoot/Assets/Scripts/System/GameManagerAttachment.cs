using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAttachment : MonoBehaviour
{
    #region 変数
    [SerializeField] Image _gameOverPanel;
    [SerializeField] Image _gameClearPanel;

    [SerializeField] float _comboTime = 3f;
    [SerializeField] float _multiplicationLimit = 2f;

    #endregion

    #region プロパティ
    public Image GameOverPanel => _gameOverPanel;
    public Image GameClearPanel => _gameClearPanel;
    public float ComboTime => _comboTime;
    public float MultiplicationLimit => _multiplicationLimit;
    #endregion

    #region デリゲート
    public delegate void MonoEvent();
    MonoEvent _updateEvent;
    #endregion

    private void Awake()
    {
        if (_gameOverPanel)
            _gameOverPanel.gameObject.SetActive(false);

        if (_gameClearPanel)
            _gameClearPanel.gameObject.SetActive(false);

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
}
