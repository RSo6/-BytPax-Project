using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BytPax.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: true),
                    BodyText = table.Column<string>(type: "text", nullable: true),
                    Article_CategoryId = table.Column<int>(type: "integer", nullable: true),
                    Article_ImagePath = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Athlete_CategoryId = table.Column<int>(type: "integer", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Athlete_Description = table.Column<string>(type: "text", nullable: true),
                    TypeOfSport = table.Column<string>(type: "text", nullable: true),
                    Category_Description = table.Column<string>(type: "text", nullable: true),
                    Category_Name = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    HistoricalEvent_Description = table.Column<string>(type: "text", nullable: true),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HistoricalEvent_SportId = table.Column<int>(type: "integer", nullable: true),
                    ImportanceLevel = table.Column<int>(type: "integer", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    SportId = table.Column<int>(type: "integer", nullable: true),
                    AthleteName = table.Column<string>(type: "text", nullable: true),
                    RecordDescription = table.Column<string>(type: "text", nullable: true),
                    RecordValue = table.Column<double>(type: "double precision", nullable: true),
                    DateAchieved = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    OriginYear = table.Column<int>(type: "integer", nullable: true),
                    User_FullName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    User_ImagePath = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    RoleType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_Article_CategoryId",
                        column: x => x.Article_CategoryId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_Athlete_CategoryId",
                        column: x => x.Athlete_CategoryId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_HistoricalEvent_SportId",
                        column: x => x.HistoricalEvent_SportId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_Article_CategoryId",
                table: "BaseEntity",
                column: "Article_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_Athlete_CategoryId",
                table: "BaseEntity",
                column: "Athlete_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_HistoricalEvent_SportId",
                table: "BaseEntity",
                column: "HistoricalEvent_SportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseEntity");
        }
    }
}
