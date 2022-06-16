using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    public Item item;
    public int cost;
    private GameObject image;

    private void Start()
    {
        if (item == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            image = gameObject.transform.GetChild(0).gameObject;
            image.GetComponent<Image>().sprite = item.itemSprite;
            gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = cost.ToString();
        }
    }

    public void Buy()
    {
        if (PlayerController.Instance.SpendGold(cost)) InventoryController.Instance.AddInventory(image, item);
    }
}