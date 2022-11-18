using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 蚊取り線香（体力）の挙動を制御するコンポーネント
/// </summary>
public class SenkouHealth : MonoBehaviour
{
    [Header("蚊取り線香の体力")]
    [SerializeField, Tooltip("体力の最大値")] float _health = 60f;

    [Header("UI")]
    [SerializeField, Tooltip("体力を表示させるテキスト")] TMP_Text _healthText;

    private void Update()
    {
        ReduceHealth();
    }

    /// <summary>
    /// 徐々に体力が減っていく処理
    /// </summary>
    private void ReduceHealth()
    {
        _health -= Time.deltaTime;

        _health = Mathf.Max(0, _health);

        if(_health <= 0)
        {
            GameManager.Instance.OnGameOver();
        }

        _healthText.text = "線香の残り時間：" + _health.ToString("F2");
    }

    /// <summary>
    /// アイテムを取得した時に呼ばれる関数
    /// </summary>
    /// <param name="value">回復アイテムの回復値</param>
    public void GetHeal(float value)
    {
        _health += value;
        Debug.Log(value + "回復した");
    }
}
