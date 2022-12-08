using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShaderEditorTools : Editor
{
    [MenuItem("ShaderEditor/ToPBRShaderTools")]
    static void ShaderEdit()
    {
        GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        int id = 0;
        foreach (var item in gameObjects)
        {
            if (!item.activeSelf)
            {
                continue;
            }
            ChangeShader(item, "TK/Material/PBRShader", "Universal Render Pipeline/Lit", item.name, id);
            id++;
            //ChangeShader(item, "TK/Material/PBRShader", "test");
        }
    }

    [MenuItem("ShaderEditor/ToCullShaderTools")]
    static void ShaderEdit2()
    {
        GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        int id = 0;
        foreach (var item in gameObjects)
        {
            if (!item.activeSelf)
            {
                continue;
            }
            ChangeShader2(item, "TK/Custom/OpacityShader", "Shader Graphs/ArnoldStandardSurfaceTransparent", item.name, id);
            id++;
        }
    }

    public static void ChangeShader(GameObject targetGameObject, string ShaderName_to, string oldShaderName,
         string objectName, int Id)
    {

        foreach (var t in targetGameObject.GetComponentsInChildren<Transform>(true))
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
                    if (materials[i].shader.name == oldShaderName)
                    {
                        Material setmaterial = new Material(Shader.Find(ShaderName_to));
                        Texture texture = materials[i].GetTexture("_BaseMap");
                        setmaterial.SetTexture("_BaseMap", texture);
                        materials[i] = setmaterial;
                        AssetDatabase.CreateAsset(setmaterial, 
                                $"Assets/Shader/test01/{objectName}test{Id}.mat");
                        
                        t.GetComponent<MeshRenderer>().sharedMaterials = materials;
                    }
                }
            }
        }
    }
    public static void ChangeShader2(GameObject targetGameObject, string ShaderName_to, string oldShaderName,
        string objectName, int Id)
    {

        foreach (var t in targetGameObject.GetComponentsInChildren<Transform>(true))
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
                    if (materials[i].shader.name == oldShaderName)
                    {
                        Material setmaterial = new Material(Shader.Find(ShaderName_to));
                        Texture texture = materials[i].GetTexture("_BASE_COLOR_MAP");
                        Texture texture2 = materials[i].GetTexture("_OPACITY_MAP");
                        setmaterial.SetTexture("_BaseMap", texture);
                        setmaterial.SetTexture("_CutMap", texture2);
                        materials[i] = setmaterial;
                        AssetDatabase.CreateAsset(setmaterial,
                                $"Assets/Shader/test02/{objectName}test{Id}.mat");

                        t.GetComponent<MeshRenderer>().sharedMaterials = materials;
                    }

                }
            }
        }
    }
}
