/// <summary>
/// �I�u�W�F�N�g�v�[���̃C���^�[�t�F�[�X
/// </summary>
public interface IObjectPool
{
    bool IsActive { get; }
    void DisactiveForInstantiate();
    void Create();
    void Destroy();
}