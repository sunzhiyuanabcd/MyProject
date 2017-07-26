using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackToEarth.FSM{
	/// <summary>
	/// 用户状态过渡的接口,从哪个状态过渡到哪个状态去;过渡名
	/// </summary>
	public interface ITransition{
		IState From{ get; set;}
		IState To{ get; set;}
		string Name{ get;}
		/// <summary>
		/// 过渡是否已经结束
		/// </summary>
		/// <returns><c>true</c>, if callback was transitioned, <c>false</c> otherwise.</returns>
		bool TransitionCallback();
		/// <summary>
		/// 能否开始过渡
		/// </summary>
		/// <returns><c>true</c>, 开始过渡, <c>false</c> 未达到开始条件</returns>
		bool ShouldBegin ();
	}
}
