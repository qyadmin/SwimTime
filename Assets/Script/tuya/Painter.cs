using UnityEngine;

public class Painter : MonoBehaviour
{
	public const int SIZE = 512;

	public Material mat;
	public float scale;
	public Color col;

	private Renderer rend;
	private RenderTexture rendTex;
	private Texture brushTex;

	private void Start()
	{
		//使用ARGB32的格式创建一个RenderTexture
		rendTex = new RenderTexture(SIZE, SIZE, 24, RenderTextureFormat.ARGB32);
		rendTex.isPowerOfTwo = true;
		rendTex.useMipMap = false;
		rendTex.Create();

		//清空画布
		Clear();

		//获取当前模型的Renderer把它的纹理替换成刚建立的RenderTexture
		rend = GetComponent<Renderer>();
		rend.material.SetTexture("_MainTex", rendTex);

		//获取画笔纹理
		brushTex = mat.mainTexture;
		//设置我们想要的颜色到画笔材质上.
		mat.SetColor("_Color", col);
	}

	private void Update()
	{
		//当鼠标按下时发射射线碰撞模型
		if (Input.GetMouseButton(0))
		{
			var mp = Input.mousePosition;
			var ray = Camera.main.ScreenPointToRay(mp);
			RaycastHit rayHit;
			if (Physics.Raycast(ray, out rayHit))
			{
				//uv坐标是0~1,而我们要的是它纹理上的坐标是0~SIZE,于是乘以纹理的SIZE
				DrawBrush((int)(rayHit.textureCoord.x * SIZE), (int)(rayHit.textureCoord.y * SIZE), col, scale);
			}
		}
	}

	private void Clear()
	{
		Graphics.SetRenderTarget(rendTex);
		GL.PushMatrix();
		GL.Clear(true, true, Color.white);
		GL.PopMatrix();
	}

	private void DrawBrush(int x, int y, Color col, float scale)
	{
		//计算画笔居中当前位置以后的四个角的坐标
		var left = x - brushTex.width * scale / 2f;
		var right = x + brushTex.width * scale / 2f;
		var top = y + brushTex.height * scale / 2f;
		var bot = y - brushTex.height * scale / 2f;

		//将GPU的绘制目标转移到当前RenderTexture上
		Graphics.SetRenderTarget(rendTex);

		//使用GL图像库绘制一个四边形
		GL.PushMatrix();
		GL.LoadOrtho();

		mat.SetPass(0);

		GL.Begin(GL.QUADS);
		GL.TexCoord2(0, 0);
		GL.Vertex3(left / SIZE, bot / SIZE, 0);
		GL.TexCoord2(0, 1);
		GL.Vertex3(left / SIZE, top / SIZE, 0);
		GL.TexCoord2(1, 1);
		GL.Vertex3(right / SIZE, top / SIZE, 0);
		GL.TexCoord2(1, 0);
		GL.Vertex3(right / SIZE, bot / SIZE, 0);

		GL.End();
		GL.PopMatrix();

		//搞定
	}
}
