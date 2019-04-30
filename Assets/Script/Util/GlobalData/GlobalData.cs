// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：常量配置
// 作成時間：2018-08-01
// 類を作る：GlobalData.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

public class GlobalData
{
    #region 图标种类
    /// <summary>
    /// 树币
    /// </summary>
    public const string treeCoin = "sb";
    /// <summary>
    /// 金币(游戏币)
    /// </summary>
    public const string goldCoin = "rmb";
    /// <summary>
    /// 木能
    /// </summary>
    public const string muEnergy = "sbcoin";
    /// <summary>
    /// 火能
    /// </summary>
    public const string huoEnergy = "rmbcoin";
    #endregion



    #region 临时常量(来源于Json数据串)
    /// <summary>
    /// 绿野金刚
    /// </summary>
    public const string GTransformers = "绿野金刚";
    /// <summary>
    /// skin1 皮肤
    /// </summary>
    public const string skin1 = "skin1";
    /// <summary>
    /// 体验矿机（火能）
    /// </summary>
    public const string rmb3 = "rmb3";
    /// <summary>
    /// 绿野金刚
    /// </summary>
    public const string sb10 = "sb10";
    /// <summary>
    /// 木属性矿机以及相关道具的字符串部分截取字段
    /// </summary>
    public const string sb = "sb";
    /// <summary>
    /// 火属性矿机以及相关道具的字符串部分截取字段
    /// </summary>
    public const string rm = "rm";
    /// <summary>
    /// 皮肤道具的字符串部分截取字段
    /// </summary>
    public const string sk = "sk";
    /// <summary>
    /// 非矿机类加成道具的字符串部分截取字段
    /// </summary>
    public const string prop = "prop";
    /// <summary>
    /// 木矿(服务端命名-_-||)
    /// </summary>
    public const string sbfruit = "sbfruit";
    /// <summary>
    /// 火矿(服务端命名-_-||)
    /// </summary>
    public const string rmbfruit = "rmbfruit";
    /// <summary>
    /// 木能(服务端命名-_-||)
    /// </summary>
    public const string sbcoin = "sbcoin";
    /// <summary>
    /// 火能(服务端命名-_-||)
    /// </summary>
    public const string rmbcoin = "rmbcoin";
    /// <summary>
    /// 木能普通矿机
    /// </summary>
    public const string sbfixed = "sbfixed";
    /// <summary>
    /// 木能变形矿机
    /// </summary>
    public const string sbfloat = "sbfloat";
    /// <summary>
    /// 火能普通矿机
    /// </summary>
    public const string rmbfixed = "rmbfixed";
    /// <summary>
    /// 火能变形矿机
    /// </summary>
    public const string rmbfloat = "rmbfloat";
    /// <summary>
    /// 木能汉字
    /// </summary>
    public const string muNeng = "木能";
    /// <summary>
    /// 火能汉字
    /// </summary>
    public const string huoNeng = "火能";
    /// <summary>
    /// 木能双倍卡汉字
    /// </summary>
    public const string doubleMuEnergyBuffCard = "木能双倍卡";
    /// <summary>
    /// 火能双倍卡汉字
    /// </summary>
    public const string doubleHuoEnergyBuffCard = "火能双倍卡";
    /// <summary>
    /// 小喇叭汉字
    /// </summary>
    public const string smallTurmpet = "小喇叭";
    #endregion



    #region 通用提示相关常量
    /// <summary>
    /// 提示
    /// </summary>
    public const string prompt = "提示";
    /// <summary>
    /// 添加
    /// </summary>
    public const string add = "添加";
    /// <summary>
    /// 确定
    /// </summary>
    public const string submit = "确定";
    /// <summary>
    /// 取消
    /// </summary>
    public const string cancel = "取消";
    #endregion




