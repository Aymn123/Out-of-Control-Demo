using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodDoorState : MonoBehaviour
{
    Animator _animator;
    [SerializeField] Input _input;

    [SerializeField] GameObject _Ui;
    [SerializeField] private int WoodDoorMoney;
    [SerializeField] Text ShowInGame;





    private void Awake()
    {

        
        _Ui.SetActive(false);

    }
    public void OpenDoor()
    {
        this.gameObject.GetComponent<WoodDoorState>().enabled = false;
        Destroy(this.gameObject);
        _Ui.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!enabled) return;
        if (other.tag == "Player")
        {
            ShowInGame.text = $"Hold E to open the door[Cost:{WoodDoorMoney}]";
            _Ui.SetActive(true);
            _input = other.gameObject.GetComponent<Input>();
            if (_input.OpenDoor)
            {
                if (other.gameObject.GetComponent<PlayerController>().Money >= WoodDoorMoney)
                {
                    other.gameObject.GetComponent<PlayerController>().CalculatorMoney(WoodDoorMoney);
                    SoundManager.PlaySound("DoorOpen_Sound");
                    OpenDoor();
                }
                else
                {
                    SoundManager.PlaySound("DoorClose_Sound");
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
