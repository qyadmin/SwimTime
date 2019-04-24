using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VerticalText : Text
{
    public enum LetterType
    {
        chinese,
        english,
    }
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        if (null == toFill)
            return;
        base.OnPopulateMesh(toFill);
        //获取所有的UIVertex,绘制一个字符对应6个UIVertex，绘制顺序为012 230 ,0在左上角
        List<UIVertex> listUIVertex = new List<UIVertex>();
        toFill.GetUIVertexStream(listUIVertex);

        var vertArray = listUIVertex.ToArray();
        for (int i = 0; i < vertArray.Length; i += 6)
        {
            float halfOfheight = Mathf.Abs(vertArray[i + 1].position.y - vertArray[i + 2].position.y) / 2.0f;
            float halfOfwidth = Mathf.Abs(vertArray[i + 1].position.x - vertArray[i].position.x) / 2.0f;
            Vector3 centerPos = (vertArray[i].position + vertArray[i + 2].position) / 2.0f;

            float angleZ = Mathf.Deg2Rad * (90);
            Matrix4x4 rMatrixZ = new Matrix4x4();
            rMatrixZ.SetRow(0, new Vector4(Mathf.Cos(angleZ), -Mathf.Sin(angleZ), 0, 0));
            rMatrixZ.SetRow(1, new Vector4(Mathf.Sin(angleZ), Mathf.Cos(angleZ), 0, 0));
            rMatrixZ.SetRow(2, new Vector4(0, 0, 1, 0));
            rMatrixZ.SetRow(3, new Vector4(0, 0, 0, 1));

            float angleY = Mathf.Deg2Rad * (-180);
            Matrix4x4 rMatrixY = new Matrix4x4();
            rMatrixY.SetRow(0, new Vector4(Mathf.Cos(angleY), 0, Mathf.Sin(angleY), 0));
            rMatrixY.SetRow(1, new Vector4(0, 1, 0, 0));
            rMatrixY.SetRow(2, new Vector4(-Mathf.Sin(angleY), 0, Mathf.Cos(angleY), 0));
            rMatrixY.SetRow(3, new Vector4(0, 0, 0, 1));

            float angleX = Mathf.Deg2Rad * (-180);
            Matrix4x4 rMatrixX = new Matrix4x4();
            rMatrixX.SetRow(0, new Vector4(1, 0, 0, 0));
            rMatrixX.SetRow(1, new Vector4(0, Mathf.Cos(angleX), -Mathf.Sin(angleX), 0));
            rMatrixX.SetRow(2, new Vector4(0, Mathf.Sin(angleX), Mathf.Cos(angleX), 0));
            rMatrixX.SetRow(3, new Vector4(0, 0, 0, 1));

            Matrix4x4 total = rMatrixZ * rMatrixY;//竖直排列从左往右读
                                                  //Matrix4x4 total = rMatrixZ;//竖直排列从右往左读

            centerPos = total.MultiplyVector(centerPos);

            vertArray[i].position = centerPos + new Vector3(-halfOfwidth, halfOfheight, 0.0f);
            vertArray[i + 1].position = centerPos + new Vector3(halfOfwidth, halfOfheight, 0.0f);
            vertArray[i + 2].position = centerPos + new Vector3(halfOfwidth, -halfOfheight, 0.0f);
            vertArray[i + 3].position = centerPos + new Vector3(halfOfwidth, -halfOfheight, 0.0f);
            vertArray[i + 4].position = centerPos + new Vector3(-halfOfwidth, -halfOfheight, 0.0f);
            vertArray[i + 5].position = centerPos + new Vector3(-halfOfwidth, halfOfheight, 0.0f);


            listUIVertex[i] = vertArray[i];
            listUIVertex[i + 1] = vertArray[i + 1];
            listUIVertex[i + 2] = vertArray[i + 2];
            listUIVertex[i + 3] = vertArray[i + 3];
            listUIVertex[i + 4] = vertArray[i + 4];
            listUIVertex[i + 5] = vertArray[i + 5];
        }

        toFill.Clear();
        toFill.AddUIVertexTriangleStream(listUIVertex);

    }
}