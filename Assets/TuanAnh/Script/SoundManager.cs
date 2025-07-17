using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip castSpell1;
    [SerializeField] private AudioClip castSpell2;
    [SerializeField] private AudioClip magicBallHitSound;


    [SerializeField] private AudioClip footing;

    private void Awake()
    {
       Instance = this;
    }

    public void PlayCastSpell1()
    {
        AudioSource.PlayClipAtPoint(castSpell1, Camera.main.transform.position);
    }

}




