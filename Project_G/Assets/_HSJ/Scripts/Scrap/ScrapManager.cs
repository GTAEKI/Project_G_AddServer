using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScrapManager
{
    public int Scrap { get; private set; }
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

}
