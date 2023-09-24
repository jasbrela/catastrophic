using LootLocker.Requests;
using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.UI.LootLockerDemoScene
{
    public class LeaderboardUI : MonoBehaviour
    {
        [SerializeField] private LootLockerManager lootLockerManager;
        [SerializeField] private LeaderboardEntryUI entryPrefab;
        [SerializeField] private RectTransform entriesHolder;

        void OnEnable()
        {
            InstanceEntries();
        }

        private async void InstanceEntries()
        {
            foreach (RectTransform child in entriesHolder)
            {
                Destroy(child.gameObject);
            }

            LootLockerLeaderboardMember[] members = await lootLockerManager.GetLeaderboardScores(10);

            foreach (LootLockerLeaderboardMember member in members)
            {
                LeaderboardEntryUI newEntry = Instantiate(entryPrefab, entriesHolder);

                newEntry.memberRank.text = member.rank.ToString();
                newEntry.memberName.text = member.player.name;
                newEntry.memberScore.text = member.score.ToString();
            }
        }
    }
}
