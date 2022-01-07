using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    private GameObject mYakitori;　　　  //今持っている焼き鳥

    // Start is called before the first frame update
    void Awake()
    {
        //実体化させる                                     //yakitoriPrefabをロード
        mYakitori = Instantiate((GameObject)Resources.Load("Model/IronStick"));
        //Yakitoriを右手と同じ位置にする
        mYakitori.transform.position = this.gameObject.transform.position;

        //yakitoriを右手の子にする。
        mYakitori.transform.SetParent(this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
