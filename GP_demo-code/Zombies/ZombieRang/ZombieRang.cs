using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieRang : MonoBehaviour
{
    //[SerializeField] public Transform Player;
    NavMeshAgent _navMeshAgent;
    [SerializeField] PlayerController _Player;
    public bool stop;
    void Start()
    {
        _Player = FindObjectOfType<PlayerController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = _Player.transform.position - transform.position;
        if (target.magnitude < _navMeshAgent.stoppingDistance)
        {
            float rotate = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg;
            Debug.Log("Angle " + rotate);
            Quaternion angle = Quaternion.AngleAxis(rotate, Vector3.up);
            transform.rotation = angle;
        }
       

        // Quaternion angle = Quaternion.AngleAxis(rotate, Vector3.up);
        // transform.eulerAngles =Vector3.up* rotate;

        _navMeshAgent.SetDestination(_Player.transform.position);
    }
}
