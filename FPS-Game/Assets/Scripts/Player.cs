using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;


    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Camera")]
    public float lookSensitivity;
    public float maxLookX;
    public float minLookX;
    private float rotx;

    private Camera cam;
    private Rigidbody rig;

    private Weapon weapon;

    void Awake()
    {
        // get the components
        cam = Camera.main;
        rig = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();

        // disable cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        // initialize the UI
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
        GameUI.instance.UpdateScoreText(0);
        GameUI.instance.UpdateAmmoText(weapon.curAmmo, weapon.maxAmmo);
    }

    void Update()
    {
        // don't do anything if game is paused
        if (GameManager.instance.gamePaused == true)
        {
            return;
        }


        Move();
        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }

        if (Input.GetButton("Fire1"))
        {
            if (weapon.CanShoot())
            {
                weapon.Shoot();
            }
        }

        CamLook();

        

    }

    void TryJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.1f))
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 dir = transform.right * x + transform.forward * z;
        dir.y = rig.velocity.y;

        rig.velocity = dir;


    }

    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotx += Input.GetAxis("Mouse Y") * lookSensitivity;

        rotx = Mathf.Clamp(rotx,minLookX,maxLookX);

        cam.transform.localRotation = Quaternion.Euler(-rotx,0,0);
        transform.eulerAngles += Vector3.up * y;

    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
        if (curHp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameManager.instance.LoseGame();
    }

    public void GiveHealth(int amountToGive)
    {
        curHp = Mathf.Clamp(curHp + amountToGive, 0, maxHp);
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
    }
    public void GiveAmmo(int amountToGive)
    {
        weapon.curAmmo = Mathf.Clamp(weapon.curAmmo + amountToGive, 0, weapon.maxAmmo);
        GameUI.instance.UpdateAmmoText(weapon.curAmmo, weapon.maxAmmo);
    }

}
