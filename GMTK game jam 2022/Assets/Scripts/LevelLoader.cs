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

    public void transition(string scene)
    {
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(String scene)
    {
        animator.SetTrigger("StartSceneTransition");

        yield return new WaitForSeconds(0.5F);
        
        SceneManager.LoadScene(scene);
    }
}
