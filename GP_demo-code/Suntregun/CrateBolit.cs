using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBolit : MonoBehaviour
{
    [SerializeField] GameObject Bolit;
    [SerializeField] Transform Respwon;

    [SerializeField] float BolitRate;
    float time;

    [SerializeField] Suntregun _Suntregun;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip Shoot;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (_Suntregun.GetComponent<Suntregun>().ShortEnemy < 7)
        {
            if (BolitRate < time)
            {
            Instantiate(Bolit, Respwon.transform.position, Respwon.rotation);
            time *= 0f;
            _audioSource.PlayOneShot(Shoot);
            }
        }

    }
}
