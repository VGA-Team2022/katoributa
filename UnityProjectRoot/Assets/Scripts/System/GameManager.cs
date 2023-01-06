using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UnityEngine.UI;

/// <summary>
/// ゲームの管理クラス
/// </summary>
public class GameManager
{
    #region プロパティ
    /// <summary>
    /// GameManagerのインスタンス
    /// </summary>
    public static GameManager Instance = new GameManager();

    public IReadOnlyReactiveProperty<float> GameTime => _gameTime;
    public IReadOnlyReactiveProperty<int> Score => _score;
    public Combo Combo => _combo;
    public GameState GameState => _gameState;

    #endregion

    #region 変数

    FloatReactiveProperty _gameTime = new FloatReactiveProperty();
    IntReactiveProperty _score = new IntReactiveProperty();
    GameState _gameState;

    Image _gameOverPanel;
    Image _gameClearPanel;

    Combo _combo;

    bool _isPause;
    #endregion

    /*ToDo
    スコアの管理
    時間の管理
    ポーズ処理周り
    */

    /*説明
    PlayerModeChange(PlayerMode mode)
        蚊取り豚のモードを変更させる関数
        引数で与えられたモードに切り替わる

    AddScore(int score)
        スコアの加算をする関数
        引数で与えられた値が負の値であれば、ログだけを出して加算はしない
        0または正の値であった場合ログを出してスコアの加算を行う
     */

    //コンストラクタ
    public GameManager() 
    {
        Debug.Log("New GameManager");
    }

    #region イベント

    /// <summary>
    /// ポーズ時の処理を登録
    /// </summary>
    public event Action OnPause;
    /// <summary>
    /// ポーズ解除時の処理を登録
    /// </summary>
    public event Action OnResume;

    /// <summary>
    /// メラメラモードがPowerUpになった時に呼ばれるイベント
    /// </summary>
    public event Action OnPowerUpEvent;
    /// <summary>
    /// メラメラモードがNormalになった時に呼ばれるイベント
    /// </summary>
    public event Action OnPowerDownEvent;

    /// <summary>
    /// ゲームオーバー時のイベント
    /// </summary>
    public event Action OnGameOverEvent;
    /// <summary>
    /// ゲーム終了時のイベント
    /// </summary>
    public event Action OnGameEndEvent;

    #endregion

    /// <summary>
    /// 蚊取り豚のモードを切り替える関数
    /// </summary>
    /// <param name="mode"></param>
    public void PlayerModeChange(PlayerMode mode)
    {
        switch(mode)
        {
            case PlayerMode.PowerUp:
                OnPowerUpEvent?.Invoke();
                break;
            case PlayerMode.Normal:
                OnPowerDownEvent?.Invoke();
                break;
        }

        Debug.Log($"モードを切り替えた {mode}");
    }

    public void GameStateChange(GameState mode)
    {
        if (_gameState == mode)
        {
            Debug.LogError("GameStateが一緒です");
            return;
        }

        switch (mode)
        {
            case GameState.GameReady:
                break;
            case GameState.InGame:
                break;
            case GameState.GameFinish:
                break;
        }

        _gameState = mode;
        Debug.Log($"モードを切り替えた {mode}");
    }


    /// <summary>
    /// スコアを加算する
    /// </summary>
    public void AddScore(int score)
    {
        if(score < 0)
        {
            Debug.Log("与えられた値が不正な値でした");
            return;
        }

        var mul = _combo.ReturnCurrentValue();
        score = (int)(score * mul);
        Debug.Log($"与えられた値:{score} 　現在のスコア:{_score.Value}");
        _score.Value += score;
    }

    /// <summary>
    /// 変数の初期設定
    /// </summary>
    /// <param name="attachment"></param>
    public void OnSetup(GameManagerAttachment attachment)
    {
        _gameTime.Value = 0;

        _score.Value = 0;
        _combo = new Combo(attachment.ComboTime, attachment.MultiplicationLimit);

        _gameClearPanel = attachment.GameClearPanel;
        _gameOverPanel = attachment.GameOverPanel;

        GameStateChange(GameState.GameReady);
    }

    public void OnGameOver()
    {
        OnGameOverEvent?.Invoke();
        GameStateChange(GameState.GameFinish);
        _gameOverPanel?.gameObject.SetActive(true);
        Debug.Log("OnGameOver");
    }

    public void OnGameEnd()
    {
        OnGameEndEvent?.Invoke();
        _gameClearPanel?.gameObject.SetActive(true);
        Debug.Log("OnGameClear");
    }

    /// <summary>
    /// フレーム処理
    /// </summary>
    void OnUpdate()
    {
        if(InputUtility.GetDownPause)
        {
            if(_isPause)
            {
                OnResume?.Invoke();
                Debug.Log("ポーズ解除");
            }
            else
            {
                OnPause?.Invoke();
                Debug.Log("ポーズ開始");
            }

            _isPause = !_isPause;
        }

        _gameTime.Value += Time.deltaTime;
        _combo.Run();
    }

    #region コールバック

    public void SetupUpdateCallback(GameManagerAttachment attachment)
    {
        attachment.SetupCallBack(OnUpdate);
    }

    #endregion
}

/// <summary>
/// 蚊取り豚のモード用のEnum
/// </summary>
public enum PlayerMode
{
    //通常モード
    Normal = 0,
    //メラメラモード
    PowerUp = 1,
}

public enum GameState
{ 
    GameReady = 0,
    InGame = 1,
    GameFinish = 2,
}
public class Combo
{

    float _timer;
    float _limit;
    float _comboTime;
    float _currentValue;
    IntReactiveProperty _count = new IntReactiveProperty();

    const float _multiplicationValue = 0.1f;
    public IReadOnlyReactiveProperty<int> ComboCount => _count;

    public Combo(float time, float maxLimit)
    {
        _comboTime = time;
        _limit = maxLimit;
    }
    public void Run()
    {
        if(_timer > _comboTime)
        {
            _currentValue = 0;
            _count.Value = 0;
            return;
        }

        _timer += Time.deltaTime;
    }
    public float ReturnCurrentValue()
    {
        _timer = 0;
        _count.Value++;

        if (_count.Value == 1)
        {
            _currentValue = _multiplicationValue;
        }
        else
        {
            _currentValue = Mathf.Min(_currentValue *= 2, _limit / 2);
        }
        Debug.Log($"現在の乗算値 {_currentValue + 1}");

        return _currentValue + 1;
    }
}