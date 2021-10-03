// SharpCompress.Lazy<T>
using System;
using System.Runtime.InteropServices;

[ComVisible(true)]
public class Lazy<T>
{
	private readonly Func<T> _lazyFunc;

	private bool _evaluated;

	private T _value;

	public T Value
	{
		get
		{
			if (!_evaluated)
			{
				_value = _lazyFunc();
				_evaluated = true;
			}
			return _value;
		}
	}

	public Lazy(Func<T> lazyFunc)
	{
		_lazyFunc = lazyFunc;
	}
}
