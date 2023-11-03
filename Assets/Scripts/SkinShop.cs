using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkinShop : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Button _toLeft;
    [SerializeField] private Button _toRight;
    [SerializeField] private ShopSelectButton _selectButton;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _skinMaterial;
    [SerializeField] private SkinsContainer _skins;

    private DataHandler _dataHandler;
    private int _currentSkinID;
    private int _currentSkinShop;

    private void Awake()
    {
        _dataHandler = FindObjectOfType<DataHandler>();
        
        
    }

    private void OnEnable()
    {
        DefineCurrentSkinId();
        _currentSkinShop = _currentSkinID;
        _selectButton.SelectSkin(_currentSkinID, _currentSkinShop, _dataHandler.PlayerData.Language.ToLower());
    }

    private void DefineCurrentSkinId()
    {
        if (_renderer.sharedMaterial.Equals(_defaultMaterial))
        {
            _currentSkinID = _skins.Count;
            return;
        }
        for (int i = 0; i < _skins.Count; i++)
        {
            if (_skins.GetTextureById(i) == _renderer.material.mainTexture)
            {
                _currentSkinID = i;
                return;
            }
        }
    }

    public void NextSkin()
    {
        _currentSkinShop++;

        if (_currentSkinShop > _skins.Count)
        {
            _currentSkinShop = 0;
        }

        UpdateSelectButton();

        UpdateSkin();
    }

    public void PreviousSkin()
    {
        _currentSkinShop--;

        if (_currentSkinShop < 0)
        {
            _currentSkinShop = _skins.Count;
        }

        UpdateSelectButton();

        UpdateSkin();

    }

    public void UpdateSelectButton()
    {
        if (_dataHandler.PlayerData.PurchasedSkinsId.Contains(_currentSkinShop) || _currentSkinShop == _skins.Count)
            _selectButton.SelectSkin(_currentSkinID, _currentSkinShop, _dataHandler.PlayerData.Language.ToLower());
        else
            _selectButton.BuyingSkin(_skins.GetPriceById(_currentSkinShop));
    }

    public void UpdateSkin()
    {
        if (_currentSkinShop == _skins.Count)
        {
            _renderer.sharedMaterial = _defaultMaterial;
            return;
        }
        _renderer.sharedMaterial = _skinMaterial;
        _renderer.sharedMaterial.mainTexture = _skins.GetTextureById(_currentSkinShop);
    }

    public void SetSkin(int skinId)
    {
        if (skinId == _skins.Count)
        {
            _renderer.sharedMaterial = _defaultMaterial;
            return;
        }
        _renderer.sharedMaterial = _skinMaterial;
        _renderer.sharedMaterial.mainTexture = _skins.GetTextureById(skinId);
    }

    public void Select()
    {
        if (_dataHandler.PlayerData.PurchasedSkinsId.Contains(_currentSkinShop) == false)
        {
            if (_dataHandler.PlayerData.Coins >= _skins.GetPriceById(_currentSkinShop))
            {
                _dataHandler.ReduceCoins(_skins.GetPriceById(_currentSkinShop));
                _dataHandler.AddSkin(_currentSkinShop);
                _currentSkinID = _currentSkinShop;
                _dataHandler.SetActiveSkinId(_currentSkinShop);

            }
            else
            {
                //print("Нет деняг");
            }
        }
        else
        {
            _currentSkinID = _currentSkinShop;
            _dataHandler.SetActiveSkinId(_currentSkinShop);

        }
        UpdateSelectButton();
    }

    public void Exit()
    {
        SetSkin(_currentSkinID);
        _dataHandler.SavePlayerData();
        gameObject.SetActive(false);
    }
}
