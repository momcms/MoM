using Microsoft.Data.Entity.Migrations;
using MoM.Module.Interfaces;
using System;

namespace MoM.Module.Managers
{
    public partial class DataMigrationManager : Migration, IDataMigration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            throw new NotImplementedException();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            base.Down(migrationBuilder);
        }
    }
}
