//// ==================================================================
//// 作    者：A.R.I.P.风暴洋-宋杨
//// 説明する：商城窗口组件
//// 作成時間：2018-07-27
//// 類を作る：Transform_Shop.cs
//// 版    本：v 1.0
//// 会    社：大连仟源科技
//// QQと微信：731483140
//// ==================================================================
//
//using UnityEngine;
//using UnityEngine.UI;
//using DG.Tweening;
//using System;
//
//[Serializable]
//public class ShopMesasge : KeyData
//{
//    public Transform _parentTrans;
//    public GameObject _listObj;
//    public override void Clear()
//    {
//        if (_listObj != null)
//            foreach (Transform child in _parentTrans)
//                ObjectPool.GetInstance().RecycleObj(child.gameObject);
//        _parentTrans.gameObject.SetActive(false);
//    }
//
//    public void Start()
//    {
//        _parentTrans.gameObject.SetActive(true);
//    }
//}
//
//public class Transform_Shop : MonoBehaviour
//{
//    public Transform_Charge transform_Charge;
//    public ShopMesasge[] Shop;
//    public DicMode<ShopMesasge> Shop_Type;
//    public ToggleEventActionBase[] _typeToggleGroup;
//    public Toggle _machineMuTgl, _machineHuoTgl, _skinTgl, _stageTgl;
//    public Button _shopBtn;
//    public GameObject _MachineToggleGroup;
//    public HttpModel http_buy;
//    public string _iconTypeStr;
//    public string _stagePrefabStr;
//
//    private EventPatcher<bool> _eventPatcher = new EventPatcher<bool>();
//    private DataDic<DataItem.ShopData> _data;
//    private DOTweenPath _skinDoTweenPath, _stageDoTweenPath;
//    private DicBase<Sprite> _iconTypeAssets;
//    private DicBase<GameObject> _stagePrefabAssets;
//    private void Start()
//    {
//        _stagePrefabAssets = ConfigManager.GetConfigManager.prefabBody.GetValueDic(_stagePrefabStr);
//
//        _iconTypeAssets = ConfigManager.GetConfigManager.EffectBody.GetValueDic(_iconTypeStr);
//        ConfigManager.GetConfigManager.prefabBody.GetValueGroup(_stagePrefabStr);
//        Shop_Type = new DicMode<ShopMesasge>(Shop);
//        DataManager.GetDataManager.shop.EventObj.Addlistener(UpdateData);
//        _machineMuTgl.onValueChanged.AddListener((bool b) => { _eventPatcher.Send(b); });
//        _shopBtn.onClick.AddListener(Init);
//    }
//
//    private void Init()
//    {
//        _skinDoTweenPath = _skinTgl.GetComponent<DOTweenPath>();
//        _stageDoTweenPath = _stageTgl.GetComponent<DOTweenPath>();
//
//        foreach (ToggleEventActionBase item in _typeToggleGroup)
//        {
//            item.toggle.isOn = false;
//        }
//        _typeToggleGroup[0].RegistEvent((bool b) =>
//        {
//            if (b)
//            {
//                DoPlayDirection(true);
//                _MachineToggleGroup.SetActive(true);
//                //http_Machine.Get();
//                Debug.Log(_typeToggleGroup[0].toggle.name);
//                _machineMuTgl.onValueChanged.AddListener((bool bb) => { _eventPatcher.Send(bb); });
//                UpdateList(GlobalData.sb);
//                _machineMuTgl.isOn = true;
//                _machineHuoTgl.isOn = false;
//            }
//        });
//        _typeToggleGroup[1].RegistEvent((bool b) =>
//        {
//            if (b)
//            {
//                _MachineToggleGroup.SetActive(false);
//                DoPlayDirection(false);
//                //http_skin.Get();
//                Debug.Log(_typeToggleGroup[1].toggle.name);
//                UpdateSkin(GlobalData.sk);
//            }
//        });
//        _typeToggleGroup[2].RegistEvent((bool b) =>
//        {
//            if (b)
//            {
//                _MachineToggleGroup.SetActive(false);
//                DoPlayDirection(false);
//                //http_stage.Get();
//                Debug.Log(_typeToggleGroup[2].toggle.name);
//                UpdateList(GlobalData.prop);
//            }
//        });
//
//        _typeToggleGroup[0].toggle.isOn = true;
//        this.SendMsg(true);
//        _machineMuTgl.isOn = true;
//        _machineHuoTgl.isOn = false;
//    }
//
//
//    //皮肤
//    public void UpdateSkin(string tag)
//    {
//        Shop_Type.Clear();
//        if (_data == null)
//            return;
//        ShopMesasge itemshop = Shop_Type.GetItem(tag);
//        itemshop.Start();
//        // GameObject go = ObjectPool.GetInstance().GetObj(itemshop._listObj, itemshop._parentTrans);
//        GoodsItem goodsItem = itemshop._parentTrans.GetComponent<GoodsItem>();
//        DataItem.ShopData skindata = _data.GetItem(GlobalData.skin1);
//        goodsItem._priceTxt.text = skindata.price.ToString();
//        goodsItem._nameTxt.text = skindata.name.ToString();
//        goodsItem._chooseBtn.onClick.AddListener(delegate ()
//        {
//            transform_Charge.Buy(skindata, goodsItem._typeIcon.sprite, null, false);
//        });
//
//    }
//
//
//    public void UpdateList(string tag)
//    {
//        Shop_Type.Clear();
//        if (_data == null)
//            return;
//        foreach (DataItem.ShopData item in _data.Data)
//        {
//            ShopMesasge itemshop;
//            GoodsItem goodsItem;
//            string str = null;
//            str = Shop_Type.GetTag(item.tp.Substring(0, 2));
//
//            if (item.tp == GlobalData.sbcoin || item.tp == GlobalData.rmbcoin)
//                str = Shop_Type.GetTag(GlobalData.prop);
//            else
//            {
//                if (str == null)
//                    str = Shop_Type.GetTag(GlobalData.prop);
//            }
//
//            if (tag == str)
//            {
//                itemshop = Shop_Type.GetItem(str);
//                itemshop.Start();
//                GameObject go = ObjectPool.GetInstance().GetObj(itemshop._listObj, itemshop._parentTrans);
//                goodsItem = go.GetComponent<GoodsItem>();
//
//                goodsItem._nameTxt.text = item.name;
//                goodsItem._priceTxt.text = item.price.ToString();
//                GameObject g = null;
//                if (item.tp.Contains(GlobalData.sbfixed))
//                {
//                    g = Instantiate(_stagePrefabAssets.GetTValue(GlobalData.MachineObj_common_green), go.transform);
//                    this.SetTransformByGoodsType(g.transform, new Vector3(0.65f, 0.65f, 0.65f), new Vector3(0, -25, 0), GlobalData.GoodsType.Machine);
//                }
//                else if (item.tp.Contains(GlobalData.sbfloat))
//                {
//                    g = Instantiate(_stagePrefabAssets.GetTValue(GlobalData.MachineObj_common_blue), go.transform);
//                    this.SetTransformByGoodsType(g.transform, new Vector3(0.65f, 0.65f, 0.65f), new Vector3(0, -25, 0), GlobalData.GoodsType.Machine);
//                }
//                else if (item.tp.Contains(GlobalData.rmbfixed))
//                {
//                    g = Instantiate(_stagePrefabAssets.GetTValue(GlobalData.MachineObj_common_orange), go.transform);
//                    this.SetTransformByGoodsType(g.transform, new Vector3(0.65f, 0.65f, 0.65f), new Vector3(0, -25, 0), GlobalData.GoodsType.Machine);
//                }
//                else if (item.tp.Contains(GlobalData.rmbfloat))
//                {
//                    g = Instantiate(_stagePrefabAssets.GetTValue(GlobalData.MachineObj_common_purple), go.transform);
//                    this.SetTransformByGoodsType(g.transform, new Vector3(0.65f, 0.65f, 0.65f), new Vector3(0, -25, 0), GlobalData.GoodsType.Machine);
//                }
//                else if (item.name.Equals(GlobalData.muNeng))
//                {
//                    g = Instantiate(_stagePrefabAssets.GetTValue(GlobalData.muEnergyIcon), go.transform);
//                    this.SetTransformByGoodsType(g.transform, Vector3.one, Vector3.zero, GlobalData.GoodsType.Stage);
//                }
//                else if (item.name.Equals(GlobalData.huoNeng))
//                {
//                    g = Instantiate(_stagePrefabAssets.GetTValue(GlobalData.huoEnergyIcon), go.transform);
//                    this.SetTransformByGoodsType(g.transform, Vector3.one, Vector3.zero, GlobalData.GoodsType.Stage);
//                }
//                else if (item.name.Equals(GlobalData.doubleMuEnergyBuffCard))
//                {
//                    g = Instantiate(_stagePrefabAssets.GetTValue(GlobalData.muEnergyDoubleCard), go.transform);
//                    this.SetTransformByGoodsType(g.transform, Vector3.one, Vector3.zero, GlobalData.GoodsType.Stage);
//                }
//                else if (item.name.Equals(GlobalData.doubleHuoEnergyBuffCard))
//                {
//                    g = Instantiate(_stagePrefabAssets.GetTValue(GlobalData.huoEnergyDoubleCard), go.transform);
//                    this.SetTransformByGoodsType(g.transform, Vector3.one, Vector3.zero, GlobalData.GoodsType.Stage);
//                }
//                else if (item.name.Equals(GlobalData.smallTurmpet))
//                {
//                    g = Instantiate(_stagePrefabAssets.GetTValue(GlobalData.trumpetCard), go.transform);
//                    this.SetTransformByGoodsType(g.transform, Vector3.one, Vector3.zero, GlobalData.GoodsType.Stage);
//                }
//
//                if (g != null)
//                {
//                    g.transform.SetSiblingIndex(3);
//                    g.SetActive(true);
//                }
//                switch (item.pricetype)
//                {
//                    case GlobalData.goldCoin:
//                        goodsItem._typeIcon.sprite = _iconTypeAssets.GetTValue(GlobalData.coin_gold);
//                        break;
//                    case GlobalData.treeCoin:
//                        if (item.name == GlobalData.GTransformers)
//                            goodsItem._typeIcon.sprite = _iconTypeAssets.GetTValue(GlobalData.energy_mu);
//                        else
//                            goodsItem._typeIcon.sprite = _iconTypeAssets.GetTValue(GlobalData.coin_tree);
//                        break;
//                    case GlobalData.huoEnergy:
//                        goodsItem._typeIcon.sprite = _iconTypeAssets.GetTValue(GlobalData.energy_huo);
//                        break;
//                    case GlobalData.muEnergy:
//                        goodsItem._typeIcon.sprite = _iconTypeAssets.GetTValue(GlobalData.energy_mu);
//                        break;
//                }
//
//
//                goodsItem._chooseBtn.onClick.AddListener(() =>
//                {
//                    if (str == GlobalData.rm)
//                        transform_Charge.Buy(item, goodsItem._typeIcon.sprite, g, true);
//                    else
//                        transform_Charge.Buy(item, goodsItem._typeIcon.sprite, g, false);
//                });
//
//                goodsItem._showMsgBtn.onClick.AddListener(() =>
//                {
//                    if (goodsItem._lock)
//                    {
//                        return;
//                    }
//                    goodsItem._lock = true;
//                    goodsItem._showMsgTxt.text = item.msg;
//                    goodsItem._msgMask.gameObject.SetActive(true);
//                    goodsItem._showMsgTxt.gameObject.SetActive(true);
//                    Color maskColor = goodsItem._msgMask.color;
//                    Color textColor = goodsItem._showMsgTxt.color;
//                    goodsItem._msgMask.DOColor(new Color(0, 0, 0, 0), 3f).OnComplete(() =>
//                    {
//                        goodsItem._lock = false;
//                        goodsItem._msgMask.gameObject.SetActive(false);
//                        goodsItem._msgMask.color = maskColor;
//                    });
//                    goodsItem._showMsgTxt.DOColor(new Color(0, 0, 0, 0), 3f).OnComplete(() =>
//                    {
//                        goodsItem._lock = false;
//                        goodsItem._showMsgTxt.gameObject.SetActive(false);
//                        goodsItem._showMsgTxt.color = textColor;
//                    });
//                });
//            }
//        }
//    }
//    private void SetTransformByGoodsType(Transform gTrans, Vector3 localScale, Vector3 localPosition, GlobalData.GoodsType goodsType)
//    {
//        switch (goodsType)
//        {
//            case GlobalData.GoodsType.Stage:
//                gTrans.localScale = localScale;
//                gTrans.localPosition = localPosition;
//                break;
//            case GlobalData.GoodsType.Machine:
//                gTrans.localScale = localScale;
//                gTrans.localPosition = localPosition;
//                break;
//        }
//    }
//
//    private void UpdateData(object data)
//    {
//        _data = data as DataDic<DataItem.ShopData>;
//        _eventPatcher.ClearAllEevnt();
//        _eventPatcher.Addlistener(SendMsg);
//        UpdateList(GlobalData.sb);
//    }
//
//    private void SendMsg(bool isOn)
//    {
//        if (isOn)
//        {
//            // Debug.Log("木矿机");
//            UpdateList(GlobalData.sb);
//        }
//        else
//        {
//            // Debug.Log("火矿机");
//            UpdateList(GlobalData.rm);
//        }
//    }
//
//    private void DoPlayDirection(bool isForward)
//    {
//        if (isForward)
//        {
//            _skinDoTweenPath.DOPlayForward();
//            _stageDoTweenPath.DOPlayForward();
//        }
//        else
//        {
//            _skinDoTweenPath.DOPlayBackwards();
//            _stageDoTweenPath.DOPlayBackwards();
//        }
//    }
//}