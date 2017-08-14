using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TagRecognizer.asset", menuName = "AI/Object Recognizers/Tag Recognizer", order = Utility.CREATE_ASSET_ORDER_ID)]
public class TagRecognizer : AIObjectRecognizer {

    [SerializeField]
    private string TagName;
    
    public override bool Poll(GameObject obj)
    {
        return obj.tag == TagName;
    }
}
