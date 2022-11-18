using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed= 70f;
    public GameObject impactEffect;
    public void seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float deltaMove = Time.deltaTime * speed;

        if (dir.magnitude <= deltaMove)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(dir.normalized*deltaMove,Space.World);
    }

    private void HitTarget()
    {
        GameObject efffectins = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(efffectins,2f);
        Destroy(gameObject);
        Destroy(target.gameObject);
    }
    
}
