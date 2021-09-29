using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject scroll = null;
    private void Start()
    {
        scroll.SetActive(false);
    }

    public void OnclickPurchase()
    {
        if (scroll.activeSelf==false)
        {
            scroll.SetActive(true);
            scroll.transform.SetAsLastSibling();
        }
        else if (scroll.activeSelf==true)
        {
            if(scroll.transform.GetSiblingIndex()==3)
            {
                scroll.SetActive(false);
            }
            else
            {
                scroll.transform.SetAsLastSibling();
            }
        }
    }

    public void CloseButton()
    {
        scroll.SetActive(false);
    }
}
