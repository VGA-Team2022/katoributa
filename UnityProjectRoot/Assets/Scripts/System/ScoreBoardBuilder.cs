using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardBuilder : MonoBehaviour
{
    [SerializeField, Tooltip("�X�R�A�\���p�e�L�X�g")]
    TMP_Text _scoreText = null;

    [SerializeField, Tooltip("�����L���O���ʕ\���p�e�L�X�g")]
    TMP_Text _rankingText = null;


    [SerializeField, Tooltip("�X�R�A���Ǖ]���Ȃ�o���_���X����")]
    GameObject[] _danceMovie = null;

    [SerializeField, Tooltip("�X�R�A�����ʕ]���Ȃ�o���摜")]
    GameObject[] _playerIllust = null;

    [SerializeField, Tooltip("�X�R�A�����]���Ȃ�o���摜")]
    GameObject[] _burnEndIllust = null;


    [SerializeField, Tooltip("�X�R�A�]����l ��")]
    int _scoreBorderGood = 3000;

    [SerializeField, Tooltip("�X�R�A�]����l ����")]
    int _scoreBorderNormal = 500;


    // Start is called before the first frame update
    void Start()
    {
        int score = GameManager.Instance.Score.Value;

        if (_scoreText) _scoreText.text = score.ToString();
        //if(_rankingText) _rankingText.text = GameManager.Instance.Score.Value.ToString();

        //�X�R�A�ɂ���ĉ摜�\����؂�ւ�
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
