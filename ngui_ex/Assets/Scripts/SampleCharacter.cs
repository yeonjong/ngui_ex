using UnityEngine;

public class SampleCharacter : MonoBehaviour {
    
    void OnHover(bool isOver) {
        if (isOver) Debug.Log("Hover over " + name);
        else Debug.Log("Hover away from " + name);
    }

    void OnPress(bool isPressed)
    {
        if (isPressed) Debug.Log("Pressed " + name);
        else Debug.Log("Unpressed " + name);
    }

    void OnClick()
    {
        Debug.Log("Click on " + name);
    }

}
