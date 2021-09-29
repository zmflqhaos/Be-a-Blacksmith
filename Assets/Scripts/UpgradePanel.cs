using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Text upNameText = null;
    [SerializeField]
    private Text buttonText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Button purchaseButton = null;

    private UpEffect upEffect = null;
    private Up up = null;

    public void SetValue(Up up)
    {
        this.up = up;
        upEffect = FindObjectOfType<UpEffect>();
        UpdateValues();
    }

    public void UpdateValues()
    {
        upNameText.text = up.upName;
        buttonText.text = string.Format("Upgrade");
        priceText.text = string.Format("{0} C", up.price);
        amountText.text = string.Format("{0}", up.amount);
    }

    public void OnclickPurchase()
    {
        if (GameManager.Instance.CurrentUser.coin < up.price)
        {
            return;
        }
        GameManager.Instance.CurrentUser.coin -= up.price;
        up.amount++;
        UpdateValues();
        ActEffect();
        GameManager.Instance.UI.UpdateCoinPanel();
    }

    private void ActEffect()
    {
        upEffect.StartingEffect(gameObject.transform.GetSiblingIndex());
    }
}

