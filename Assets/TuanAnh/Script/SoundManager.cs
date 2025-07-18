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

    private AudioSource playerFootingAudioSource;
    private AudioSource playerGeneralAudioSource;

    private List <AudioSource> allAudioSource;


    private void Awake()
    {
       Instance = this;
    }

    public void Init()
    {
        InitPlayerFootingAudioSource();
        InitPlayerGeneralAudioSource();

    }

    private void InitPlayerGeneralAudioSource()
    {
        playerGeneralAudioSource = gameObject.AddComponent<AudioSource>();
        AddAudioSource(playerGeneralAudioSource);
        playerGeneralAudioSource.loop = false;
        playerGeneralAudioSource.spatialBlend = 0f;
        playerGeneralAudioSource.volume = 1f;

        PlayerController.Instance.EvPlayerCastSpell1 += () => PlaySound(playerCastSpell1);  // Do su kien khong tra ve gia tri, nen () =>
        PlayerController.Instance.EvPlayerCastSpell2 += () => PlaySound(playerCastSpell2);
        PlayerController.Instance.EvPlayerJump += () => PlaySound(playerJumping);
        PlayerController.Instance.EvPlayerLand += () => PlaySound(playerLanding);
        PlayerController.Instance.EvPlayerDie += () => StopAllSounds(); // Khi player chet, tat het am thanh hien tai
    }

    private void InitPlayerFootingAudioSource()
    {
        playerFootingAudioSource = gameObject.AddComponent<AudioSource>();
        AddAudioSource(playerFootingAudioSource);

        playerFootingAudioSource.clip = playerFooting;
        playerFootingAudioSource.loop = true;
        playerFootingAudioSource.volume = 0.5f; 

        PlayerController.Instance.EvPlayerStartRun += PlayerFootingSourceOn;
        PlayerController.Instance.EvPlayerStopRun += PlayerFootingSourceOff;
    }

    
    private void PlayerFootingSourceOn()
    {
        if (playerFootingAudioSource.isPlaying)
        {
            return;
        }
        playerFootingAudioSource.Play();
    }

    private void PlayerFootingSourceOff()
    {
         playerFootingAudioSource.Stop();
    }


    public void PlaySound(AudioClip sound)
    {
        playerGeneralAudioSource.clip = sound;
        playerGeneralAudioSource.Play();
    }

    public void AddAudioSource( AudioSource audioSource)
    {
        if (allAudioSource == null)
        {
            allAudioSource = new List<AudioSource>();
        }
        allAudioSource.Add(audioSource);
    }

    public void RemoveAudioSource(AudioSource audioSource)
    {
        if ( audioSource != null)
        {
            allAudioSource.Remove(audioSource);
        }
    }

    public void StopAllSounds()
    {
        if (allAudioSource == null || allAudioSource.Count == 0)
        {
            return;
        }
        foreach (var audioSource in allAudioSource)
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }

    public void ResetInstance()
    {
        Instance = null;
    }


}




