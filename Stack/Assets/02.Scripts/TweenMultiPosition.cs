//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[AddComponentMenu("NGUI/Tween/Tween Multi Position")]
//public class TweenMultiPosition : UITweener
//{
//    public Vector3 from;
//    public List<Vector3> via;
//    public Vector3 to;

//    [HideInInspector]
//    public bool worldSpace = false;

//    Transform mTrans;
//    UIRect mRect;

//    public Transform cachedTransform {
//        get {
//            if (mTrans == null)
//                mTrans = transform;
//            return mTrans;
//        }
//    }

//    [System.Obsolete("Use 'value' instead")]
//    public Vector3 position {
//        get {
//            return this.value;
//        }
//        set {
//            this.value = value;
//        }
//    }

//    public Vector3 value
//    {
//        get {
//            return worldSpace ? cachedTransform.position : cachedTransform.localPosition;
//        }
//        set {
//            if (mRect == null || !mRect.isAnchored || worldSpace) {
//                if (worldSpace) cachedTransform.position = value;
//                else cachedTransform.localPosition = value;
//            }
//            else {
//                value -= cachedTransform.localPosition;
//                NGUIMath.MoveRect(mRect, value.x, value.y);
//            }
//        }
//    }

//    void Awake() {
//        mRect = GetComponent<UIRect>();
//    }

//    protected override void OnUpdate(float factor, bool isFinished)
//    {
//        //Vector3 result;
//        //result = from * (1f - factor) + to * factor;
//        //for(int i = 0; i < via.Count; i++)
//        //{
//        //    result += via[i] * factor;
//        //}

//        //value = result;
//    }

//    static public TweenMultiPosition Begin (GameObject go, float duration, Vector3 pos, bool worldSpace)
//    {
//        int count = via.len + 1;

//        for (int i = 0; i < count; i++)
//        {
//            TweenMultiPosition comp = UITweener.Begin<TweenMultiPosition>(go, duration / count);
//            comp.worldSpace = worldSpace;
//            comp.from = comp.value;
//            comp.to = pos;

//            if (duration <= 0f)
//            {
//                comp.Sample(1f, true);
//                comp.enabled = false;
//            }
//        }
//        return comp;
//    }
//}
