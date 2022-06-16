using DG.Tweening;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Space] [Header("Inventory")] public Slot[] inventory;
    private Transform canvas;

    public static InventoryController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
    }

    private void Update()
    {
        //FOR INVENTORY
        if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && inventory[0] != null)
            inventory[0].Use();
        else if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && inventory[1] != null)
            inventory[1].Use();
        else if ((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) && inventory[2] != null)
            inventory[2].Use();
        else if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) && inventory[3] != null)
            inventory[3].Use();
        else if ((Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) && inventory[4] != null)
            inventory[4].Use();
        else if ((Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) && inventory[5] != null)
            inventory[5].Use();
    }

    public void AddInventory(GameObject gameObj, Item item)
    {
        foreach (var i in inventory)
        {
            if (!i.isEmpty) continue;
            i.isEmpty = false;
            var duplicate = Instantiate(gameObj, canvas);
            duplicate.transform.DOMove(i.itemImage.gameObject.transform.position, 3f)
                .SetEase(Ease.OutSine)
                .OnComplete(() =>
                {
                    Destroy(duplicate);
                    i.Set(item);
                })
                .SetUpdate(true);

            break;
        }
    }

    public void ResetInventory()
    {
        foreach (var i in inventory)
        {
            i.Reset();
        }
    }
}