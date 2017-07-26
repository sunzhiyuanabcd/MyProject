using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 在 GameManager 中将所用到的音频加入到该 Dic 中
*/
/// <summary>
/// 这里负责管理游戏所有用到的的音频,并根据关键字来播放和停止相关的音频
/// </summary>
public class AudioManager:Singleton<AudioManager>{
	private AudioSource m_AudioSource;
	/// <summary>
	/// Dic容器,用来存储所有用到的音频资源
	/// </summary>
	private Dictionary<string,AudioClip> m_AudioClips;
	private AudioManager(){
		m_AudioSource = null;
		m_AudioClips = new Dictionary<string, AudioClip> ();
	}
	/// <summary>
	/// 向容器中添加一个音频
	/// </summary>
	public void AddClip(string audioKey,AudioClip audioClip){
		if (m_AudioClips.ContainsKey (audioKey)) {
			Debug.LogError ("This key already exist!");
			return;
		}
		m_AudioClips.Add (audioKey, audioClip);
	}
	/// <summary>
	///  从容器中删除一个音频
	/// </summary>
	public void RemoveClip(string audioKey){
		if (!m_AudioClips.ContainsKey (audioKey)) {
			Debug.LogError ("This key not exist!");
			return;
		}
		m_AudioClips.Remove (audioKey);
	}
	/// <summary>
	/// 根据key 来播放相应的音频
	/// </summary>
	public void PlayClip(AudioSource audioSource,string audioKey){
		if (!m_AudioClips.ContainsKey (audioKey)||m_AudioClips.Count==0) {
			Debug.LogError ("This key not exist!");
			return;
		}
		m_AudioSource = audioSource;
		m_AudioSource.clip=m_AudioClips [audioKey];
		m_AudioSource.Play ();
		if (!m_AudioSource.isPlaying) {
			m_AudioSource.clip = null;
			m_AudioSource = null;
		}
	
	}
	public void DebugInfo(){
		Debug.Log ("This is AudioManager");
	}
}
