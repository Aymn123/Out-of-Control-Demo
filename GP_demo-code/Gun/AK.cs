using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK : MonoBehaviour
{
    public  AmmoSystem _ammoSystem;

    public Input _input;

    RecoilSystem _recoilSystem;
    [SerializeField] UI_Handler uI_Handler;
    [Header("FireRate")]
    [SerializeField] private float TimeToShoot = 0.3f;
    [SerializeField] private float timer = 1;
    [SerializeField] private float fireRate = 1f;

    [Header("Recoil")]
    [SerializeField] float snappiness;
    [SerializeField] float returnSpeed;
    [SerializeField] float recoilX;
    [SerializeField] float recoilY;
    [SerializeField] float recoilZ;

    [SerializeField]public bool addboilt;

    PlayerControllerShooter _playerControllerShooter;


    [SerializeField] private Transform vfxHit;

    public ZombieMelee _zombieMelee;

    public AudioSource _audio;

    void Start()
    {
        _playerControllerShooter = GetComponent<PlayerControllerShooter>();
        _zombieMelee = FindObjectOfType<ZombieMelee>();
        _input = GetComponent<Input>();
        _recoilSystem = transform.Find("RotationWithCamera/CameraRecoil").GetComponent<RecoilSystem>();
        _ammoSystem = new AmmoSystem(40, 500);
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * fireRate;
        //Debug.Log(timer);

        uI_Handler.UI_Ammo(getCurrentBulitInGun(), getCurrentMaxAmmo());

        _recoilSystem.Recoil(snappiness, returnSpeed);
        Reload();
    }

    public void Reload()
    {
        if (getCurrentBulitInGun() == 0 || _input.Reload)
            _ammoSystem.reloadBulitInGun();
        _input.Reload = false;
    }

    public void Shoot()
    {
        
        if (getCurrentMaxAmmo() <= 0 && getCurrentBulitInGun() <= 0)
            return;
        //Time to Shoot
        if (timer > TimeToShoot)
        {
            _ammoSystem.Shoot();
            _recoilSystem.RecoilFire(recoilX, recoilY, recoilZ);
            SoundManager.PlaySound("Fire_Sound");
            //if GameOject NotEmety
            if (_playerControllerShooter.hitTransform != null)
            {
                switch (_playerControllerShooter.hitTransform.tag)
                {
                    case "enemyMelee":
                        _playerControllerShooter.hitTransform.transform.gameObject.GetComponent<ZombieMelee>().TakeDmg(10);
                        Transform f = Instantiate(vfxHit, _playerControllerShooter.raycastHit.point, Quaternion.LookRotation(_playerControllerShooter.raycastHit.normal));
                        Destroy(f.gameObject, 0.5f);
                        break;
                }
            }
            //rest Timer
            timer *= 0;
        } 
    }

    
    public int getCurrentBulitInGun()
    {
        return _ammoSystem.currentBulitInGun;
    }
    public int getCurrentMaxAmmo()
    {
        return _ammoSystem.currentMaxAmmo;
    }

    public void addAmmo(int ammo)
    {
        
        _ammoSystem.AddAmmo(ammo);
    }
}
