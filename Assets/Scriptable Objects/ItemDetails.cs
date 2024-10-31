using UnityEngine;

[CreateAssetMenu]
public class ItemDetails : ScriptableObject
{
    public string _itemName;
    public Sprite _itemIcon;
    [TextArea]
    public string _itemDescription;
}
