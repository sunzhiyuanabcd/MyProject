using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackToEarth.MessageFram;
using BackToEarth.FSM;
/// <summary>
/// 对各个 Manager 的测试
/// </summary>
public class TestManagers : MonoBehaviour,IMsgReceiver{
	//以下是对 AudioManager的变量声明
	public AudioClip[] AudioClips;
	public GameObject Cube1;
	public GameObject Cube2;

	void Awake(){
		//要实现发送和接收信息要引入命名空间BackToEarth.MessageFram.发送的一方要实现接口IMsgSender,
		//接收的一方要实现IMsgReceiver.
		this.RegisterMsg ("CubeDeath", DeathMessage);
	}
	void Start () {
		//以下是对 AudioManager的初始化
		MainManager.Instance.DebugInfo ();
	
		AudioManager.Instance.AddClip ("UpGrade", AudioClips [0]);
		AudioManager.Instance.AddClip ("Win", AudioClips [1]);
		Debug.Log ("Git 第一次修改");
	}

	void Update (){
		//以下是对 AudioManager的测试
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Debug.Log("Alpha1 Down");
			AudioManager.Instance.PlayClip (Cube1.GetComponent<AudioSource> (), "UpGrade");
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Debug.Log("Alpha2 Down");
			AudioManager.Instance.PlayClip (Cube1.GetComponent<AudioSource> (), "Win");
		}
			
	}

	void DeathMessage(params object[] paramsList){
		Debug.Log (paramsList [0].ToString ());
	}
}