    #region 绿野金刚游戏相关常量
    /// <summary>
    /// 没有相关信息 (配套使用LanUtil.formateStr方法，为了统一Lua语言的索引机制，索引要从1开始)
    /// </summary>
    public static string fillTipByNon = "您没有{1}信息";
    /// <summary>
    /// 网络请求失败
    /// </summary>
    public const string NETWORK_REQUEST_FAILED = "网络请求失败！";
    /// <summary>
    /// 请同意用户条款后开始游戏
    /// </summary>
    public const string CONSENT_TO_THE_TERMS_OF_THE_USER = "请同意用户条款后开始游戏";
    /// <summary>
    /// 完成的交易
    /// </summary>
    public const string completeBusiness = "完成的交易";
    /// <summary>
    /// 交易
    /// </summary>
    public const string business = "交易";
    /// <summary>
    /// 我的求购
    /// </summary>
    public const string myQiugou = "我的求购";
    /// <summary>
    /// 求购
    /// </summary>
    public const string qiuGou = "求购";
    /// <summary>
    /// 我的挂售
    /// </summary>
    public const string myGuaShou = "我的挂售";
    /// <summary>
    /// 挂售
    /// </summary>
    public const string guashou = "挂售";
    /// <summary>
    /// 我的匹配
    /// </summary>
    public const string myMate = "我的匹配";
    /// <summary>
    /// 匹配
    /// </summary>
    public const string mate = "匹配";
    /// <summary>
    /// 我的担保
    /// </summary>
    public const string myDanbao = "我的担保";
    /// <summary>
    /// 担保
    /// </summary>
    public const string danBao = "担保";
    /// <summary>
    /// 运行中
    /// </summary>
    public const string working = "运行中";
    /// <summary>
    /// 未启动
    /// </summary>
    public const string noActivated = "未启动";
    /// <summary>
    /// 双倍卡
    /// </summary>
    public const string doubleCard = "双倍卡";
    /// <summary>
    /// 无道具
    /// </summary>
    public const string non_Stage = "无道具";
    /// <summary>
    /// 无段位
    /// </summary>
    public const string non_san = "无段位";
    /// <summary>
    /// 青铜
    /// </summary>
    public const string QINGTONG = "青铜";
    /// <summary>
    /// 白银
    /// </summary>
    public const string BAIYIN = "白银";
    /// <summary>
    /// 黄金
    /// </summary>
    public const string HUANGJIN = "黄金";
    /// <summary>
    /// 今日矿机已经启动
    /// </summary>
    public const string THE_MINER_HAS_BEEN_ACTIVATED_TODAY = "今日矿机已经启动";
    /// <summary>
    /// 今日矿机已经收取
    /// </summary>
    public const string THE_MINER_HAS_BEEN_GET_GAINS_TODAY = "今日矿机已经收取";
    /// <summary>
    /// 部分收益已进入矿池
    /// </summary>
    public const string PART_OF_THE_INCOME_HAS_ENTERED_THE_ORE_POOL = "部分收益已进入矿池";
    /// <summary>
    /// 收取成功
    /// </summary>
    public const string GAINS_SUCCESS = "收取成功";
    /// <summary>
    /// 启动成功
    /// </summary>
    public const string START_SUCCESS = "启动成功";
    /// <summary>
    /// 绑定成功
    /// </summary>
    public const string BINGING_SUCCESS = "绑定成功";
    /// <summary>
    /// 恭喜您升级成功
    /// </summary>
    public const string UPGRADE_SUCCESS = "恭喜您升级成功";
    /// <summary>
    /// 此种矿机并不能升级
    /// </summary>
    public const string THIS_TYPEOF_MACHINE_NOT_UPGRADE = "此种矿机并不能升级";
    /// <summary>
    /// 请选择木能或火能
    /// </summary>
    public const string PLEAST_SELECT_MUENERGY_OR_HUOENERGY = "请选择木能或火能";
    /// <summary>
    /// 请选择天数
    /// </summary>
    public const string PLEASE_SELECT_DAYS = "请选择天数";
    /// <summary>
    /// 时间格式 1："yyyy-MM-dd HH:mm:ss"
    /// </summary>
    public const string timeStrForm_1 = "yyyy-MM-dd HH:mm:ss";
    #endregion




    #region 资源名字
    /// <summary>
    /// 木能普通矿机预设
    /// </summary>
    public const string MachineObj_common_green = "MachineObj_common_green";
    /// <summary>
    /// 木能变形矿机预设
    /// </summary>
    public const string MachineObj_common_blue = "MachineObj_common_blue";
    /// <summary>
    /// 火能普通矿机预设
    /// </summary>
    public const string MachineObj_common_orange = "MachineObj_common_orange";
    /// <summary>
    /// 火能变形矿机预设
    /// </summary>
    public const string MachineObj_common_purple = "MachineObj_common_purple";
    /// <summary>
    /// 木能图标预设
    /// </summary>
    public const string muEnergyIcon = "muEnergyIcon";
    /// <summary>
    /// 火能图标预设
    /// </summary>
    public const string huoEnergyIcon = "huoEnergyIcon";
    /// <summary>
    /// 木能双倍卡图标预设
    /// </summary>
    public const string muEnergyDoubleCard = "muEnergyDoubleCard";
    /// <summary>
    /// 火能双倍卡图标预设
    /// </summary>
    public const string huoEnergyDoubleCard = "huoEnergyDoubleCard";
    /// <summary>
    /// 小喇叭图标预设
    /// </summary>
    public const string trumpetCard = "trumpetCard";
    /// <summary>
    /// 金币精灵图预设
    /// </summary>
    public const string coin_gold = "coin_gold";
    /// <summary>
    /// 木能精灵图预设
    /// </summary>
    public const string energy_mu = "energy_mu";
    /// <summary>
    /// 树币精灵图预设
    /// </summary>
    public const string coin_tree = "coin_tree";
    /// <summary>
    /// 火能精灵图预设
    /// </summary>
    public const string energy_huo = "energy_huo";
    /// <summary>
    /// 机甲1
    /// </summary>
    public const string jiqiren1 = "jiqiren1";

