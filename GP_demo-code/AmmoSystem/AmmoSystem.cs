using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem 
{
    public int maxAmmo;
    public int currentMaxAmmo;

    private int bulitInGun;
    public int currentBulitInGun;
    
    private int Ammothru;
    private int ammo;

    public AmmoSystem(int bulitInGun, int maxAmmo)
    {
        this.maxAmmo = maxAmmo;
        this.bulitInGun = bulitInGun;

        currentBulitInGun = bulitInGun;
        currentMaxAmmo = maxAmmo;

    }
   /* public void getMaxAmmo()
    {
        currentMaxAmmo = maxAmmo;
        if (currentMaxAmmo > maxAmmo)
            currentMaxAmmo = maxAmmo;
    }*/

    public void reloadBulitInGun()
    {
        calculation_AmmoSystem(currentMaxAmmo);
    }

    public void Shoot()
    {
        currentBulitInGun -= 1;
        if (currentBulitInGun <= 0) currentBulitInGun = 0;
    }
    void calculation_AmmoSystem(int maxAmmo)
    {
        int cul = maxAmmo - Remainder_currentBulitInGun(currentBulitInGun);
        int rem = maxAmmo - cul;
        if (cul < 0)
        {
            currentMaxAmmo = 0;
            currentBulitInGun += maxAmmo;
        }
        else
        {
            currentBulitInGun += rem;
            currentMaxAmmo -= rem;
        }
    }
    int Remainder_currentBulitInGun(int num)
    {
        
        return bulitInGun - num;
    }
    
    /*public void Upgrade(int newBulitInGun,int newMaxAmmo)
    {
        this.maxAmmo = newMaxAmmo;
        this.bulitInGun = newBulitInGun;
    }*/

    public void AddAmmo(int addAmmo)
    {
        currentMaxAmmo += addAmmo;
        if (currentMaxAmmo >= maxAmmo)
        {
            currentMaxAmmo = maxAmmo;
        }
    }

   

    











}
