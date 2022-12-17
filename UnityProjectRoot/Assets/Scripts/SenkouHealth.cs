using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

/// <summary>
/// 蚊取り線香（体力）の挙動を制御するコンポーネント
/// </summary>
public class SenkouHealth : MonoBehaviour
{
    [Header("蚊取り線香の残り時間")]
    [SerializeField, Tooltip("蚊取り線香の残り時間")] FloatReactiveProperty _senkouTime = new FloatReactiveProperty(60f);
    [SerializeField, Tooltip("メラメラモード時の線香の減る時間")] float _onPowerUpSpeed = 2f;
    [Tooltip("現在の減るスピード")] float _currentSpeed = 1f;
    [Tooltip("ポーズしているかどうか")] bool _isPaused;

    [Header("無敵フラグ")]
    [SerializeField] bool _godMode;

    public IReactiveProperty<float> Health => _senkouTime;

    private void Update()
    {
        ReduceHealth();
    }

    /// <summary>
    /// 徐々に体力が減っていく処理
    /// </summary>
    private void ReduceHealth()
    {
        if (_godMode) return;

        if (_isPaused) return;

        _senkouTime.Value -= Time.deltaTime * _currentSpeed;

        _senkouTime.Value = Mathf.Max(0, _senkouTime.Value);

        if(_senkouTime.Value <= 0)
        {
            GameManager.Instance.OnGameEnd();
        }
    }

    private void OnPause()
    {
        _isPaused = true;
    }

    private void OnResume()
    {
        _isPaused = false;
    }

    private void EnabledPowerUp()
    {
        _currentSpeed = _onPowerUpSpeed;
    }

    private void DisabledPowerUp()
    {
        _currentSpeed = 1f;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPowerUpEvent += EnabledPowerUp;
        GameManager.Instance.OnPowerDownEvent += DisabledPowerUp;
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPowerUpEvent -= EnabledPowerUp;
        GameManager.Instance.OnPowerDownEvent -= DisabledPowerUp;
        GameManager.Instance.OnPause -= OnPause;
        GameManager.Instance.OnResume -= OnResume;
    }

    /// <summary>
    /// アイテムを取得した時に呼ばれる関数
    /// </summary>
    /// <param name="value">回復アイテムの回復値</param>
    public void GetHeal(float value)
    {
        _senkouTime.Value += value;
        Debug.Log(value + "回復した");
    }
}
