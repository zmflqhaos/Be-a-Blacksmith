using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpEffect : MonoBehaviour
{
    WpgradePanel wpgrade;

    private void Start()
    {
        wpgrade = FindObjectOfType<WpgradePanel>();
    }

    public void StartingEffect(int a)
    {
        if(a==1)
        {
            TagWeapUp();
        }
        else if(a==2)
        {
            TagArmorUp();
        }
        else if (a == 3)
        {
            TagIronUp();
        }
        else if(a==4)
        {
            TagGoldUp();
        }
    }
    private void TagWeapUp()
    {
        foreach(Wp wp in GameManager.Instance.CurrentUser.wpList)
        {
            if(wp.wpTag == "Weap")
            {
                wp.gExp += wp.fExp / 5;
                wpgrade.UpdateValues();
            }
        }
    }
    private void TagArmorUp()
    {
        foreach (Wp wp in GameManager.Instance.CurrentUser.wpList)
        {
            if (wp.wpTag == "Armor")
            {
                wp.gExp += wp.fExp / 5;
                wpgrade.UpdateValues();
            }
        }
    }
    private void TagIronUp()
    {
        foreach (Wp wp in GameManager.Instance.CurrentUser.wpList)
        {
            if (wp.wpCate == "Iron")
            {
                wp.gCoin += wp.fCoin / 5;
                wpgrade.UpdateValues();
            }
        }
    }
    private void TagGoldUp()
    {
        foreach (Wp wp in GameManager.Instance.CurrentUser.wpList)
        {
            if (wp.wpCate == "Gold")
            {
                wp.gCoin += wp.fCoin / 5;
                wpgrade.UpdateValues();
            }
        }
    }
}
