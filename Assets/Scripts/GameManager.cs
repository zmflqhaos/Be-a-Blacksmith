using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private User user = null;

    public User CurrentUser { get { return user; } }

    private UIManager uiManager = null;
    public UIManager UI {
        get
        {
            if (uiManager == null)
            {
                uiManager = GetComponent<UIManager>();
            }
            return uiManager;
        }
    }

    private Canvas canvas = null;
    public Canvas Canvas
    {
        get
        {
            if (canvas == null)
            {
                canvas = FindObjectOfType<Canvas>();
            }
            return canvas;
        }
    }

    [SerializeField]
    private Transform poolManager = null;
    public Transform Pool { get { return poolManager; } }

    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.txt";

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        // Application.persistentDataPath
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        LoadFromJson();
    }

    private void Back()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void Start()
    {
        InvokeRepeating("SaveToJson", 1f, 60f);
    }

    private void Update()
    {
        Back();
    }
    public void AddCE(long a, float b)
    {
        user.coin += a;
        user.exp += b;
        GameManager.Instance.UI.UpdateCoinPanel();
        GameManager.Instance.UI.UpdateExpBar();
    }

    private void LoadFromJson()
    {
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
    }

    private void SaveToJson()
    {
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    private void OnApplicationQuit()
    {
        SaveToJson();
    }
}
