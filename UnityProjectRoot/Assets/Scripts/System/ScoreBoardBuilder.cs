using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
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


    /// <summary>�A�j���[�^�[</summary>
    Animator _animator = null;

    [SerializeField, Tooltip("�A�j���[�V������ : �Ǖ]��")]
    string _animNameOnGood = "OnGood";

    [SerializeField, Tooltip("�A�j���[�V������ : ���ʕ]��")]
    string _animNameOnNormal = "OnNormal";

    [SerializeField, Tooltip("�A�j���[�V������ : ���]��")]
    string _animNameOnBad = "OnBad";


    [SerializeField, Tooltip("�X�R�A�]����l ��")]
    int _scoreBorderGood = 3000;

    [SerializeField, Tooltip("�X�R�A�]����l ����")]
    int _scoreBorderNormal = 500;

    /// <summary>�擾�X�R�A</summary>
    int _score = 0;


    [SerializeField, Tooltip("�_���X�f���𗬂��v���C���[")]
    VideoPlayer _vPlayer = null;

    [SerializeField, Tooltip("���ʂ̃_���X�f��")]
    VideoClip _normalDance = null;

    [SerializeField, Tooltip("�����������̃_���X�f��")]
    VideoClip _meraDance = null;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _vPlayer = GetComponentInChildren<VideoPlayer>();

        _score = GameManager.Instance.Score.Value;

        if (_scoreText) _scoreText.text = _score.ToString();
        //if(_rankingText) _rankingText.text = GameManager.Instance.Score.Value.ToString();

        //�X�R�A�ɂ���ĉ摜�\����؂�ւ�
        Array.ForEach(_danceMovie, dm => dm.SetActive(false));
        Array.ForEach(_playerIllust, pi => pi.SetActive(false));
        Array.ForEach(_burnEndIllust, bei => bei.SetActive(false));
        if (_score >= _scoreBorderGood)
        {
            //�ǂ���̉f���𗬂�������
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
