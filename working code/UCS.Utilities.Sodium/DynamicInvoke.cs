using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UCS.Utilities.Sodium
{
	internal static class DynamicInvoke
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class DynamicInvokeInternal
        {
			public static readonly DynamicInvoke.DynamicInvokeInternal ins = new DynamicInvoke.DynamicInvokeInternal();

			public static Func<ParameterInfo, Type>  func;

			internal Type GetDynamicInvoke(ParameterInfo param)
			{
				return param.ParameterType;
			}
		}

		public static T GetDynamicInvoke<T>(string function, string library)
		{
			TypeBuilder arg_73_0 = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("DynamicDllInvoke"), AssemblyBuilderAccess.Run).DefineDynamicModule("DynamicDllModule").DefineType("DynamicDllInvokeType", TypeAttributes.Public | TypeAttributes.UnicodeClass);
			MethodInfo method = typeof(T).GetMethod("Invoke");
			IEnumerable<ParameterInfo> arg_68_0 = method.GetParameters();
            Func<ParameterInfo, Type> arg_68_1 = null;
			if (arg_68_1 == null)
			{
                arg_68_1 = DynamicInvokeInternal.ins.GetDynamicInvoke;

            }
			Type[] parameterTypes = arg_68_0.Select(arg_68_1).ToArray<Type>();
			MethodBuilder expr_8A = arg_73_0.DefinePInvokeMethod(function, library, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.PinvokeImpl, CallingConventions.Standard, method.ReturnType, parameterTypes, CallingConvention.Cdecl, CharSet.Ansi);
			expr_8A.SetImplementationFlags(expr_8A.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			MethodInfo method2 = arg_73_0.CreateType().GetMethod(function);
			return (T)((object)Delegate.CreateDelegate(typeof(T), method2, true));
		}
	}
}
