using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    
    public Image itemImage;
    public bool isEmpty;

    private void Start()
    {
        itemImage.enabled = false;
        isEmpty = true;
    }

    public void Set(Item ite)
    {
        item = ite;
        itemImage.enabled = true;
        isEmpty = false;
        itemImage.sprite = item.itemSprite;
    }

    public void Use()
    {
        if (item == null) return;
        
        Invoke(item.itemName, 0);
            
        item = null;
        itemImage.enabled = false;
        isEmpty = true;
    }

    public void Potion()
    {
        Debug.Log("Potion kullanıldı!");
    }
}
