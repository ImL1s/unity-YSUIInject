using System;

namespace YSFramework
{
	public class BaseManager<T> : Singleton<T>, IManager 
		where T:IManager,new()
	{
		/// <summary>
		/// 初始化Manager
		/// </summary>
		public static void PreInit()
		{
			Instance.Init ();
		}

		#region IManager implementation
		public virtual void Init ()
		{
			
		}
		#endregion

	}
}

