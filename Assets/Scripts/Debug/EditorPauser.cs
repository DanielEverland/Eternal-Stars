using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPauser : MonoBehaviour {

	private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F8))
        {
            Debug.Break();
        }
    }
}
