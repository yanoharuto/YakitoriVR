using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    //焼き鳥の設置場所
    private Yakitori mYakitoriComponent;
    private ParticleSystem mFlameParticle;
    private ParticleSystem mCollisionParticle;

    void Start()
    {
        mFlameParticle = this.gameObject.transform.GetChild(5).gameObject.GetComponent<ParticleSystem>();
        mFlameParticle.Stop();
        mCollisionParticle=this.gameObject.transform.GetChild(6).gameObject.GetComponent<ParticleSystem>();
        mCollisionParticle.Stop();
    }
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="_Other"></param>
    void OnTriggerStay(Collider _Other)
    {
        //左手で焼きぐしをひっくり返す
        if (_Other.CompareTag("LeftHand") ) 
        {
            if(mYakitoriComponent) //(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
            {
                //初めてひっくり返すとき
                mYakitoriComponent.Turn();
                
                if (mYakitoriComponent._state == Yakitori.state.Cooked)
                {
                    //焼き鳥の調理を終える
                    mYakitoriComponent = null;
                    mFlameParticle.Stop();
                }
            }
        }
        //右手に当たったなら光る
        if (_Other.gameObject.CompareTag("RightHand"))
        {
            if (!mCollisionParticle.isPlaying)
            {
                mCollisionParticle.Play();
            }
        }
        //当たったのがYakitoriで
        if (_Other.gameObject.CompareTag("Yakitori"))
        {
            
            if (_Other.gameObject.transform.parent == null &&　//焼き鳥の串を持っていなくて
                mYakitoriComponent == null &&　　　　　　　　　//Grillに何も乗っておらず
                _Other.gameObject.GetComponent<Yakitori>().mFoodMaterialList.Count == 4)//焼き鳥に具が四つ刺さっている
            {
                mYakitoriComponent = _Other.gameObject.GetComponent<Yakitori>();
                mYakitoriComponent.Roast();
                //炎を出す
                if (!mFlameParticle.isPlaying)
                {
                    mFlameParticle.Play();
                }
            }
        }
    }

    private void OnTriggerExit(Collider _Hand)
    {
        if (_Hand.gameObject.CompareTag("RightHand"))
        { 
            mCollisionParticle.Stop();
        }
    }
}