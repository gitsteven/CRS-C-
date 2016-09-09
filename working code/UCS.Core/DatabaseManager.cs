using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UCS.Database;
using UCS.Logic;

namespace UCS.Core
{
	internal class DatabaseManager
	{
		private static DatabaseManager singelton;

		private readonly string m_vConnectionString;

		public static DatabaseManager Singelton
		{
			get
			{
				if (DatabaseManager.singelton == null)
				{
					DatabaseManager.singelton = new DatabaseManager();
				}
				return DatabaseManager.singelton;
			}
		}

		public DatabaseManager()
		{
			this.m_vConnectionString = ConfigurationManager.AppSettings["databaseConnectionName"];
		}

		public void CreateAccount(Level l)
		{
			try
			{
				Debugger.WriteLine("[CRS]    Saving new account to database (player id: " + l.GetPlayerAvatar().GetId() + ")", null, 4);
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					ucsdbEntities.player.Add(new player
					{
						PlayerId = l.GetPlayerAvatar().GetId(),
						AccountStatus = l.GetAccountStatus(),
						AccountPrivileges = l.GetAccountPrivileges(),
						LastUpdateTime = l.GetTime(),
						IPAddress = l.GetIPAddress(),
						Avatar = l.GetPlayerAvatar().SaveToJSON(),
						GameObjects = l.SaveToJSON()
					});
					ucsdbEntities.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    An exception occured during CreateAccount processing :", ex, 4);
			}
		}

		public void CreateAlliance(Alliance a)
		{
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					ucsdbEntities.clan.Add(new clan
					{
						ClanId = a.GetAllianceId(),
						LastUpdateTime = DateTime.Now,
						Data = a.SaveToJSON()
					});
					ucsdbEntities.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    An exception occured during CreateAlliance processing :", ex, 4);
			}
		}

