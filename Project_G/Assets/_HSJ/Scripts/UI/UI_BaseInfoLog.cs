using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BaseInfoLog : UI_Base
{
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     

    public override bool Init()
    {
        if (base.Init() == false) { return false; }
        Register();
        return true;
    }

    protected override void Register()
    {
        Managers.UI.Register(this);
    }

    private void OnDestroy()
    {
        Managers.UI.Remove<UI_ScrapInfo>();
    }
}
