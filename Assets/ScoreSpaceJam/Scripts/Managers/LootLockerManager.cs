using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using ScoreSpaceJam.Scripts.Utils;
using System.Threading.Tasks;

namespace ScoreSpaceJam.Scripts.Managers
{
    public class LootLockerManager : MonoBehaviour
    {
        public bool SessionStarted { get; private set; } = false;

        private string playerIdentifier = null;
        private const string defaultLeaderboardKey = "dev_leaderboard";

        // REVIEW: eu odeio tudo sobre isso
        public async Task<bool> TryStartGuestSession()
        {
            LootLockerGuestSessionResponse response = await StartGuestSessionAsync();
            SessionStarted = response.success;

            if (!response.success)
            {
                Debug.LogWarning($"[{this.name}]: ".Bold() + $"Failed to start guest session!");
                return SessionStarted;
            }

            Debug.Log($"[{this.name}]: ".Bold() + $"Guest session successfully started!");
            playerIdentifier = response.player_identifier;
            return SessionStarted;
        }

        private Task<LootLockerGuestSessionResponse> StartGuestSessionAsync()
        {
            TaskCompletionSource<LootLockerGuestSessionResponse> tcs = new();
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                tcs.SetResult(response);
            });

            return tcs.Task;
        }

        public async Task<bool> SetPlayerName(string playerName)
        {
            if (!SessionStarted)
            {
                Debug.LogWarning($"[{this.name}]: ".Bold() + $"Tried to set player name without valid session!");
                return false;
            }

            PlayerNameResponse response = await SetPlayerNameAsync(playerName);

            if (!response.success)
            {
                Debug.LogWarning($"[{this.name}]: ".Bold() + $"Failed to set player name!");
                return false;
            }

            Debug.Log($"[{this.name}]: ".Bold() + $"Player name set to {playerName}!");
            return true;
        }

        private Task<PlayerNameResponse> SetPlayerNameAsync(string playerName)
        {
            TaskCompletionSource<PlayerNameResponse> tcs = new();
            LootLockerSDKManager.SetPlayerName(playerName, (response) =>
            {
                tcs.SetResult(response);
            });

            return tcs.Task;
        }

        public async Task<bool> SubmitToLeaderboard(int score, string leaderboardKey = defaultLeaderboardKey)
        {
            if (!SessionStarted)
            {
                Debug.LogWarning($"[{this.name}]: ".Bold() + $"Tried to send score without valid session!");
                return false;
            }

            LootLockerSubmitScoreResponse response = await SubmitScoreAsync(score, leaderboardKey);

            if (!response.success)
            {
                Debug.LogWarning($"[{this.name}]: ".Bold() + $"Failed to send score to leaderboard with key {leaderboardKey}!");
                return false;
            }

            Debug.Log($"[{this.name}]: ".Bold() + $"Score sent to leaderboard with key {leaderboardKey}!");
            return true;
        }

        private Task<LootLockerSubmitScoreResponse> SubmitScoreAsync(int score, string leaderboardKey)
        {
            TaskCompletionSource<LootLockerSubmitScoreResponse> tcs = new();
            LootLockerSDKManager.SubmitScore(playerIdentifier, score, leaderboardKey, (response) =>
            {
                tcs.SetResult(response);
            });

            return tcs.Task;
        }
    }
}