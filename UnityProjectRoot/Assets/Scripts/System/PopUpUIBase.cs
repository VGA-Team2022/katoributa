using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Fade))]
public class PopUpUIBase : MonoBehaviour
{
    [SerializeField] float _animSpeed = 0.1f;
    [SerializeField] Ease _ease;
    [SerializeField] float _changeSceneTime = 1f;
    [SerializeField] string _nextScene;

    RectTransform _transform;
    Fade _fade;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _transform.localScale = Vector3.zero;
        _fade = GetComponent<Fade>();
        OnAwake();
    }
    protected virtual void OnAwake() { }
    protected void PopUpAnimation()
    {
        _transform.DOScale(Vector3.one, _animSpeed)
            .SetEase(_ease)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(_changeSceneTime, () => ChangeScene(), false);
            });
    }
    void ChangeScene() 
    {
        _fade.OnFadeIn(_nextScene);
    }
}
