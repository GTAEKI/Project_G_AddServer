using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : MonoBehaviour
{
    public string BaseUrl { get; set; } // ip:port

    public string ip = "127.0.0.1";
    public int port = 7777;

    public void Init()
    {
        BaseUrl = $"http://{ip}:{port}";
        Debug.Log($"BaseUrl : {BaseUrl}");
    }

    public void SendPostRequest<T>(string url, object obj, Action<T> res) 
    {
        Managers.Instance.StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbPOST,obj,res));
    }

    IEnumerator CoSendWebRequest<T>(string url, string method, object obj, Action<T> res) 
    {
        if (string.IsNullOrEmpty(BaseUrl))
            Init();

        string sendUrl = $"{BaseUrl}/{url}";

        byte[] jsonBytes = null;
        if (obj != null) 
        {
            string jsonStr = JsonUtility.ToJson(obj);
            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }

        using (var uwr = new UnityWebRequest(sendUrl, method)) 
        {
            uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log($"CoSendWebRequest Failed : {uwr.error}");
            }
            else 
            {
                T resObj = JsonUtility.FromJson<T>(uwr.downloadHandler.text);
                res.Invoke(resObj);
            }
        }
    }
}
