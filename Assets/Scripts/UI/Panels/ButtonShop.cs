using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{
    public int idShop;
    public TypeShop typeShop;
}

public enum TypeShop
{
    Skin,
    Horn,
    Trail,
    Pack
}