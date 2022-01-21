using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMaterial : MonoBehaviour
{
    private const float mRoastTime = 10.0f;   //焼けるまでの時間
    private const float mBurntTime = 20.0f;   //焦げるまでの時間
    private Vector3 Size;
    public Texture[] BakingConditionTexture = new Texture[8];

    public enum BakingCondition　//肉の焼け具合
    {
        Raw,
        Roasted,
        Burnt,
    }
    public BakingCondition[] BakeSideCondition = new BakingCondition[2];//表と裏の焼き具合

    //時間と焼き具合の関係
    private Dictionary<float, BakingCondition> BakingConditionMap = new Dictionary<float, BakingCondition>()
    {
        {0.0f, BakingCondition.Raw},
        {mRoastTime, BakingCondition.Roasted},
        {mBurntTime, BakingCondition.Burnt}
    };

    /// <summary>
    /// 焼け具合の初期化と焼けた時のテクスチャを配列に登録
    /// </summary>
    private void Start()
    {
        BakeSideCondition[0] = BakingCondition.Raw;
        BakeSideCondition[1] = BakingCondition.Raw;
        string CloneName;
        string FoodName;

        CloneName = this.gameObject.name;
        FoodName = CloneName;
        BakingConditionTexture[0] = (Texture)Resources.Load("Model/" + FoodName + "/RoastAndRaw" + FoodName + "Texture");
        BakingConditionTexture[1] = (Texture)Resources.Load("Model/" + FoodName + "/RawAndRoast" + FoodName + "Texture");
        BakingConditionTexture[2] = (Texture)Resources.Load("Model/" + FoodName + "/BurntAndRaw" + FoodName + "Texture");
        BakingConditionTexture[3] = (Texture)Resources.Load("Model/" + FoodName + "/RawAndBurnt" + FoodName + "Texture");
        BakingConditionTexture[4] = (Texture)Resources.Load("Model/" + FoodName + "/RoastAndBurnt" + FoodName + "Texture");
        BakingConditionTexture[5] = (Texture)Resources.Load("Model/" + FoodName + "/BurntAndRoast" + FoodName + "Texture");
        BakingConditionTexture[6] = (Texture)Resources.Load("Model/" + FoodName + "/Roast" + FoodName + "Texture");
        BakingConditionTexture[7] = (Texture)Resources.Load("Model/" + FoodName + "/Burnt" + FoodName + "Texture");

        Size = this.gameObject.transform.localScale;
    }

    /// <summary>
    /// 地面に当たると消えます
    /// </summary>
    /// <param name="Ground"></param>
    private void OnTriggerEnter(Collider Ground)
    {
        if (Ground.gameObject.tag == "Ground")
        {
            this.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 焼いて色が変わるよ
    /// </summary>
    /// <param name="_GrillTime">焼かれている時間</param>
    /// <param name="__State">ひっくり返されたかどうか</param>
    public void Roast(float _GrillTime,Yakitori.state _State)
    {

        //ひっくり返されていて色が変わる時間になったか？
        if (Mathf.Floor(_GrillTime) == mRoastTime || Mathf.Floor(_GrillTime) == mBurntTime)
        {
            if (_State == Yakitori.state.Turnd) 
            {
                BakeSideCondition[0] = BakingConditionMap[Mathf.Floor(_GrillTime)];
            }
            else
            {
                BakeSideCondition[1] = BakingConditionMap[Mathf.Floor(_GrillTime)];
            }
        }
        //半生
        if (BakeSideCondition[0] == BakingCondition.Roasted && BakeSideCondition[1] == BakingCondition.Raw)
        {
            Texture NextTexture = this.gameObject.GetComponent<FoodMaterial>().BakingConditionTexture[0];
            this.gameObject.GetComponent<Renderer>().material.mainTexture = NextTexture;
        }
        else if (BakeSideCondition[0] == BakingCondition.Raw && BakeSideCondition[1] == BakingCondition.Roasted)
        {
            Texture NextTexture = this.gameObject.GetComponent<FoodMaterial>().BakingConditionTexture[1];
            this.gameObject.GetComponent<Renderer>().material.mainTexture = NextTexture;
        }
        //焦げ生
        else if (BakeSideCondition[0] == BakingCondition.Burnt && BakeSideCondition[1] == BakingCondition.Raw)
        {
            Texture NextTexture = this.gameObject.GetComponent<FoodMaterial>().BakingConditionTexture[2];
            this.gameObject.GetComponent<Renderer>().material.mainTexture = NextTexture;
        }
        else if (BakeSideCondition[0] == BakingCondition.Raw && BakeSideCondition[1] == BakingCondition.Burnt)
        {
            Texture NextTexture = this.gameObject.GetComponent<FoodMaterial>().BakingConditionTexture[3];
            this.gameObject.GetComponent<Renderer>().material.mainTexture = NextTexture;
        }
        //焦げ焼け
        else if (BakeSideCondition[0] == BakingCondition.Burnt && BakeSideCondition[1] == BakingCondition.Roasted)
        {
            Texture NextTexture = this.gameObject.GetComponent<FoodMaterial>().BakingConditionTexture[4];
            this.gameObject.GetComponent<Renderer>().material.mainTexture = NextTexture;
        }
        else if (BakeSideCondition[0] == BakingCondition.Roasted && BakeSideCondition[1] == BakingCondition.Burnt)
        {
            Texture NextTexture = this.gameObject.GetComponent<FoodMaterial>().BakingConditionTexture[5];
            this.gameObject.GetComponent<Renderer>().material.mainTexture = NextTexture;
        }
        //焼けた
        else if (BakeSideCondition[0] == BakingCondition.Roasted && BakeSideCondition[1] == BakingCondition.Roasted)
        {
            Debug.Log("やけた");
            Texture NextTexture = this.gameObject.GetComponent<FoodMaterial>().BakingConditionTexture[6];
            this.gameObject.GetComponent<Renderer>().material.mainTexture = NextTexture;
        }
        //焦げ焦げ
        else if (BakeSideCondition[0] == BakingCondition.Burnt && BakeSideCondition[1] == BakingCondition.Burnt)
        {
            Debug.Log("焦げた");
            Texture NextTexture = this.gameObject.GetComponent<FoodMaterial>().BakingConditionTexture[7];
            this.gameObject.GetComponent<Renderer>().material.mainTexture = NextTexture;
        }
    }

    /// <summary>
    /// 回転させ焼く面を変える
    /// </summary>
    /// <param name="_Turn">上に向かせるか向かせないか</param>
    public void Turn(Yakitori.state _state)
    {
        Vector3 Location = this.gameObject.transform.position;
        if (_state==Yakitori.state.Turnd)
        {
            Location.y += 0.1f;
        }
        else
        {
            Location.y -= 0.1f;
        }  
        this.transform.LookAt(Location);
        this.gameObject.transform.localScale = Size;
    }
}