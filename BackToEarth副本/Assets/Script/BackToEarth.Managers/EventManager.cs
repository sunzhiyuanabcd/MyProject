using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager :Singleton<MainManager> {

	private EventManager(){}

	public void DebugInfo(){
		Debug.Log ("This is EventManager");
	}
}
