using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    public Yakitori mYakitoriComponent;

    /// <summary>
    /// 焼き鳥をつかみます
    /// </summary>
    /// <param name="_Yakitori">焼き鳥</param>
    private void OnTriggerStay(Collider _Yakitori)
    {
        //左手のボタンを押し
        //if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //両面を焼いた焼き鳥に触れて
            if (_Yakitori.CompareTag("Yakitori"))
            {
                //今当たっている焼き鳥のYakitoriComponent
                mYakitoriComponent = _Yakitori.gameObject.GetComponent<Yakitori>();

                //焼き鳥の調理が終わって
                if (mYakitoriComponent._state == Yakitori.state.Cooked &&
                    _Yakitori.gameObject.transform.parent == null)
                {
                    //左手に焼き鳥をくっつける
                    _Yakitori.gameObject.transform.position = this.gameObject.transform.position;
                    _Yakitori.gameObject.transform.SetParent(this.gameObject.transform);
                }
            }
        }
    }
}
