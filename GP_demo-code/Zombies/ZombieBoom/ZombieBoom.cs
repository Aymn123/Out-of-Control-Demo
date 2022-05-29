using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieBoom : MonoBehaviour
{
    SpwonSystem spwonSystem;
    NavMeshAgent _navMeshAgent;
    [SerializeField] PlayerController _Player;
    [SerializeField] [Range (1,7)] float rang=4;
    public bool stop;

    [SerializeField] GameObject boomVfx;
    [SerializeField] GameObject Spwon;

    Vector3 target;
    bool canBoom=true;

    
    void Start()
    {
        _Player = FindObjectOfType<PlayerController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
         target = _Player.transform.position - transform.position;
        
        if (target.magnitude <= _navMeshAgent.stoppingDistance)
        {
            if (canBoom)
            {
                
                Boom(target.magnitude);
            }
           
            
        }

        _navMeshAgent.SetDestination(_Player.transform.position);
    }
   /* void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, rang);
    }*/

    void Boom(float target)
    {
        
        if (target <= rang)
        {

            GameObject BoomVfx=Instantiate(boomVfx, Spwon.transform.position, Quaternion.identity);
            
            _Player._healthSystem.Dmg(30);
            Destroy(BoomVfx, 1);
            Destroy(this.gameObject);
            canBoom = false;
        }

    }
}
