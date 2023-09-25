using System.Collections;
using System.Collections.Generic;
using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardButton : MonoBehaviour
{
    [SerializeField] Button button;

    void Update()
    {
        button.interactable = LootLockerManager.Instance.SessionStarted;
    }
}
