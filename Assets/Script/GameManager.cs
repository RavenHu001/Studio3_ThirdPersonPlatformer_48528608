using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float Score=0f;
    private CollisionTrigger[] coins;

    public void Start()
    {
        coins = FindObjectsByType<CollisionTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }
}
