using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.Scripts;
using Game.Core.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public Player PlayerInstance { get; set; }

    public GameSettings gameSettings;

    private void OnEnable()
    {
        PlayerInstance = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
