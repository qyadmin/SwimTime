using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Reflection;
using System;
using DataItem;
namespace Dh_json
{
    public class DataJson
    {
        public static T ToObject<T>(string json) where T : new()
        {
            T p = new T();
            JsonData JD = JsonMapper.ToObject(json);

            foreach (FieldInfo field in p.GetType().GetFields())
            {
                if (!JD.Keys.Contains(field.Name))
                {
                    Debug.Log("JsonData没有对应的Key： " + field.Name);
                    continue;
                }
                if (field.FieldType == typeof(System.UInt32))
                {
                    p.GetType().GetField(field.Name).SetValue(p, UInt32.Parse(JD[field.Name].ToString()));
                    continue;
                }
                else if (field.FieldType == typeof(System.Int32))
                {
                    p.GetType().GetField(field.Name).SetValue(p, int.Parse(JD[field.Name].ToString()));
                    continue;
                }
                else if (field.FieldType == typeof(System.UInt64))
                {
                    p.GetType().GetField(field.Name).SetValue(p, ulong.Parse(JD[field.Name].ToString()));
                    continue;
                }
                else if (field.FieldType == typeof(System.Int64))
                {
                    p.GetType().GetField(field.Name).SetValue(p, long.Parse(JD[field.Name].ToString()));
                    continue;
                }
                else if (field.FieldType == typeof(System.Double))
                {
                    p.GetType().GetField(field.Name).SetValue(p, Double.Parse(JD[field.Name].ToString()));
                    continue;
                }
                else if (field.FieldType == typeof(System.Single))
                {
                    p.GetType().GetField(field.Name).SetValue(p, float.Parse(JD[field.Name].ToString()));
                    continue;
                }
                else if (field.FieldType == typeof(string))
                {
                    p.GetType().GetField(field.Name).SetValue(p, JD[field.Name].ToString());
                    continue;
                }
                else
                {
                    p.GetType().GetField(field.Name).SetValue(p, JD[field.Name]);
                }
            }
            return p;
        }
    }
}
