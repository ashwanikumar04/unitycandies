using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Collections.Specialized;


public class RequestManager : MonoBehaviour
{
    private const int REQUEST_MAX_POST_TIME = 30;
    private Queue<Request> requestQueue = new Queue<Request>();
    private bool queueIsProcessing = false;
    public static RequestManager Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        requestQueue = new Queue<Request>();
    }

    public void AddRequest(Request request)
    {
        PostRequest(request);
    }

    private void PostRequest(Request request)
    {
        request.RequestTme = Time.time;
        requestQueue.Enqueue(request);
        try
        {
            StartCoroutine(ProcessQueue());
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private IEnumerator ProcessQueue()
    {
        if (queueIsProcessing)
            yield break;

        queueIsProcessing = true;
        while (requestQueue.Count > 0)
        {
            Request request = requestQueue.Peek();
            float postTime = Time.time;
            WWW www = new WWW(request.URL);
            while (!www.isDone)
            {
                if (Time.time - postTime > REQUEST_MAX_POST_TIME)
                {
                    break;
                }
                yield return null;
            }
            if (request.OnComplete != null)
            {
                request.OnComplete(www, request.CurrentGameObject);
            }
            requestQueue.Dequeue();
        }
        queueIsProcessing = false;
    }
}

public class Request
{
    public float RequestTme
    {
        get;
        set;
    }

    public string Data
    {
        get;
        set;
    }

    public String URL
    {
        get;
        set;
    }

    public GameObject CurrentGameObject { get; set; }
    public Action<WWW, GameObject> OnComplete
    {
        get;
        set;
    }

}

