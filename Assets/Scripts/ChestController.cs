using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerController.Instance.keyStatus)
        {
            anim.SetTrigger("openChest");
            SoundManager.Instance.PlayOneShot("Chest");
            PlayerController.Instance.ChangeKey(true);
        }
    }
}