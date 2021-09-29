using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Text wpNameText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Text needHitText = null;
    [SerializeField]
    private Button purchaseButton = null;


    private Wp wp = null;

    public void SetValue(Wp wp)
    {
        this.wp = wp;
        UpdateValues();
    }

    public void UpdateValues()
    {
        wpNameText.text = wp.wpName;
        amountText.text = string.Format("{0}", wp.amount);
        needHitText.text = string.Format("{0} H", wp.needHit);
    }

    public void OnclickPurchase()
    {
        if (GameManager.Instance.CurrentUser.hit < wp.needHit)
        {
            return;
        }
        GameManager.Instance.CurrentUser.hit -= wp.needHit;
        wp.amount++;
        GameManager.Instance.AddCE(wp.gCoin, wp.gExp);
        UpdateValues();
        GameManager.Instance.UI.UpdateCoinPanel();
    }
}
