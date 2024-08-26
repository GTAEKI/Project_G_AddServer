using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeroSpawnController : InitBase,IController
{
    private Ray ray;
    private RaycastHit hit;

    public GameObject SelectedSpawnPoint { get; private set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        RegisterController();

        return true;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && Managers.Game.IsGameEnded == true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer(Define.SpawnPoint);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) == true)
            {
                HeroSpawnArea selectedSpawnArea = hit.transform.GetComponent<HeroSpawnArea>();
                selectedSpawnArea.SelectedArea();
            }
            else 
            {
                Debug.Log("타겟이 아닙니다.");
            }
        }
    }

    public void RegisterController()
    {
        Managers.Controller.Register(this);
    }
}
