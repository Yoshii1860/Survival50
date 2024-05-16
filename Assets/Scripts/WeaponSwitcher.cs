using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    [SerializeField] Weapon pistol;
    [SerializeField] Weapon shotgun;
    [SerializeField] Weapon rifle;
    [SerializeField] Weapon sniper;
    [SerializeField] AudioSource switcher;


    void Start() 
    {
        SetWeaponActive(0);    
    }

    void Update() 
    {

        ProcessKeyInput();
        ProcessScrollWheel();

    }

    void ProcessScrollWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
            SetWeaponActivePlus();
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
            SetWeaponActiveMinus();
        }
    }

    void ProcessKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && pistol.pickedUp)
        {
            currentWeapon = 0;
            SetWeaponActive(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && shotgun.pickedUp)
        {
            currentWeapon = 1;
            SetWeaponActive(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && rifle.pickedUp)
        {
            currentWeapon = 2;
            SetWeaponActive(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) && sniper.pickedUp)
        {
            currentWeapon = 3;
            SetWeaponActive(1);
        }
    }

    void SetWeaponActivePlus()
    {
        int weaponIndex = 0;
        switcher.Stop();
        switcher.Play();
        foreach(Transform weapon in transform)
        {
            if(weaponIndex == currentWeapon && weapon.gameObject.GetComponent<Weapon>().pickedUp == true)
            {
                weapon.gameObject.SetActive(true);
            }
            else if(weaponIndex == currentWeapon && weapon.gameObject.GetComponent<Weapon>().pickedUp == false)
            {
                currentWeapon++;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    void SetWeaponActiveMinus()
    {
        int weaponIndex = 0;
        switcher.Stop();
        switcher.Play();
        foreach(Transform weapon in transform)
        {
            if(weaponIndex == currentWeapon && weapon.gameObject.GetComponent<Weapon>().pickedUp == true)
            {
                weapon.gameObject.SetActive(true);
            }
            else if(weaponIndex == currentWeapon && weapon.gameObject.GetComponent<Weapon>().pickedUp == false)
            {
                currentWeapon--;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    void SetWeaponActive(int num)
    {
        int weaponIndex = 0;
        if (num != 0)
        {
            switcher.Stop();
            switcher.Play();
        }
        foreach(Transform weapon in transform)
        {
            if(weaponIndex == currentWeapon && weapon.gameObject.GetComponent<Weapon>().pickedUp == true)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}
