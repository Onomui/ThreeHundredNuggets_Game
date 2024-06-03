using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip towerSpawnSound;
    [SerializeField]
    private AudioClip bgAudioClip;
    public AudioSource bgMusic;
    [SerializeField]
    private AudioClip gameOverSound;
    [SerializeField]
    private AudioClip victorySound;

    private void Start()
    {
        PlayBgMusic();
    }
    public void PlayTowerSpawn()
    {
        GameObject soundGameObject = new GameObject("buildSound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(towerSpawnSound);
    }
    public void PlayBgMusic()
    {
        GameObject bgMusicPlayer = new GameObject("bgMusic");
        AudioSource audioSource = bgMusicPlayer.AddComponent<AudioSource>();
        bgMusic = audioSource;
        audioSource.loop = true;
        audioSource.volume = 0.02f;
        audioSource.PlayOneShot(bgAudioClip);
    }
    
    public void PlayGameOver()
    {
        GameObject soundGameObject = new GameObject("gameOverSound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        bgMusic.enabled = false;
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(gameOverSound);
    }

    public void PlayVictory()
    {
        GameObject soundGameObject = new GameObject("victorySound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        bgMusic.enabled = false;
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(victorySound);
    }
}
