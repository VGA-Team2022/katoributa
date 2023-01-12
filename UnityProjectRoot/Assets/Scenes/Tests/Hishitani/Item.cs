using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float _heal = 10;
    public int _objectCount;
    [SerializeField] SenkouHealth _senko;
    ItemSpawn _itemSpawn;
    bool _pauseSwich = false;
    private void Start()
    {
        _itemSpawn = GameObject.FindObjectOfType<ItemSpawn>();
        _senko = GameObject.FindGameObjectWithTag("Player").GetComponent<SenkouHealth>();
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnResume += Resume;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_senko)
            {
                Debug.Log("_senko‚É‰½‚à“ü‚Á‚Ä‚È‚¢");
            }
            else
            {
                _itemSpawn._getCount++;
                _itemSpawn.Spawn(_objectCount);
                _senko.GetHeal(_heal);
                Destroy(this.gameObject);
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
