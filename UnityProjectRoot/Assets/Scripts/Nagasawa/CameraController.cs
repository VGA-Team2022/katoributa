using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] TestCameraPattern _cameraPattern;

    /// <summary>
    /// Test用のカメラのパターン
    /// プランナーさんにその挙動がいいか最終的に決定していただきます
    /// </summary>
    enum TestCameraPattern
    {
        Pattern1,
        Pattern2,
    }
}
