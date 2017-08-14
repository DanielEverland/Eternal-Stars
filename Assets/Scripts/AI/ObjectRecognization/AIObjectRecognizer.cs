using UnityEngine;

public abstract class AIObjectRecognizer : AIComponent {

    public abstract bool Poll(GameObject obj);
}
