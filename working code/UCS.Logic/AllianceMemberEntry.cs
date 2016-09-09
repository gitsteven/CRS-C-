using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UCS.Core;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class AllianceMemberEntry
	{
		private readonly int m_vDonatedTroops;

		private readonly byte m_vIsNewMember;

		private readonly int m_vReceivedTroops;

		private readonly int[] m_vRoleTable = new int[]
		{
			1,
			1,
			4,
			2,
			3
		};

		private readonly int m_vWarCooldown;

		private readonly int m_vWarOptInStatus;

		private long m_vAvatarId;

		private int m_vOrder;

		private int m_vPreviousOrder;

		private int m_vRole;

		public AllianceMemberEntry(long avatarId)
		{
			this.m_vAvatarId = avatarId;
			this.m_vIsNewMember = 0;
			this.m_vOrder = 1;
			this.m_vPreviousOrder = 1;
			this.m_vRole = 1;
			this.m_vDonatedTroops = 200;
			this.m_vReceivedTroops = 100;
			this.m_vWarCooldown = 0;
			this.m_vWarOptInStatus = 1;
		}

		public static void Decode(byte[] avatarData)
		{
			using (new BinaryReader(new MemoryStream(avatarData)))
			{
			}
		}

		public byte[] Encode()
		{
			List<byte> arg_12_0 = new List<byte>();
			Level player = ResourcesManager.GetPlayer(this.m_vAvatarId, false);
			arg_12_0.AddInt64(this.m_vAvatarId);
			arg_12_0.AddString(player.GetPlayerAvatar().GetAvatarName());
			arg_12_0.AddInt32(this.m_vRole);
			arg_12_0.AddInt32(player.GetPlayerAvatar().GetAvatarLevel());
			arg_12_0.AddInt32(player.GetPlayerAvatar().GetArenaId());
			arg_12_0.AddInt32(player.GetPlayerAvatar().GetScore());
			arg_12_0.AddInt32(this.m_vDonatedTroops);
			arg_12_0.AddInt32(this.m_vReceivedTroops);
			arg_12_0.AddInt32(this.m_vOrder);
			arg_12_0.AddInt32(this.m_vPreviousOrder);
			arg_12_0.Add(this.m_vIsNewMember);
			arg_12_0.AddInt32(this.m_vWarCooldown);
			arg_12_0.AddInt32(this.m_vWarOptInStatus);
			arg_12_0.Add(1);
			arg_12_0.AddInt64(this.m_vAvatarId);
			return arg_12_0.ToArray();
		}

		public long GetAvatarId()
		{
			return this.m_vAvatarId;
		}

		public static int GetDonations()
		{
			return 150;
		}

		public int GetOrder()
		{
			return this.m_vOrder;
		}

		public int GetPreviousOrder()
		{
			return this.m_vPreviousOrder;
		}

		public int GetRole()
		{
			return this.m_vRole;
		}

		public bool HasLowerRoleThan(int role)
		{
			bool result = true;
			if (role < this.m_vRoleTable.Length && this.m_vRole < this.m_vRoleTable.Length && this.m_vRoleTable[this.m_vRole] >= this.m_vRoleTable[role])
			{
				result = false;
			}
			return result;
		}

		public byte IsNewMember()
		{
			return this.m_vIsNewMember;
		}

		public void Load(JObject jsonObject)
		{
			this.m_vAvatarId = jsonObject["avatar_id"].ToObject<long>();
			this.m_vRole = jsonObject["role"].ToObject<int>();
		}

		public JObject Save(JObject jsonObject)
		{
			jsonObject.Add("avatar_id", this.m_vAvatarId);
			jsonObject.Add("role", this.m_vRole);
			return jsonObject;
		}

		public void SetAvatarId(long id)
		{
			this.m_vAvatarId = id;
		}

		public void SetOrder(int order)
		{
			this.m_vOrder = order;
		}

		public void SetPreviousOrder(int order)
		{
			this.m_vPreviousOrder = order;
		}

		public void SetRole(int role)
		{
			this.m_vRole = role;
		}
	}
}
