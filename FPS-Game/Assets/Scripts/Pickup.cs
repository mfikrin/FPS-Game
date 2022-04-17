using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PickupType
{
    Health,
    Ammo
}
public class Pickup : MonoBehaviour
{
    public AudioClip pickupSfx;
    public PickupType type;
    public int value;

    [Header("Bobbing")]
    public float rotateSpeed;
    public float bobSpeed;
    public float bobHeight;

    private Vector3 startPos;
    private bool bobbingUp;

    public static bool pickupHealth;
    public static bool pickupAmmo;

    void Start()
    {
        // set the start position
        pickupHealth = false;
        pickupAmmo = false;
        startPos = transform.position;
    }

    void Update()
    {
        // rotating
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        // bob up and down
        Vector3 offset = (bobbingUp == true ? new Vector3(0, bobHeight / 2, 0) : new Vector3(0, -bobHeight / 2, 0));
        transform.position = Vector3.MoveTowards(transform.position, startPos + offset, bobSpeed * Time.deltaTime);
        if (transform.position == startPos + offset) {
            bobbingUp = !bobbingUp;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            switch (type)
            {
                case PickupType.Health:
                    player.GiveHealth(value);
                    pickupHealth = true;
                    break;
                case PickupType.Ammo:
                    player.GiveAmmo(value);
                    pickupAmmo = true;
                    break;
            }

            other.GetComponent<AudioSource>().PlayOneShot(pickupSfx);

            Destroy(gameObject);
        }
    }


}
