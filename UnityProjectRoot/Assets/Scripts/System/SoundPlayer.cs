using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

/// <summary>
/// ����炷�N���X
/// </summary>
[RequireComponent(typeof(CriAtomSource))]
public class SoundPlayer : MonoBehaviour
{
    [Header("�ݒ�")]
    [SerializeField, Tooltip("���̉����ɂ��鎞�̓`�F�b�N")] bool _3DPositioning = true;
    [SerializeField, Tooltip("���[�v�Đ����̓`�F�b�N")] bool _loop;
    [SerializeField, Range(0f,1f), Tooltip("���̃{�����[��")] float _volume = 1f;
    [Space(10)]
    [SerializeField, Tooltip("�g�p����CueSheet")] CueSheet _cueSheet = CueSheet.SE;
    [Space(10)]
    [SerializeField, Tooltip("Log��\������")] bool _debugLog = true;

    CriAtomSource _source;
    CriAtomEx.CueInfo[] _cueInfoList;
    CriAtomExAcb _atomExAcb;

    private void Awake()
    {
        //Source���擾
        _source = GetComponent<CriAtomSource>();

        ///���̉����ɂ��邩�̐ݒ�
        _source.use3dPositioning = _3DPositioning;
        _source.volume = _volume;
        _source.loop = _loop;

        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        //�L���[�V�[�g�̃��[�h��҂�
        while (CriAtom.CueSheetsAreLoading)
        {
            yield return null;
        }

        //Cue�����擾
        _atomExAcb = CriAtom.GetAcb(_cueSheet.ToString());
        _cueInfoList = _atomExAcb.GetCueInfoList();
    }

    /// <summary>
    /// Name�ɂ��Đ�
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name)
    {
        if (_cueInfoList is null || _atomExAcb is null)
        {
            if (_debugLog)
                Debug.LogError("�f�[�^�̎擾���o���Ă��܂���");

            return;
        }

        //var atomExPlayer = _source.player;

        ////�Đ����Ŗ������
        //if(atomExPlayer.GetStatus() != CriAtomExPlayer.Status.Playing)
        //{
            //��O���`�F�b�N
            for(int i = 0; i < _cueInfoList.Length; i++)
            {
                //CueInfoList�Ɏw�肳�ꂽ���O��Cue����������ʂ�
                if(_cueInfoList[i].name == name)
                {
                    break;
                }

                //�Ō�܂Ō�����Ȃ�������߂�
                if(i == _cueInfoList.Length - 1)
                {
                    if(_debugLog)
                        Debug.LogError($"[{_cueSheet}]��[{name}]������܂���");

                    return;
                }
            }

            //�ݒ�
            _source.cueName = name;
            _source.cueSheet = _cueSheet.ToString();

            _source.Play();
        //}
    }
    /// <summary>
    /// ID�ɂ��Đ�
    /// </summary>
    /// <param name="id"></param>
    public void PlaySound(int id)
    {
        if (_cueInfoList is null || _atomExAcb is null)
        {
            if (_debugLog)
                Debug.LogError("�f�[�^�̎擾���o���Ă��܂���");

            return;
        }

        var atomExPlayer = _source.player;

        ////���̃I�u�W�F�N�g�ɂ��Ă���Source���Đ����Ŗ������
        if (atomExPlayer.GetStatus() != CriAtomExPlayer.Status.Playing)
        {
            //��O���`�F�b�N
            for (int i = 0; i < _cueInfoList.Length; i++)
            {
                //CueInfoList�Ɏw�肳�ꂽID����������ʂ�
                if (_cueInfoList[i].id == id)
                {
                    break;
                }

                //�Ō�܂Ō�����Ȃ�������߂�
                if (i == _cueInfoList.Length - 1)
                {
                    if(_debugLog)
                        Debug.LogError($"[{_cueSheet}]�Ɏw�肳�ꂽID[{id}]������܂���");

                    return;
                }
            }

            //�ݒ�
            var name = _cueInfoList[id].name;
            _source.cueName = name;
            _source.cueSheet = _cueSheet.ToString();

            _source.Play();
        }
    }
    /**
 * <summary>�ꎞ��~�^�ĊJ���܂��B</summary>
 * <param name='sw'>true:�ꎞ��~�Afalse:�ĊJ</param>
 * <remarks>
 * <para header='����'>�Đ��̃|�[�Y�^�|�[�Y�������s���܂��B<br/>
 * sw �� true ���w�肵�Ė{�֐������s����ƁA CriAtomSource
 * �R���|�[�l���g�͍Đ����̉������|�[�Y�i�ꎞ��~�j���܂��B<br/>
 * sw �� false ���w�肵�Ė{�֐������s����ƁA CriAtomSource
 * �R���|�[�l���g�̓|�[�Y���������A�ꎞ��~���Ă��������̍Đ����ĊJ���܂��B<br/></para>
 * </remarks>
 */
    public void PauseSound(bool sw)
    {
        _source.Pause(sw);
    }

    public void StopSound()
    {
        _source.Stop();
    }

    /*MEMO
    �T�E���h���񂩂瑗���Ă���f�[�^�ɂ���ẮA�ύX����\������
    (�o���邾���ς����������̂ŁA�v�b������)
    */
    enum CueSheet
    {
        BGM,
        SE,
        ME,
    }
}
