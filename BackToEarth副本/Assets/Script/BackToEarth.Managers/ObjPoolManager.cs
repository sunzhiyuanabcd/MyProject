using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager :Singleton<ObjPoolManager>{
	//对象池管理器,场景中除了静态的物体,都要交到该管理器来管理比如Monster,Particle,MaterialItem等
	//字典用来维护各个 Obj的 List,并用一个 Tag 来对每个 List 标识
	private Dictionary<string,List<GameObject>> poolList;
	private GameObject objectInPoolList;
	private ObjPoolManager(){
		poolList = new Dictionary<string, List<GameObject>> ();
	}
	/// <summary>
	/// 清空对象池
	/// </summary>
	public void PoolClear(){
		poolList.Clear ();
	}

	/// <summary>
	/// 从对象池中得到一个游戏对象,如果该对象池中有实例对象,那么返回一个实例对象
	/// 如果这个对象池中没有实例对象那么则实例化一个
	/// </summary>
	/// <returns>The game object.</returns>
	/// <param name="prefab">Prefab.</param>
	/// <param name="position">Position.</param>
	/// <param name="rotation">Rotation.</param>
	public GameObject GetGameObject(GameObject prefab,Vector3 position,Quaternion rotation){
		//每个 List 前有一个 String 来作为 Tag 标识
		string tag=prefab.tag;
		if (poolList.ContainsKey (tag) && poolList [tag].Count > 0) {
			objectInPoolList = poolList [tag] [0];
			objectInPoolList.transform.position = position;
			objectInPoolList.transform.rotation = rotation;
			objectInPoolList.SetActive (true);
			poolList [tag].Remove (objectInPoolList);
		} else {
			if (!poolList.ContainsKey (tag)) {
				poolList.Add (tag, new List<GameObject> ());
			}
			objectInPoolList = GameObject.Instantiate (prefab, position, rotation);

		}
		return objectInPoolList;
	}

	/// <summary>
	/// 回收一个游戏对象到对象池中,如果你对象池中还保有该游戏对象的 List
	/// , 那么将其放入到 List 中 
	/// </summary>
	/// <param name="recycledObj">Recycled object.</param>
	public void RecycleObject(GameObject recycledObj){
		if (poolList.ContainsKey (recycledObj.tag)) {
			poolList [recycledObj.tag].Add (recycledObj);
			recycledObj.SetActive (false);
		}
	}

	/// <summary>
	/// 将某一个不再使用的 List 移除掉
	/// </summary>
	/// <param name="prefab">Prefab.</param>
	public void DestroyList(GameObject prefab){
		if (poolList.ContainsKey (prefab.tag)) {
			poolList.Remove (prefab.tag);
		}
	}

}
