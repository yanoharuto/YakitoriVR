using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderUI : MonoBehaviour
{
    private GameObject mUIindicator;
    [SerializeField] GameObject mGreenPepper;
    [SerializeField] GameObject mOnion;
    [SerializeField] GameObject mPork;
    [SerializeField] GameObject mMeatBall;

    private const int mOrders = 4;

    private ObjectPool m_GPObjectPool;
    private ObjectPool m_OnionObjectPool;
    private ObjectPool m_PorkObjectPool;
    private ObjectPool m_MBObjectPool;

    private List<GameObject> mOrderSpriteList = new List<GameObject>();
    //食べ物の種類と数字を紐づけ
    private Dictionary<int ,ObjectPool> FoodDic = new Dictionary<int ,ObjectPool>();
    private void Start()
    {
        m_GPObjectPool = gameObject.AddComponent<ObjectPool>();
        m_OnionObjectPool = gameObject.AddComponent<ObjectPool>();
        m_PorkObjectPool = gameObject.AddComponent<ObjectPool>();
        m_MBObjectPool = gameObject.AddComponent<ObjectPool>();

        FoodDic[0] = m_GPObjectPool;
        FoodDic[1] = m_OnionObjectPool;
        FoodDic[2] = m_PorkObjectPool;
        FoodDic[3] = m_MBObjectPool;


        FoodDic[0].CreatePool(mGreenPepper, mOrders);
        FoodDic[1].CreatePool(mOnion, mOrders);
        FoodDic[2].CreatePool(mPork, mOrders);
        FoodDic[3].CreatePool(mMeatBall, mOrders);


        mUIindicator = this.gameObject.transform.GetChild(8).gameObject;

    }
    public void OnDecideOrder(List<int> _OrderList)
    {
        if (m_GPObjectPool == null)
        {
            Start();
        }
        for (int i = 0; i < mUIindicator.transform.childCount; i++)
        {
            GameObject ChildSprit = mUIindicator.transform.GetChild(i).gameObject;
            ChildSprit.SetActive(false);
            ChildSprit.transform.SetParent(null);
        }

        float addUIPositionY = 0;
        foreach (int i in _OrderList)
        {
            //使えるスプライトを探す
            GameObject newSprit;
            newSprit = FoodDic[i].GetObject();

            Vector3 indicateLocation = mUIindicator.transform.position;
            indicateLocation.y += addUIPositionY;
            newSprit.transform.position = indicateLocation;
            //spriteがプレイヤーに見えるようにする
            Vector3 PlayerLocation=new Vector3(0.0f,1.0f,0.0f);
            newSprit.transform.LookAt(PlayerLocation);
            addUIPositionY+=0.4f;
        }

    }
}
