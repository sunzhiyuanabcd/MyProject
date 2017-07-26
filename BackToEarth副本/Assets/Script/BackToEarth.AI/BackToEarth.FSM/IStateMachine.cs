using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackToEarth.FSM{
	public interface IStateMachine{
		/// <summary>
		/// 当前状态
		/// </summary>
		/// <value>The state of the current.</value>
		IState CurrentState{ get;}
		/// <summary>
		/// 默认状态
		/// </summary>
		/// <value>The default state.</value>
		IState DefaultState{ get; set;}
		/// <summary>
		/// 向状态机中添加一个状态
		/// </summary>
		/// <param name="state">State.</param>
		void AddState (IState state);
		/// <summary>
		/// 从状态机中移除一个状态
		/// </summary>
		/// <param name="state">State.</param>
		void RemoveState (IState state);
		/// <summary>
		/// 根据 Tag 值从状态机中获取一个状态
		/// </summary>
		/// <returns>查找到的状态</returns>
		/// <param name="tag">Tag.</param>
		IState GetStateWithTag (string tag);
	}
}
