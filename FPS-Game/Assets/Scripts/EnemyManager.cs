using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int spawnEnemyTag;
    public Transform[] pos;

    [SerializeField]
    public MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn),1f,6f);
    }

    // Update is called once per frame
    void Spawn()
    {
        int idx = Random.Range(0, pos.Length);
        Instantiate(Factory.FactoryMethod(spawnEnemyTag),pos[idx].position,Quaternion.identity);
    }
}
