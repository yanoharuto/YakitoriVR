using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yakitori : MonoBehaviour
{
    //食べ物のRigidBody
    Rigidbody FoodRigidbody;
    MeshCollider FoodMeshCollider;
    //食べ物の刺さる場所
    GameObject ChildObject;
    Vector3 AttachPoint;
    private List<GameObject> FoodMaterialList = new List<GameObject>();//刺さっている食べ物のtransform
    private int FoodMaterialNum = 0;          //今刺さっている食べ物の数
    private const int FoodMaterialMax = 4;        //最大四個刺さります
 
    public float GrillTime;                  //焼かれた時間
    public bool Turn = true;                 //ひっくり返したことがあるか
    private const float RoastTime = 10.0f;   //焼けるまでの時間
    private const float BurntTime = 20.0f;   //焦げるまでの時間

    // Start is called before the first frame update
    void Start()
    {
        FoodMaterialNum = 0;
        GrillTime = 0.0f;
    }
    
    public void Roast()
    {
        GrillTime += 0.1f;
        for (int i = 0; i < FoodMaterialMax; i++)
        {
            FoodMaterialList[i].GetComponent<FoodMaterial>().Roast(GrillTime,Turn);
        }
    }
    /// <summary>
    /// 串に食べ物を刺す
    /// </summary>
    /// <param name="_FoodMaterial">串に当たった食べ物</param>
    private void OnTriggerEnter(Collider _FoodMaterial)
    {
        if (_FoodMaterial.gameObject.CompareTag("FoodMaterial") && FoodMaterialNum < FoodMaterialMax) //&& this.gameObject.transform.GetChild(FoodMaterialMax).gameObject.transform.parent == null) 
        {
            FoodMaterialNum++;
            //食べ物のRigidBody
            FoodRigidbody = _FoodMaterial.gameObject.GetComponent<Rigidbody>();
            FoodMeshCollider = _FoodMaterial.gameObject.GetComponent<MeshCollider>() ;

            //食べ物の刺さる場所　串に子オブジェクトがありそこに食べ物を付与する
            ChildObject = this.gameObject.transform.GetChild(FoodMaterialNum).gameObject;
            AttachPoint = ChildObject.transform.position;

            //今の串の一番奥にまで刺す
            _FoodMaterial.transform.position = AttachPoint;
            _FoodMaterial.transform.Rotate(this.gameObject.transform.rotation.eulerAngles);
            _FoodMaterial.tag = "Yakitori";

            //食べ物を親子付け
            _FoodMaterial.gameObject.transform.SetParent(ChildObject.transform);
           
            FoodRigidbody.useGravity = false;
            //isKinematicを付ける。つけないと重力落下の慣性みたいので滑っていく
            FoodRigidbody.isKinematic = true;
            _FoodMaterial.gameObject.layer = 9;

            //Listに食べ物を追加
            FoodMaterialList.Add(_FoodMaterial.gameObject);
        }
    }
}
