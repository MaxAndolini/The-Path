using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;

    public Image itemImage;
    public bool isEmpty;

    public void Reset()
    {
        item = null;
        itemImage.enabled = false;
        isEmpty = true;
    }

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

    public void RedPotion()
    {
        SoundManager.Instance.PlayOneShot("Potion");
        HealthController.Instance.GiveHealth(2);
        Debug.Log("Red Potion kullanıldı!");
    }

    public void BluePotion()
    {
        SoundManager.Instance.PlayOneShot("Potion");
        HealthController.Instance.UseBluePotion();
        Debug.Log("Blue Potion kullanıldı!");
    }
}