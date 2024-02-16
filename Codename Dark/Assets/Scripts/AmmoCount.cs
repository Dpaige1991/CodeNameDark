using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    public TMP_Text ammunitionText;
    public TMP_Text magText;

    public static AmmoCount occurrence;

    private void Awake()
    {
        occurrence = this; 
    }

    public void UpdateAmmoText(int presentAmmunition)
    {
        ammunitionText.text = "Ammo.." + presentAmmunition;
    }

    public void UpdateMagText(int mag)
    {
        magText.text = "Magazines" + mag;
    }
}
