using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    private GameObject mYakitori;　　　  //今持っている焼き鳥
    private ObjectPool _MYakitoriObjectPool;
    /// <summary>
    /// 焼き鳥をゲームが始まった時に実体化させる
    /// </summary>
    private void SetIronStick()
    {
        mYakitori=_MYakitoriObjectPool.GetObject();
        //Yakitoriを右手と同じ位置にする
        mYakitori.transform.position = this.gameObject.transform.position;

        //yakitoriを右手の子にする。
        mYakitori.transform.SetParent(this.gameObject.transform);
        mYakitori.transform.position = this.gameObject.transform.position;
    }
    private void Awake()
    {
        _MYakitoriObjectPool = gameObject.AddComponent<ObjectPool>();
        _MYakitoriObjectPool.CreatePool((GameObject)Resources.Load("Model/IronStick"), 5);

        SetIronStick();

    }
    private void Update()
    {
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        mYakitori.transform.localScale = this.gameObject.transform.localScale;
    }
    /// <summary>
    /// 調理場に焼き鳥を渡す
    /// </summary>
    /// <param name="_Grill">調理場</param>
    private void OnTriggerStay(Collider _Grill)
    {
        if (_Grill.gameObject.CompareTag("Grill"))
        {
            //右手人差し指で焼き鳥を設置
            if (//OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)&&
                mYakitori.GetComponent<Yakitori>().mFoodMaterialList.Count == 4)
            {
                Vector3 SetPosition = _Grill.transform.position;
                SetPosition.y += 0.5f;
                mYakitori.gameObject.transform.position = SetPosition;
                mYakitori.gameObject.transform.SetParent(null) ;
                SetIronStick();
            }
        }
    }
}
