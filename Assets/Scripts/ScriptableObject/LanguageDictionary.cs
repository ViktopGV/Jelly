using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "Localization/Dictionary", order = 0)]
public class LanguageDictionary : ScriptableObject
{
    public KeyValue[] Dictionary;

    public string this[string key]
    {
        get
        {
            for (int i = 0; i < Dictionary.Length; ++i)
            {
                if (Dictionary[i].Key == key) return Dictionary[i].Value;
            }
            return null;
        }
    }
}

[Serializable]
public class KeyValue
{
    public string Key;
    [Multiline]
    public string Value;
}