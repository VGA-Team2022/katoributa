using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /// <summary>
    /// 蚊取り豚のモード
    /// </summary>
    public PlayerMode PlayerMode => _playerMode;
    #endregion

    #region 変数
    float _gameTime;
    int _score;
    PlayerMode _playerMode = PlayerMode.Normal;
    #endregion

    /*ToDo
    スコアの管理
    時間の管理
    ポーズ処理周り
    */

    //コンストラクタ
    public GameManager() 
    {
        Debug.Log("New GameManager");
    }

    /// <summary>
    /// 蚊取り豚のモードを切り替える関数
    /// </summary>
    /// <param name="mode"></param>
    public void PlayerModeChange(PlayerMode mode)
    {
        _playerMode = mode;
        Debug.Log($"モードを切り替えた {mode}");
    }
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
