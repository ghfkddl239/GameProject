using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> mainViewList;

    private static UIManager _instance;
    private string WaitingRoomViewName = "";

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
        WaitingRoomMainView(WaitingRoomViewName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene("Scenes/" + sceneName);
    }

    public void WaitingRoomMainView(string viewName = "")
    {
        WaitingRoomViewName = viewName;
        foreach (GameObject mainView in mainViewList)
        {
            if (WaitingRoomViewName != mainView.name)
            {
                mainView.SetActive(false);
            }

            if (WaitingRoomViewName == mainView.name)
            {
                mainView.SetActive(true);
            }
        }
    }
}
