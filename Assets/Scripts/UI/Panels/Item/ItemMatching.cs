using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMatching : MonoBehaviour
{
    public Image iconCountry;
    public TextMeshProUGUI namePlayer;
    private CountryData countryData;

    public void Init(CountryData _countryData)
    {
        this.countryData = _countryData;
        SetupUI();
    }

    private void SetupUI()
    {
        iconCountry.sprite = countryData.icon;
        iconCountry.SetNativeSize();
        namePlayer.text = countryData.name;
    }
}