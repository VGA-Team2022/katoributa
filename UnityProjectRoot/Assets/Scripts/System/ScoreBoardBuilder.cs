using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    [SerializeField, Tooltip("スコア評価基準値 良")]
    int _scoreBorderGood = 3000;

    [SerializeField, Tooltip("スコア評価基準値 普通")]
    int _scoreBorderNormal = 500;


    // Start is called before the first frame update
    void Start()
    {
        int score = GameManager.Instance.Score.Value;

        if (_scoreText) _scoreText.text = score.ToString();
        //if(_rankingText) _rankingText.text = GameManager.Instance.Score.Value.ToString();

        //スコアによって画像表示を切り替え
        Array.ForEach(_danceMovie, dm => dm.SetActive(false));
        Array.ForEach(_playerIllust, pi => pi.SetActive(false));
        Array.ForEach(_burnEndIllust, bei => bei.SetActive(false));
        if (score >= _scoreBorderGood)
        {
            Array.ForEach(_danceMovie, dm => dm.SetActive(true));
        }
        else if(score >= _scoreBorderNormal)
        {
            Array.ForEach(_playerIllust, pi => pi.SetActive(true));
        }
        else
        {
            Array.ForEach(_burnEndIllust, bei => bei.SetActive(true));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
