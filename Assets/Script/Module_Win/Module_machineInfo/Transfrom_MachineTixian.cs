using DataItem;
using UnityEngine;
using UnityEngine.UI;

public class Transfrom_MachineTixian : MonoBehaviour
{
    public HttpModel http_kuangchehuanqian;
    public InputField addressInput;
    public Button tixianBtn;
    public Transform WindowBASE_kuangchetixian;

    public void TixianTask(MachineInfoData machineInfoData)
    {
        tixianBtn.onClick.RemoveAllListeners();
        tixianBtn.onClick.AddListener(() =>
        {
            http_kuangchehuanqian.Data.AddData("id", machineInfoData.id.ToString());
            http_kuangchehuanqian.Data.AddData("address", addressInput.text);
            http_kuangchehuanqian.HttpSuccessCallBack.Addlistener((ReturnHttpMessage msg) =>
            {
                GameManager.GetGameManager.CloseWindow(WindowBASE_kuangchetixian);
            });
            http_kuangchehuanqian.Get();
        });
    }
}
