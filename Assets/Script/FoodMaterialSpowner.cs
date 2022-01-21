using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMaterialSpowner : MonoBehaviour
{
    
    [SerializeField] GameObject mFoodMaterialPrefab;
    private Vector3 mTeleportPosition;
    private float mInterval = 3.0f;
    private float mDelta = 0;
    private ObjectPool m_Pool;
    private const int mFirstFoodObjectCount = 10;

    private void Start()
    {
        m_Pool = this.gameObject.GetComponent<ObjectPool>();
        m_Pool.CreatePool(mFoodMaterialPrefab,mFirstFoodObjectCount);
        mTeleportPosition = this.gameObject.transform.position;
        mTeleportPosition.y += 1;
        mTeleportPosition.z += 1;
    }
   /// <summary>
   /// 食べ物を呼び出す
   /// </summary>
    void Update()
    {
        this.mDelta += Time.deltaTime;
        if (this.mDelta > this.mInterval) 
        {
            this.mDelta = 0;
            this.mInterval = Random.Range(1.0f, 3.0f);
            GameObject Food = m_Pool.GetObject();
            Food.transform.position = mTeleportPosition;
        }
    }
}