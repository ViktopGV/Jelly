using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinsContainer : MonoBehaviour
{
    public int Count => _skins.Count;
    [SerializeField] private List<Skin> _skins;

    public Texture GetTextureById(int id) => _skins[id].SkinTexture;
    public int GetPriceById(int id) => _skins[id].Price;
}

[Serializable]
public struct Skin
{
    public int Price;
    public Texture SkinTexture;
}
