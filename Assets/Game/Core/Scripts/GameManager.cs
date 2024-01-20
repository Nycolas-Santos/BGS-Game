using System.Collections;
using System.Collections.Generic;
using Game.Core.Scripts;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player PlayerInstance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        PlayerInstance = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
