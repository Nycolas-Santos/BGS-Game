using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.Scripts;
using Game.Core.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public Player PlayerInstance { get; set; }

    public GameSettings gameSettings;

    private void OnEnable()
    {
        PlayerInstance = FindObjectOfType<Player>();
    }


    public void LoadScene(string sceneName, int entranceIndex)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        StartCoroutine(IELoadScene(sceneName, entranceIndex));

    }
    private IEnumerator IELoadScene(string sceneName, int entranceIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        var entrancePoints = FindObjectsOfType<SceneEntrancePoint>();
        foreach (var entrancePoint in entrancePoints)
        {
            if (entrancePoint.EntranceIndex == entranceIndex)
            {
                PlayerInstance.transform.position = entrancePoint.transform.position;
            }
        }
    }
    
    
}
