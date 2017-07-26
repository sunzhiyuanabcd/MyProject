using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackToEarth.MessageFram;
public class TestCube : MonoBehaviour,IMsgSender{
	void Awake(){
		
	}
	// Use this for initialization
	void Start () {
		this.SendMsg ("CubeDeath", "10 minutes ago");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
