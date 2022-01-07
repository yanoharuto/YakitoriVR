using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    //焼き鳥の設置場所
    private GameObject mAttachPoint;
    private Rigidbody mYakitoriRigidbody;
    private Yakitori mYakitori;


    void Start()
    {
        mAttachPoint = GameObject.Find("Grill/AttachPoint");
    }
    //焼き鳥をGrillに親子付け
    void OnCollisionEnter(Collision _Yakitori)
    {
        //to do 焼き鳥が当たったらではなく右手が当たったらに変える
        //右手人差し指で焼き鳥を設置OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) &&
        if ( _Yakitori.gameObject.CompareTag("Yakitori"))
        {
            mYakitoriRigidbody = _Yakitori.gameObject.GetComponent<Rigidbody>();
            
            _Yakitori.gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);//横向きに設置
            //焼き鳥を位置調整     
            _Yakitori.gameObject.transform.position = mAttachPoint.transform.position;


            mYakitori = _Yakitori.gameObject.GetComponent<Yakitori>();
        }
    }

    /// <summary>
    /// 焼き鳥を焼いて色を付ける
    /// </summary>
    /// <param name="_Yakitori">焼き鳥</param>
    private void OnTriggerStay(Collider _Yakitori)
    {
        //Yakitoriが親子付けされているなら
        if (_Yakitori.gameObject.CompareTag("Yakitori") && !(mYakitori == null))
        {
            _Yakitori.gameObject.GetComponent<Yakitori>().Roast();
            Debug.Log("やいてる");
        }
    }

}