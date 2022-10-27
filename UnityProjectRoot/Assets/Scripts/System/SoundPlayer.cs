using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

/// <summary>
/// 音を鳴らすクラス
/// </summary>
[RequireComponent(typeof(CriAtomSource))]
public class SoundPlayer : MonoBehaviour
{
    [Header("設定")]
    [SerializeField, Tooltip("立体音響にする時はチェック")] bool _3DPositioning;
    [SerializeField, Tooltip("使用するCueSheet")] CueSheet _cueSheet;
    [Space(10)]
    [SerializeField, Tooltip("Logを表示する")] bool _debugLog = true; 

    CriAtomSource _source;
    CriAtomEx.CueInfo[] _cueInfoList;
    CriAtomExAcb _atomExAcb;

    private void Awake()
    {
        //Sourceを取得
        _source = GetComponent<CriAtomSource>();

        ///立体音響にするかの設定
        _source.use3dPositioning = _3DPositioning;

        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        //キューシートのロードを待つ
        while (CriAtom.CueSheetsAreLoading)
        {
            yield return null;
        }

        //Cue情報を取得
        _atomExAcb = CriAtom.GetAcb(_cueSheet.ToString());
        _cueInfoList = _atomExAcb.GetCueInfoList();
    }

    /// <summary>
    /// Nameによる再生
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name)
    {
        var atomExPlayer = _source.player;

        //再生中で無ければ
        if(atomExPlayer.GetStatus() != CriAtomExPlayer.Status.Playing)
        {
            //例外をチェック
            for(int i = 0; i < _cueInfoList.Length; i++)
            {
                //CueInfoListに指定された名前のCueがあったら通す
                if(_cueInfoList[i].name == name)
                {
                    break;
                }

                //最後まで見つからなかったら戻る
                if(i == _cueInfoList.Length - 1)
                {
                    if(_debugLog)
                        Debug.LogError($"[{_cueSheet}]に[{name}]がありません");

                    return;
                }
            }

            //設定
            atomExPlayer.SetCue(_atomExAcb, name);
            atomExPlayer.Start();
        }
    }
    /// <summary>
    /// IDによる再生
    /// </summary>
    /// <param name="id"></param>
    public void PlaySound(int id)
    {
        var atomExPlayer = _source.player;

        //このオブジェクトについているSourceが再生中で無ければ
        if (atomExPlayer.GetStatus() != CriAtomExPlayer.Status.Playing)
        {
            //例外をチェック
            for (int i = 0; i < _cueInfoList.Length; i++)
            {
                //CueInfoListに指定されたIDがあったら通す
                if (_cueInfoList[i].id == id)
                {
                    break;
                }

                //最後まで見つからなかったら戻る
                if (i == _cueInfoList.Length - 1)
                {
                    if(_debugLog)
                        Debug.LogError($"[{_cueSheet}]に指定されたID[{id}]がありません");

                    return;
                }
            }

            //設定
            atomExPlayer.SetCue(_atomExAcb, id);
            atomExPlayer.Start();
        }
    }

    /*MEMO
    サウンドさんから送られてくるデータによっては、変更する可能性あり
    (出来るだけ変えたく無いので、要話し合い)
    */
    enum CueSheet
    {
        BGM,
        SE,
        ME,
    }
}