		public Level GetAccount(long playerId)
		{
			Level level = null;
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					player player = ucsdbEntities.player.Find(new object[]
					{
						playerId
					});
					if (player != null)
					{
						level = new Level();
						level.SetAccountStatus(player.AccountStatus);
						level.SetAccountPrivileges(player.AccountPrivileges);
						level.SetTime(player.LastUpdateTime);
						level.SetIPAddress(player.IPAddress);
						level.GetPlayerAvatar().LoadFromJSON(player.Avatar);
						level.LoadFromJSON(player.GameObjects);
					}
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    An exception occured during GetAccount processing :", ex, 4);
			}
			return level;
		}

		public Alliance GetAlliance(long allianceId)
		{
			Alliance alliance = null;
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					clan clan = ucsdbEntities.clan.Find(new object[]
					{
						allianceId
					});
					if (clan != null)
					{
						alliance = new Alliance();
						alliance.LoadFromJSON(clan.Data);
					}
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    An exception occured during GetAlliance processing :", ex, 4);
			}
			return alliance;
		}

		public List<Alliance> GetAllAlliances()
		{
			List<Alliance> list = new List<Alliance>();
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					foreach (clan current in ucsdbEntities.clan)
					{
						Alliance alliance = new Alliance();
						alliance.LoadFromJSON(current.Data);
						list.Add(alliance);
					}
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    An exception occured during GetAlliance processing:", ex, 4);
			}
			return list;
		}

		public long GetMaxAllianceId()
		{
			long result = 0L;
			using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
			{
				result = (from alliance in ucsdbEntities.clan
				select ((long?)alliance.ClanId) ?? 0L).DefaultIfEmpty<long>().Max<long>();
			}
			return result;
		}

		public List<long> GetAllPlayerIds()
		{
			List<long> list = new List<long>();
			using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
			{
				foreach (player current in ucsdbEntities.player)
				{
					list.Add(current.PlayerId);
				}
			}
			return list;
		}

		public long GetMaxPlayerId()
		{
			long result = 0L;
			using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
			{
				result = (from ep in ucsdbEntities.player
				select ((long?)ep.PlayerId) ?? 0L).DefaultIfEmpty<long>().Max<long>();
			}
			return result;
		}

		public void RemoveAlliance(Alliance alliance)
		{
			long allianceId = alliance.GetAllianceId();
			using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
			{
				ucsdbEntities.clan.Remove(ucsdbEntities.clan.Find(new object[]
				{
					(int)allianceId
				}));
				ucsdbEntities.SaveChanges();
			}
			ObjectManager.RemoveInMemoryAlliance(allianceId);
		}

		public void Save(Level avatar)
		{
			ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString);
            ucsdbEntities.Configuration.AutoDetectChangesEnabled = false;
            ucsdbEntities.Configuration.ValidateOnSaveEnabled = false;
			player player = ucsdbEntities.player.Find(new object[]
			{
				avatar.GetPlayerAvatar().GetId()
			});
			if (player != null)
			{
				player.LastUpdateTime = avatar.GetTime();
				player.AccountStatus = avatar.GetAccountStatus();
				player.AccountPrivileges = avatar.GetAccountPrivileges();
				player.IPAddress = avatar.GetIPAddress();
				player.Avatar = avatar.GetPlayerAvatar().SaveToJSON();
				player.GameObjects = avatar.SaveToJSON();
                ucsdbEntities.Entry<player>(player).State = System.Data.Entity.EntityState.Modified;
            }
			else
			{
				ucsdbEntities.player.Add(new player
				{
					PlayerId = avatar.GetPlayerAvatar().GetId(),
					AccountStatus = avatar.GetAccountStatus(),
					AccountPrivileges = avatar.GetAccountPrivileges(),
					LastUpdateTime = avatar.GetTime(),
					IPAddress = avatar.GetIPAddress(),
					Avatar = avatar.GetPlayerAvatar().SaveToJSON(),
					GameObjects = avatar.SaveToJSON()
				});
			}
			ucsdbEntities.SaveChanges();
		}

		public void Save(List<Level> avatars)
		{
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
                    ucsdbEntities.Configuration.AutoDetectChangesEnabled = false;
                    ucsdbEntities.Configuration.ValidateOnSaveEnabled = false;
					int num = 0;
					foreach (Level current in avatars)
					{
						Level obj = current;
						lock (obj)
						{
							player player = ucsdbEntities.player.Find(new object[]
							{
								current.GetPlayerAvatar().GetId()
							});
							if (player != null)
							{
								player.LastUpdateTime = current.GetTime();
								player.AccountStatus = current.GetAccountStatus();
								player.AccountPrivileges = current.GetAccountPrivileges();
								player.IPAddress = current.GetIPAddress();
								player.Avatar = current.GetPlayerAvatar().SaveToJSON();
								player.GameObjects = current.SaveToJSON();
                                ucsdbEntities.Entry<player>(player).State = System.Data.Entity.EntityState.Modified;
							}
							else
							{
								ucsdbEntities.player.Add(new player
								{
									PlayerId = current.GetPlayerAvatar().GetId(),
									AccountStatus = current.GetAccountStatus(),
									AccountPrivileges = current.GetAccountPrivileges(),
									LastUpdateTime = current.GetTime(),
									IPAddress = current.GetIPAddress(),
									Avatar = current.GetPlayerAvatar().SaveToJSON(),
									GameObjects = current.SaveToJSON()
								});
							}
						}
					}
					num++;
					if (num >= 500)
					{
						ucsdbEntities.SaveChanges();
						num = 0;
					}
					ucsdbEntities.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    An exception occured during Save processing for avatars :", ex, 4);
			}
		}

		public void Save(List<Alliance> alliances)
		{
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
                    ucsdbEntities.Configuration.AutoDetectChangesEnabled = false;
                    ucsdbEntities.Configuration.ValidateOnSaveEnabled = false;
					int num = 0;
					foreach (Alliance current in alliances)
					{
						Alliance obj = current;
						lock (obj)
						{
							clan clan = ucsdbEntities.clan.Find(new object[]
							{
								(int)current.GetAllianceId()
							});
							if (clan != null)
							{
								clan.LastUpdateTime = DateTime.Now;
								clan.Data = current.SaveToJSON();
                                ucsdbEntities.Entry<clan>(clan).State = System.Data.Entity.EntityState.Modified;
							}
							else
							{
								ucsdbEntities.clan.Add(new clan
								{
									ClanId = current.GetAllianceId(),
									LastUpdateTime = DateTime.Now,
									Data = current.SaveToJSON()
								});
							}
						}
					}
					num++;
					if (num >= 500)
					{
						ucsdbEntities.SaveChanges();
						num = 0;
					}
					ucsdbEntities.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    An exception occured during Save processing for alliances :", ex, 4);
			}
		}
	}
}
