using UnityEngine;
using LootLocker.Requests;
using ScoreSpaceJam.Scripts.Utils;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace ScoreSpaceJam.Scripts.Managers
{
    public class LootLockerManager : MonoBehaviourSingleton<LootLockerManager>
    {
        public bool SessionStarted { get; private set; }

        private string playerIdentifier;
        private const string DefaultLeaderboardKey = "default_leaderboard";

        private async void Start()
        {
            await TryStartGuestSession();
        }

        #region Session authentication and validation
        public async Task<bool> TryStartGuestSession()
        {
            LootLockerGuestSessionResponse response = await StartGuestSessionAsync();
            SessionStarted = response.success;

            if (!response.success)
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Failed to start guest session!");
                return SessionStarted;
            }

            Debug.Log($"[{name}]: ".Bold() + $"Guest session successfully started!");
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

        bool ValidateSession()
        {
            if (!SessionStarted)
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Session has not started!");
                return false;
            }

            return true;
        }
        #endregion


        public async Task<bool> SetPlayerName(string playerName)
        {
            if (!ValidateSession())
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Tried to set player name without valid session!");
                return false;
            }

            PlayerNameResponse response = await SetPlayerNameAsync(playerName);

            if (!response.success)
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Failed to set player name!");
                return false;
            }

            Debug.Log($"[{name}]: ".Bold() + $"Player name set to {playerName}!");
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

        public async Task<bool> SubmitToLeaderboard(int score, string leaderboardKey = DefaultLeaderboardKey)
        {
            if (!ValidateSession())
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Tried to send score without valid session!");
                return false;
            }

            LootLockerSubmitScoreResponse response = await SubmitScoreAsync(score, leaderboardKey);

            if (!response.success)
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Failed to send score to leaderboard with key {leaderboardKey}!");
                return false;
            }

            Debug.Log($"[{name}]: ".Bold() + $"Score sent to leaderboard with key {leaderboardKey}!");
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

        // C#: Async methods cannot have ref, in or out parameters.
        // in other words I cannot make this return bool and just have an out param;
        // so this will just return null when it fails.
        public async Task<LootLockerLeaderboardMember[]> GetLeaderboardScores(int entriesCount, string leaderboardKey = DefaultLeaderboardKey)
        {
            if (!ValidateSession())
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Tried to get leaderboard scores without valid session!");
                return null;
            }

            LootLockerGetScoreListResponse response = await GetScoreListAsync(entriesCount, leaderboardKey);

            if (!response.success)
            {
                Debug.LogWarning($"[{name}]: ".Bold() + $"Failed to get leaderboard scores with key {leaderboardKey}!");
                return null;
            }

            return response.items;
        }

        private Task<LootLockerGetScoreListResponse> GetScoreListAsync(int entriesCount, string leaderboardKey)
        {
            TaskCompletionSource<LootLockerGetScoreListResponse> tcs = new();
            LootLockerSDKManager.GetScoreList(leaderboardKey, entriesCount, (response) =>
            {
                tcs.SetResult(response);
            });

            return tcs.Task;
        }
    }
}