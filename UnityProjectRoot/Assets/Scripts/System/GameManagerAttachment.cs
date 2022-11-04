using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAttachment : MonoBehaviour
{
    #region •Ï”
    [SerializeField, Tooltip("“|‚³‚È‚¢‚Æ‚¢‚¯‚È‚¢“G‚Ìƒmƒ‹ƒ}(ŒÂ)")] int _quota = 10;
    #endregion

    #region ƒvƒƒpƒeƒB
    /// <summary>
    /// “G‚Ìƒmƒ‹ƒ}
    /// </summary>
    public int Quota => _quota;
    #endregion

    #region ƒfƒŠƒQ[ƒg
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
    /// Update‚ÅŒÄ‚Ñ‚½‚¢ˆ—‚ğ“o˜^‚µ‚Ä‚¨‚­
    /// </summary>
    public void SetupCallBack(MonoEvent e)
    {
        _updateEvent = e;
    }
}
