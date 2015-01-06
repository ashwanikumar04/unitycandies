using UnityEngine;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    static LoadingScreenController instance;
    Transform dots;
    string startText;
    float updateTime = .1f;
    float startTime;
    public static LoadingScreenController Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            Instance.Hide();
            return;
        }
        instance = this;
        instance.startText = "";
        foreach (Transform item in transform)
        {
            if (item.name == "Dots")
            {
                dots = item;
            }
        }
        DontDestroyOnLoad(this);
        instance.startTime = Time.time;
    }


    public void Show()
    {
        instance.startText = "";
        instance.startTime = Time.time;
        if (instanceAvailable())
        {
            instance.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        if (instanceAvailable())
        {
            if (Time.time > instance.startTime + instance.updateTime)
            {
                if (instance.startText.Length == 3)
                {
                    instance.startText = "";
                }
                startText = startText + ".";
                instance.dots.gameObject.guiText.text = startText;
                instance.startTime = Time.time;
            }
        }
    }
    public void Hide()
    {
        if (instanceAvailable())
        {
            instance.gameObject.SetActive(false);
        }
    }

    bool instanceAvailable()
    {
        return instance != null;
    }
}
