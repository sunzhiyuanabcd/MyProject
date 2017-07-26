using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackToEarth.FSM{
	//状态机的核心是将控制类解放出来,分到不同的类中实现
	//由于这是一个状态机那么必然要实现 IStateMachine 接口,又因为我们要实现分层的状态机,那么我们
	//还需要继承于状态类,并且实现状态机的接口,那么它本身既可以作为一个普通的状态来使用,还可以作为状态机
	//去控制和管理状态
	public class SUNStateMachine:SUNState,IStateMachine{
		private List<IState> _states;
		private IState _currentState;
		private IState _defaultState;
		//由于转化过程并不是瞬间执行的我们可能要在过度状态中做一些其他的逻辑,所以
		//在转化到下一个过程中的时候我们要判断过度状态执行完了没有
		private bool _isTransition=false;
		private ITransition _t;
		#region IStateMachine implementation
		public IState CurrentState {
			get {
				return _currentState;
			}
		}
		public IState DefaultState {
			get {
				return _defaultState;
			}
			set {
				//每次给默认状态赋值的时候要检测状态机中有没有该状态,如果没有就将其加入
				AddState(value);
				_defaultState = value;
			}
		}
		public SUNStateMachine(string name,IState defaultState):base(name){
			_states = new List<IState> ();
			_defaultState = defaultState;
		}
		public void AddState (IState state)
		{
			if (state != null && !_states.Contains (state)) {
				_states.Add (state);
				//当将每一个状态加入到状态机的时候,将该 state 的 parent 设置为该状态机
				state.Parent=this;
				if (_defaultState == null) {
					_defaultState = state;
				}
			}
		}
		public void RemoveState (IState state)
		{
			//要删的状态是否是当前状态
			//在状态机运行过程中不能删除当前状态
			if(_currentState==state){
				return;
			}
			if (state != null && _states.Contains (state)) {
				_states.Remove (state);
				//删除状态时,将其父状态机置为空
				state.Parent=null;
				//检测删除的是不是默认状态,如果是的话,重新指定默认状态
				//指定的方式是如果 List 长度大于1,那么将第一个元素作为默认状态,否则为 null
				if(_defaultState==state){
					_defaultState = (_states.Count >= 1) ? _states [0] : null;
				}
			}
		}
		public IState GetStateWithTag (string tag)
		{
			return null;
		}
		/// <summary>
		/// 重写 由SUNState 中继承来的UpdateCallback方法,从而加入每帧判断是否满足过度条件的逻辑
		/// </summary>
		/// <param name="deltaTime">Delta time.</param>
		public override void UpdateCallback(float deltaTime){
			if (_isTransition) {
				
				if (_t.TransitionCallback()) {
					DoTransition (_t);
					_isTransition = false;
				}
				//如果处于过度状态,不应该执行以下任何的操作
				return;
			}
			//如果没有处在过度状态
			base.UpdateCallback (deltaTime);
			if (_currentState == null) {
				_currentState = _defaultState;
			}
			//检测每个状态是否满足转换条件如果满足的话,替换当前状态
			List<ITransition> ts=_currentState.Transitions;
			int count = ts.Count;
			for (int i = 0; i <count ; i++) {
				ITransition t = ts [i];
				if (t.ShouldBegin ()) {
					_isTransition = true;
					//这里就不能直接过度到目标状态了
					//DoTransition (t);
					_t=t;
					return;
				}
			}
			//让当前状态每帧更新
			_currentState.UpdateCallback ( deltaTime);
		}
		public override void LateUpdateCallback (float deltaTime)
		{
			base.LateUpdateCallback (deltaTime);
			//当前状态也需要每帧更新
			_currentState.LateUpdateCallback(deltaTime);
		}
		public override void FixedUpdateCallback ()
		{
			base.FixedUpdateCallback ();
			//让当前状态每帧更新
			_currentState.FixedUpdateCallback();
		}
		#endregion
		/// <summary>
		/// 开始进行过度
		/// </summary>
		private void DoTransition( ITransition t){
			//分3步  1:退出当前状态
			//       2:设置当前的状态	
			//		  3:转到下一个状态
			_currentState.ExitCallback(t.To);
			_currentState = t.To;
			_currentState.EnterCallback (t.From);
		}
	}
}