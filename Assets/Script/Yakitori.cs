using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yakitori : MonoBehaviour
{

    public List<Transform> mFoodMaterialList = new List<Transform>();//刺さっている食べ物のtransform
    private int mFoodMaterialNum = 0;          //今刺さっている食べ物の数
    private const int mFoodMaterialMax = 4;        //最大四個刺さります
 
    public float GrillTime;                  //焼かれた時間
    public enum state
    {
        Default,
        Turnd,
        Cooked
    }
    public state _state;
    // Start is called before the first frame update
    void Start()
    {
        mFoodMaterialNum = 0;
        GrillTime = 0.0f;
        _state = state.Default;
    }
    
    public void Roast()
    {
        float Rotate = 90;
        this.gameObject.transform.rotation.Set(Rotate,0,Rotate,0);
        GrillTime += 0.1f;
        for (int i = 0; i < mFoodMaterialMax; i++)
        {
            FoodMaterial Food = mFoodMaterialList[i].GetComponent<FoodMaterial>();
            //焼き色を付けていく
            Food.Roast(GrillTime,_state);
        }
    }
    public void Turn()
    {
        if (_state == Yakitori.state.Default)
        {
            _state = Yakitori.state.Turnd;
        }
        else if(_state==Yakitori.state.Turnd)
        {
            _state = Yakitori.state.Cooked;
        }
        for (int i = 0; i < mFoodMaterialMax; i++)
        {
            FoodMaterial Food = mFoodMaterialList[i].GetComponent<FoodMaterial>();
            Food.Turn(_state);
        }
    }
    /// <summary>
    /// 串に食べ物を刺す
    /// </summary>
    /// <param name="_Food">串に当たった食べ物</param>
    private void OnTriggerEnter(Collider _Food)
    {
        //食べ物のRigidBody
        Rigidbody FoodRigidbody;
        MeshCollider FoodMeshCollider;
        //食べ物の刺さる場所
        GameObject ChildObject;
        Vector3 AttachPoint;

        if (_Food.gameObject.CompareTag("FoodMaterial") && mFoodMaterialNum < mFoodMaterialMax)
        {
            mFoodMaterialNum++;
            //食べ物のRigidBody
            FoodRigidbody = _Food.gameObject.GetComponent<Rigidbody>();
            FoodMeshCollider = _Food.gameObject.GetComponent<MeshCollider>();

            //食べ物の刺さる場所　串に子オブジェクトがありそこに食べ物を付与する
            ChildObject = this.gameObject.transform.GetChild(mFoodMaterialNum).gameObject;
            AttachPoint = ChildObject.transform.position;

            //今の串の一番奥にまで刺す
            _Food.transform.position = AttachPoint;
            _Food.transform.Rotate(this.gameObject.transform.rotation.eulerAngles);
            _Food.tag = "Untagged";

            //食べ物を親子付け
            _Food.gameObject.transform.SetParent(ChildObject.transform);

            FoodRigidbody.useGravity = false;
            //isKinematicを付ける。つけないと重力落下の慣性みたいので滑っていく
            FoodRigidbody.isKinematic = true;
            _Food.gameObject.layer.ToString("Child");

            //Listに食べ物を追加
            mFoodMaterialList.Add(_Food.gameObject.transform);
        }
    }
    /// <summary>
    /// お客さんにぶつかったらActiveをfalseにする
    /// </summary>
    /// <param name="_Customer">お客さん</param>
    //private void OnTriggerStay(Collider _Customer)
    //{
    //    //お客さんとぶつかってトリガーボタンを離したなら
    //    if (_Customer.gameObject.transform.CompareTag("Customer") &&
    //        _state==state.Cooked
    //        //&& OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)
    //        )
    //    {
    //        //this.gameObject.SetActive(false);
    //    }
    //}


}