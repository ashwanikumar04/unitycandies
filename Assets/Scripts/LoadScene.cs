using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{

    float loadTime = 2F;
    float startTime;
    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > loadTime + startTime)
        {
            Application.LoadLevel("Level1");
        }

    }
}
