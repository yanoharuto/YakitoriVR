using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMaterial : MonoBehaviour
{
    private const float RoastTime = 10.0f;   //焼けるまでの時間
    private const float BurntTime = 20.0f;   //焦げるまでの時間
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
        {RoastTime, BakingCondition.Roasted},
        {BurntTime, BakingCondition.Burnt}
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
        FoodName = CloneName.Replace("(Clone)", "");
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
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 焼いて色が変わるよ
    /// </summary>
    /// <param name="_GrillTime">焼かれている時間</param>
    /// <param name="_Turn">ひっくり返されたかどうか</param>
    public void Roast(float _GrillTime, bool _Turn)
    {
        Debug.Log("何色？");

        //ひっくり返されていて色が変わる時間になったか？
        if (_Turn && (Mathf.Floor(_GrillTime) == RoastTime || Mathf.Floor(_GrillTime) == BurntTime))
        {
            BakeSideCondition[0] = BakingConditionMap[Mathf.Floor(_GrillTime)];
        }
        else if (!_Turn && (Mathf.Floor(_GrillTime) == RoastTime || Mathf.Floor(_GrillTime) == BurntTime))
        {
            BakeSideCondition[1] = BakingConditionMap[Mathf.Floor(_GrillTime)];
        }
        //半生
        if (BakeSideCondition[0] == BakingCondition.Roasted && BakeSideCondition[0] == BakingCondition.Raw)
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

    public void Turn(bool _Turn)
    {
        Vector3 Location = this.gameObject.transform.position;
        if (_Turn)
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