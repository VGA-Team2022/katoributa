using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Tooltip("リジッドボディコンポーネント")]
    Rigidbody _rb;

    [Tooltip("動いているかどうか")]
    bool _isMove;

    [Tooltip("地面にいるときの重力"), SerializeField]
    float _groundDrag = 0f;

    [Tooltip("空中にいるときの重力"), SerializeField]
    float _airDrag = 0f;

    [Tooltip("プレイヤーの中心点"), SerializeField]
    Vector3 _playerCentor;

    [Tooltip("地面のレイヤー"), SerializeField]
    LayerMask _groundLayer;

    [Tooltip("現状のプレイヤーのモード")]
    int _currentPlayerMode;

    [Tooltip("中心")]
    Vector3 _centor;

    [Tooltip("設置判定を可視化するかどうか")]
    bool _isGroundDebug = true;

    [Tooltip("設置判定のサイズ"), SerializeField]
    Vector3 _groundCollisionSize;

    public bool IsMove => _isMove;

    void Update()
    {
        PlayerStateMethod();
        ControlDrag();
        PlayerPowerUpControll();
    }

    /// <summary>
    /// Updateで回すPlayerのState
    /// </summary>
    void PlayerStateMethod()
    {
        _centor = transform.position + _playerCentor;

        if (InputUtility.GetDirectionMove == Vector2.zero || !IsGround())
        {
            _isMove = false;
        }
        else
        {
            _isMove = true;
        }
    }

    /// <summary>
    /// PowerUpボタンを押されるコントローラー
    /// </summary>
    void PlayerPowerUpControll()
    {
        if (InputUtility.GetDownActionSwitch)
        {
            _currentPlayerMode = _currentPlayerMode++ % 2;
            GameManager.Instance.PlayerModeChange((PlayerMode)_currentPlayerMode);
        }
    }

    /// <summary>
    /// 重力管理
    /// </summary>
    void ControlDrag()
    {
        if (IsGround())
        {
            _rb.drag = _groundDrag;
        }
        else
        {
            _rb.drag = _airDrag;
        }
    }


    /// <summary>
    /// 設置判定
    /// </summary>
    /// <returns>設置しているかどうか</returns>
    public bool IsGround()
    {
        Collider[] collision = Physics.OverlapBox(_centor, _groundCollisionSize, Quaternion.identity, _groundLayer);
        if (collision.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Layer判定のGizmo表示
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isGroundDebug)
        {
            Gizmos.DrawCube(_centor, _groundCollisionSize);
        }
    }
}
