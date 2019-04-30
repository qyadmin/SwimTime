using LitJson;

namespace DataItem
{
    //                "current":0,
    //                "count":10,
    //                "id":"friendmine",
    //                "coin":10,
    //                "seq":6,
    //                "req":false,
    //                "complete":false

    public class TaskData
    {
        public int current;
        public int count;
        public string id;
        public double coin;
        public double seq;
        public bool req;
        public bool complete;
    }

    //用户数据
    public class UserData
    {
        public string createtime;
        public string lastlogin;
        public int status;
        public int cumulative;
        public string ip;
        public int level;
        public double eth;
        public double cra;
        public string password2;
        public double dsb;
        public string avatar;
        public int sex;
        public string superior;
        public string nickname;
        public double sb;
        public string strlevel;
        public int direct;
        public string phone;
        public string password;
        public int id;
        public string address;
    }

    //矿机相关
    public class MachineInfoData
    {
        public int power;
        public int createtime;
        public int id;
        public int profit;
        public string tp;
    }

    //商城数据
    public class ShopData
    {
        public int index;
        public string msg;
        public string _id;
        public string name;
        public string tp;
        public double price;
    }

    public class PaomaDengData
    {

    }


    public class RankData
    {
        public int score;
        public int rank;
        public JsonData userinfo;
    }

    public class KuangFirendData
    {
        public JsonData userinfo;
    }

    public class gs_Data
    {
        public JsonData sellinfo;
    }

    public class NoticeData
    {
        public string time;
        public string title;
        public string genid;
        public string text;
    }
}