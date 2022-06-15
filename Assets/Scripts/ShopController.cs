using DG.Tweening;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shop;
    private GameObject text;

    private void Start()
    {
        text = gameObject.transform.GetChild(0).gameObject;

        var sequence = DOTween.Sequence();
        sequence.Append(text.transform.DOLocalMoveY(-2f, 1f));
        sequence.Append(text.transform.DOLocalMoveY(-1f, 1f));
        sequence.SetUpdate(true);
        sequence.SetLoops(-1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) shop.SetActive(true);
    }
}