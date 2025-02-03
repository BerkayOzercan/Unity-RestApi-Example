using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Score { get; set; }

    public void AddScore(int score)
    {
        Score += score;
        Debug.Log($"Score: {Score}");
    }
}
