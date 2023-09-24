using ScoreSpaceJam.Scripts.Managers;
using ScoreSpaceJam.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreSpaceJam.Scripts.UI.LootLockerDemoScene
{
    public class LootLockerDemoUI : MonoBehaviour
    {
        [SerializeField] private LootLockerManager lootlockerManager;

        [Header("Login")]
        [SerializeField] private GameObject loginGameObject;
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private Button confirmLoginButton;

        [Header("Submit Score")]
        [SerializeField] private GameObject submitScoreGameObject;
        [SerializeField] private TMP_InputField scoreInputField;
        [SerializeField] private Button submitScoreButton;

        [Header("Leaderboard")]
        [SerializeField] private GameObject leaderboardGameObject;

        public async void OnPlayerLogin()
        {
            confirmLoginButton.interactable = false;
            await lootlockerManager.TryStartGuestSession();
            if (!lootlockerManager.SessionStarted)
            {
                Debug.LogWarning($"[{this.name}]: ".Bold() + $"Failed to start guest session! Cancelling interface flow.");
                confirmLoginButton.interactable = true;
                return;
            }

            // since lootlocker remembers the last set player name, only change the name if it is not null or empty
            if (!string.IsNullOrEmpty(nameInputField.text))
            {
                bool success = await lootlockerManager.SetPlayerName(nameInputField.text);
                if (!success)
                {
                    Debug.LogWarning($"[{this.name}]: ".Bold() + $"Failed to set player name! Cancelling interface flow.");
                    confirmLoginButton.interactable = true;
                }
            }

            submitScoreGameObject.SetActive(true);
            loginGameObject.SetActive(false);
        }

        public async void OnSubmitPlayerScore()
        {
            submitScoreButton.interactable = false;
            if (!lootlockerManager.SessionStarted)
            {
                Debug.LogWarning($"[{this.name}]: ".Bold() + $"Tried to send score without a valid session! Cancelling interface flow.");
                submitScoreButton.interactable = true;
                return;
            }

            if (!int.TryParse(scoreInputField.text, out int castedScore))
            {
                Debug.LogWarning($"[{this.name}]: ".Bold() + $"Failed to parse score from input field! Cancelling interface flow.");
                submitScoreButton.interactable = true;
                return;
            }

            bool success = await lootlockerManager.SubmitToLeaderboard(castedScore);
            if (!success)
            {
                Debug.LogWarning($"[{this.name}]: ".Bold() + $"Failed to send score! Cancelling interface flow.");
                submitScoreButton.interactable = true;
                return;
            }

            leaderboardGameObject.SetActive(true);
            submitScoreGameObject.SetActive(false);
        }
    }
}
