using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{

    //Health system can atached all oject
     public float _helthBar;
     public float _currntHealth;    

     public HealthSystem(float helthBar)
    {
        this._helthBar = helthBar;
        _currntHealth = helthBar;
    }
    public float GetHealth() 
    {
        return _currntHealth;
    }

    public void Dmg(float Dmg)
    {
        _currntHealth -= Dmg;
        if (_currntHealth < 0) _currntHealth = 0; 
    }
    public void IncreeseHealth(float GetHealth)
    {
        _currntHealth += GetHealth;
        if (_currntHealth > _helthBar) _currntHealth = _helthBar;
    }

    
   
}
