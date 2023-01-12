using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(SceneChanger))]
public class Fade : MonoBehaviour
{
    [SerializeField] Image _fadePanel;
    [SerializeField] float _fadeSpeed = 0.5f;
    [SerializeField] bool _isFadeOut;

    const float _fadeIn = 1;
    const float _fadeOut = 0;
    SceneChanger _changer;

    private void Awake()
    {
        _changer = GetComponent<SceneChanger>();
    }
    private void Start()
    {
        OnFadeOut();
    }

    public void OnFadeIn(string sceneName)
    {
        if (_fadePanel)
        {
            AlphaReset(0);
            _fadePanel.DOFade(_fadeIn, _fadeSpeed)
                .OnComplete(() =>
                {
                    _changer.ChangeScene(sceneName);
                });
            _fadePanel.raycastTarget = true;
        }
        else
        {
            Debug.LogError($"Panelがセットされていません");
        }
    }
    void OnFadeOut()
    {
        if (!_isFadeOut) return;

        if (_fadePanel)
        {
            AlphaReset(1);
            _fadePanel.DOFade(_fadeOut, _fadeSpeed);
            _fadePanel.raycastTarget = false;
        }
        else
        {
            Debug.LogError($"Panelがセットされていません");
        }
    }
    void AlphaReset(float alpha)
    {
        var c = _fadePanel.color;
        c.a = alpha;
        _fadePanel.color = c;
    }
}
