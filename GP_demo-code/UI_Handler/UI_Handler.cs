using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    public Text _Ammo;
    public Text _Bulit;

    public Text _Money;

    public Text _Waves;

    public void UI_Ammo(int bulit,int ammo)
    {
        _Ammo.text = $"{ammo}";
        _Bulit.text= $"{bulit}";
    }
    public void UI_Money(int money)
    {
        _Money.text = $"${money}";
    }
    public void UI_Waves(int wave)
    {
        _Waves.text = $"{wave}";
    }
}
