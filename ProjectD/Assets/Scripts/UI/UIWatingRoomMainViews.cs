using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWatingRoomMainViews : MonoBehaviour
{
    [SerializeField] List<GameObject> mainViews;
    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.Instance.MainViewCount() != mainViews.Count)
        {
            UIManager.Instance.ResetMainView();
            RegisterMainViews();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RegisterMainViews()
    {
        foreach(GameObject mainView in mainViews)
        {
            UIManager.Instance.RegisterMainView(mainView);
        }
    }
}
