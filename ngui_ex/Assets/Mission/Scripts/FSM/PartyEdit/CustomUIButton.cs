using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomUIButton : MonoBehaviour {

	static public CustomUIButton current;
	public List<EventDelegate> onClick = new List<EventDelegate>();
		
	void OnClick ()
	{
		if (current == null && UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3)
		{
			current = this;
			EventDelegate.Execute(onClick);
			current = null;
		}

	}

}