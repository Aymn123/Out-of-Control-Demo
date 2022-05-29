using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Spwonbulit : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform SpwonBulit;
    [SerializeField] private float fireRate=0.4f;
    [SerializeField] private float TimeToShoot = 0.5f;
    float _Time;
    NavMeshAgent _navMeshAgent;
   // zombie zombie;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        shoot();


    }
    public void shoot()
    {
        _Time += Time.deltaTime * fireRate;
        if (_Time > TimeToShoot)
        {
            Instantiate(Bullet, SpwonBulit.position, SpwonBulit.rotation);
            _Time *= 0;
        }
    }

}
