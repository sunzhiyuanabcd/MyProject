using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 加载,卸载场景,并为场景配置相关的信息
/// </summary>
public class LevelManager :Singleton<LevelManager> {
	private LevelManager(){
	}

	public void DebugInfo(){
		Debug.Log ("This is LevelManager");
	}
	public void ChangeScene(){
		
	}
}
