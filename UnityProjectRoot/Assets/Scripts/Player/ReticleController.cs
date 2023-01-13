using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    [SerializeField] Transform _muzzleTransform;

    //[SerializeField] RectTransform _reticleRectTrans;
    //[SerializeField] RectTransform _uiCanvasRectTrans;
    [SerializeField] float _length = 50;
    [SerializeField] Transform _reticleTransform;
    [SerializeField] LayerMask _onReticle;
    //[SerializeField] Camera _camera;

    void Awake()
    {
        _muzzleTransform = transform;
    }

    void Update()
    {
        Debug.DrawRay(_muzzleTransform.position, _muzzleTransform.forward * _length, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(_muzzleTransform.position + new Vector3(0, 0.741f, 0), _muzzleTransform.forward, out hit, _length, _onReticle))
        {
            _reticleTransform.position = hit.point;
            _reticleTransform.rotation = _muzzleTransform.rotation;
            //_reticleRectTrans.position = hit.point;
            //var targetScreenPos = _camera.WorldToScreenPoint(_reticleRectTrans.position);
            //Vector2 pos;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(
            //_reticleRectTrans,
            //targetScreenPos,
            //null,
            //out pos
            //);
            //_reticleRectTrans.localPosition = pos;
        }
    }
}
