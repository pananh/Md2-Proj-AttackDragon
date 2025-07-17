using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip playerCastSpell1;
    [SerializeField] private AudioClip playerCastSpell2;
    [SerializeField] private AudioClip playerFooting;
    [SerializeField] private AudioClip playerLanding;
    [SerializeField] private AudioClip playerJumping;

    private AudioSource playerFootingSource;

    private void Awake()
    {
       Instance = this;
    }

    public void Init()
    {
        InitPlayerFootingSource();
        PlayerController.Instance.EvPlayerCastSpell1 += () => PlaySound(playerCastSpell1);  // Do su kien khong tra ve gia tri, nen () =>
        PlayerController.Instance.EvPlayerCastSpell2 += () => PlaySound(playerCastSpell2);
        PlayerController.Instance.EvPlayerJump += () => PlaySound(playerJumping);
        PlayerController.Instance.EvPlayerLand += () => PlaySound(playerLanding);


    }

    private void InitPlayerFootingSource()
    {
        playerFootingSource = gameObject.AddComponent<AudioSource>();
        playerFootingSource.clip = playerFooting;
        playerFootingSource.loop = true;
        playerFootingSource.volume = 0.5f; 

        PlayerController.Instance.EvPlayerStartRun += PlayerFootingSourceOn;
        PlayerController.Instance.EvPlayerStopRun += PlayerFootingSourceOff;
    }

    
    private void PlayerFootingSourceOn()
    {
        if (playerFootingSource.isPlaying)
        {
            return;
        }
        playerFootingSource.Play();
    }

    private void PlayerFootingSourceOff()
    {
         playerFootingSource.Stop();
    }


    public void PlaySound(AudioClip sound)
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
    }

}




