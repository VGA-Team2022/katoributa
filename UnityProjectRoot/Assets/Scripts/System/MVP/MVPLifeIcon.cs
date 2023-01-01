using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MVPLifeIcon : MonoBehaviour
{
    [SerializeField] Sprite _lifeImage;
    [SerializeField] RectTransform _parent;
    [SerializeField] Vector2 _spriteSize;

    Transform _thisTransform;

    private void Start()
    {
        _thisTransform = GetComponent<RectTransform>();
    }

    public void UpdateUI(int num)
    {
        //àÍíUè¡Ç∑
        foreach (Transform t in _parent)
        {
            Destroy(t.gameObject);
        }
        //UIÇÃçXêV
        for (int i = 1; i < num + 1; i++)
        {
            var go = new GameObject();
            var image = go.AddComponent<Image>();

            image.sprite = _lifeImage;

            var r = go.GetComponent<RectTransform>();
            r.sizeDelta = _spriteSize;

            go.transform.SetParent(_parent);
        }
    }
}
