using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameStateController", menuName = "ScriptableObjects/GameStateController", order = 51)]
public class GameStateController : ScriptableObject
{
    private bool gameOver;

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    internal void GameOver()
    {
        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {
            Reload();
        });
    }
}
