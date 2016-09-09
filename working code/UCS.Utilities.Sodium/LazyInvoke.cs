using System;

namespace UCS.Utilities.Sodium
{
	public class LazyInvoke<T>
	{
		private readonly string _function;

		private readonly string _library;

		private T _method;

		private bool _missing;

		public T Method
		{
			get
			{
				if (this._missing)
				{
					this._method = DynamicInvoke.GetDynamicInvoke<T>(this._function, this._library);
					this._missing = false;
				}
				return this._method;
			}
		}

		public LazyInvoke(string function, string library)
		{
			this._function = function;
			this._library = library;
			this._missing = true;
		}
	}
}
