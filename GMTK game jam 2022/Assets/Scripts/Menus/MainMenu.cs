using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Animator anim = GameObject.Find("Dice").GetComponent<Animator>();
            Debug.Log(anim);
            anim.Play("Dice_Fall");
        Invoke("playDinoHurt", 0.3F);
            
        LevelLoader.SLevelLoader.transition("level1", 1.5F);

    }
    public void Quit()
    {
        Application.Quit();
    }

    private void playDinoHurt()
    {
        GameObject dino = GameObject.Find("Dino");
        Animator anim = dino.GetComponent<Animator>();
        anim.Play("DinoHurt");
        dino.GetComponent<AudioSource>().Play();
    }
}
