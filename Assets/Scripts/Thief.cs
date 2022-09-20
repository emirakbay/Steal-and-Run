using UnityEngine;

public class Thief : MonoBehaviour
{
    private int currentGold;

    private bool isRunning = false;
    private bool isStealing = false;
    private bool isLeft = false;
    private bool isRight = false;

    private float stealStreak;

    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public int CurrentGold { get => currentGold; set => currentGold = value; }
    public float StealStreak { get => stealStreak; set => stealStreak = value; }
    public bool IsStealing { get => isStealing; set => isStealing = value; }
    public bool IsLeft { get => isLeft; set => isLeft = value; }
    public bool IsRight { get => isRight; set => isRight = value; }
}
