using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject theMainCamera;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");

        if (theMainCamera == null)
        {
            theMainCamera = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }


    }
    
    void Start(){

    }
}
