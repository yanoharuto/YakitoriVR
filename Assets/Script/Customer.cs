using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] GameObject mAttachPoint;
    private const int mFoodMaterialMax = 4;
    private List<int> mOrderList = new List<int>();
    private bool mIsEatable => StateEnum.Idle == _state;
    private ScoreUI _scoreUI;
    private CustomerOrderUI _customerOrderUI;
    //食べ物の種類と数字を紐づけ
    private Dictionary<string, int> FoodDic = new Dictionary<string, int>()
    {
        {"GreenPepper",0 },
        {"Onion",1 },
        {"Pork",2 },
        {"MeatBall",3 }
    };
    //食べ物の評価基準
    private Dictionary<string, bool> ScoreDic = new Dictionary<string, bool>()
    {
        {"Raw",false },
        {"Roast",true },
        {"Burnt",false}
    };
    //お客さんの状態
    private enum StateEnum
    {
        Idle,
        Eat,
        Happy,
        Angry
    }StateEnum _state = StateEnum.Idle;
    protected Animator _animator;
    /// <summary>
    /// 食べ物を注文する.FoodDicで登録した名前と参照する
    /// </summary>
    private void SetOrder()
    {
        mOrderList.Clear();
        for (int i = 0; i < mFoodMaterialMax; i++)
        {
            int OrderNumber;
            OrderNumber = Random.Range(0, 3);
            mOrderList.Add(OrderNumber);

        }
        _customerOrderUI.OnDecideOrder(mOrderList);
    }
    // Start is called before the first frame update
    void Start()
    {
        _customerOrderUI = this.gameObject.GetComponent<CustomerOrderUI>();
        _scoreUI = this.gameObject.GetComponent<ScoreUI>();
        SetOrder();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider _LeftHand)
    {

        //焼き鳥にぶつかって左手のトリガーボタンを押したなら
        if (_LeftHand.gameObject.CompareTag("LeftHand"))//&&OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger,OVRInput.Controller.LTouch)
        {
            Debug.Log("L");
            if (_LeftHand.gameObject.GetComponent<LeftHand>().mYakitoriComponent._state == Yakitori.state.Cooked)
            {
                _animator.SetTrigger("Eat");
                mAttachPoint = _LeftHand.gameObject;
                for (int i = 0; i < mFoodMaterialMax; i++)
                {
                    //串に刺さっている食べ物
                    Transform food;
                    food = _LeftHand.gameObject.GetComponent<Yakitori>().mFoodMaterialList[i];
                    //注文したものと名前が一緒？
                    _scoreUI.OnDecideScore(mOrderList[i] == FoodDic[food.name]);
                    //焼き加減はちょうどいい？
                    FoodMaterial FoodM = food.gameObject.GetComponent<FoodMaterial>();
                    _scoreUI.OnDecideScore(ScoreDic[FoodM.BakeSideCondition[1].ToString()]);
                    _scoreUI.OnDecideScore(ScoreDic[FoodM.BakeSideCondition[0].ToString()]);
                }
                SetOrder();
            }
        }
    }
}