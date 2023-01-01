using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InGameUIPresenter : MonoBehaviour
{
    [SerializeField] MVPText _comboText;
    [SerializeField] MVPText _scoreText;
    [SerializeField] MVPMosquitCoil _mosquitCoil;
    [SerializeField] MVPLifeIcon _lifeIcon;

    /* MEMO
        �R���{
        �X�R�A
        ��������
     */

    private void Start()
    {
        if (_scoreText)
        {
            GameManager.Instance.Score.Subscribe(x =>
            {
                _scoreText.SetText($"{x:0000}");
            }).AddTo(this);
        }
        if (_comboText)
        {
            GameManager.Instance.Combo.ComboCount.Subscribe(x =>
            {
                _comboText.SetText($"{x:00}");
            }).AddTo(this);
        }
        if (_lifeIcon)
        {
            var hp = GameObject.FindGameObjectWithTag("Player").GetComponent<Durability>();

            if (!hp)
            {
                Debug.LogError("Durability��������܂���ł���");
                return;
            }

            hp.HP.Subscribe(x =>
            {
                _lifeIcon.UpdateUI(x);
            }).AddTo(this);
        }
        else
        {
            Debug.LogError("MVPIcon���ݒ肳��Ă��܂���");
        }
    }
}
