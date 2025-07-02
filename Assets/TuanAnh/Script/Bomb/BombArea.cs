using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (Bomb.instance.ActiveBomb)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            Bomb.instance.ActiveBomb = true;
            Debug.Log("Bomb Triggered: " + Bomb.instance.ActiveBomb);
        }


    }

}
