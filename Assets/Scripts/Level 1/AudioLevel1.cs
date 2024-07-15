using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLevel1 : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip walkingFloor;
    public AudioClip walkingGrass;

    public AudioClip takePlastik;
    public AudioClip takeKertas;
    public AudioClip takeDaun;

    public AudioClip monsterBreathe;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
