using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPanel : MonoBehaviour
{
    [SerializeField]
    private Text endNameText = null;
    [SerializeField]
    private Text needText = null;
    [SerializeField]
    private Button activeButton = null;

    private End end = null;

    private void Start()
    {
        if (end.active == true)
            activeButton.gameObject.SetActive(false);
        activeButton.interactable = false;
    }
    public void SetValue(End end)
    {
        this.end = end;
        UpdateValues();
    }

    public void UpdateValues()
    {
        endNameText.text = end.edName;
        needText.text = string.Format("{0}", end.need);
    }

    public void Update()
    {
        if(end.active==false)
        {
            Check1();
            Check2();
            Check3();
            Check4();
            Check5();
        }
    }

    private void Check1()
    {
        if (GameManager.Instance.CurrentUser.level >= 10 && endNameText.text == "����� ��������") 
        {
            activeButton.interactable=true;
        }
    }
    private void Check2()
    {
        if (GameManager.Instance.CurrentUser.level >= 30 && endNameText.text == "������ ��������")
        {
            activeButton.interactable = true;
        }
    }
    private void Check3()
    {
        int count=0;
        if (endNameText.text == "�ܱ��λ�")
        {
            foreach(Wp wp in GameManager.Instance.CurrentUser.wpList)
            {
                if(wp.amount!=0)
                {
                    count++;
                }
                if(count>=2)
                {
                    activeButton.interactable = false;
                    break;
                }
                else if(count==1&&wp.amount>=500)
                    activeButton.interactable = true;
            }
        }
    }
    private void Check4()
    {
        if (GameManager.Instance.CurrentUser.level >= 20 && endNameText.text == "�������")
        {
            foreach(Sp sp in GameManager.Instance.CurrentUser.spList)
            {
                if (sp.amount != 0)
                {
                    activeButton.interactable = true;
                    break;
                }
                else
                    activeButton.interactable = true;
            }
        }
    }
    private void Check5()
    {
        if (endNameText.text == "������ ��������")
        {
            foreach(Wp wp in GameManager.Instance.CurrentUser.wpList)
            {
                if(wp.wpName=="MasterPiece")
                {
                    if(wp.amount>=1)
                    {
                        activeButton.interactable = true;
                    }
                    else
                    {
                        activeButton.interactable = false;
                    }
                }
                    
            }

        }
    }

    public void OnClick()
    {
        end.active = true;
        SaveClean();
        activeButton.gameObject.SetActive(false);
    }
    
    private void SaveClean()
    {
        User user = GameManager.Instance.CurrentUser;
        user.userName = "";
        user.claerending++;
        user.userCount++;
        user.hit=0;
        user.tgH=1;
        user.coin=50;
        user.level=1;
        user.exp=0;
        user.maxExp=10;
        GameManager.Instance.UI.ChooseName();
    }
}
