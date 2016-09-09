using System;

namespace UCS.Utilities.Sodium
{
	public class DetachedBox
	{
		public byte[] CipherText
		{
			get;
			set;
		}

		public byte[] Mac
		{
			get;
			set;
		}

		public DetachedBox()
		{
		}

		public DetachedBox(byte[] cipherText, byte[] mac)
		{
			this.CipherText = cipherText;
			this.Mac = mac;
		}
	}
}
