using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    #region 单例
    private static ObjectPool instance;
//    private ObjectPool()
//    {
//        pool = new Dictionary<string, List<GameObject>>();
//        prefabs = new Dictionary<string, GameObject>();
//    }
    public static ObjectPool GetInstance()
    {
        if (instance == null)
        {
            instance = new ObjectPool();
        }
        return instance;
    }
	public void   Clear()
	{
		instance = new ObjectPool();
	}


    #endregion

//    public void ClearChild(Transform parent)
//    {
//        foreach (Transform childin in parent)
//            GameObject.Destroy(childin.gameObject);
//    }

    /// <summary>
    /// 对象池
    /// </summary>
    private Dictionary<string, List<GameObject>> pool;

    /// <summary>
    /// 预设体
    /// </summary>
    private Dictionary<string, GameObject> prefabs;

    /// <summary>
    /// 获取对象装在父物体中
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="parentTrans"></param>
    /// <returns></returns>
	public GameObject GetObj(GameObject obj, Transform parentTrans=null)
    {
        GameObject go = GameObject.Instantiate(obj);
        go.transform.parent = parentTrans;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
		go.SetActive (true);
        return go;
    }

    /// <summary>
    /// 从对象池中获取对象
    /// </summary>
    /// <param name="objName"></param>
    /// <returns></returns>
    public GameObject GetObj(GameObject obj)
    {
        //结果对象
        GameObject result = null;
        //判断是否有该名字的对象池
        if (pool.ContainsKey(obj.name))
        {
            //对象池里有对象
            if (pool[obj.name].Count > 0)
            {
                //获取结果
                result = pool[obj.name][0];
                //激活对象
                result.SetActive(true);
                //从池中移除该对象
                pool[obj.name].Remove(result);
                //返回结果
                return result;
            }
        }
        //如果没有该名字的对象池或者该名字对象池没有对象

        GameObject prefab = null;
        ////如果已经加载过该预设体
        //if (prefabs.ContainsKey(objName))
        //{
        //    prefab = prefabs[objName];
        //}
        //else     //如果没有加载过该预设体
        //{
        //    //加载预设体
        //    prefab = Resources.Load<GameObject>("Prefabs/" + objName);
        //    //更新字典
        //    prefabs.Add(objName, prefab);
        //}

        //生成
        result = UnityEngine.Object.Instantiate(obj);
        //改名（去除 Clone）
        result.name = obj.name;
        //返回
        return result;
    }

    /// <summary>
    /// 回收对象到对象池
    /// </summary>
    /// <param name="objName"></param>
    public void RecycleObj(GameObject obj)
    {
        GameObject.Destroy(obj);
        ////设置为非激活
        //obj.SetActive(false);
        ////判断是否有该对象的对象池
        //if (pool.ContainsKey(obj.name))
        //{
        //    //放置到该对象池
        //    pool[obj.name].Add(obj);
        //}
        //else
        //{
        //    //创建该类型的池子，并将对象放入
        //    pool.Add(obj.name, new List<GameObject>() { obj });
        //}
        //obj.transform.SetParent(null);
    }

    public void ClearAll()
    {
      //  pool.Clear();
		Clear ();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

//public class Bullet : MonoBehaviour
//{

//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    /// <summary>
//    /// 3秒后自动回收到对象池
//    /// </summary>
//    /// <returns></returns>
//    IEnumerator AutoRecycle()
//    {
//        yield return new WaitForSeconds(3f);

//        ObjectPool.GetInstance().RecycleObj(gameObject);
//    }

//    private void OnEnable()
//    {
//        StartCoroutine(AutoRecycle());
//    }
//}


