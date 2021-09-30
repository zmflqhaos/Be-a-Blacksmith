using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text hitText = null;
    [SerializeField]
    private Text hitTextTemplate = null;
    [SerializeField]
    private Text coinText = null;
    [SerializeField]
    private Text levelText = null;
    [SerializeField]
    private GameObject wpPanelTemplate = null;
    [SerializeField]
    private GameObject upPanelTemplate = null;
    [SerializeField]
    private GameObject spPanelTemplate = null;
    [SerializeField]
    private GameObject edPanelTemplate = null;
    [SerializeField]
    private GameObject scrolls = null;
    [SerializeField]
    private Text expText;
    [SerializeField]
    private Image bar;
    [SerializeField]
    private GameObject effect = null;
    [SerializeField]
    private GameObject hammer = null;
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private GameObject inputField = null;
    [SerializeField]
    private Text inputText = null;
    [SerializeField]
    private GameObject resetWp = null;
    [SerializeField]
    private GameObject resetSp = null;
    [SerializeField]
    private GameObject resetUp = null;
    private List<WpgradePanel> wpgradePanelList = new List<WpgradePanel>();
    private List<UpgradePanel> upgradePanelList = new List<UpgradePanel>();
    private List<SpgradePanel> spgradePanelList = new List<SpgradePanel>();
    private List<EndingPanel> edPanelList = new List<EndingPanel>();

    private void Start()
    {
        UpdateCoinPanel();
        CreateWpPanels();
        CreateUpPanels();
        SetSpPanels();
        SetEdPanels();
        UpdateExpBar();
        effect.SetActive(false);
        GameManager.Instance.CurrentUser.tgH = GameManager.Instance.CurrentUser.level;
        if (GameManager.Instance.CurrentUser.claerending == 0)
            GameManager.Instance.CurrentUser.claerending = 1;
        if (GameManager.Instance.CurrentUser.userCount == 0)
            GameManager.Instance.CurrentUser.userCount = 1;
        if(GameManager.Instance.CurrentUser.userName!="")
            SetName();
    }

    private void CreateWpPanels()
    {
        GameObject newPanel = null;
        WpgradePanel newPanelComponent = null;
        foreach (Wp wp in GameManager.Instance.CurrentUser.wpList)
        {
            newPanel = Instantiate(wpPanelTemplate, wpPanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<WpgradePanel>();
            newPanelComponent.SetValue(wp);
            newPanel.SetActive(true);
            wpgradePanelList.Add(newPanelComponent);
        }
    }

    private void CreateUpPanels()
    {
        GameObject newPanel = null;
        UpgradePanel newPanelComponent = null;
        foreach (Up up in GameManager.Instance.CurrentUser.upList)
        {
            newPanel = Instantiate(upPanelTemplate, upPanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<UpgradePanel>();
            newPanelComponent.SetValue(up);
            newPanel.SetActive(true);
            upgradePanelList.Add(newPanelComponent);
        }
    }

    private void SetSpPanels()
    {
        GameObject newPanel = null;
        SpgradePanel newPanelComponent = null;
        foreach (Sp sp in GameManager.Instance.CurrentUser.spList)
        {
            newPanel = Instantiate(spPanelTemplate, spPanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<SpgradePanel>();
            newPanelComponent.SetValue(sp);
            newPanel.SetActive(true);
            spgradePanelList.Add(newPanelComponent);
        }
    }

    private void SetEdPanels()
    {
        GameObject newPanel = null;
        EndingPanel newPanelComponent = null;
        foreach (End end in GameManager.Instance.CurrentUser.edList)
        {
            newPanel = Instantiate(edPanelTemplate, edPanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<EndingPanel>();
            newPanelComponent.SetValue(end);
            newPanel.SetActive(true);
            edPanelList.Add(newPanelComponent);
        }
    }

    public void SetAllPanels()
    {
        GameObject newPanel = null;
        SpgradePanel newPanelComponent1 = null;
        WpgradePanel newPanelComponent2 = null;
        UpgradePanel newPanelComponent3 = null;
        for (int i = 1; i < resetSp.transform.childCount; i++)
        {
            Destroy(resetSp.transform.GetChild(i).gameObject);
        }
        for (int i = 1; i < resetWp.transform.childCount; i++)
        {
            Destroy(resetWp.transform.GetChild(i).gameObject);
        }
        for (int i = 1; i < resetUp.transform.childCount; i++)
        {
            Destroy(resetUp.transform.GetChild(i).gameObject);
        }
        foreach (Sp sp in GameManager.Instance.CurrentUser.spList)
        {
            newPanel = Instantiate(spPanelTemplate, spPanelTemplate.transform.parent);
            newPanelComponent1 = newPanel.GetComponent<SpgradePanel>();
            newPanelComponent1.SetValue(sp);
            newPanel.SetActive(true);
            spgradePanelList.Add(newPanelComponent1);
        }
        foreach (Wp wp in GameManager.Instance.CurrentUser.wpList)
        {
            newPanel = Instantiate(wpPanelTemplate, wpPanelTemplate.transform.parent);
            newPanelComponent2 = newPanel.GetComponent<WpgradePanel>();
            newPanelComponent2.SetValue(wp);
            newPanel.SetActive(true);
            wpgradePanelList.Add(newPanelComponent2);
        }
        foreach (Up up in GameManager.Instance.CurrentUser.upList)
        {
            newPanel = Instantiate(upPanelTemplate, upPanelTemplate.transform.parent);
            newPanelComponent3 = newPanel.GetComponent<UpgradePanel>();
            newPanelComponent3.SetValue(up);
            newPanel.SetActive(true);
            upgradePanelList.Add(newPanelComponent3);
        }
    }

    public void OnClickPlayer()
    {
        for (int i = 0; i < scrolls.transform.childCount; i++)
        {
            Transform trChild = scrolls.transform.GetChild(i);
            trChild.gameObject.SetActive(false);
        }
        GameManager.Instance.CurrentUser.hit+=GameManager.Instance.CurrentUser.tgH* GameManager.Instance.CurrentUser.claerending;
        UpdateCoinPanel();
        StartCoroutine(ClickAni());
        HitPool newText = null;
        if (GameManager.Instance.Pool.childCount > 0)
        {
            newText = GameManager.Instance.Pool.GetChild(0).GetComponent<HitPool>();
        }
        else
        {
            newText = Instantiate(hitTextTemplate, GameManager.Instance.Canvas.transform).GetComponent<HitPool>();
        }
        newText.Show(Input.mousePosition);
    }

    private IEnumerator ClickAni()
    {
        effect.SetActive(true);
        hammer.transform.Rotate(0, 0, 40f);
        yield return new WaitForSeconds(0.1f);
        hammer.transform.Rotate(0, 0, -40f);
        effect.SetActive(false);
    }

    public void UpdateCoinPanel()
    {
           hitText.text = string.Format("{0} H", GameManager.Instance.CurrentUser.hit);
           coinText.text = string.Format("{0} C", GameManager.Instance.CurrentUser.coin);
    }

    public void UpdateExpBar()
    {
        while(GameManager.Instance.CurrentUser.exp >= GameManager.Instance.CurrentUser.maxExp)
        {
            if (GameManager.Instance.CurrentUser.exp >= GameManager.Instance.CurrentUser.maxExp)
            {
                GameManager.Instance.CurrentUser.exp -= GameManager.Instance.CurrentUser.maxExp;
                GameManager.Instance.CurrentUser.level++;
                GameManager.Instance.CurrentUser.tgH = GameManager.Instance.CurrentUser.level;
                GameManager.Instance.CurrentUser.maxExp = (2f * GameManager.Instance.CurrentUser.level) * (2f * GameManager.Instance.CurrentUser.level) * (GameManager.Instance.CurrentUser.level/2) / 2 + 11 - GameManager.Instance.CurrentUser.level;
            }
        }     
        levelText.text = string.Format("LV : {0}", GameManager.Instance.CurrentUser.level);
        expText.text = string.Format("{0} / {1}", GameManager.Instance.CurrentUser.exp, GameManager.Instance.CurrentUser.maxExp);         
        bar.fillAmount = GameManager.Instance.CurrentUser.exp / GameManager.Instance.CurrentUser.maxExp;
    }

    public void ChooseName()
    {
        inputField.SetActive(true);
        nameText.gameObject.SetActive(false);
    }

    private void SetName()
    {
        nameText.gameObject.SetActive(true);
        nameText.text = string.Format("{0} {1}¼¼", GameManager.Instance.CurrentUser.userName, GameManager.Instance.CurrentUser.userCount);
        inputField.SetActive(false);
    }

    public void anjName()
    {
        GameManager.Instance.CurrentUser.userName = inputText.text;
        for (int i = 0; i < GameManager.Instance.CurrentUser.userName.Length; i++)
        {
            if (GameManager.Instance.CurrentUser.userName[i] != '\0' && GameManager.Instance.CurrentUser.userName[i] != ' ')
            {
                SetName();
                break;
            }
        }
    }
}
