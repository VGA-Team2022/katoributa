using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public  class ShaderEditorTools : Editor
{
    #region OldFunction
    //[MenuItem("ShaderEditor/ToPBRShaderTools")]
    //static void ShaderEdit()
    //{
    //    GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    //    int id = 0;
    //    foreach (var item in gameObjects)
    //    {
    //        if (!item.activeSelf)
    //        {
    //            continue;
    //        }
    //        ChangeShader(item, "TK/Material/PBRShader", "Universal Render Pipeline/Lit", item.name, id);
    //        id++;
    //        //ChangeShader(item, "TK/Material/PBRShader", "test");
    //    }
    //}

    //[MenuItem("ShaderEditor/ToCullShaderTools")]
    //static void ShaderEdit2()
    //{
    //    GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    //    int id = 0;
    //    foreach (var item in gameObjects)
    //    {
    //        if (!item.activeSelf)
    //        {
    //            continue;
    //        }
    //        ChangeShader2(item, "TK/Custom/OpacityShader", "Shader Graphs/ArnoldStandardSurfaceTransparent", item.name, id);
    //        id++;
    //    }
    //}

    //public static void ChangeShader(GameObject targetGameObject, string ShaderName_to, string oldShaderName,
    //     string objectName, int Id)
    //{

    //    foreach (var t in targetGameObject.GetComponentsInChildren<Transform>(true))
    //    {
    //        if (t.GetComponent<MeshRenderer>() != null)
    //        {
    //            var renderer = t.GetComponent<MeshRenderer>();
    //            if (renderer == null)
    //            {
    //                continue;
    //            }
    //            var materials = renderer.sharedMaterials;
    //            for (int i = 0; i < materials.Length; i++)
    //            {
    //                if (materials[i].shader.name == oldShaderName)
    //                {
    //                    Material setmaterial = new Material(Shader.Find(ShaderName_to));
    //                    Texture texture = materials[i].GetTexture("_BaseMap");
    //                    setmaterial.SetTexture("_BaseMap", texture);
    //                    materials[i] = setmaterial;
    //                    AssetDatabase.CreateAsset(setmaterial, 
    //                            $"Assets/Shader/test01/{objectName}test{Id}.mat");

    //                    t.GetComponent<MeshRenderer>().sharedMaterials = materials;
    //                }
    //            }
    //        }
    //    }
    //}
    //public static void ChangeShader2(GameObject targetGameObject, string ShaderName_to, string oldShaderName,
    //    string objectName, int Id)
    //{

    //    foreach (var t in targetGameObject.GetComponentsInChildren<Transform>(true))
    //    {
    //        if (t.GetComponent<MeshRenderer>() != null)
    //        {
    //            var renderer = t.GetComponent<MeshRenderer>();
    //            if (renderer == null)
    //            {
    //                continue;
    //            }
    //            var materials = renderer.sharedMaterials;
    //            for (int i = 0; i < materials.Length; i++)
    //            {
    //                if (materials[i].shader.name == oldShaderName)
    //                {
    //                    Material setmaterial = new Material(Shader.Find(ShaderName_to));
    //                    Texture texture = materials[i].GetTexture("_BASE_COLOR_MAP");
    //                    Texture texture2 = materials[i].GetTexture("_OPACITY_MAP");
    //                    setmaterial.SetTexture("_BaseMap", texture);
    //                    setmaterial.SetTexture("_CutMap", texture2);
    //                    materials[i] = setmaterial;
    //                    AssetDatabase.CreateAsset(setmaterial,
    //                            $"Assets/Shader/test02/{objectName}test{Id}.mat");

    //                    t.GetComponent<MeshRenderer>().sharedMaterials = materials;
    //                }

    //            }
    //        }
    //    }
    //}

    //[MenuItem("CreatePBRShaderTool")]
    //static void EditShader()
    //{
    //    GameObject[] sceneObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    //    List<Transform[]> transforms = new List<Transform[]>();
    //    foreach (var item in sceneObjects)
    //    {
    //        transforms.Add(GetChildren(item.transform));
    //    }
    //}
    //public static Transform[] GetChildren(Transform parent)
    //{
    //    // 親を含む子オブジェクトを再帰的に取得
    //    Transform[] parentAndChildren = parent.GetComponentsInChildren<Transform>();
    //    // 子オブジェクトの格納用配列作成
    //    //var children = new Transform[parentAndChildren.Length - 1];

    //    // 親を除く子オブジェクトを結果にコピー
    //    //Array.Copy(parentAndChildren, 1, children, 0, children.Length);

    //    // 子オブジェクトが再帰的に格納された配列
    //    return parentAndChildren;
    //}

    //public static void ChangeShaders(GameObject targetGameObject, string ShaderName_to, string oldShaderName,
    //    string objectName, int Id, string pass)
    //{

    //    foreach (var t in targetGameObject.GetComponentsInChildren<Transform>(true))
    //    {
    //        if (t.GetComponent<MeshRenderer>() != null)
    //        {
    //            var renderer = t.GetComponent<MeshRenderer>();
    //            if (renderer == null)
    //            {
    //                continue;
    //            }
    //            var materials = renderer.sharedMaterials;
    //            for (int i = 0; i < materials.Length; i++)
    //            {
    //                if (materials[i].shader.name == oldShaderName)
    //                {
    //                    Material setmaterial = new Material(Shader.Find(ShaderName_to));
    //                    Texture texture = materials[i].GetTexture("_BASE_COLOR_MAP");
    //                    Texture texture2 = materials[i].GetTexture("_OPACITY_MAP");
    //                    setmaterial.SetTexture("_BaseMap", texture);
    //                    setmaterial.SetTexture("_CutMap", texture2);
    //                    materials[i] = setmaterial;
    //                    AssetDatabase.CreateAsset(setmaterial,
    //                            $"Assets/{pass}/{objectName}test{Id}.mat");

    //                    t.GetComponent<MeshRenderer>().sharedMaterials = materials;
    //                }

    //            }
    //        }
    //    }
    //}
    #endregion
}
namespace TK.ShaderEditor.ShaderEditorWindow
{

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

