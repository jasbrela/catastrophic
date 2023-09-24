using ScoreSpaceJam.Scripts.Managers;
using ScoreSpaceJam.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreSpaceJam.Scripts.UI.LootLocker
{
    public class LootLockerDemoUI : MonoBehaviour
    {
        [SerializeField] private LootLockerManager lootLockerManager;

        [Header("Login")]
        [SerializeField] private GameObject loginGameObject;
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private Button confirmLoginButton;

        [Header("Leaderboard")]
        [SerializeField] private GameObject leaderboardGameObject;

        public async void OnPlayerLogin()
        {
            confirmLoginButton.interactable = false;
            
            await lootLockerManager.TryStartGuestSession();
            
            if (!lootLockerManager.SessionStarted)
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Failed to start guest session! Cancelling interface flow.");
                confirmLoginButton.interactable = true;
                return;
            }

            if (!string.IsNullOrEmpty(nameInputField.text))
            {
                bool success = await lootLockerManager.SetPlayerName(nameInputField.text);
                if (!success)
                {
                    Debug.LogWarning($"[{name}]: ".Bold() + $"Failed to set player name! Cancelling interface flow.");
                    confirmLoginButton.interactable = true;
                }
            }

            loginGameObject.SetActive(false);
            
            SubmitScoreToLeaderboard();
        }

        private async void SubmitScoreToLeaderboard()
        {
            if (!lootLockerManager.SessionStarted)
            {
                Debug.LogWarning($"[{name}]: ".Bold() +
                                 $"Tried to send score without a valid session! Cancelling interface flow.");
                return;
            }

            var score = PlayerPrefs.GetInt("Score", 0);

            bool success = await lootLockerManager.SubmitToLeaderboard(score);
            
            if (!success)
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Failed to send score! Cancelling interface flow.");
                return;
            }
            
            leaderboardGameObject.SetActive(true);
        }
    }
}
