using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    [SerializeField] private Image _image;

    public IItem Item { get; private set; }

    internal void Initialize(IItem item)
    {
        Item = item;
        _image.sprite = item.Icon;
    }
}
