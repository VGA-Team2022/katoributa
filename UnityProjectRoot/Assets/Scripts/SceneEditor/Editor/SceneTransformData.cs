using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK.SceneEditorTool
{
    public class SceneTransformData : ScriptableObject
    {
        public List<SceneObjData> sceneData = new List<SceneObjData>();
        [System.Serializable]
        public class SceneObjData
        {
            //public GameObject _object;
            public string _objectName;
            public string _pass;
            public Vector3 _position;
            public Quaternion _rotation;
            public Vector3 _scale;
        }
    }
}
