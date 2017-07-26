using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自定义一个命名空间
namespace BackToEarth.FSM{
	//状态接口IState
	public interface IState{
		//状态名字,可以不设置 set 方法
		string Name {
			get;
		}
		//状态标签,这里有3个不同的时间节点,每个节点对应一个回调的方法
		//Enter进入状态
		//Stay处于状态中
		//Exit退出状态
		//有了每个状态对应的回调函数那么我们就可以在子类中实现或者是在外部指定实现逻辑
		string Tag{ set; get;}
		/// <summary>
		/// 当前状态的状态机,一个状态对应一个状态机,有时有根据状态获取状态机的需求
		/// </summary>
		/// <value>The parent.</value>
		IStateMachine Parent{ get; set;}
		/// <summary>
		/// 从进入状态时开始计算的时差,只有 get 方法
		/// </summary>
		/// <value>The timer.</value>
		float Timer{ get;}
		/// <summary>
		/// 当前状态的所有过度
		/// </summary>
		/// <value>The transitions.</value>
		List<ITransition> Transitions{ get;}
		//进入状态时的回调,参数是上一个状态
		void EnterCallback (IState pre);
		//退出状态时的回调,参数是下一个状态
		void ExitCallback (IState next);
		//处于状态中的回调有许多种不同的实现方法,我们这里将可能用到的实现方法一一都列出来
		//确保在需要用到的时候该方法存在

		void UpdateCallback(float deltaTime);
		void LateUpdateCallback (float deltaTime);
		void FixedUpdateCallback ();
		/// <summary>
		/// 添加状态过度
		/// </summary>
		/// <param name="t">T.</param>
		void AddTransition (ITransition t);
	}
}
