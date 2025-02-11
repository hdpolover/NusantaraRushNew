﻿using UnityEngine;
using UnityEngine.UI;

public class FireHandler : MonoBehaviour
{ 
    public GameObject firePoint;
    public GameObject firePoint1;

    private int chosenPlayerShip;

    public GameObject rocketPrefab;
    public GameObject mgPrefab;
    public GameObject cannonPrefab;

    public bool hasMg;
    public bool hasCannon;
    public bool hasRocket;
    
    public Button mgBtn;
    public Button cannonBtn;
    public Button rocketBtn;

    BulletHandler bh;

    public float bulletForce;

    void Start()
    {
        bh = GameObject.Find(PlayerManager.instance.playerShipNaame).GetComponent<BulletHandler>();
        chosenPlayerShip = PlayerManager.instance.chosen_ship;

        mgBtn = GameObject.Find("Mg").GetComponent<Button>();
        cannonBtn = GameObject.Find("Cannon").GetComponent<Button>();
        rocketBtn = GameObject.Find("Rocket").GetComponent<Button>();
        
        CheckPlayerShip();
    }

    void CheckPlayerShip()
    {
        switch (chosenPlayerShip)
        {
            case 0:
                bh.mgBulletCount = bh.maxMgBulletCount;
                hasMg = true;
                hasCannon = false;
                hasRocket = false;

                rocketBtn.interactable = false;
                bh.rocketBulletCount = 0;
                rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                cannonBtn.interactable = false;
                bh.cannonBulletCount = 0;
                cannonBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                break;
            case 1:
                bh.cannonBulletCount = bh.maxCannonBulletCount;
                hasMg = false;
                hasCannon = true;
                hasRocket = false;

                mgBtn.interactable = false;
                bh.mgBulletCount = 0;
                mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                rocketBtn.interactable = false;
                bh.rocketBulletCount = 0;
                rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                break;
            case 2:
                bh.rocketBulletCount = bh.maxRocketBulletCount;
                hasMg = false;
                hasCannon = false;
                hasRocket = true;

                mgBtn.interactable = false;
                bh.mgBulletCount = 0;
                mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                cannonBtn.interactable = false;
                bh.cannonBulletCount = 0;
                cannonBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                break;
            case 3:
                bh.cannonBulletCount = bh.maxCannonBulletCount;
                bh.mgBulletCount = bh.maxMgBulletCount;
                hasMg = true;
                hasCannon = true;
                hasRocket = false;

                rocketBtn.interactable = false;
                bh.rocketBulletCount = 0;
                rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                break;
            case 4:
                bh.cannonBulletCount = bh.maxCannonBulletCount;
                bh.rocketBulletCount = bh.maxRocketBulletCount;
                hasMg = false;
                hasCannon = true;
                hasRocket = true;

                mgBtn.interactable = false;
                bh.mgBulletCount = 0;
                mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                break;
        }
    }

    public void FireRocket()
    {
        if (bh.rocketBulletCount <= 0)
        {
            rocketBtn.interactable = false;
            rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        } else
        {
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(rocketPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(transform.forward * bulletForce);
            ReduceRocketAmmo();

            Destroy(Temporary_Bullet_Handler, 5.0f);
        }
    }

    public void FireMg()
    {
        if (bh.mgBulletCount <= 0)
        {
            mgBtn.interactable = false;
            mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        } else
        {
            mgBtn.interactable = true;
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(mgPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
            GameObject Temporary_Bullet_Handler1;
            Temporary_Bullet_Handler1 = Instantiate(mgPrefab, firePoint1.transform.position, firePoint1.transform.rotation) as GameObject;
            Temporary_Bullet_Handler1.transform.Rotate(Vector3.left * 90);

            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            Rigidbody Temporary_RigidBody1;
            Temporary_RigidBody1 = Temporary_Bullet_Handler1.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(transform.forward * bulletForce);
            Temporary_RigidBody1.AddForce(transform.forward * bulletForce);
            ReduceMgAmmo();

            Destroy(Temporary_Bullet_Handler, 2.0f);
            Destroy(Temporary_Bullet_Handler1, 2.0f);
        }
    }

    public void FireCannon()
    {
        if (bh.cannonBulletCount <= 0.5f)
        {
            cannonBtn.interactable = false;
            cannonBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        }
        else
        {
            cannonBtn.interactable = true;
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(cannonPrefab, firePoint.transform.position, Quaternion.identity) as GameObject;
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(transform.forward * bulletForce);
            ReduceCannonAmmo();

            Destroy(Temporary_Bullet_Handler, 3.0f);
        }
    }

    public void ReduceMgAmmo()
    {
       bh.mgBulletCount -= 2;
    }

    public void ReduceCannonAmmo()
    {
        bh.cannonBulletCount -= 1;
    }
    public void ReduceRocketAmmo()
    {
        bh.rocketBulletCount -= 1;
    }
}