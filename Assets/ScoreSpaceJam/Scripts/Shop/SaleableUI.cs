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
            if (data == null) return;
            
            title.text = data.title;
            description.text = data.description;
            price.text = data.price.ToString();
        }
        
        public void Display(SaleableData data, int overridePrice)
        {
            title.text = data.title;
            description.text = data.description;
            price.text = overridePrice.ToString();
        }
    }
}
