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
    private AudioSource bgMusic;
    [SerializeField]
    private AudioClip gameOverSound;

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
        audioSource.PlayOneShot(bgAudioClip);
    }
    public void PlayGameOver()
    {
        GameObject soundGameObject = new GameObject("gameOverSound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        bgMusic.enabled = false;
        audioSource.PlayOneShot(gameOverSound);

    }
}
