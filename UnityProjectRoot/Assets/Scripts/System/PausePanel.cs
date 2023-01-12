using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PausePanel : MonoBehaviour
{
    [SerializeField] float _animSpeed = 0.5f;
    [SerializeField] Ease _ease;
    RectTransform _transform;
    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _transform.localScale = Vector3.zero;
    }
    private void OnEnable()
    {
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnResume += Resume;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnPause -= Pause;
        GameManager.Instance.OnResume -= Resume;
    }
    void Pause()
    {
        OnAnimation(true);
    }
    void Resume()
    {
        OnAnimation(false);
    }
    void OnAnimation(bool flag)
    {
        var scale = Vector3.one;

        if (!flag)
            scale = Vector3.zero;

        _transform.DOScale(scale, _animSpeed)
            .SetEase(_ease);
    }
}
