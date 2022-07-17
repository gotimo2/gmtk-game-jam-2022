using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicManager : MonoBehaviour
{
    public List<AudioClip> tracks;

    public AudioSource _audioSource;

    private AudioClip activeTrack;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += (arg0, scene) =>
        {
            playMusicForScene();
        };
        playMusicForScene();
    }



    private void playMusicForScene()
    {
        if (tracks[SceneManager.GetActiveScene().buildIndex] == activeTrack)
        {
            return;
        }
        
        _audioSource.clip = activeTrack = tracks[SceneManager.GetActiveScene().buildIndex];
        
        _audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
