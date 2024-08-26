using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WorldSpace : InitBase
{
    private Camera _mainCamera;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _mainCamera = Camera.main;

        return true;
    }

    void LateUpdate()
    {
        if (_mainCamera != null)
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
    }
}
