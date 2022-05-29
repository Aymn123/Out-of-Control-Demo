using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (!enabled)
            return;
        
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<PlayerController>())
            {
                Debug.Log("dmg");
                other.GetComponent<PlayerController>()._healthSystem.Dmg(10);
            }
                
            else if(other.GetComponent<Suntregun>())
            {
                other.GetComponent<Suntregun>().TakeDmg(100);
                //Debug.Log("hit turret");
            }
            
        }
    }
}
