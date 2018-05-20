using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Makes");

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Makes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MakeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Models_MakeId",
                table: "Models",
                column: "MakeId");


            // SEED
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Volkswagen')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('GM')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Ford')");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('Fusion',(Select Id from Makes Where Name = 'Volkswagen'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('Golf',(Select Id from Makes Where Name = 'Volkswagen'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('Jetta',(Select Id from Makes Where Name = 'Volkswagen'))");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('Captiva',(Select Id from Makes Where Name = 'GM'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('Camaro',(Select Id from Makes Where Name = 'GM'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('Mustang',(Select Id from Makes Where Name = 'GM'))");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('Fiesta',(Select Id from Makes Where Name = 'Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('KA',(Select Id from Makes Where Name = 'Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) Values ('EcoSport',(Select Id from Makes Where Name = 'Ford'))");

            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature 1')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature 2')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature 3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "Model");

            migrationBuilder.DropTable(
                name: "Make");

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Make",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Make", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.id);
                    table.ForeignKey(
                        name: "FK_Model_Make_id",
                        column: x => x.id,
                        principalTable: "Make",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            //UNSEED
            migrationBuilder.Sql("DELETE * FROM Features");
            migrationBuilder.Sql("DELETE * FROM Makes");
        }
    }
}
