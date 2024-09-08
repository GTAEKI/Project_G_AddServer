using System;
using System.Collections;
using TMPro;
using UnityEngine;
using WebPacket;

public class ScrapManager
{
    // BKT
    private int _scrap;
    public int Scrap 
    {
        get { return _scrap; } 
        private set 
        {
            _scrap = value;
            //SendScrapUpdate();
        }
    }
    // BKT

    public string ScrapTxt { get; private set; }
    public EventHandler addScrapEvent;

    private bool scrapUIActive;
    public ScrapManager()
    {
        Init();
    }

    public void Init()
    {
        Scrap = 500;


        ScrapTxt = Scrap.ToString();
    }

    public int GetCurrentScrap()
    {
        return Scrap;
    }

    public void AddScrap(int scrap)
    {
        Scrap += scrap;
        ScrapTxt = Scrap.ToString();
    }

    public void RemoveScrap(int scrap)
    {
        if (Scrap >= scrap)
        {
            Scrap -= scrap;
        }
        ScrapTxt = Scrap.ToString();
    }

    public bool CheckScrapChange(TMP_Text tmp)
    {
        if(ScrapTxt == tmp.text) { return false; }
        GetCurrentScrapText();
        return true;
    }
    public string GetCurrentScrapText()
    {
            return ScrapTxt;        
    }

    
    IEnumerator InfoTimer()
    {
        float time = 0;
        Managers.UI.Get<UI_ScrapInfo>().gameObject.SetActive(true);

        while (time <= 2)
        {
            time += Time.deltaTime;
            
        }
        Managers.UI.Get<UI_ScrapInfo>().gameObject.SetActive(false);

        yield return null;
    }

    //BKT
    public void SendScrapUpdate() 
    {
        #region WebPacket Test
        ScrapPacketReq req = new ScrapPacketReq()
        {
            userId = "Taek",
            token = "2222",
            scrap = Scrap
        };

        Managers.Web.SendPostRequest<ScrapPacketRes>("scrap/input", req, (result) =>
        {
            if (result == null)
            {
                Debug.Log("Web Response NULL");
                return;
            }

            Debug.Log($"Wep Res : {result.success}");
        });
        #endregion
    }

    private int GetScrapFromDb() 
    {
        int ret = 0;

        ScrapPacketReq req = new ScrapPacketReq()
        {
            userId = "Taek",
            token = "2222",
            scrap = Scrap
        };

        Managers.Web.SendPostRequest<ScrapPacketRes>("scrap/get", req, (result) =>
        {
            if (result == null)
            {
                Debug.Log("Web Response NULL");
                return;
            }

            ret = result.scrap;
            Debug.Log($"Wep Res : {result.success}, Scrap : {result.scrap}");
        });

        return ret;
    }
    //BKT
}
