using System;
using UnityEngine;

public class GameManager
{
    public bool IsGameEnded { get; private set; } = true;
    public bool IsGameWin { get; private set; } = false;

    public void GameStart() 
    {
        if (IsGameWin == true) 
        {
            Managers.Round.NextRound();
            Debug.Log($"현재 라운드는 {Managers.Round.CurrentRound}입니다.");
        }

        IsGameEnded = false;
        IsGameWin = false;
        Managers.Projectile.Clear();
    }

    public event Action<Transform> OnSelectHeroSpawnPoint;
    public void SelectHeroSpawnPoint(Transform transform) 
    {
        OnSelectHeroSpawnPoint?.Invoke(transform);
    }


    public event Action OnGameWin;
    public void Win() 
    {
        if (IsGameEnded == false)
        {
            IsGameWin = true;
            int winReward = Managers.Round.GetCurrentRoundSetting().scrapCount;

            UI_WinResult ui_win = Managers.UI.Get<UI_WinResult>();
            ui_win.gameObject.SetActive(true);
            ui_win.DisplayScrap(winReward);

            Managers.HeroSpawn.OnSetUsedSpawnArea();

            Managers.Scrap.AddScrap(winReward);
            OnGameWin?.Invoke();
            Result();
        }
    }


    public event Action OnGameLose;
    public void Lose() 
    {
        if (IsGameEnded == false) 
        {
            Managers.UI.Get<UI_LoseResult>().gameObject.SetActive(true);
            OnGameLose?.Invoke();
            Result();
        }
    }

    public event Action OnGameResult;
    public void Result() 
    {
        IsGameEnded = true;

        OnGameResult?.Invoke();
        Managers.Obj.Clear();
        Managers.Controller.Clear();
        Managers.Map.Clear();
        Managers.Pool.Clear();
        Managers.Projectile.Clear();        
        Clear();

    }

    public void Clear() 
    {
        OnSelectHeroSpawnPoint = null;
        OnGameWin = null;
        OnGameLose = null;
    }

    public void GameResultReset() 
    {
        IsGameWin = false;
    }

}
