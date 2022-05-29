using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorState : MonoBehaviour
{
   
    Animator _animator;
    [SerializeField] Input _input;
    [SerializeField] GameObject _Ui;
    [SerializeField] private int DoorMoney;
    [SerializeField] Text ShowInGame;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _Ui.SetActive(false);
    }
    public void OpenDoor()
    {
        _animator.SetBool("OpenDoor", true);
        this.gameObject.GetComponent<DoorState>().enabled=false;
        _Ui.SetActive(false);
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (!enabled) return;
        if (other.tag == "Player")
        {
            ShowInGame.text = $"Hold E to open the door[Cost:{DoorMoney}]";
            _Ui.SetActive(true);

            _input = other.gameObject.GetComponent<Input>();

            if (_input.OpenDoor)
            {
                if (other.gameObject.GetComponent<PlayerController>().Money >= DoorMoney)
                {
                    other.gameObject.GetComponent<PlayerController>().CalculatorMoney(DoorMoney);
                    SoundManager.PlaySound("DoorOpen_Sound");
                    OpenDoor();
                }
                else
                {
                    SoundManager.PlaySound("DoorClose_Sound");
                }
                _input.OpenDoor=false;
            }   
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (!enabled) return;
        if (other.tag == "Player") _Ui.SetActive(false);
    }
}
