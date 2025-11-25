using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float _damage = 20f;
    private bool canHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (canHit)
        {

        }
    }

    public void EnableHitDetection()
    {
        Debug.Log("enable");
        canHit = true;
    }

    public void DisableHitDetection()
    {
        Debug.Log("disable");
        canHit = false;
    }
}
