using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController :InitBase, IController
{
    private Dictionary<string, GameObject> virtualCams= new Dictionary<string,GameObject>();

    public override bool Init()
    {
        if(base.Init()==false)
            return false;

        RegisterController();

        GameObject StartViewCamera = GameObject.Find(Define.EVirtualCamera.StartViewCamera.ToString());
        virtualCams.Add(StartViewCamera.name, StartViewCamera);
        //StartViewCamera.SetActive(true);

        GameObject TopViewCamera = GameObject.Find(Define.EVirtualCamera.TopViewCamera.ToString());
        virtualCams.Add(TopViewCamera.name, TopViewCamera);
        //TopViewCamera.SetActive(true);

        GameObject GameViewCamera = GameObject.Find(Define.EVirtualCamera.GameViewCamera.ToString());
        virtualCams.Add(GameViewCamera.name, GameViewCamera);
        //GameViewCamera.SetActive(true);
        
        StartCoroutine(SwitchToTopViewCamera());

        return true;
    }

    private IEnumerator SwitchToTopViewCamera()
    {
        yield return new WaitForSeconds(0.01f);
        SwitchCamera(Define.EVirtualCamera.StartViewCamera);
        yield return new WaitForSeconds(0.3f);
        SwitchCamera(Define.EVirtualCamera.TopViewCamera);
    }

    public void SwitchCamera(Define.EVirtualCamera changeCam, Action OnCameraSwitchComplete = null)
    {
        string name = changeCam.ToString();
        foreach (var cam in virtualCams)
        {
            if (cam.Key.Equals(name)) 
            {
                cam.Value.SetActive(true);
                continue;
            }

            cam.Value.SetActive(false);
        }

        if (OnCameraSwitchComplete != null)
            StartCoroutine(CoOnCameraSwitchComplete(OnCameraSwitchComplete));
    }

    private IEnumerator CoOnCameraSwitchComplete(Action onCameraChangeComplete)
    {
        yield return new WaitForSeconds(2f);
        onCameraChangeComplete();
    }

    public GameObject GetVirtualCamera(Define.EVirtualCamera camera) 
    {
        return virtualCams[camera.ToString()];
    }

    public void RegisterController()
    {
        Managers.Controller.Register(this);
    }
}
