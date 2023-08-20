using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] public GameObject boxHands;
    [SerializeField] public GameObject boxPrefab;

    private void Awake()
    {
        instance = this;
        cursorState(true, false);
    }

    public bool cursorState(bool locked, bool visible)
    {
        Cursor.lockState = (locked == false) ? CursorLockMode.None : CursorLockMode.Locked; 
        Cursor.visible = visible;
        return Cursor.visible;
    }
}
