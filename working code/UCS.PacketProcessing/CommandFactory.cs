using ClashRoyaleProxy;
using System;
using System.Collections.Generic;
using System.IO;
using UCS.Helpers;
using UCS.PacketProcessing.Commands;

namespace UCS.PacketProcessing
{
	internal static class CommandFactory
	{
		private static readonly Dictionary<uint, Type> m_vCommands;

		static CommandFactory()
		{
			CommandFactory.m_vCommands = new Dictionary<uint, Type>();
			CommandFactory.m_vCommands.Add(120u, typeof(NextCardCommand));
			CommandFactory.m_vCommands.Add(537u, typeof(SearchOppenentCommand));
			CommandFactory.m_vCommands.Add(506u, typeof(UnlockChestCommand));
			CommandFactory.m_vCommands.Add(516u, typeof(LevelUpCommand));
			CommandFactory.m_vCommands.Add(528u, typeof(BuyChestCommand));
			CommandFactory.m_vCommands.Add(530u, typeof(BuyCardCommand));
			CommandFactory.m_vCommands.Add(535u, typeof(ClaimAchievementCommand));
			CommandFactory.m_vCommands.Add(536u, typeof(TvReplaySeenCommand));
			CommandFactory.m_vCommands.Add(538u, typeof(ChestNextCardCommand));
		}

		public static object Read(BinaryReader br)
		{
			uint num = br.ReadUInt32WithEndian();
			if (CommandFactory.m_vCommands.ContainsKey(num))
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"[CRS]    Processing command ",
					PacketTypes.GetPacketTypeByID((int)num),
					" (",
					num,
					")"
				}));
				return Activator.CreateInstance(CommandFactory.m_vCommands[num], new object[]
				{
					br
				});
			}
			Console.WriteLine("[CRS]    The command " + num + " is unhandled");
			return null;
		}
	}
}
