using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Weapon Date")]
    public float range = 5f;
    public float shootingRate = 1;
    private float shootingCountDown = 0f;
    
    [Header("Unity SetUp Field")]
    public Transform partToRotate;
    public Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private string tagString = "Enemy";
    private GameObject[] enemies;
    public float rotateSpeed = 10f;
    
    void Start()
    {
        InvokeRepeating("TargetUpdate",0f,0.5f);
    }

    void TargetUpdate()
    {
        GameObject nearestEnemy = null;
        float shorestDistence = Mathf.Infinity;
        enemies = GameObject.FindGameObjectsWithTag(tagString);
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= shorestDistence)
            {
                shorestDistence = distance;
                nearestEnemy = enemy;
                Debug.Log(shorestDistence);
            }
        }

        if (shorestDistence <= range && nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
            return;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime*rotateSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
        
        if (shootingCountDown <= 0f)
        {
            shoot();
            shootingCountDown = 1f / shootingRate;
        }

        shootingCountDown -= Time.deltaTime;
    }

    private void shoot()
    {
        Debug.Log("SHOOT!");
        GameObject BulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = BulletGo.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.seek(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
