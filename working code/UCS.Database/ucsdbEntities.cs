using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace UCS.Database
{
	internal class ucsdbEntities : DbContext
	{
		public virtual DbSet<clan> clan
		{
			get;
			set;
		}

		public virtual DbSet<player> player
		{
			get;
			set;
		}

		public ucsdbEntities(string connectionString) : base("name=" + connectionString)
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			throw new UnintentionalCodeFirstException();
		}
	}
}
