using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CountryConfig", menuName = "Configs/CountryConfig")]
public class CountryConfig : ScriptableObject
{
    public List<CountryData> ListCountryDatas = new List<CountryData>();

    public List<CountryData> GetRandomTenValue()
    {
        List<CountryData> _listTemp = new List<CountryData>();
        ListCountryDatas.ForEach(dt => _listTemp.Add(dt));
        _listTemp.Shuffle();
        List<CountryData> listResult = new List<CountryData>();
        for (int i = 0; i < 10; i++)
        {
            listResult.Add(_listTemp[i]);
        }

        if (listResult.Count > 0)
        {
            return listResult;
        }
        else
        {
            return null;
        }
    }
}

[Serializable]
public class CountryData
{
    public Sprite icon;
    public string name;
}