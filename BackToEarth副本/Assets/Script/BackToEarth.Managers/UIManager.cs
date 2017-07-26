using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UIManager统一管理所有用到的 UI
/// </summary>
public class UIManager:Singleton<UIManager> {

	private UIManager(){}
	public void DebugInfo(){
		Debug.Log ("This is UIManager");
	}
}
