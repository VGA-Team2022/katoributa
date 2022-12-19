using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace TK.SceneEditorTool
{
#if UNITY_EDITOR
    /// <summary>
    /// Directory クラスに関する汎用関数を管理するクラス
    /// </summary>
    public static class DirectoryUtils
    {
        /// <summary>
        /// 指定したパスにディレクトリが存在しない場合
        /// すべてのディレクトリとサブディレクトリを作成します
        /// </summary>
        public static DirectoryInfo SafeCreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return null;
            }
            return Directory.CreateDirectory(path);
        }
    }
    public class SceneSaveEditor : EditorWindow
    {
        private GUIStyle _style;
        private string _text;
        private string current = "";

        [MenuItem("SceneDataEditor/Save")]
        private static void Create()
        {
            var window = (SceneSaveEditor)GetWindow(typeof(SceneSaveEditor));
            window.Show();
        }

        private void Awake()
        {
            _style = new GUIStyle();
            _style.wordWrap = true;

            _text = "シーンをデータ化します。";
        }

        private void OnGUI()
        {
            string pass = $"Assets/SceneJoint/Resources/";
            EditorGUILayout.BeginVertical();

            GUILayout.Label("保存する名前を入力してください。（英語のみ）");

            Input.imeCompositionMode = IMECompositionMode.On;


            //文字入力できる領域を作る（改行可能）
            current = GUILayout.TextArea(current, GUILayout.Height(50));

            if (GUILayout.Button("シーンのデータ保存"))
            {
                if (current == "")
                {
                    _text = "No name";
                    return;
                }

                DirectoryUtils.SafeCreateDirectory(pass);

                SceneDataSave(pass, current);

                _text = "complete save";
            }
            EditorGUILayout.LabelField(_text, _style);
            EditorGUILayout.EndVertical();
        }
        private void SceneDataSave(string pass, string name)
        {
            string dPass = pass + name + ".asset";

            SceneTransformData data = ScriptableObject.CreateInstance<SceneTransformData>();

            AssetDatabase.DeleteAsset(dPass);
            AssetDatabase.CreateAsset((ScriptableObject)data, dPass);

            List<GameObject> goData =  GetAllObjectList();
            foreach (var item in goData)
            {
                SceneTransformData.SceneObjData sceneObj = new SceneTransformData.SceneObjData();
                if (item is GameObject)              
                sceneObj._objectName = item.name;
                if (item.gameObject.transform.parent != null)
                {

                    sceneObj._pass = item.transform.parent.gameObject.name;
                }
                else
                {
                    sceneObj._pass = "";
                }
                sceneObj._position = item.transform.position;
                sceneObj._rotation = item.transform.rotation;
                sceneObj._scale = item.transform.localScale;
                data.sceneData.Add(sceneObj);
            }
            ScriptableObject _scObj = AssetDatabase.LoadAssetAtPath(dPass, typeof(ScriptableObject)) as ScriptableObject;
            EditorUtility.SetDirty(_scObj);
            Debug.Log(dPass + "生成");
        }
        private List<GameObject> GetAllObjectList()
        {
            List<GameObject> items = new List<GameObject>();
            GameObject[] ot = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (var item in ot)
            {
                items.Add(item);
            }
            foreach (var item in items)
            {
                Debug.Log(item.name + "Copied");
            }


            return items;
        }
    }

    public class SceneLodeEditor : EditorWindow
    {
        private SceneTransformData _transformDatas;
        private GUIStyle _style;
        private string _text;

        [MenuItem("SceneDataEditor/Lode")]
        private static void Create()
        {
            var window = (SceneLodeEditor)GetWindow(typeof(SceneLodeEditor));
            window.Show();
        }

        private void Awake()
        {
            _style = new GUIStyle();
            _style.wordWrap = true;

            _text = "シーンデータをロードします。";
        }

        private void OnGUI()
        {
            string pass = $"Assets/SceneJoint/Resources/";
            EditorGUILayout.BeginVertical();

            _transformDatas = (SceneTransformData)EditorGUILayout.ObjectField("SourceData", _transformDatas, typeof(SceneTransformData), false);
            

            if (GUILayout.Button("シーンのデータロード"))
            {
                if (_transformDatas == null)
                {
                    _text = "No sources";
                    return;
                }

                LodeSceneTransform(_transformDatas);

                _text = "complete lode";
                Debug.Log("ロード完了");
            }
            EditorGUILayout.LabelField(_text, _style);
            EditorGUILayout.EndVertical();
        }
        private void LodeSceneTransform(SceneTransformData source)
        {
            List<GameObject> scene = GetAllObjectList();

            foreach (var item in scene)
            {
                foreach (var data in source.sceneData)
                {
                    if (data._pass == "" && item.name == data._objectName)
                    {
                        item.transform.position = data._position;
                        item.transform.rotation = data._rotation;
                        item.transform.localScale = data._scale;
                    }
                    else
                    {
                        if (item.name == data._objectName && item.transform.parent.gameObject.name == data._pass)
                        {
                            item.transform.position = data._position;
                            item.transform.rotation = data._rotation;
                            item.transform.localScale = data._scale;
                        }
                    }
                }
            }
            string[] path = EditorApplication.currentScene.Split(char.Parse("/"));
            path[path.Length - 1] = path[path.Length - 1];
            EditorApplication.SaveScene(string.Join("/", path), true);
            Debug.Log("Saved Scene");
        }
        private List<GameObject> GetAllObjectList()
        {
            List<GameObject> items = new List<GameObject>();
            GameObject[] ot = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (var item in ot)
            {
                items.Add(item);
            }
            foreach (var item in items)
            {
                Debug.Log(item.name + "Copied");
            }


            return items;
        }
    }
#endif
}