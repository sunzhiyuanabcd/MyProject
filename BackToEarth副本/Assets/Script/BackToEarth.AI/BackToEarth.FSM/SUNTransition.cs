using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackToEarth.FSM{

	//带有bool返回值的委托
	public delegate bool SUNTransitionDelegate();
	/// <summary>
	/// 用于进行状态的过度
	/// </summary>
	public class SUNTransition:ITransition{
		private IState _from;
		private IState _to;
		private string _name;
		//需要从外界赋执行逻辑
		public event SUNTransitionDelegate OnTransitionEvent;
		public event SUNTransitionDelegate OnCheckEvent;
		//构造函数
		public SUNTransition( string name,IState from,IState to){
			this._name = name;
			this._from = from;
			this._to = to;
		}

		#region ITransition implementation
		/// <summary>
		/// 从哪个状态开始过度
		/// </summary>
		/// <value>The form.</value>
		public IState From {
			get {
				return _from;
			}
			set {
				_from = value;	
			}
		}
		/// <summary>
		/// 过度到哪个状态去
		/// </summary>
		/// <value>To.</value>
		public IState To {
			get {
				return _to;
			}
			set {
				_to = value;
			}
		}
		public string Name{
			get{ 
				return _name; 
				}
		}
		#endregion
		/// <summary>
		/// 这个方法用于过度状态时的回调,判断是否在过度是执行额外的逻辑
		/// </summary>
		/// <returns><c>true</c>,返回 True 说明过度已经结束了可以切换到目标状态, <c>false</c> 过度没有结束</returns>
		public bool TransitionCallback(){
			if (OnTransitionEvent != null) {
				return OnTransitionEvent();
			}
			return true;
		}
		/// <summary>
		/// 能否开始过度,如果没有检测的话,这个状态就不应该开始,我们可以从外界做过度条件的编写,然后 OnCheck事件
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		public bool ShouldBegin()
		{
			if (OnCheckEvent != null) {
				return OnCheckEvent ();
			}
			return false;
		}
	}
}