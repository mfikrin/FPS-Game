using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public Transform[] pos;
    public int tag;
    private float tempTime;
    [SerializeField]
    public MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }

    

    // Start is called before the first frame update
    void Start()
    { 
        Spawn(tag);
    }

    void Update()
    {
        if (Pickup.pickupAmmo)
        {
            Spawn(0);
            Pickup.pickupAmmo = false;
        }
        else if (Pickup.pickupHealth)
        {
            Spawn(1);
            Pickup.pickupHealth = false;
        }
    }

    // Update is called once per frame
    void Spawn(int spawnEnemyTag)
    {
        int idx = Random.Range(0, pos.Length);
        Instantiate(Factory.FactoryMethod(spawnEnemyTag), pos[idx].position, Quaternion.identity);
    }
}
