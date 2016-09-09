using System;

namespace UCS.PacketProcessing
{
	public static class Key
	{
		private static readonly byte[] _standardPrivateKey = new byte[]
		{
			24,
			145,
			212,
			1,
			250,
			219,
			81,
			210,
			93,
			58,
			145,
			116,
			212,
			114,
			169,
			246,
			145,
			164,
			91,
			151,
			66,
			133,
			212,
			119,
			41,
			196,
			92,
			101,
			56,
			7,
			13,
			133
		};

		private static readonly byte[] _standardPublicKey = new byte[]
		{
			114,
			241,
			164,
			164,
			196,
			142,
			68,
			218,
			12,
			66,
			49,
			15,
			128,
			14,
			150,
			98,
			78,
			109,
			198,
			166,
			65,
			169,
			212,
			28,
			59,
			80,
			57,
			216,
			223,
			173,
			194,
			126
		};

		public static ClashKeyPair Crypto
		{
			get
			{
				return new ClashKeyPair((byte[])Key._standardPublicKey.Clone(), (byte[])Key._standardPrivateKey.Clone());
			}
		}
	}
}
