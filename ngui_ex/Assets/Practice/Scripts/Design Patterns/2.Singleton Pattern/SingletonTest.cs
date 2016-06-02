using UnityEngine;
using System.Collections;

public class SingletonTest : MonoBehaviour {
    
	void Start () {
        Singleton s = Singleton.getSingleton();
        Singleton s1 = Singleton.getSingleton();

        Debug.Log("s == s1 ? " + (s == s1));   
	}
	
}
