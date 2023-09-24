using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreSpaceJam.Scripts.Shop
{
    public class SaleableUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI price;
    
        public void Display(SaleableData data)
        {
            title.text = data.title;
            description.text = data.description;
            price.text = data.price.ToString();
        }
    }
}
