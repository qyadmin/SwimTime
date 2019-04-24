using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextStyleMessage : BaseMeshEffect {

    public TextTag DemoTag;

    public bool UseBasicStyle;
    //描边
    public bool IsDrawOutline = false;
    public bool IsDrawGradent = false;
    public bool IsDrawShadow = false;

    public Color32 Outline_effectColor = Color.white;
    public Vector2 Outline_effectDistance;
    public OutLineVector ShadowType = OutLineVector.Shadow_8;

    public Color32 Shadow_effectColor = Color.white;
    public Vector2 Shadow_effectDistance;


    public TypeHV GradientType = TypeHV.Vertica;
    public Color colorTop = Color.white;
    public Color colorCenter = Color.grey;
    public Color colorBottom = Color.black;

    public bool MultiplyTextColor = false;
    public bool IsThreeColor;


    [SerializeField]
    private bool m_UseGraphicAlpha;

    public void UpdateState()
    {
        base.OnEnable();
    }

    public bool StateStyle = false;
    public string NowName;
    public void ChangState (string Name)
    {
        StateStyle = true;
        NowName = Name;
    }

    public override void ModifyMesh(VertexHelper vh)
    {

        if (IsDrawGradent)
        {
            if (!IsActive())
            {
                return;
            }

            var vertexList = new List<UIVertex>();
            vh.GetUIVertexStream(vertexList);
            int count = vertexList.Count;

            if (GradientType == TypeHV.Vertica)
            {
                ApplyGradientV(vertexList, 0, count);
                vh.Clear();
                vh.AddUIVertexTriangleStream(vertexList);
            }
            if (GradientType == TypeHV.Horizontal)
            {
                ApplyGradientH(vertexList, 0, count);
                vh.Clear();
                vh.AddUIVertexTriangleStream(vertexList);
            }
            if (GradientType == TypeHV.VerticaThreeColor)
            {
                ModifyVertices(vh);
            }

        }
        if (IsDrawOutline)
        {
            var verts = new List<UIVertex>();
            vh.GetUIVertexStream(verts);
            var neededCpacity = verts.Count * 2;
            if (verts.Capacity < neededCpacity)
                verts.Capacity = neededCpacity;


            //4point
            var start = 0;
            var end = verts.Count;
            ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, Outline_effectDistance.x, Outline_effectDistance.y);
            start = end;
            end = verts.Count;
            ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, Outline_effectDistance.x, -Outline_effectDistance.y);
            start = end;
            end = verts.Count;
            ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, -Outline_effectDistance.x, Outline_effectDistance.y);
            start = end;
            end = verts.Count;
            ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, -Outline_effectDistance.x, -Outline_effectDistance.y);

            //8point
            switch (ShadowType)
            {
                case OutLineVector.Shadow_4:
                    break;

                case OutLineVector.Shadow_8:
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, -Outline_effectDistance.x, 0);
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, Outline_effectDistance.x, 0);
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, 0, Outline_effectDistance.y);
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, 0, -Outline_effectDistance.y);
                    break;

                //16point
                case OutLineVector.Shadow_16:
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, -Outline_effectDistance.x, Outline_effectDistance.y / 2);
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, Outline_effectDistance.x, Outline_effectDistance.y / 2);
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, -Outline_effectDistance.x, -Outline_effectDistance.y / 2);
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, Outline_effectDistance.x, -Outline_effectDistance.y / 2);

                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, -Outline_effectDistance.x / 2, Outline_effectDistance.y);
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, -Outline_effectDistance.x / 2, -Outline_effectDistance.y);
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, Outline_effectDistance.x / 2, Outline_effectDistance.y);
                    start = end;
                    end = verts.Count;
                    ApplyShadowZeroAlloc(verts, Outline_effectColor, start, verts.Count, Outline_effectDistance.x / 2, -Outline_effectDistance.y);
                    break;
            }

            vh.Clear();
            vh.AddUIVertexTriangleStream(verts);
        }

       

        if (IsDrawShadow)
        {
            var verts = new List<UIVertex>();
            vh.GetUIVertexStream(verts);
            var neededCpacity = verts.Count * 2;
            if (verts.Capacity < neededCpacity)
                verts.Capacity = neededCpacity;
            //4点绘制
            var start = 0;
            var end = verts.Count;
            ApplyShadowZeroAlloc(verts, Shadow_effectColor, start, verts.Count, Shadow_effectDistance.x, Shadow_effectDistance.y);

            vh.Clear();
            vh.AddUIVertexTriangleStream(verts);
        }
    }


    protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
    {
        UIVertex vt;
        var neededCapacity = verts.Count + end - start;
        if (verts.Capacity < neededCapacity)
            verts.Capacity = neededCapacity;
        for (int i = start; i < end; ++i)
        {
            vt = verts[i];
            verts.Add(vt);
            Vector3 v = vt.position;
            v.x += x;
            v.y += y;
            vt.position = v;
            var newColor = color;
            if (m_UseGraphicAlpha)
                newColor.a = (byte)((newColor.a * verts[i].color.a) / 255);
            vt.color = newColor;
            verts[i] = vt;
        }
    }

    //VerticaColor
    private void ApplyGradientV(List<UIVertex> vertexList, int start, int end)
    {
        float bottomY = vertexList[0].position.y;
        float topY = vertexList[0].position.y;
        for (int i = start; i < end; ++i)
        {
            float y = vertexList[i].position.y;
            if (y > topY)
            {
                topY = y;
            }
            else if (y < bottomY)
            {
                bottomY = y;
            }
        }

        float uiElementHeight = topY - bottomY;
        for (int i = start; i < end; ++i)
        {
            UIVertex uiVertex = vertexList[i];
            uiVertex.color = Color32.Lerp(colorBottom, colorTop, (uiVertex.position.y - bottomY) / uiElementHeight);
            vertexList[i] = uiVertex;
        }
    }


    //HorizontalColor
    private void ApplyGradientH(List<UIVertex> vertexList, int start, int end)
    {
        float leftX = 0;
        float RightX = 0;

        for (int i = 0; i < end; i++)
        {
            float x = vertexList[i].position.x;

            if (x > RightX)
            {
                RightX = x;
            }
            else if (x < leftX)
            {
                leftX = x;
            }
        }

        float uiElementWeight = RightX - leftX;

        for (int i = 0; i < end; i++)
        {
            UIVertex uiVertex = vertexList[i];
            uiVertex.color = Color32.Lerp(colorTop, colorBottom,(uiVertex.position.x - leftX) / uiElementWeight);
            vertexList[i] = uiVertex;
        }
    }

    //ThreeColor
    private void ModifyVertices(VertexHelper vh)
    {
        List<UIVertex> verts = new List<UIVertex>(vh.currentVertCount);
        vh.GetUIVertexStream(verts);
        vh.Clear();

        int step = 6;

        for (int i = 0; i < verts.Count; i += step)
        {
            //6 point
            var tl = multiplyColor(verts[i + 0], colorTop);
            var tr = multiplyColor(verts[i + 1], colorTop);
            var bl = multiplyColor(verts[i + 4], colorBottom);
            var br = multiplyColor(verts[i + 3], colorBottom);
            var cl = calcCenterVertex(verts[i + 0], verts[i + 4]);
            var cr = calcCenterVertex(verts[i + 1], verts[i + 2]);

            vh.AddVert(tl);
            vh.AddVert(tr);
            vh.AddVert(cr);
            vh.AddVert(cr);
            vh.AddVert(cl);
            vh.AddVert(tl);

            vh.AddVert(cl);
            vh.AddVert(cr);
            vh.AddVert(br);
            vh.AddVert(br);
            vh.AddVert(bl);
            vh.AddVert(cl);
        }

        for (int i = 0; i < vh.currentVertCount; i += 12)
        {
            vh.AddTriangle(i + 0, i + 1, i + 2);
            vh.AddTriangle(i + 3, i + 4, i + 5);
            vh.AddTriangle(i + 6, i + 7, i + 8);
            vh.AddTriangle(i + 9, i + 10, i + 11);
        }
    }

    private UIVertex multiplyColor(UIVertex vertex, Color color)
    {
        if (MultiplyTextColor)
            vertex.color = Multiply(vertex.color, color);
        else
            vertex.color = color;
        return vertex;
    }

    private UIVertex calcCenterVertex(UIVertex top, UIVertex bottom)
    {
        UIVertex center = new UIVertex();
        center.normal = (top.normal + bottom.normal) / 2;
        center.position = (top.position + bottom.position) / 2;
        center.tangent = (top.tangent + bottom.tangent) / 2;
        center.uv0 = (top.uv0 + bottom.uv0) / 2;
        center.uv1 = (top.uv1 + bottom.uv1) / 2;

        if (MultiplyTextColor)
        {
            //multiply color
            var color = Color.Lerp(top.color, bottom.color, 0.5f);
            center.color = Multiply(color, colorCenter);
        }
        else
        {
            center.color = colorCenter;
        }

        return center;
    }


    public static Color32 Multiply(Color32 a, Color32 b)
    {
        a.r = (byte)((a.r * b.r) >> 8);
        a.g = (byte)((a.g * b.g) >> 8);
        a.b = (byte)((a.b * b.b) >> 8);
        a.a = (byte)((a.a * b.a) >> 8);
        return a;
    }



}
