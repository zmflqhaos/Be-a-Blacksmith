using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndingPanel : MonoBehaviour
{
    [SerializeField]
    private Text endNameText = null;
    [SerializeField]
    private Text needText = null;
    [SerializeField]
    private Button activeButton = null;
    [SerializeField]
    private GameObject cradit = null;
    [SerializeField]
    private Text endTex = null;

    private End end = null;

    private void Awake()
    {
        cradit.SetActive(false);
    }
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
        if (GameManager.Instance.CurrentUser.level >= 10 && endNameText.text == "평범한 대장장이") 
        {
            activeButton.interactable=true;
        }
    }
    private void Check2()
    {
        if (GameManager.Instance.CurrentUser.level >= 30 && endNameText.text == "유명한 대장장이")
        {
            activeButton.interactable = true;
        }
    }
    private void Check3()
    {
        int count=0;
        if (endNameText.text == "외길인생")
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
        if (GameManager.Instance.CurrentUser.level >= 20 && endNameText.text == "독고다이")
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
        if (endNameText.text == "전설의 대장장이")
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
        ViewCradit();
        //SaveClean();
        activeButton.gameObject.SetActive(false);
    }

    private void SaveClean()
    {
        User user = GameManager.Instance.CurrentUser;
        user.userName = "";
        user.claerending++;
        user.userCount++;
        user.hit = 0;
        user.tgH = 1;
        user.coin = 50;
        user.level = 1;
        user.exp = 0;
        user.maxExp = 10;
        foreach (Wp wp in GameManager.Instance.CurrentUser.wpList) { wp.amount = 0; } 
        foreach (Up up in GameManager.Instance.CurrentUser.upList) { up.amount = 0; }
        foreach (Sp sp in GameManager.Instance.CurrentUser.spList) { sp.amount = 0; }
        GameManager.Instance.UI.ChooseName();
        GameManager.Instance.UI.SetAllPanels();
    }
    private void ViewCradit()
    {
        cradit.SetActive(true);
        endTex.text = string.Format
       ("\n이름 : {0}\n{1}대 대장장이\n레벨 : {2}\n\n만든 장비의 수\n{3}개\n\n업그레이드 한 횟수\n{4}번\n\n고용한 서포터의 수\n{5}명\n\n엔딩\n{6}",
       GameManager.Instance.CurrentUser.userName, GameManager.Instance.CurrentUser.userCount, GameManager.Instance.CurrentUser.level,
       CountMake(), CountUpgrade(), CountSupporter(), endNameText.text);
       endTex.transform.DOMove(new Vector3(0, 0, 0), 10f);
    }

    public void ClickButton()
    {
        SaveClean();
        cradit.SetActive(false);
    }

    private int CountMake()
    {
        int sum = 0;
        foreach(Wp wp in GameManager.Instance.CurrentUser.wpList)
        {
            sum += wp.amount;
        }
        return sum;
    }
    private int CountUpgrade()
    {
        int sum = 0;
        foreach (Up up in GameManager.Instance.CurrentUser.upList)
        {
            sum += up.amount;
        }
        return sum;
    }
    private int CountSupporter()
    {
        int sum = 0;
        foreach (Sp sp in GameManager.Instance.CurrentUser.spList)
        {
            sum += sp.amount;
        }
        return sum;
    }
}
