using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class LoginMessage : Message
	{
		public string AdvertisingGUID;

		public string AndroidDeviceID;

		public string DeviceModel;

		public string Language;

		public string MacAddress;

		public string MasterHash;

		public string OpenUDID;

		public string OSVersion;

		public int Unknown;

		public string Unknown1;

		public byte Unknown2;

		public string Unknown3;

		public long UserID;

		public string UserToken;

		public LoginMessage(Client client, BinaryReader br) : base(client, br)
		{
			base.Decrypt();
		}

		public override void Decode()
		{
			if (base.Client.CState == 1)
			{
				using (CoCSharpPacketReader coCSharpPacketReader = new CoCSharpPacketReader(new MemoryStream(base.GetData())))
				{
					this.UserID = coCSharpPacketReader.ReadInt64();
					this.UserToken = coCSharpPacketReader.ReadString();
					this.Unknown = coCSharpPacketReader.ReadInt32();
					this.MasterHash = coCSharpPacketReader.ReadString();
					this.Unknown1 = coCSharpPacketReader.ReadString();
					this.OpenUDID = coCSharpPacketReader.ReadString();
					this.MacAddress = coCSharpPacketReader.ReadString();
					this.DeviceModel = coCSharpPacketReader.ReadString();
					this.AdvertisingGUID = coCSharpPacketReader.ReadString();
					this.OSVersion = coCSharpPacketReader.ReadString();
					this.Unknown2 = coCSharpPacketReader.ReadByte();
					this.Unknown3 = coCSharpPacketReader.ReadString();
					this.AndroidDeviceID = coCSharpPacketReader.ReadString();
					this.Language = coCSharpPacketReader.ReadString();
				}
			}
		}

		public override void Process(Level level)
		{
			if (Convert.ToBoolean(ConfigurationManager.AppSettings["maintenanceMode"]) || base.Client.CState == 0)
			{
				LoginFailedMessage expr_2E = new LoginFailedMessage(base.Client);
				expr_2E.SetErrorCode(10);
				PacketManager.ProcessOutgoingPacket(expr_2E);
				return;
			}
			level = ResourcesManager.GetPlayer(this.UserID, false);
			if (level != null)
			{
				if (level.GetAccountStatus() == 99)
				{
					LoginFailedMessage expr_62 = new LoginFailedMessage(base.Client);
					expr_62.SetErrorCode(11);
					PacketManager.ProcessOutgoingPacket(expr_62);
					return;
				}
			}
			else
			{
				level = ObjectManager.CreateAvatar(this.UserID);
				byte[] buffer = new byte[20];
				new Random().NextBytes(buffer);
				using (SHA1 sHA = new SHA1CryptoServiceProvider())
				{
					this.UserToken = BitConverter.ToString(sHA.ComputeHash(buffer)).Replace("-", string.Empty);
				}
			}
			if (Convert.ToBoolean(ConfigurationManager.AppSettings["useCustomPatch"]) && this.MasterHash != ObjectManager.FingerPrint.sha)
			{
				LoginFailedMessage expr_FB = new LoginFailedMessage(base.Client);
				expr_FB.SetErrorCode(7);
				expr_FB.SetResourceFingerprintData(ObjectManager.FingerPrint.SaveToJson());
				expr_FB.SetContentURL(ConfigurationManager.AppSettings["patchingServer"]);
				expr_FB.SetUpdateURL("http://www.ultrapowa.com/client");
				PacketManager.ProcessOutgoingPacket(expr_FB);
				return;
			}
			base.Client.ClientSeed = this.Unknown;
			ResourcesManager.LogPlayerIn(level, base.Client);
			level.Tick();
			LoginOkMessage arg_16D_0 = new LoginOkMessage(base.Client);
			ClientAvatar playerAvatar = level.GetPlayerAvatar();
			arg_16D_0.SetAccountId(playerAvatar.GetId());
			arg_16D_0.SetPassToken(this.UserToken);
			arg_16D_0.SetServerEnvironment("prod");
			arg_16D_0.SetDaysSinceStartedPlaying(10);
			arg_16D_0.SetServerTime(Math.Round(level.GetTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000.0).ToString());
			arg_16D_0.SetAccountCreatedDate("1414003838000");
			arg_16D_0.SetStartupCooldownSeconds(0);
			arg_16D_0.SetCountryCode(this.Language);
			PacketManager.ProcessOutgoingPacket(arg_16D_0);
			if (ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId()) == null)
			{
				level.GetPlayerAvatar().SetAllianceId(0L);
			}
			PacketManager.ProcessOutgoingPacket(new OwnHomeDataMessage(base.Client, level));
		}
	}
}
