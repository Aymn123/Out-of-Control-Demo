using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip Fire_Sound, EnemyDeath_Sound, Steps_Sounds, PlayerHit_Sound, PlayerDie_Sound, ZombieHit_Sound, DoorClose_Sound, DoorOpen_Sound, FootSteps_Sound, BuyAmmo_Sound;
    static AudioSource _audioSrc;
    void Start()
    {
        Fire_Sound = Resources.Load<AudioClip>("Sounds/_ak47");
        DoorClose_Sound = Resources.Load<AudioClip>("Sounds/DoorClose_Sound");
        DoorOpen_Sound = Resources.Load<AudioClip>("Sounds/DoorOpen_Sound");
        FootSteps_Sound = Resources.Load<AudioClip>("Sounds/FootSteps_Sound");
        BuyAmmo_Sound = Resources.Load<AudioClip>("Sounds/BuyAmmo_Sound");
        _audioSrc = GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    

    public static void PlaySound(string name)
    {
        switch (name)
        {
            case "Fire_Sound":
                _audioSrc.PlayOneShot(Fire_Sound);
                break;

            case "DoorClose_Sound":
                _audioSrc.PlayOneShot(DoorClose_Sound);
                break;

            case "DoorOpen_Sound":
                _audioSrc.PlayOneShot(DoorOpen_Sound);
                break;
            case "FootSteps_Sound":
                _audioSrc.PlayOneShot(FootSteps_Sound);
                break;
            case "BuyAmmo_Sound":
                _audioSrc.PlayOneShot(BuyAmmo_Sound);
                break;
        }
    }
}
