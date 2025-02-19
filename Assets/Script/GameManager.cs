using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score=0f;
    [SerializeField] private TextMeshProUGUI scoreText;
    private CollisionTrigger[] coins;

    public void Start()
    {
        coins = FindObjectsByType<CollisionTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (CollisionTrigger coin in coins)
        {
            coin.getCoin.AddListener(IncrementScore);
        }
    }

    private void IncrementScore() 
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
    
}
