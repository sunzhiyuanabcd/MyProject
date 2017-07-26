using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager:Singleton<MenuManager>{
	//所有 UI 用到的无关事件和行为的动画和特效都在这里进行配置
	private MenuManager(){
	}

	public void DebugInfo(){
		Debug.Log ("This is MenuManager");
	}
}
