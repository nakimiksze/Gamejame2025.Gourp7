using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable] public class ItemEntry
{
    [SerializeField] string key;
    [SerializeField] GameObject gameObject;
}
public class ItemManager : MonoBehaviour
{
    [SerializeField] protected GameObject[] items = new GameObject[4];

    void Update()
    {

    }
}
