using TMPro;
using UnityEngine;

public class ShopSelectButton : MonoBehaviour
{
    [SerializeField] private GameObject _paidContainer;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _selectText;

    public void BuyingSkin(int price)
    {
        _selectText.gameObject.SetActive(false);
        _paidContainer.SetActive(true);
        _priceText.text = price.ToString();
    }

    public void SelectSkin(int currentSkinId, int selectableSkinId, string lang)
    {
        _selectText.gameObject.SetActive(true);
        _paidContainer.SetActive(false);
        if (currentSkinId == selectableSkinId)
        {
            _selectText.text = lang == "ru" ? "Выбран" : "Selected";
        }
        else
        {
            _selectText.text = lang == "ru" ? "Выбрать" : "Select";
        }
    }
}
