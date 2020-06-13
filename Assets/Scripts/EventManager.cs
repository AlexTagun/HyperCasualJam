using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public enum GameState
    {
        Gameplay,
        End
    }

    public static GameState gameState = GameState.Gameplay;

    public static Action OnEndGame;
    public static void HandleOnEndGame()
    {
        OnEndGame?.Invoke();
    }

    // Start is called before the first frame update

}
