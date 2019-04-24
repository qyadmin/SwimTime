using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;


public class ConfigManager : MonoBehaviour
{
    public static ConfigManager GetConfigManager;
    public bool IsLoaded = false;
    public void Awake()
    {
        GetConfigManager = this;
    }

    public DicResources<Sprite> EffectBody = new DicResources<Sprite>();
    public DicResources<GameObject> prefabBody = new DicResources<GameObject>();

    public string[] Body_ConfigTag;
    public void LoadConfig()
    {
        //添加需要配置信息的消息模块
        AddConfig();
        //配置特效资源
        EffectBody.LoadAssets(GlobalData.Success, GlobalData.EffectSuccess);
        EffectBody.LoadAssets(GlobalData.Fail, GlobalData.EffectFail);
        EffectBody.LoadAssets(GlobalData.Lighting, GlobalData.EffectLighting);
        //配置图标资源
        EffectBody.LoadAssets(GlobalData.EnergyIcon, GlobalData.SpriteEnergyIcon);
        //配置预设资源
        prefabBody.LoadAssets(GlobalData.Stage, GlobalData.Stage);
        //配置矿机资源
        prefabBody.LoadAssets(GlobalData.MechArmor, GlobalData.MechArmor);
        IsLoaded = true;
    }

    //主体消息模块
    public EventPatcher<JsonData> ServerBody = new EventPatcher<JsonData>();

    public Dictionary<string, EventPatcher<JsonData>> ConfigBody = new Dictionary<string, EventPatcher<JsonData>>();

    public EventPatcher<JsonData> GetBody(string name)
    {
        EventPatcher<JsonData> obj = null;
        ConfigBody.TryGetValue(name, out obj);
        return obj;
    }

    public void AddConfig()
    {
        foreach (string child in Body_ConfigTag)
        {
            ConfigTag config = new ConfigTag();
            config.Addself(child, ConfigBody);
        }

        foreach (TaskMessage child in AllTaskData)
            TaskMessageBody.Add(child.id, child);
    }

    public void SendMessage(JsonData Data)
    {
        //主体消息分发
        ServerBody.Send(Data);
    }


    //道具颜色组件配置
    public Sprite[] ColorGroup;

    //头像配置信息
    [SerializeField]
    private Sprite[] HeadIamgeGroup;
    [SerializeField]
    private Sprite[] HeadSmallIamgeGroup;

    public void SetIamge(Image headiamge, int nub)
    {
        headiamge.sprite = HeadIamgeGroup[nub - 1];
    }

    public void SetSmallIamge(Image headiamge, int nub)
    {
        headiamge.sprite = HeadSmallIamgeGroup[nub - 1];
    }

    //当前加载装备的矿工id;
    public string kd_id = "-1";
    //当前加载装备的位置信息;
    public string position_id = "-1";
    //记录当前是否为否买状态
    public bool IsBuy = false;
    public void CloseBuy()
    {
        IsBuy = false;
    }

    public EventPatcher<bool> SyncState = new EventPatcher<bool>();

    public void Update()
    {
        SyncState.Send(IsBuy);
    }


    //道具图标配置
    public Sprite[] PropSprite;

    [System.Serializable]
    public class ImageMessage
    {
        public Sprite[] all;
    }

    public ImageMessage[] BodyIamgeProp;

    public void SetImageProp(Image GetImage, string id, string lvl)
    {
        int nub = GetCL.GetColorNub(lvl);
        GetImage.sprite = BodyIamgeProp[nub].all[int.Parse(id) - 1];
    }

    public void SetImagePropColor(Image GetImage, string id, string color)
    {
        GetImage.sprite = BodyIamgeProp[int.Parse(color) - 1].all[int.Parse(id) - 1];
    }


    public Color[] configcolor;
    //颜色信息配置
    public Color GetColor(string colorvalue)
    {
        return configcolor[int.Parse(colorvalue) - 1];
    }

    //矿工动画配置
    [System.Serializable]
    public class spriteBody
    {
        public Sprite[] image;
    }

    public spriteBody[] allanimation = new spriteBody[4];


    //任务配置信息
    public TaskMessage[] AllTaskData;
    private Dictionary<string, TaskMessage> TaskMessageBody = new Dictionary<string, TaskMessage>();
    public TaskMessage GetTaskmessage(string id)
    {
        TaskMessage obj = null;
        TaskMessageBody.TryGetValue(id, out obj);
        return obj;
    }

    [SerializeField]
    private Texture2D[] Car;
    public void SetCarImage(RawImage img, int Num)
    {
        img.texture = Car[Num];
    }
}

[System.Serializable]
public class TaskMessage
{
    public string id;
    public Sprite iamge;
    public string name;
    public string desc;
    public Transform TagWindow;
}


