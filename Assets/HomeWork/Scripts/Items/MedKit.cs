using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour, IItem
{
    [SerializeField] private int _value;

    public int Value => _value;

    public void Collect()
    {
        Destroy(gameObject);
    }
}
