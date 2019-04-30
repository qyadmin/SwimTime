using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class CreatIP : MonoBehaviour
{
    [SerializeField]
    public Toggle UserProtocolTgl;
    [SerializeField]
    public GameObject LoginObj;
    [SerializeField]
    public GameObject loadingObj;
    [SerializeField]
    public Image progressImgFilling;
    [SerializeField]
    private Text IP, PORT;
    public UnityEvent ActionEvent;
    [SerializeField]
    private HttpModel HTTP_ip;
    private void Start()
    {
        HTTP_ip.Get();
    }
    public void Creat()
    {
        Static.Instance.URL = "http://" + IP.text + ":" + PORT.text + "/";
        //Login();
    }

    public void SaveJwt()
    {
        Static.Instance.DeleteFile(Application.persistentDataPath, "jwt.text");
        Static.Instance.CreateFile(Application.persistentDataPath, "jwt.text", Static.Instance.GetValue("jwt"));
    }


    public HttpModel JwtLogin, PasswordLogin;
    public void Login()
    {
        //bool jwt_Ishave = Static.Instance.Getjwt();
        //if(jwt_Ishave)
        //JwtLogin.Get();
        //else
        if (UserProtocolTgl.isOn)
            PasswordLogin.Get();
        else
        {
            string content = GlobalData.CONSENT_TO_THE_TERMS_OF_THE_USER,
                 buttonName = GlobalData.submit,
                 title = GlobalData.prompt;
            MessageManager._Instantiate.WindowShowMessage(content, null, buttonName, title, true);
        }
    }

    public void LoadSuS()
    {
		Debug.Log ("LOGIN");
        SaveJwt();
		SceneManager.LoadScene ("LabbyScene");
        // GetLoad.IsGetIn = true;
       // StartCoroutine(LoadAsyncScene("LabbyScene"));
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingObj.SetActive(true);
        LoginObj.SetActive(false);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressImgFilling.fillAmount = progress;
            yield return null;
        }
    }
}
