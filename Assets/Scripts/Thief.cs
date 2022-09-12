using UnityEngine;

public class Thief : MonoBehaviour
{
    private int currentGold;
    private bool isRunning = false;
    private float stealStreak;

    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public int CurrentGold { get => currentGold; set => currentGold = value; }
    public float StealStreak { get => stealStreak; set => stealStreak = value; }
}
