using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    GameObject cameraObj;
    void Start()
    {
        cameraObj = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraObj.transform.position);
    }
}