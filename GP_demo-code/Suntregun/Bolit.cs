using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolit : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        Destroy(this.gameObject, 3f);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemyMelee")
        {
            other.gameObject.GetComponent<ZombieMelee>().TakeDmg(10);
            _playerController.GetComponent<PlayerController>().Money += 10;
            Destroy(this.gameObject);
        }
    }
}
