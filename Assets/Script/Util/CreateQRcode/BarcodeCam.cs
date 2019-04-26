// ==================================================================
// 作    者：Pablo.风暴洋-宋杨
// 説明する：字符串生成二维码
// 作成時間：2019-04-25
// 類を作る：BarcodeCam.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class BarcodeCam : MonoBehaviour
{
    /// <summary>
    /// 编码测试用纹理
    /// </summary>
    private Texture2D encoded;
    private Thread qrThread;

    private Color32[] c;
    private int W, H;

    private Rect screenRect;

    private bool isQuit;

    public string LastResult;
    private bool shouldEncodeNow;


    public void GetQRcodeImg(Image image, string QRcodeString)
    {
        encoded = new Texture2D(256, 256);
        LastResult = QRcodeString;
        shouldEncodeNow = true;

        screenRect = new Rect(0, 0, 256, 256);

        qrThread = new Thread(DecodeQR);
        qrThread.Start();

        LastResult = QRcodeString;
        image.sprite = Sprite.Create(encoded, screenRect, new Vector2(0.5f, 0.5f));
    }



    void OnDestroy()
    {
        qrThread.Abort();
    }

    /// <summary>
    /// 与其中止线程，不如自行停止线程
    /// </summary>
    void OnApplicationQuit()
    {
        isQuit = true;
    }

    void Update()
    {
        //最后识别
        var textForEncoding = LastResult;
        if (shouldEncodeNow && textForEncoding != null)
        {
            var color32 = Encode(textForEncoding, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
            shouldEncodeNow = false;
        }
    }

    void DecodeQR()
    {
        // 创建具有自定义亮度源的读取器
        var barcodeReader = new BarcodeReader { AutoRotate = false, TryHarder = false };

        while (true)
        {
            if (isQuit)
                break;

            try
            {
                // 解码当前帧
                var result = barcodeReader.Decode(c, W, H);
                if (result != null)
                {
                    LastResult = result.Text;
                    shouldEncodeNow = true;
                    print(result.Text);
                }

                //暂时休眠当前线程，然后设置以获得下一个帧的标志位
                Thread.Sleep(200);
                c = null;
            }
            catch { }
        }
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
}
