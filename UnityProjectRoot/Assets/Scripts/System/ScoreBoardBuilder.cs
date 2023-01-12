using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class ScoreBoardBuilder : MonoBehaviour
{
    [SerializeField, Tooltip("スコア表示用テキスト")]
    TMP_Text _scoreText = null;

    [SerializeField, Tooltip("ランキング順位表示用テキスト")]
    TMP_Text _rankingText = null;


    [SerializeField, Tooltip("スコアが良評価なら出すダンス動画")]
    GameObject[] _danceMovie = null;

    [SerializeField, Tooltip("スコアが普通評価なら出す画像")]
    GameObject[] _playerIllust = null;

    [SerializeField, Tooltip("スコアが悪評価なら出す画像")]
    GameObject[] _burnEndIllust = null;


    /// <summary>アニメーター</summary>
    Animator _animator = null;

    [SerializeField, Tooltip("アニメーション名 : 良評価")]
    string _animNameOnGood = "OnGood";

    [SerializeField, Tooltip("アニメーション名 : 普通評価")]
    string _animNameOnNormal = "OnNormal";

    [SerializeField, Tooltip("アニメーション名 : 悪評価")]
    string _animNameOnBad = "OnBad";


    [SerializeField, Tooltip("スコア評価基準値 良")]
    int _scoreBorderGood = 3000;

    [SerializeField, Tooltip("スコア評価基準値 普通")]
    int _scoreBorderNormal = 500;

    /// <summary>取得スコア</summary>
    int _score = 0;


    [SerializeField, Tooltip("ダンス映像を流すプレイヤー")]
    VideoPlayer _vPlayer = null;

    [SerializeField, Tooltip("普通のダンス映像")]
    VideoClip _normalDance = null;

    [SerializeField, Tooltip("メラメラ時のダンス映像")]
    VideoClip _meraDance = null;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _vPlayer = GetComponentInChildren<VideoPlayer>();

        _score = GameManager.Instance.Score.Value;

        if (_scoreText) _scoreText.text = _score.ToString();
        //if(_rankingText) _rankingText.text = GameManager.Instance.Score.Value.ToString();

        //スコアによって画像表示を切り替え
        Array.ForEach(_danceMovie, dm => dm.SetActive(false));
        Array.ForEach(_playerIllust, pi => pi.SetActive(false));
        Array.ForEach(_burnEndIllust, bei => bei.SetActive(false));
        if (_score >= _scoreBorderGood)
        {
            //どちらの映像を流すか分岐
            if(UnityEngine.Random.value > 0.8f)
            {
                _vPlayer.clip = _meraDance;
            }
            else
            {
                _vPlayer.clip = _normalDance;
            }

            Array.ForEach(_danceMovie, dm => dm.SetActive(true));
            _animator.Play(_animNameOnGood);
        }
        else if (_score >= _scoreBorderNormal)
        {
            Array.ForEach(_playerIllust, pi => pi.SetActive(true));
            _animator.Play(_animNameOnNormal);
        }
        else
        {
            Array.ForEach(_burnEndIllust, bei => bei.SetActive(true));
            _animator.Play(_animNameOnBad);
        }
    }
}
