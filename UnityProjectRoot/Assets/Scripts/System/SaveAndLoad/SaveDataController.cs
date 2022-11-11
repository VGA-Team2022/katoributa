using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    public  SaveDataController I { get; private set; }

    [SerializeField] string _testSaveDataPath = "test-save-data";

    private void Awake()
    {
        I = this;
    }

    public void TestLoad()
    {
        TestSaveData saveData = JsonSaveManager<TestSaveData>.Load(_testSaveDataPath);

        if (saveData == null)//セーブデータが存在しない場合は任意の値で初期化
        {
            //新たなセーブデータを作成
            saveData = new TestSaveData()
            {
                _num = 3,
                _str = "テスト",
                _vec = Vector3.one
            };
            Debug.Log("セーブデータが存在しなかったため任意の値で初期化しました");
        }

        TestClass.I.SetValue(saveData);

    }

    public void TestSave()
    {
        TestSaveData testSaveData = new TestSaveData()
        {
            _num = TestClass.I._num,
            _str = TestClass.I._str,
            _vec = TestClass.I._vec,
        };
        JsonSaveManager<TestSaveData>.Save(testSaveData, _testSaveDataPath);
    }

    //private void OnApplicationPause(bool isPaused)
    //{
    //    if (isPaused)
    //    {
    //        OverWriteSaveData();
    //    }
    //}

    //private void OnApplicationQuit()//アプリケーション終了時に呼び出す
    //{
    //    OverWriteSaveData();
    //}

    ////セーブデータの上書き
    //void OverWriteSaveData()
    //{
    //    TestSaveData testSaveData = new TestSaveData()
    //    {
    //        _num = TestClass.I._num,
    //        _str = TestClass.I._str,
    //        _vec = TestClass.I._vec,
    //    };
    //    JsonSaveManager<TestSaveData>.Save(testSaveData, _testSaveDataPath);
    //}
}