    public class ShaderEditorWindow : EditorWindow
    {
        private Shader _baseShader;
        private static int _counter;
        private Shader _replaceShader;
        private GameObject[] _selection;

        private GUIStyle _style;
        private string _text;


        private string lab = "ShaderPassName{指定のない場合はMasterの中のMaterialにできます。}(Assets/より下層のディレクトリ名を指定してください)";
        private string current = "";


        [MenuItem("ShaderEditor/Edit Shader of objects in scene")]
        private static void Create()
        {
            var window = (ShaderEditorWindow)GetWindow(typeof(ShaderEditorWindow));
            window.Show();
        }

        private void Awake()
        {
            _style = new GUIStyle();
            _style.wordWrap = true;

            _text = "Select one or more objects, if base material is found it will be replaced by replace material";
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            _baseShader = (Shader)EditorGUILayout.ObjectField("Base Shader", _baseShader, typeof(Shader), false);
            _replaceShader =
                (Shader)EditorGUILayout.ObjectField("Replace Shader", _replaceShader, typeof(Shader), false);

            GUILayout.Label(lab);

            //日本語入力に変更できるようにする
            Input.imeCompositionMode = IMECompositionMode.On;


            //文字入力できる領域を作る（改行可能）
            current = GUILayout.TextArea(current, GUILayout.Height(50));


            if (GUILayout.Button("Replace All Object Shader　すべて変える"))
            {
                _counter = 0;
                if (!_baseShader)
                {
                    _text = "No base Shader selected, please fill base shader slot　帰る前のシェーダーが指定されていないです";
                    return;
                }
                if (!_replaceShader)
                {
                    _text = "No replace Shader selected, please fill replace shader slot　変えるシェーダーが指定されていないです";
                    return;
                }
                if (current == "")
                {
                    _text = "No Dir selected";
                    current = "Master/Material";
                }

                DirectoryUtils.SafeCreateDirectory($"Assets/{current}");

                SelectAll(current);

                _text = _counter + " materials replaced";
            }

            if (GUILayout.Button("Replace Select Object Shader　選択したものだけ変える"))
            {
                _counter = 0;
                if (!_baseShader)
                {
                    _text = "No base Shader selected, please fill base shader slot　帰る前のシェーダーが指定されていないです";
                    return;
                }
                if (!_replaceShader)
                {
                    _text = "No replace Shader selected, please fill replace shader slot　変えるシェーダーが指定されていないです";
                    return;
                }
                if (current == "")
                {
                    _text = "No Dir selected";

                    current = "Master/Material";
                    
                }

                DirectoryUtils.SafeCreateDirectory($"Assets/{current}");
                _selection = Selection.gameObjects;
                if (_selection.Length > 0)
                {
                    SelectOnly(current, _selection);
                    _text = _counter + " materials replaced";
                }
                else
                {
                    _text =
                        "No object selected, please select one or more objects that should have their materials replaced.";
                }
            }
            EditorGUILayout.LabelField(_text, _style);

            EditorGUILayout.EndVertical();
        }
        private void SelectAll(string passlog)
        {
            GameObject[] sceneObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            List<Transform[]> transforms = new List<Transform[]>();
            int id = 0;
            foreach (var item in sceneObjects)
            {
                transforms.Add(GetChildren(item.transform));
            }
            foreach (var item in transforms)
            {
                ChangeShaders(item, _replaceShader, _baseShader, id, passlog);
                id++;
            }
        }
        private void SelectOnly(string passlog, GameObject[] gameObjects)
        {
            List<Transform[]> transforms = new List<Transform[]>();
            int id = 0;
            foreach (var item in gameObjects)
            {
                transforms.Add(GetChildren(item.transform));
            }
            foreach (var item in transforms)
            {
                ChangeShaders(item, _replaceShader, _baseShader, id, passlog);
                id++;
            }
        }
        public static Transform[] GetChildren(Transform parent)
        {
            Transform[] parentAndChildren = parent.GetComponentsInChildren<Transform>();
            return parentAndChildren;
        }

