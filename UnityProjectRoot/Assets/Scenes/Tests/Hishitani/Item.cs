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
    [SerializeField] string _soundName = "SE_item get mono";
    bool _pauseSwich = false;
    private void Start()
    {
        _collider = gameObject.GetComponent<Collider>();
        _renderer = gameObject.GetComponent<Renderer>();
        _senko = GameObject.FindGameObjectWithTag("Player").GetComponent<SenkouHealth>();

        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnResume += Resume;
    }
    private void Update()
    {
        if(_collider.enabled == false && _timer > _respawnTime)
        {
            _collider.enabled = true;
            _renderer.enabled = true;
        }
        if(!_pauseSwich)
        {
            _timer += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(!_senko)
            {
                Debug.Log("_senko‚É‰½‚à“ü‚Á‚Ä‚È‚¢");
            }
            else
            {
                _soundPlayer.PlaySound(_soundName);
                Debug.Log("ŒÄ‚Î‚ê‚½");
                _senko.GetHeal(_heal);
                _timer = 0.0f;
                _collider.enabled = false;
                _renderer.enabled = false;
                Debug.Log("‚³‚í‚Á‚½");
            } 
        } 
    }
    void Pause() 
    {
        _pauseSwich = true;
    }
    void Resume()
    {
        _pauseSwich = false;
    }
}
