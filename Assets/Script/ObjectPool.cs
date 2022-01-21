using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> m_PoolObjList;
    private GameObject m_PoolObj;

    // オブジェクトプールを作成
    public void CreatePool(GameObject obj, int maxCount)
    {
        m_PoolObj = obj;
        m_PoolObjList = new List<GameObject>();
        for (int i = 0; i < maxCount; i++)
        {
            var newObj = CreateNewObject();
            newObj.SetActive(false);
            m_PoolObjList.Add(newObj);
        }
    }
    /// <summary>
    /// 使用中のオブジェクトを探す
    /// </summary>
    /// <returns>新しく使えるオブジェクト</returns>
    public GameObject GetObject()
    {
        // 使用中でないものを探して返す
        foreach (var obj in m_PoolObjList)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // 全て使用中だったら新しく作って返す
        var newObj = CreateNewObject();
        newObj.SetActive(true);
        m_PoolObjList.Add(newObj);

        return newObj;
    }

    private GameObject CreateNewObject()
    {
        var newObj = Instantiate(m_PoolObj);
        newObj.name = m_PoolObj.name;

        return newObj;
    }
}