        [System.Obsolete]
        public static void ChangeShaders(Transform[] targetGameObject, Shader setShader, Shader oldShader,
         int Id, string pass)
        {

            foreach (var t in targetGameObject)
            {
                if (t.GetComponent<MeshRenderer>() != null)
                {
                    var renderer = t.GetComponent<MeshRenderer>();
                    if (renderer == null)
                    {
                        continue;
                    }
                    var materials = renderer.sharedMaterials;
                    for (int i = 0; i < materials.Length; i++)
                    {
                        if (materials[i].shader == oldShader)
                        {
                            Material setmaterial = new Material(setShader);
                            Texture texture = null;
                            Texture texture2 = null;
                            texture = materials[i].GetTexture("_BaseMap");
                            texture2 = materials[i].GetTexture("_BumpMap");
                            if (texture != null)
                            {

                                setmaterial.SetTexture("_BaseMap", texture);
                            }
                            if (texture2 != null)
                            {
                                setmaterial.SetTexture("_BumpMap", texture2);
                            }
                            //materials[i] = setmaterial;
                            AssetDatabase.CreateAsset(setmaterial,
                                    $"Assets/{pass}/{t.name}test{Id}{i}.mat");
                            materials[i] = setmaterial;//(Material)Resources.Load($"{t.name}test{Id}{i}");
                           
                            _counter++;
                        }

                    }
                    t.GetComponent<MeshRenderer>().sharedMaterials = materials;
                    //foreach (var item in materials)
                    //{
                    //    Destroy(item);
                    //}
                    string[] path = EditorApplication.currentScene.Split(char.Parse("/"));
                    path[path.Length - 1] = path[path.Length - 1];
                    EditorApplication.SaveScene(string.Join("/", path), true);
                    Debug.Log("Saved Scene");
                }
            }
        }
    }
}
