using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackToEarth.FSM
{
	//没有参数和返回值的 Delegate
	public delegate void SUNStateDelegate();
	//没有返回值但是带有一个单数的 Delegate,参数为 IState 类型
	public delegate void SUNStateDelegateState(IState state);
	//没有返回值但有一个参数的 Delegate, 参数为 Float 类型
	public delegate void SUNStateDelegateFloat(float f);

	public class SUNState:IState
	{
		/// <summary>
		/// 当进入状态时调用的事件
		/// </summary>
		public event SUNStateDelegateState OnEnter;
		/// <summary>
		/// 当退出状态时调用的事件
		/// </summary>
		public event SUNStateDelegateState OnExite;
		/// <summary>
		/// 在Update时调用的事件
		/// </summary>
		public event SUNStateDelegateFloat OnUpdate;
		/// <summary>
		/// 在 LateUpdate 中调用的事件
		/// </summary>
		public event SUNStateDelegateFloat OnLateUpdate;
		/// <summary>
		/// 在 FixedUpdate 中调用的事件
		/// </summary>
		public SUNStateDelegate OnFixedUpdate;

		private string _name;
		private string _tag;
		private IStateMachine _parent;
		private float _timer;
		/// <summary>
		/// 一个状态可能有多个状态的转换,这里容器存储可能用到的各种状态过度
		/// </summary>
		private List<ITransition> _transitions;
		#region IState implementation

		public string Name {
			get {
				return _name;
			}
		}
		
		public string Tag {
			get {
				return _tag;
			}
			set {
				_tag = value;
			}
		}
		public IStateMachine Parent{
			get{ 
				return _parent;
			}
			set{ 
				_parent = value;
			}
		}
		public float Timer{
			get{ 
				return _timer;
			}
		}
		public SUNState(){
		}
		public SUNState(string name){
			_name = name;
			//由于 name 方法并没有 set 方法, name 一旦确定了就不能修改了
			//只能修改 tag 值
			_transitions=new List<ITransition>();
		}
		public List<ITransition> Transitions{
			get{ 
				return _transitions;
			}
		}
		public virtual void AddTransition(ITransition t){
			if(t!=null&& !_transitions.Contains(t)){
				_transitions.Add (t);
			}
		}
		public virtual void EnterCallback (IState pre)
		{
			_timer = 0f;
			//进入状态时系统调用 OnEnter 事件
			if(OnEnter!=null){
				OnEnter (pre);
			}
		}

		public virtual void ExitCallback (IState next)
		{
			if (OnExite != null) {
				OnExite (next);
			}	
			//重置计时器
			_timer = 0f;
		}

		public virtual void UpdateCallback (float deltaTime)
		{
			//从进入到现在的时间
			_timer += deltaTime;
			//Update时系统调用 OnUpdate 事件
			if (OnUpdate != null) {
				OnUpdate (deltaTime);
			}
		}

		public virtual void LateUpdateCallback (float deltaTime)
		{
			if (OnLateUpdate != null) {
				OnLateUpdate (deltaTime);
			}
	
		}

		public virtual void FixedUpdateCallback ()
		{
			if (OnFixedUpdate != null) {
				OnFixedUpdate ();
			}
		}

		#endregion
		
	}
}
