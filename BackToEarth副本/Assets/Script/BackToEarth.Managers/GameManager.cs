using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :Singleton<GameManager>{
	//游戏的管理者,相关游戏的数据都在这里进行配置,相关的改变都在这里进行
	private GameManager(){
	}
	public void DebugInfo(){
		Debug.Log ("This is GameManager");
	}
}
