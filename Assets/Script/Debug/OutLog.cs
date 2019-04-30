// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：将Unity系统log映射到OnGUI中
// 作成時間：2018-07-13
// 類を作る：OutLog.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class OutLog : MonoBehaviour {
    private static bool isNotPrintAndRecord = true;
    private static bool isCanWriteFile = false;

    static List<string> mLines = new List<string>();
    static List<string> mWriteTxt = new List<string>();
    private string outpath;

    private bool WindowSwitch = false;
    private Rect windowRect = new Rect(Screen.width / 10, Screen.height / 10, Screen.width / 10 * 8, Screen.height / 10 * 8);
    private Vector2 pos = Vector2.zero;

    void Start()
    {
        //========================这个文件会侦听报错，并且会将错误写入outLot.txt文件==========
        //========================在电脑上的报错，不会写入文件================================
        //========================在安卓或IOS上报错，才会写入文件=============================
#if UNITY_EDITOR||UNITY_WEBPLAYER
        listenErrorAndWriteToFile(true);//=================只侦听报错，将报错信息显示到左边==========================
#else 
            listenErrorAndWriteToFile(true);//=================侦听报错，将报错信息显示到左边=的同时写入outLog.txt========
#endif
    }

    private void listenErrorAndWriteToFile(bool writeToFile)
    {
        isNotPrintAndRecord = false;

        outpath = Application.persistentDataPath + "/outLog.txt";
        //每次启动客户端删除之前保存的Log
#if !UNITY_WEBPLAYER
        if (System.IO.File.Exists(outpath))
        {
            File.Delete(outpath);
        }
#endif
        isCanWriteFile = writeToFile;

        Application.logMessageReceived += HandleLog;
    }

    void Update()
    {
        if (isNotPrintAndRecord)
        {
            return;
        }

        //因为写入文件的操作必须在主线程中完成，所以在Update中写入文件。
        if (isCanWriteFile && mWriteTxt.Count > 0)
        {
            string[] temp = mWriteTxt.ToArray();
            foreach (string t in temp)
            {
                using (StreamWriter writer = new StreamWriter(outpath, true, Encoding.UTF8))
                {
                    writer.WriteLine(t);
                }
                mWriteTxt.Remove(t);
            }
        }
    }

    public static void forceUpdate()
    {
        if (isNotPrintAndRecord)
        {
            return;
        }

        string temp_outpath = Application.persistentDataPath + "/outLog.txt";
        if (isCanWriteFile && mWriteTxt.Count > 0)
        {
            string[] temp = mWriteTxt.ToArray();
            foreach (string t in temp)
            {
                using (StreamWriter writer = new StreamWriter(temp_outpath, true, Encoding.UTF8))
                {
                    writer.WriteLine(t);
                }
                mWriteTxt.Remove(t);
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (isNotPrintAndRecord)
        {
            return;
        }

        //mWriteTxt.Add(logString);
        if (type == LogType.Error || type == LogType.Exception)
        {
            Log(logString);
            Log(stackTrace);
        }
    }
    //错误的信息保存起来，用来输出在手机屏幕上
    static public void Log(params object[] objs)
    {
        if (isNotPrintAndRecord)
        {
            return;
        }

        StringBuilder strBuild = new StringBuilder();
        for (int i = 0; i < objs.Length; ++i)
        {
            if (i == 0)
            {
                strBuild.Append(objs[i].ToString());
            }
            else
            {
                strBuild.Append(LanUtil.StringProfile(", ", objs[i].ToString()));
            }
        }
        if (Application.isPlaying)
        {
            if (mLines.Count > 20)
            {
                mLines.RemoveAt(0);
            }
            string str = strBuild.ToString();
            mLines.Add(str);
            mWriteTxt.Add(str);
        }
    }



    void OnGUI()
    {
        GUILayout.BeginVertical("box");
        if (OutLog.mLines.Count == 0)
        {
            if (GUI.Button(new Rect(Screen.width - 200, 5, 100, 30), "错误窗口"))
            {
                WindowSwitch = true;
            }
        }
        else
        {
            GUI.color = Color.red;
            if (GUI.Button(new Rect(Screen.width - 170, 5, 150, 30), "错误窗口发现错误了"))
            {
                WindowSwitch = true;
            }
        }
        if (WindowSwitch)
        {
            windowRect = GUI.Window(0, windowRect, WindowContain, "错误窗口");
        }
        GUILayout.EndVertical();
    }
    public static List<string> fileList = new List<string>();
    void WindowContain(int windowID)
    {
        pos = GUILayout.BeginScrollView(pos);
        if (state == true)
        {
            GUILayout.Label("Error");
            for (int i = 0, imax = OutLog.mLines.Count; i < imax; ++i)
            {
                GUILayout.Label(OutLog.mLines[i]);
            }
        }
        else
        {
            GUILayout.Label("Log");
            for (int i = LogManager.getInstance().list.Count - 1; i >= 0; i--)
            {
                GUILayout.Label(LogManager.getInstance().list[i]);
            }
        }
        GUILayout.EndScrollView();
        if (GUILayout.Button("Clean"))
        {
            //GC.Collect();
            OutLog.mLines.Clear();
        }
        if (GUILayout.Button("ShowLog"))
        {
            state = !state;
            //GC.Collect();
        }
        if (GUILayout.Button("Close"))
        {
            WindowSwitch = false;
        }
    }
    private bool state = true;
}
public class LogManager
{
    private static LogManager _instance;
    public static LogManager getInstance()
    {
        if (_instance == null)
        {
            _instance = new LogManager();
        }
        return _instance;
    }

    public List<string> list = new List<string>();
}
