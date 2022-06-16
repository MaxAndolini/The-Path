using System.Collections;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    [Space] [Header("Trampoline")] public float trampolineSpeed = 35.0f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!Menu.Instance.gamePause)
            if (col.CompareTag("Player"))
            {
                SoundManager.Instance.PlayOneShot("Trampoline");
                col.GetComponent<Rigidbody2D>().velocity = Vector2.up * trampolineSpeed;
                anim.SetBool("isPlayerTouch", true);
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Menu.Instance.gamePause)
            if (other.CompareTag("Player"))
                StartCoroutine(CancelTouch());
    }

    private IEnumerator CancelTouch()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isPlayerTouch", false);
    }
}