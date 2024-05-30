using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingleTon<UIManager>
{
    List<GameObject> mainViewList = new List<GameObject>();

    private static string WaitingRoomViewName = "";

    // Start is called before the first frame update
    void Start()
    {
        WaitingRoomMainView(WaitingRoomViewName);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void RegisterMainView(GameObject mainView)
    {
        mainViewList.Add(mainView);
    }

    public int MainViewCount()
    {
        return mainViewList.Count;
    }

    public void ResetMainView()
    {
        mainViewList.Clear();
    }
}
