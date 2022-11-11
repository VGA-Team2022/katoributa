using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSaveManager<T>
{
    static string SavePath(string path)
        => $"{Application.dataPath}/{path}.json";

    public static void Save(T data, string path)
    {
        using (StreamWriter sw = new StreamWriter(SavePath(path), false))
        {
            string jsonstr = JsonUtility.ToJson(data, true);
            sw.Write(jsonstr);
            sw.Flush();
            Debug.Log($"セーブ完了{Application.dataPath}/{path}");
        }
    }

    public static T Load(string path)
    {
        if (File.Exists(SavePath(path)))//データが存在する場合は返す
        {
            using (StreamReader sr = new StreamReader(SavePath(path)))
            {
                string datastr = sr.ReadToEnd();
                Debug.Log("ロード完了");
                return JsonUtility.FromJson<T>(datastr);
            }
            
        }
        //存在しない場合はdefaultを返却
        return default;
    }
}
