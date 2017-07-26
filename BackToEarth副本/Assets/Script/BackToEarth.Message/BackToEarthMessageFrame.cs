using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackToEarth.MessageFram{
	/// <summary>
	///  消息接收接口
	/// </summary>
	public interface IMsgReceiver
	{

	}

	/// <summary>
	/// 消息发送接口
	/// </summary>
	public interface IMsgSender
	{

	}

	/// <summary>
	/// 消息分发器,用 c# 的 this 拓展实现
	/// this 拓展是一种拓展方式,只能写在静态类的静态方法中,拓展方法的第一个参数要用 this 修饰
	/// 虽然是类方法但是使用的时候是通过实例来调用的.
	/// </summary>
	public static class IMsgDispatcher
	{

		/// <summary>
		/// 消息处理器,对于去注册监听事件的收听用户的事件回调
		/// </summary>
		class IMsgHandler
		{
			public IMsgReceiver receiver;
			public VoidDelegate.WithParams callback;

			public IMsgHandler (IMsgReceiver receiver, VoidDelegate.WithParams callback)
			{
				this.receiver = receiver;
				this.callback = callback;
			}


		}

		/// <summary>
		/// 回调处理类,有一个带参的委托
		/// </summary>
		public class VoidDelegate
		{
			public delegate void WithParams (params object [] paramList);
		}
		/// <summary>
		/// 维护注册信息接收者的容器
		/// </summary>
		private static Dictionary<string,List<IMsgHandler>> handlers =
			new Dictionary<string, List<IMsgHandler>> ();
		public static void CleanMsg(){
			handlers.Clear ();
		}
		/// <summary>
		/// 注册消息监听, this 拓展实现
		/// </summary>
		/// <param name="self">Self.</param>
		/// <param name="msgName">Message name.</param>
		/// <param name="callback">Callback.</param>
		public static void RegisterMsg(this IMsgReceiver self,string msgName,VoidDelegate.WithParams callback){
			//在注册前会有一系列的检查和容错操作
			if(string.IsNullOrEmpty(msgName)){
				Debug.Log ("注册的消息监听名字为空");
				return;
			}
			if (callback == null) {
				Debug.Log ("注册的消息监听回调为空");
				return;
			}
			if (!handlers.ContainsKey (msgName)) {
				handlers [msgName] = new List<IMsgHandler> ();
			}
			var handlersOfThisMsg = handlers [msgName];
			foreach (var handler in handlersOfThisMsg) {
				if (handler.receiver == self && handler.callback == callback) {
					Debug.Log ("该监听已经被注册");
					return;
				}
			}
			handlersOfThisMsg.Add (new IMsgHandler (self, callback));
		}
		/// <summary>
		/// 发送消息方法
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="msgName">Message name.</param>
		/// <param name="paramList">Parameter list.</param>
		public static void SendMsg(this IMsgSender sender,string msgName,params object[] paramList){
			if (string.IsNullOrEmpty (msgName)) {
				Debug.Log ("发送的消息为空");
				return;
			}
			if (!handlers.ContainsKey (msgName)) {
				Debug.Log ("发送的消息未注册");
			}
			var handlersOfThisMsg = handlers [msgName];
			var handlerCount = handlersOfThisMsg.Count;
			for (int index = handlerCount - 1; index >= 0; --index) {
				var handler = handlersOfThisMsg [index];
				if (handler.receiver != null) {
					Debug.Log ("消息发送成功");
					handler.callback (paramList);
				} else {
					handlersOfThisMsg.Remove (handler);
				}
			}
		}

	}
}