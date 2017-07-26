using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager :Singleton<MainManager>{
	//The Manager of Managers,可以去配置其他的管理器,但是仅仅可以去接触管理器,不可以对
	//其他部分进行修改
	private MainManager(){
		Debug.Log ("Constructor");
	}
	public void DebugInfo(){
		Debug.Log ("This is main manager");
	}
}
