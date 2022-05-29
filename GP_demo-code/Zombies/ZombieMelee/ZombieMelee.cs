using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public  class  ZombieMelee : MonoBehaviour
{
     public int health=0 ;
    
    NavMeshAgent _navMeshAgent;
    
    public bool stop;
    Animator _anim;

    [SerializeField] Transform ClosePlayer;

    [SerializeField] PlayerController _Player;
    [SerializeField] Hit _hit;
    [SerializeField] Collider co;

    public HealthSystem _healthSystem;

    public SpwonSystem _spwonSystem;
    void Start()
    {
        _Player = FindObjectOfType<PlayerController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _hit= FindObjectOfType<Hit>();
        co= GetComponent<Collider>();
        _healthSystem = new HealthSystem(health);
    }
    
        

    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_healthSystem.GetHealth()+"enemy");
        GameObject[] target = GameObject.FindGameObjectsWithTag("Player");
        GameObject closePlayer = null;
        float shortEnemy = float.MaxValue;
        foreach (GameObject player in target)
        {
            float enemyToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (enemyToPlayer < shortEnemy)
            {
                shortEnemy = enemyToPlayer;
                closePlayer = player;
                
            }
            if (closePlayer == null)
            {
                return;
            } 
        }
        ClosePlayer = closePlayer.transform;

        Vector3 between = ClosePlayer.transform.position - transform.position;

        //Debug.Log(between);
        if (between.magnitude < _navMeshAgent.stoppingDistance)
        {
            float rotate = Mathf.Atan2(between.x, between.z) * Mathf.Rad2Deg;

            Quaternion angle = Quaternion.AngleAxis(rotate, Vector3.up);
            transform.rotation = angle;
            _anim.SetBool("CanAttack", true);
        }
        else
        {
            _anim.SetBool("CanAttack", false);
        }
        _navMeshAgent.SetDestination(ClosePlayer.position);
        
    }

    void die()
    {
            if (!enabled)
                return;

            co.enabled = false;
            _hit.enabled = false;
            _navMeshAgent.enabled = false;
            this.enabled = false;
            _anim.SetBool("Die", true);
            gameObject.tag = "Untagged";
    }

    public void TakeDmg(int dmg)
    {
        if (_healthSystem.GetHealth() <= 0)
        {
            SpwonSystem.currentEnemyAmount -= 1;
            die();
        }
        _healthSystem.Dmg(dmg);
        _Player.Money += 10;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
