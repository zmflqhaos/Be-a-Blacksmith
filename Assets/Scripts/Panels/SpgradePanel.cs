using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Image spImage = null;
    [SerializeField]
    private Sprite[] spSprite = null;

    private Sp sp = null;

    public void SetValue(Sp sp)
    {
        this.sp = sp;
        UpdateValues();
        if(sp.name=="Assistant")
        {
            InvokeRepeating("Assistant", 0f, 0.5f);
        }    
        else if(sp.name=="Shopman")
        {
            InvokeRepeating("Shopman", 0f, 0.5f);
        }
        else if(sp.name=="Scholar")
        {
            InvokeRepeating("Scholar", 0f, 1f);
        }     
        InvokeRepeating("Cost", 0f, 5f);
    }

    public void UpdateValues()
    {
        spImage.sprite = spSprite[sp.number];
        sp.price = 50+50*sp.amount;
        priceText.text = string.Format("{0} C", sp.price);
        nameText.text = string.Format("{0}", sp.name);
        amountText.text = string.Format("{0}", sp.amount);
    }

    public void OnclickPurchase()
    {
        int lev = GameManager.Instance.CurrentUser.level;
        if (GameManager.Instance.CurrentUser.coin < sp.price)
        {
            return;
        }
        foreach(Sp sp in GameManager.Instance.CurrentUser.spList)
        {
            lev -= sp.amount;
        }
        if (lev <= 0) { return; }
        GameManager.Instance.CurrentUser.coin -= sp.price;
        sp.amount++;
        UpdateValues();
        GameManager.Instance.UI.UpdateCoinPanel();
    }

    private void Assistant()
    {
        foreach (Sp sp in GameManager.Instance.CurrentUser.spList)
        {
            if(sp.name=="Assistant")
            {
                GameManager.Instance.CurrentUser.hit += (long)(Mathf.Round(GameManager.Instance.CurrentUser.tgH / 2f) * sp.amount);
                GameManager.Instance.UI.UpdateCoinPanel();
            }       
        }
    }
    private void Shopman()
    {
        foreach (Sp sp in GameManager.Instance.CurrentUser.spList)
        {
            if (sp.name == "Shopman")
            {
                GameManager.Instance.CurrentUser.coin += ((long)Mathf.Round(GameManager.Instance.CurrentUser.tgH / 3)) * sp.amount;
                GameManager.Instance.UI.UpdateCoinPanel();
            }
        }
    }
    private void Scholar()
    {
        foreach (Sp sp in GameManager.Instance.CurrentUser.spList)
        {
            if (sp.name == "Scholar")
            {
                GameManager.Instance.CurrentUser.exp += ((long)Mathf.Round(GameManager.Instance.CurrentUser.tgH / 4)) * sp.amount;
                GameManager.Instance.UI.UpdateExpBar();
            }
        }
    }

    private void Cost()
    {
        GameManager.Instance.CurrentUser.coin -= ((long)Mathf.Round(GameManager.Instance.CurrentUser.tgH / 2)) * sp.amount;
        GameManager.Instance.UI.UpdateCoinPanel();   
    }
}


