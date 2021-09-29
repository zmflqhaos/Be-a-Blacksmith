using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string userName;
    public int userCount;
    public long hit;
    public long tgH;
    public long coin;
    public int level;
    public float exp;
    public float maxExp;
    public int claerending;
    public List<Wp> wpList = new List<Wp>();
    public List<Up> upList = new List<Up>();
    public List<Sp> spList = new List<Sp>();
    public List<End> edList = new List<End>();
}

