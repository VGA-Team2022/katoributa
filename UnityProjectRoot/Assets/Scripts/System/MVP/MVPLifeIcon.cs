using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MVPLifeIcon : MonoBehaviour
{
    [SerializeField] Sprite _lifeImage;
    [SerializeField] Sprite _backImage;
    [SerializeField] RectTransform _lifeParent;
    [SerializeField] RectTransform _backParent;
    [SerializeField] Vector2 _spriteSize;

    Transform _thisTransform;

    private void Start()
    {
        _thisTransform = GetComponent<RectTransform>();
    }

    public void UpdateUI(int num)
    {
        //àÍíUè¡Ç∑
        foreach (Transform t in _lifeParent)
        {
            Destroy(t.gameObject);
        }
        //UIÇÃçXêV
        for (int i = 1; i < num + 1; i++)
        {
            OnSetting(_lifeImage, _lifeParent);
        }
    }
    public void Setup(int num)
    {
        for (int i = 1; i < num + 1; i++)
        {
            OnSetting(_backImage, _backParent);
        }
    }

    private void OnSetting(Sprite sp, RectTransform rect)
    {
        var go = new GameObject();
        var image = go.AddComponent<Image>();

        image.sprite = sp;

        var r = go.GetComponent<RectTransform>();
        r.sizeDelta = _spriteSize;

        go.transform.SetParent(rect);
    }
}
