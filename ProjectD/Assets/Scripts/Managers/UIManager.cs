using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    private void Awake()
    {
        if (_instance is null)
        {
            _instance = this;
        }    
        else
        {
            _instance = null;
        }
    }

    public static UIManager Intance
    {
        get
        {
            if (_instance is null)
            {
                return null;
            }
            else
            {
                return _instance;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SceneChange(string sceneName)
    {
        print("Scenes/" + sceneName);
        SceneManager.LoadScene("Scenes/" + sceneName);
    }
}
