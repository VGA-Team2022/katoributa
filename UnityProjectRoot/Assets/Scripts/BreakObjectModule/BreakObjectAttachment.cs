using UnityEngine;

public class BreakObjectAttachment : MonoBehaviour
{
    [SerializeField] Transform _parentTransform;
    void Update()
    {
        transform.position = _parentTransform.position;
    }
}
