using UnityEngine;

namespace ScoreSpaceJam.Scripts.Shop
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SaleableData")]
    public class SaleableData : ScriptableObject
    {
        public SaleableData nextSaleable;
        public SaleableType type;
        public string title;
        public string description;
        public int price;
    }
}
