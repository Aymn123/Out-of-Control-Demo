using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAmmoArea : MonoBehaviour
{
    [SerializeField] Input _input;
    [SerializeField] GameObject _Ui;
    [SerializeField] private int AmmoMoney;
    [SerializeField] Text ShowInGame;


    private void Awake()
    { 
        _Ui.SetActive(false);
    }
  

    private void OnTriggerStay(Collider other)
    {
        if (!enabled) return;
        if (other.tag == "Player")
        {
            ShowInGame.text = $"Press E To Buy Ammo[Cost:{AmmoMoney}]";
            _Ui.SetActive(true);
            _input = other.gameObject.GetComponent<Input>();
            if (_input.OpenDoor)
            {
                if (other.gameObject.GetComponent<PlayerController>().Money >= AmmoMoney&&
                    other.gameObject.GetComponent<AK>()._ammoSystem.currentMaxAmmo< other.gameObject.GetComponent<AK>()._ammoSystem.maxAmmo)
                {
                    SoundManager.PlaySound("BuyAmmo_Sound");
                    other.gameObject.GetComponent<PlayerController>().CalculatorMoney(AmmoMoney);
                    other.gameObject.GetComponent<AK>().addAmmo(30);
                }
                _input.OpenDoor = false;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!enabled) return;
        if (other.tag == "Player") _Ui.SetActive(false);
    }
}
