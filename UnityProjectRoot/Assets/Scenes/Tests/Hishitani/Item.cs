using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float _heal = 10;
    [SerializeField] float _respawnTime = 1.0f;
    float _timer = 0.0f;
    Collider _collider;
    Renderer _renderer;
    [SerializeField] SenkouHealth _senko;
    [SerializeField] SoundPlayer _soundPlayer;
    private void Start()
    {
        _collider = gameObject.GetComponent<Collider>();
        _renderer = gameObject.GetComponent<Renderer>();
    }
    private void Update()
    {
        if(_collider.enabled == false && _timer > _respawnTime)
        {
            _collider.enabled = true;
            _renderer.enabled = true;
        }
        _timer += Time.deltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _soundPlayer.PlaySound("SE_item get mono");
            Debug.Log("ŒÄ‚Î‚ê‚½");
            _senko.GetHeal(_heal);
            _timer = 0.0f;
            _collider.enabled = false;
            _renderer.enabled = false;
            Debug.Log("‚³‚í‚Á‚½");
        } 
    }
}
