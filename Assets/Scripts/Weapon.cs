using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;
    [SerializeField] ParticleSystem muzzleFlashOne;
    [SerializeField] ParticleSystem muzzleFlashTwo;
    [SerializeField] GameObject muzzleFlashThree;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject bloodEffect;
    [SerializeField] GameObject chunkEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] bool fullAuto = false;
    [SerializeField] public bool pickedUp = true;
    [SerializeField] public bool chunkBlast = false;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] AudioSource weaponShotAudio;
    [SerializeField] AudioSource noAmmoAudio;
    
    bool canShoot = true;

    void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();

        if(Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if(ammoSlot.GetCurrentAmmo(ammoType) >= 1)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            weaponShotAudio.Play();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        else
        {
            noAmmoAudio.Play();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;

        if (Input.GetMouseButton(0) && fullAuto == true && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    void PlayMuzzleFlash()
    {
        muzzleFlashOne.Play();
        muzzleFlashTwo.Play();
        muzzleFlashThree.SetActive(true);
        Invoke("TurnOffLight", 0.2f);
    }

    void TurnOffLight()
    {
        muzzleFlashThree.SetActive(false);
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            EnemyHealth target = hit.transform.parent.GetComponent<EnemyHealth>();
            if (target == null)
            {
                CreateHitImpact(hit);
                return;
            }
            else if(hit.transform.gameObject.tag == "Head")
            {
                target.TakeDamage(damage, "Head");
            }
            else if(hit.transform.gameObject.tag == "Body")
            {
                target.TakeDamage(damage, "Body");
            }
            bool dead = target.IsDead();
            if (!dead) 
            {
                CreateHitImpactEnemy(hit);
            }
            else return;
        }
        else
        {
            return;
        }
    }

    void CreateHitImpactEnemy(RaycastHit hit)
    {
        if(hit.transform.parent.gameObject.tag == "Enemy")
        {
            if(!chunkBlast)
            {
                GameObject impact = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                Destroy(impact, 0.3f);
            }
            else
            {
                GameObject impact = Instantiate(chunkEffect, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);
                Destroy(impact, 0.5f);
            }
        }
    }

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
}
