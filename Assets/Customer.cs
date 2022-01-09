using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{

    private List<int> Order = new List<int>();
    private List<Transform> FoodList = new List<Transform>();
    private const int FoodMaterialMax = 4;
    private int Score;
    private const int CorrectAnswerScore = 30;
    //食べ物の種類と数字を紐づけ
    private Dictionary<string, int> FoodDic = new Dictionary<string, int>()
    {
        {"GreenPepper",1 },
        {"Onion",2 },
        {"Pork",3 },
        {"MeatBall",4 }
    };
    //食べ物の評価基準
    private Dictionary<string, int> ScoreDic = new Dictionary<string, int>()
    {
        {"Raw",10 },
        {"Roast",25 },
        {"Burnt",10 }
    };
    private void ScoreAdd(int _AddScore)
    {
        Score += _AddScore;
        Debug.Log(Score);
    }
    /// <summary>
    /// 食べ物を注文するFoodDicで登録した名前と参照する
    /// </summary>
    private void SetOrder()
    {
        for (int i = 0; i < FoodMaterialMax; i++)
        {
            Order.Add(Random.Range(0, 4));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        SetOrder();
    }

    private void OnTriggerStay(Collider _LeftHand)
    {
        for (int i = 0; i < FoodMaterialMax; i++)
        {
            //左手が持っている串に刺さっている食べ物をFoodListに追加
            //FoodList.Add(_LeftHand.gameObject.transform.GetChild(2).gameObject.GetComponent<Yakitori>().FoodMaterialList[i]);
            FoodList.Add(_LeftHand.gameObject.GetComponent<Yakitori>().FoodMaterialList[i]);
            //注文したものと名前が一緒？
            if (Order[i] == FoodDic[FoodList[i].name])
            {
                ScoreAdd(CorrectAnswerScore);
            }
            FoodMaterial FoodM = FoodList[i].GetComponent<FoodMaterial>();
            ScoreAdd(ScoreDic[FoodM.BakeSideCondition[0].ToString()]);
            ScoreAdd(ScoreDic[FoodM.BakeSideCondition[1].ToString()]);
        }

        SetOrder();
    }
}
