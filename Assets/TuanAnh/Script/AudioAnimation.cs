using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioName
{
    SpellCast,
    Kick,
}

public class AudioAnimation : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
       audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudio(int index)
    {
        if (index < 0 || index >= audioClips.Length)
        {
            Debug.LogWarning("Audio index out of range.");
            return;
        }
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }   



}
