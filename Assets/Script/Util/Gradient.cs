// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：字体渐变工具
// 作成時間：2017-08-14
// 類を作る：Gradient.cs
// 版    本：v 1.0
// 会    社：广州恩赐方信息科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Gradient")]
public class Gradient : BaseMeshEffect
{
    [SerializeField]
    private Color32 topColor = Color.white;

    [SerializeField]
    private Color32 bottomColor = Color.black;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
        {
            return;
        }

        var vertexList = new List<UIVertex>();
        vh.GetUIVertexStream(vertexList);
        int count = vertexList.Count;
        if (count == 0)
            return;
        ApplyGradient(vertexList, 0, count);
        vh.Clear();
        vh.AddUIVertexTriangleStream(vertexList);
    }

    private void ApplyGradient(List<UIVertex> vertexList, int start, int end)
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
            uiVertex.color = Color32.Lerp(bottomColor, topColor, (uiVertex.position.y - bottomY) / uiElementHeight);
            vertexList[i] = uiVertex;
        }
    }
}