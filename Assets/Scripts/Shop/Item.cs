using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDate", menuName = "Shop/Item")]
public class Item : ScriptableObject
{
    public string Name = "Item";
    public GameObject Prefab;
    public int price = 500;
}