    //资源文件夹名
    public const string Success = "Success";
    public const string EffectSuccess = "Effect/Success";
    public const string Fail = "Fail";
    public const string EffectFail = "Effect/Fail";
    public const string Lighting = "Lighting";
    public const string EffectLighting = "Effect/Lighting";
    public const string EnergyIcon = "EnergyIcon";
    public const string SpriteEnergyIcon = "Sprite/EnergyIcon";
    public const string Stage = "Stage";
    public const string MechArmor = "MechArmor";
    #endregion





    #region 服务端返回部分字段

    #region 排行榜
    /// <summary>
    /// 昵称
    /// </summary>
    public const string nickname = "nickname";
    /// <summary>
    /// 段位
    /// </summary>
    public const string paragraph = "paragraph";
    /// <summary>
    /// 头像php链接
    /// </summary>
    public const string avatar = "avatar";
    #endregion


    #endregion




    #region 请求服务端的推送字段

    #region 商城接口
    /// <summary>
    /// 购买数量
    /// </summary>
    public const string num = "num";
    /// <summary>
    /// 购买的道具类型
    /// </summary>
    public const string tp = "tp";
    /// <summary>
    /// 折扣
    /// </summary>
    public const string discount = "discount";
    #endregion

    #region 操作矿机接口
    /// <summary>
    /// 操作矿机的ID
    /// </summary>
    public const string pos = "pos";
    #endregion

    #region 一键收取返回值接口
    /// <summary>
    /// 收益值
    /// </summary>
    public const string change = "change";
    #endregion

    #region 矿机合成
    /// <summary>
    /// 选择天数
    /// </summary>
    public const string day = "day";
    #endregion

    #region 绑定上级
    /// <summary>
    /// 绑定目标玩家的ID(原来是手机号)
    /// </summary>
    public const string tophone = "tophone";
    #endregion

    #endregion




    #region 枚举类型
    public enum GoodsType
    {
        Stage,
        Machine
    }

    /// <summary>
    /// 窗口级别
    /// </summary>
    public enum SizeType
    {
        FullSize,
        Scale,
        FilledByVerticalAndTop
    }
    #endregion




    #region 游戏常量switch配置
    public class GameConstConfig
    {
        public static string GetSan(int Num)
        {
            string str = string.Empty;
            switch (Num)
            {
                case 0:
                    str = GlobalData.non_san;
                    break;
                case 1:
                    str = GlobalData.QINGTONG;
                    break;
                case 2:
                    str = GlobalData.BAIYIN;
                    break;
                case 3:
                    str = GlobalData.HUANGJIN;
                    break;
            }
            return str;
        }


        public static string GetGuojikuangyouUserLevel(int num)
        {
            string str = string.Empty;
            switch (num)
            {
                case 0:
                    str = "士兵";
                    break;
                case 1:
                    str = "连长";
                    break;
                case 2:
                    str = "团长";
                    break;
                case 3:
                    str = "师长";
                    break;
            }
            return str;
        }

        public static int GetCarIndex(string name)
        {
            int index = 0;
            switch (name)
            {
                case "sb500":
                    index = 0;
                    break;
                case "sb1000":
                    index = 1;
                    break;
                case "sb2000":
                    index = 2;
                    break;
                case "sb5000":
                    index = 3;
                    break;
            }
            return index;
        }

        public static string GetCarName(string tp)
        {
            string str = string.Empty;
            switch (tp)
            {
                case "sb500":
                    str = "普通矿车";
                    break;
                case "sb1000":
                    str = "高级矿车";
                    break;
                case "sb2000":
                    str = "超级矿车";
                    break;
                case "sb5000":
                    str = "黄金矿车";
                    break;
            }
            return str;
        }
    }

    #endregion




    #region 动画字段配置
    /// <summary>
    /// 启动机甲动画
    /// </summary>
    public const string to_start = "to_start";
    #endregion
}
