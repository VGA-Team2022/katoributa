using UnityEngine;

public class AuraAttachment : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;

    void Update()
    {
        transform.eulerAngles = new Vector3(-_playerTransform.rotation.x, 0, 0);
    }
}
