using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Space] [Header("Inventory")] public Slot[] inventory;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //FOR INVENTORY
        if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && inventory[0] != null)
        {
            inventory[0].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && inventory[1] != null)
        {
            inventory[1].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) && inventory[2] != null)
        {
            inventory[2].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) && inventory[3] != null)
        {
            inventory[3].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) && inventory[4] != null)
        {
            inventory[4].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) && inventory[5] != null)
        {
            inventory[5].Use();
        }
    }
    
    public void AddInventory(GameObject gameObj, Item item)
    {
        foreach (var i in inventory)
        {
            if (!i.isEmpty) continue;
            //i.itemImage.gameObject.GetComponent<Transform>().position

            /*Sequence animationSequence = DOTween.Sequence();
            animationSequence.Append(gameObj.transform.DOMove(i.itemImage.gameObject.transform.position, 4f)).
                SetEase(Ease.OutSine)
                .OnComplete(() => {
                    Destroy(gameObj);
                    i.Set(item);
                });*/
            break;
        }
    }
}
