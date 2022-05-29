using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suntregun : MonoBehaviour
{
    [SerializeField] Transform LookAt;
    [SerializeField] Transform PartToRotate;
    [SerializeField] float _speedRotate;
    GameObject[] Enemy;
    public float ShortEnemy;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip Die;
    public HealthSystem _healthSystem;

    void Start()
    {
        _audioSource= GetComponent<AudioSource>();
        HealthTurret(100);
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        LookAtEnemy();
        if (LookAt == null)
        {
            GetComponent<CrateBolit>().enabled = false;
            return;
        }
        else
        {
            GetComponent<CrateBolit>().enabled = true;
            Vector3 dir = LookAt.position - transform.position;
            Quaternion lookRotion = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotion, _speedRotate * Time.deltaTime).eulerAngles;
            PartToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
        }
    }

    void LookAtEnemy()
    {
        Enemy = GameObject.FindGameObjectsWithTag("enemyMelee");
        GameObject CloseEnemy = null;
         ShortEnemy = float.MaxValue;
        foreach (GameObject enemy in Enemy)
        {
            float PlayerToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (PlayerToEnemy < ShortEnemy)
            {
                CloseEnemy = enemy;
                ShortEnemy = PlayerToEnemy;
            }
            if (ShortEnemy < 7)
                LookAt = CloseEnemy.transform;
        }
    }

    public void TakeDmg(int dmg)
    {
        _healthSystem.Dmg(dmg);
        if (_healthSystem.GetHealth() <= 0)
            die();
    }
    void die()
    {
        _audioSource.PlayOneShot(Die);
        Destroy(this.gameObject,0.3f);
    }
    public void HealthTurret(int health)
    {
        _healthSystem = new HealthSystem(health);
    }
}
