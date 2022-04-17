using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFactory : MonoBehaviour,IFactory
{
    [SerializeField]
    public GameObject[] pickupPrefab;

    public GameObject FactoryMethod(int tag)
    {
        GameObject pickup = pickupPrefab[tag];
        return pickup;
    }
}
