using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Reflection;
using System;

public class Singleton<T> where T:class{
	//将单例的公共代码提炼出来,形成一个普通的单例模板

	private static T instance;
	public static readonly Type[] EmptyTypes = new Type[0];
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				ConstructorInfo ci = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, EmptyTypes, null);
				if (ci == null) { throw new InvalidOperationException("class must contain a private constructor"); }
				instance = (T)ci.Invoke(null);
			}
			return instance;
		}
	}
}
