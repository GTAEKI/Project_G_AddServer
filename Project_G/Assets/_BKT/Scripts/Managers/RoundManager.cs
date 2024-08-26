using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundManager
{
    public class RoundSetting
    {
        public RoundSetting(int _maxEnemyCount, float _hpMultiple, float _powerMultiple, float _speedMultiple, int scrapCount)
        {
            maxEnemyCount = _maxEnemyCount;
            enemyHpMultiple = _hpMultiple;
            enemyPowerMultiple = _powerMultiple;
            enemySpeedMultiple = _powerMultiple;
            this.scrapCount = scrapCount;
        }

        public int maxEnemyCount;
        public float enemyHpMultiple;
        public float enemyPowerMultiple;
        public float enemySpeedMultiple;
        public int scrapCount;
    }

    private List<RoundSetting> rounds = new List<RoundSetting>();
    public int CurrentRound { get; private set; } = 0;
    public bool IsAllRoundClear { get; private set; } = false;

    public RoundManager()
    {
        rounds.Add(new RoundSetting(30, 1f, 1f, 1f, 400));
        rounds.Add(new RoundSetting(35, 1.8f, 1.1f, 1.1f, 600));
        rounds.Add(new RoundSetting(40, 2f, 1.2f, 1.2f, 800));
        rounds.Add(new RoundSetting(45, 2.2f, 1.3f, 1.3f, 1000));
        rounds.Add(new RoundSetting(45, 2.5f, 1.4f, 1.4f, 1200));
    }

    public RoundSetting GetCurrentRoundSetting() 
    {
        return rounds[CurrentRound];
    }

    public void NextRound() 
    {
        if (CurrentRound < rounds.Count - 1) 
        {
            CurrentRound++;
        }
    }

    public bool HasNextRound() 
    {
        if (CurrentRound == rounds.Count-1)
        {
            return false;
        }

        return true;
    }

    public void RoundAllClear() 
    {
        IsAllRoundClear = true;
    }

    public void ResetRound() 
    {
        CurrentRound = 0;
        IsAllRoundClear = false;
    }
    
}
