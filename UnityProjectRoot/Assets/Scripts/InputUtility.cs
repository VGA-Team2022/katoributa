using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputUtility : MonoBehaviour
{
    #region 定数
    /// <summary>InputActionAsset上の入力名 : カメラ操作</summary>
    const string _INPUT_ACTION_NAME_CAMERA_MOVE = "Look";

    /// <summary>InputActionAsset上の入力名 : 移動操作</summary>
    const string _INPUT_ACTION_NAME_MOVE = "Move";

    /// <summary>InputActionAsset上の入力名 : 煙を発射</summary>
    const string _INPUT_ACTION_NAME_FIRE = "Fire";

    /// <summary>InputActionAsset上の入力名 : ジャンプ</summary>
    const string _INPUT_ACTION_NAME_JUMP = "Jump";

    /// <summary>InputActionAsset上の入力名 : スタイル切り替え</summary>
    const string _INPUT_ACTION_NAME_SWITCH_STYLE = "SwitchStyle";

    /// <summary>InputActionAsset上の入力名 : ポーズ切り替え</summary>
    const string _INPUT_ACTION_NAME_SWITCH_PAUSE = "Pause";
    #endregion

    #region メンバ
    /// <summary>カメラ操作情報</summary>
    static InputAction _actionCameraMove = null;
    /// <summary>移動操作情報</summary>
    static InputAction _actionMove = null;
    /// <summary>煙を発射操作情報</summary>
    static InputAction _actionFire = null;
    /// <summary>ジャンプ操作情報</summary>
    static InputAction _actionJump = null;
    /// <summary>スタイル切り替え操作情報</summary>
    static InputAction _actionSwitchStyle = null;
    /// <summary>ポーズ切り替え操作情報</summary>
    static InputAction _actionPause = null;
    #endregion

    #region プロパティ
    /// <summary> ポーズボタン押下直後 </summary>
    static public bool GetDownPause { get => _actionPause.triggered; }
    /// <summary> 移動操作直後 </summary>
    static public bool GetDownMove { get => _actionMove.triggered; }
    /// <summary> 移動操作の方向取得 </summary>
    static public Vector2 GetDirectionMove { get => _actionMove.ReadValue<Vector2>(); }
    /// <summary> カメラ操作の方向取得 </summary>
    static public Vector2 GetDirectionCameraMove { get => _actionCameraMove.ReadValue<Vector2>(); }
    /// <summary> ジャンプボタン押下直後 </summary>
    static public bool GetDownJump { get => _actionJump.triggered; }
    /// <summary> ジャンプボタン押下中 </summary>
    static public bool GetJump { get => _actionJump.IsPressed(); }
    /// <summary> 煙を発射ボタン押下直後 </summary>
    static public bool GetDownFire { get => _actionFire.triggered; }
    /// <summary> 煙を発射ボタン押下中 </summary>
    static public bool GetFire { get => _actionFire.IsPressed(); }
    /// <summary> スタイル切り替えボタン押下直後 </summary>
    static public bool GetDownActionSwitch { get => _actionSwitchStyle.triggered; }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //入力を関連付け
        PlayerInput input = GetComponent<PlayerInput>();
        InputActionMap actionMap = input.currentActionMap;
        _actionCameraMove = actionMap[_INPUT_ACTION_NAME_CAMERA_MOVE];
        _actionMove = actionMap[_INPUT_ACTION_NAME_MOVE];
        _actionFire = actionMap[_INPUT_ACTION_NAME_FIRE];
        _actionJump = actionMap[_INPUT_ACTION_NAME_JUMP];
        _actionSwitchStyle = actionMap[_INPUT_ACTION_NAME_SWITCH_STYLE];
        _actionPause = actionMap[_INPUT_ACTION_NAME_SWITCH_PAUSE];
    }
}
