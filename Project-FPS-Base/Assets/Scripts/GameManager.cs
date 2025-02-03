using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public float Score { get; set; }

    public void AddScore(float score)
    {
        Score += score;
        Debug.Log($"Score: {Score}");
    }
}
