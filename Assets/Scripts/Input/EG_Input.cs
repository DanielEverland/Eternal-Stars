using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_Input : MonoBehaviour {

    public static bool IsSuppressed { get; private set; }

    private static int suppressFrame;

    private const int MAX_FRAMES_FOR_UNSUPPRESS = 2;

	public static void SuppressInput()
    {
        suppressFrame = Time.frameCount;

        IsSuppressed = true;
    }
    private void Update()
    {
        if(Time.frameCount - suppressFrame > MAX_FRAMES_FOR_UNSUPPRESS && IsSuppressed == true)
        {
            IsSuppressed = false;
        }
    }
}
