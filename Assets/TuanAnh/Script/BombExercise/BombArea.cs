using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (Bomb.instance.countDown)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            Bomb.instance.Activate();
        }


    }

}
