using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HitPool : MonoBehaviour
{
    private Text hitText = null;
    public void Show(Vector2 mousePosition)
    {
        hitText = GetComponent<Text>();
        hitText.text = string.Format("+{0}H", GameManager.Instance.CurrentUser.tgH*GameManager.Instance.CurrentUser.claerending);

        gameObject.SetActive(true);
        transform.SetParent(GameManager.Instance.Canvas.transform);
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPositionY = rectTransform.anchoredPosition.y + 100f;

        hitText.DOFade(0f, 0.5f).OnComplete(() => Despawn());
        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
    }

    private void Despawn()
    {
        hitText.DOFade(1f, 0f);
        transform.SetParent(GameManager.Instance.Pool);
        gameObject.SetActive(false);
    }
}

