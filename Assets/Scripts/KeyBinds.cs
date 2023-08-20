using UnityEngine;

public class KeyBinds : MonoBehaviour
{
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode actionKey = KeyCode.F;

    public static KeyCode sprint;
    public static KeyCode action;
    public static int lmb = 0;
    public static int rmb = 1;

    private void Awake()
    {
        sprint = sprintKey;
        action = actionKey;
    }
}