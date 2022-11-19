using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAttachment : MonoBehaviour
{
    #region •Ï”
    [SerializeField, Tooltip("“|‚³‚È‚¢‚Æ‚¢‚¯‚È‚¢“G‚Ìƒmƒ‹ƒ}(ŒÂ)")] int _quota = 10;
    [Space(10)]
    [SerializeField] Image _gameOverPanel;
    [SerializeField] Image _gameClearPanel;
    #endregion

    #region ƒvƒƒpƒeƒB
    /// <summary>
    /// “G‚Ìƒmƒ‹ƒ}
    /// </summary>
    public int Quota => _quota;

    public Image GameOverPanel => _gameOverPanel;
    public Image GameClearPanel => _gameClearPanel;
    #endregion

    #region ƒfƒŠƒQ[ƒg
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
    /// Update‚ÅŒÄ‚Ñ‚½‚¢ˆ—‚ğ“o˜^‚µ‚Ä‚¨‚­
    /// </summary>
    public void SetupCallBack(MonoEvent e)
    {
        _updateEvent = e;
    }
}
