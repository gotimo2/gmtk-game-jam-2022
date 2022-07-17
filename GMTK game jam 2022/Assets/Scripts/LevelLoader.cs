using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator animator;
    public static LevelLoader SLevelLoader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        SLevelLoader = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transition(string scene, float time = 0.5F)
    {
        StartCoroutine(LoadLevel(scene, time));
    }

    IEnumerator LoadLevel(String scene, float time = 0.5F)
    {

        yield return new WaitForSeconds(time);
            
        animator.SetTrigger("StartSceneTransition");

        yield return new WaitForSeconds(0.5F);
        
        SceneManager.LoadScene(scene);
    }
}
