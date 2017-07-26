using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 管理数据和文件,所有用到的数据和文件都交由这里统一管理
/// </summary>
public class DataManager:Singleton<DataManager>{

	private DataManager(){
	}

	public void DebugInfo(){
		Debug.Log ("This is DataManager");
	}
}
