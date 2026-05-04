using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CruxSuits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Num = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    DecimalSeparator = table.Column<int>(type: "int", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataFieldGeneralPermissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    EveryOneCanVisible = table.Column<bool>(type: "bit", nullable: false),
                    EveryOneCanModified = table.Column<bool>(type: "bit", nullable: false),
                    EveryOneCanRemoved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserCanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserCanModified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserCanRemoved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedRoleCanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedRoleCanModified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedRoleCanRemoved = table.Column<bool>(type: "bit", nullable: false),
                    OverCreatedRoleCanVisible = table.Column<bool>(type: "bit", nullable: false),
                    OverCreatedRoleCanModified = table.Column<bool>(type: "bit", nullable: false),
                    OverCreatedRoleCanRemoved = table.Column<bool>(type: "bit", nullable: false),
                    UnderCreatedRoleLevelCanVisible = table.Column<byte>(type: "tinyint", nullable: false),
                    UnderCreatedRoleLevelCanModified = table.Column<byte>(type: "tinyint", nullable: false),
                    UnderCreatedRoleLevelCanRemoved = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFieldGeneralPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataObjectGeneralPermissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    EveryOneCanVisible = table.Column<bool>(type: "bit", nullable: false),
                    EveryOneCanModified = table.Column<bool>(type: "bit", nullable: false),
                    EveryOneCanRemoved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserCanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserCanModified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserCanRemoved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedRoleCanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreatedRoleCanModified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedRoleCanRemoved = table.Column<bool>(type: "bit", nullable: false),
                    OverCreatedRoleCanVisible = table.Column<bool>(type: "bit", nullable: false),
                    OverCreatedRoleCanModified = table.Column<bool>(type: "bit", nullable: false),
                    OverCreatedRoleCanRemoved = table.Column<bool>(type: "bit", nullable: false),
                    UnderCreatedRoleLevelCanVisible = table.Column<byte>(type: "tinyint", nullable: false),
                    UnderCreatedRoleLevelCanModified = table.Column<byte>(type: "tinyint", nullable: false),
                    UnderCreatedRoleLevelCanRemoved = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataObjectGeneralPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonIdentityTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonIdentityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Roles_ParentRoleId",
                        column: x => x.ParentRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Traces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IpAdress = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OS = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Device = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UiResourceTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HasEditPermission = table.Column<bool>(type: "bit", nullable: false),
                    HasDeletePermission = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UiResourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Id3Digit = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    PhoneCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataObjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataObjects_DataObjectGeneralPermissions_Id",
                        column: x => x.Id,
                        principalTable: "DataObjectGeneralPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyNames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    CurrencyId = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyNames_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyNames_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenderNames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    GenderId = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenderNames_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenderNames_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonIdentityTypeNames",
                columns: table => new
                {
                    LanguageId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    PersonIdentityTypeId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonIdentityTypeNames", x => new { x.LanguageId, x.PersonIdentityTypeId });
                    table.ForeignKey(
                        name: "FK_PersonIdentityTypeNames_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonIdentityTypeNames_PersonIdentityTypes_PersonIdentityTypeId",
                        column: x => x.PersonIdentityTypeId,
                        principalTable: "PersonIdentityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneTypeNames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PhoneTypeId = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneTypeNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneTypeNames_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneTypeNames_PhoneTypes_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalTable: "PhoneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataFieldPermissionByRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFieldGeneralPermissionId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanEdited = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFieldPermissionByRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataFieldPermissionByRoles_DataFieldGeneralPermissions_DataFieldGeneralPermissionId",
                        column: x => x.DataFieldGeneralPermissionId,
                        principalTable: "DataFieldGeneralPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataFieldPermissionByRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataObjectPermissionByRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataObjectGeneralPermissionId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanEdited = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataObjectPermissionByRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataObjectPermissionByRoles_DataObjectGeneralPermissions_DataObjectGeneralPermissionId",
                        column: x => x.DataObjectGeneralPermissionId,
                        principalTable: "DataObjectGeneralPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataObjectPermissionByRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UiResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: false),
                    ParentUiResourceId = table.Column<int>(type: "int", nullable: true),
                    UiResourceTypeId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    RelatedDTOName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedFieldName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UiResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UiResources_UiResourceTypes_UiResourceTypeId",
                        column: x => x.UiResourceTypeId,
                        principalTable: "UiResourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UiResources_UiResources_ParentUiResourceId",
                        column: x => x.ParentUiResourceId,
                        principalTable: "UiResources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryNames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryNames_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryNames_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "Date", nullable: true),
                    BirthCountryId = table.Column<int>(type: "int", nullable: true),
                    BirthCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GenderId = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    NationalityId = table.Column<int>(type: "int", nullable: true),
                    BirthCountryId1 = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    NationalityCountryId = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonBase_Countries_BirthCountryId1",
                        column: x => x.BirthCountryId1,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonBase_Countries_NationalityCountryId",
                        column: x => x.NationalityCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonBase_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataFields",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ShortId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    DataObjectId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataFields_DataFieldGeneralPermissions_Id",
                        column: x => x.Id,
                        principalTable: "DataFieldGeneralPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataFields_DataObjects_DataObjectId",
                        column: x => x.DataObjectId,
                        principalTable: "DataObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataFieldPermissionByRoleDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFieldPermissionByRoleId = table.Column<int>(type: "int", nullable: false),
                    RecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanEdited = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFieldPermissionByRoleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataFieldPermissionByRoleDetail_DataFieldPermissionByRoles_DataFieldPermissionByRoleId",
                        column: x => x.DataFieldPermissionByRoleId,
                        principalTable: "DataFieldPermissionByRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataObjectPermissionByRoleDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataObjectPermissionByRoleId = table.Column<int>(type: "int", nullable: false),
                    RecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanEdited = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataObjectPermissionByRoleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataObjectPermissionByRoleDetail_DataObjectPermissionByRoles_DataObjectPermissionByRoleId",
                        column: x => x.DataObjectPermissionByRoleId,
                        principalTable: "DataObjectPermissionByRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUiResourcePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UiResourceId = table.Column<int>(type: "int", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanModified = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUiResourcePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleUiResourcePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUiResourcePermissions_UiResources_UiResourceId",
                        column: x => x.UiResourceId,
                        principalTable: "UiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UiResourceNames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UiResourceId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UiResourceNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UiResourceNames_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UiResourceNames_UiResources_UiResourceId",
                        column: x => x.UiResourceId,
                        principalTable: "UiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UiResourceTypeNames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UiResourceTypeId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UiResourceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UiResourceTypeNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UiResourceTypeNames_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UiResourceTypeNames_UiResourceTypes_UiResourceTypeId",
                        column: x => x.UiResourceTypeId,
                        principalTable: "UiResourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UiResourceTypeNames_UiResources_UiResourceId",
                        column: x => x.UiResourceId,
                        principalTable: "UiResources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AddressString = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    PersonBaseId = table.Column<int>(type: "int", nullable: false),
                    CountryId1 = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId1",
                        column: x => x.CountryId1,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_PersonBase_PersonBaseId",
                        column: x => x.PersonBaseId,
                        principalTable: "PersonBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EMails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMailAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    PersonBaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EMails_PersonBase_PersonBaseId",
                        column: x => x.PersonBaseId,
                        principalTable: "PersonBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IBANs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    PersonBaseId = table.Column<int>(type: "int", nullable: false),
                    CountryId1 = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IBANs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IBANs_Countries_CountryId1",
                        column: x => x.CountryId1,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IBANs_PersonBase_PersonBaseId",
                        column: x => x.PersonBaseId,
                        principalTable: "PersonBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonIdentities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    PersonIdentityTypeId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "Date", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonIdentities_PersonBase_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PersonBase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonIdentities_PersonIdentityTypes_PersonIdentityTypeId",
                        column: x => x.PersonIdentityTypeId,
                        principalTable: "PersonIdentityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PhoneTypeId = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    PersonBaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phones_PersonBase_PersonBaseId",
                        column: x => x.PersonBaseId,
                        principalTable: "PersonBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phones_PhoneTypes_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalTable: "PhoneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_PersonBase_Id",
                        column: x => x.Id,
                        principalTable: "PersonBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IBANId = table.Column<int>(type: "int", nullable: true),
                    PersonBaseId = table.Column<int>(type: "int", nullable: true),
                    PersonIdentityId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalFiles_IBANs_IBANId",
                        column: x => x.IBANId,
                        principalTable: "IBANs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DigitalFiles_PersonBase_PersonBaseId",
                        column: x => x.PersonBaseId,
                        principalTable: "PersonBase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DigitalFiles_PersonIdentities_PersonIdentityId",
                        column: x => x.PersonIdentityId,
                        principalTable: "PersonIdentities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataFieldPermissionByUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFieldGeneralPermissionId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanEdited = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFieldPermissionByUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataFieldPermissionByUsers_DataFieldGeneralPermissions_DataFieldGeneralPermissionId",
                        column: x => x.DataFieldGeneralPermissionId,
                        principalTable: "DataFieldGeneralPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataFieldPermissionByUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataObjectPermissionByUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataObjectGeneralPermissionId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanEdited = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataObjectPermissionByUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataObjectPermissionByUsers_DataObjectGeneralPermissions_DataObjectGeneralPermissionId",
                        column: x => x.DataObjectGeneralPermissionId,
                        principalTable: "DataObjectGeneralPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataObjectPermissionByUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalFileTraces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DigitalFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalFileTraces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalFileTraces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUiResourcePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UiResourceId = table.Column<int>(type: "int", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanModified = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUiResourcePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserUiResourcePermissions_UiResources_UiResourceId",
                        column: x => x.UiResourceId,
                        principalTable: "UiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUiResourcePermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataFieldPermissionByUserDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFieldPermissionByUserId = table.Column<int>(type: "int", nullable: false),
                    RecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanEdited = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFieldPermissionByUserDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataFieldPermissionByUserDetails_DataFieldPermissionByUsers_DataFieldPermissionByUserId",
                        column: x => x.DataFieldPermissionByUserId,
                        principalTable: "DataFieldPermissionByUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataObjectPermissionByUserDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataObjectPermissionByUserId = table.Column<int>(type: "int", nullable: false),
                    RecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanVisible = table.Column<bool>(type: "bit", nullable: false),
                    CanEdited = table.Column<bool>(type: "bit", nullable: false),
                    CanRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataObjectPermissionByUserDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataObjectPermissionByUserDetails_DataObjectPermissionByUsers_DataObjectPermissionByUserId",
                        column: x => x.DataObjectPermissionByUserId,
                        principalTable: "DataObjectPermissionByUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "CreatedUserName", "DecimalSeparator", "DeletedDate", "DeletedUserId", "DeletedUserName", "IsDeleted", "LastUpdatedDate", "LastUpdatedUserId", "LastUpdatedUserName", "Num", "Symbol" },
                values: new object[,]
                {
                    { "AED", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "784", "د.إ" },
                    { "AFN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "971", "؋" },
                    { "ALL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "008", "L" },
                    { "AMD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "051", "֏" },
                    { "AOA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "973", "Kz" },
                    { "ARS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "032", null },
                    { "AUD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "036", null },
                    { "AWG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "533", "ƒ" },
                    { "AZN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "944", "₼" },
                    { "BAM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "977", "KM" },
                    { "BBD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "052", null },
                    { "BDT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "050", "৳" },
                    { "BGN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "975", "лв" },
                    { "BHD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 3, null, null, null, false, null, null, null, "048", null },
                    { "BIF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "108", null },
                    { "BMD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "060", null },
                    { "BND", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "096", null },
                    { "BOB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "068", "Bs" },
                    { "BOV", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "984", null },
                    { "BRL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "986", "R$" },
                    { "BSD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "044", null },
                    { "BTN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "064", null },
                    { "BWP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "072", "P" },
                    { "BYN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "933", "Br" },
                    { "BZD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "084", null },
                    { "CAD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "124", null },
                    { "CDF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "976", null },
                    { "CHE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "947", null },
                    { "CHF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "756", "Fr" },
                    { "CHW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "948", null },
                    { "CLF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 4, null, null, null, false, null, null, null, "990", null },
                    { "CLP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "152", null },
                    { "CNY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "156", null },
                    { "COP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "170", null },
                    { "COU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "970", null },
                    { "CRC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "188", "₡" },
                    { "CUP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "192", null },
                    { "CVE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "132", null },
                    { "CZK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "203", "Kč" },
                    { "DJF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "262", null },
                    { "DKK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "208", "kr" },
                    { "DOP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "214", null },
                    { "DZD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "012", "دج" },
                    { "EGP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "818", null },
                    { "ERN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "232", null },
                    { "ETB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "230", "Br" },
                    { "EUR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "978", "€" },
                    { "FJD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "242", null },
                    { "FKP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "238", null },
                    { "GBP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", 2, null, null, null, false, null, null, null, "826", "£" },
                    { "GEL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "981", "₾" },
                    { "GHS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "936", "₵" },
                    { "GIP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "292", "£" },
                    { "GMD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "270", "D" },
                    { "GNF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "324", null },
                    { "GTQ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "320", "Q" },
                    { "GYD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "328", null },
                    { "HKD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "344", null },
                    { "HNL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "340", "L" },
                    { "HTG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "332", "G" },
                    { "HUF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "348", "Ft" },
                    { "IDR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "360", "Rp" },
                    { "INR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "356", "₹" },
                    { "IQD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 3, null, null, null, false, null, null, null, "368", "ع.د" },
                    { "IRR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "364", "﷼" },
                    { "ISK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "352", "kr" },
                    { "JMD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "388", null },
                    { "JOD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 3, null, null, null, false, null, null, null, "400", null },
                    { "JPY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "392", "¥" },
                    { "KES", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "404", "Sh" },
                    { "KGS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "417", "с" },
                    { "KHR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "116", "៛" },
                    { "KMF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "174", null },
                    { "KPW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "408", "₩" },
                    { "KRW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "410", null },
                    { "KWD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 3, null, null, null, false, null, null, null, "414", null },
                    { "KYD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "136", null },
                    { "KZT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "398", "₸" },
                    { "LAK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "418", "₭" },
                    { "LBP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "422", "ل.ل" },
                    { "LKR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "144", "₨" },
                    { "LRD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "430", null },
                    { "LSL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "426", "L" },
                    { "LYD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 3, null, null, null, false, null, null, null, "434", "ل.د" },
                    { "MAD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "504", "د.م." },
                    { "MDL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "498", "L" },
                    { "MGA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "969", "Ar" },
                    { "MKD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "807", "ден" },
                    { "MMK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "104", "Ks" },
                    { "MNT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "496", "₮" },
                    { "MOP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "446", "P" },
                    { "MRU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "929", "UM" },
                    { "MUR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "480", "₨" },
                    { "MVR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "462", "Rf" },
                    { "MWK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "454", "MK" },
                    { "MXN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "484", null },
                    { "MXV", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "979", null },
                    { "MYR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "458", "RM" },
                    { "MZN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "943", "MT" },
                    { "NAD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "516", null },
                    { "NGN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "566", "₦" },
                    { "NIO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "558", "C$" },
                    { "NOK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "578", "kr" },
                    { "NPR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "524", "₨" },
                    { "NZD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "554", null },
                    { "OMR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 3, null, null, null, false, null, null, null, "512", null },
                    { "PAB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "590", null },
                    { "PEN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "604", "S/" },
                    { "PGK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "598", "K" },
                    { "PHP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "608", "₱" },
                    { "PKR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "586", "₨" },
                    { "PLN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "985", "zł" },
                    { "PYG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "600", "₲" },
                    { "QAR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "634", "﷼" },
                    { "RON", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "946", "lei" },
                    { "RSD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "941", "дин" },
                    { "RUB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "643", "₽" },
                    { "RWF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "646", null },
                    { "SAR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "682", "﷼" },
                    { "SBD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "090", null },
                    { "SCR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "690", "₨" },
                    { "SDG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "938", null },
                    { "SEK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "752", "kr" },
                    { "SGD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "702", null },
                    { "SHP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "654", null },
                    { "SLE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "925", "Le" },
                    { "SLL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "694", "Le" },
                    { "SOS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "706", "Sh" },
                    { "SRD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "968", null },
                    { "SSP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "728", null },
                    { "STN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "930", "Db" },
                    { "SVC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "222", null },
                    { "SYP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "760", null },
                    { "SZL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "748", "E" },
                    { "THB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "764", "฿" },
                    { "TJS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "972", "SM" },
                    { "TMT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "934", "m" },
                    { "TND", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 3, null, null, null, false, null, null, null, "788", "د.ت" },
                    { "TOP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "776", "T$" },
                    { "TRY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "949", "₺" },
                    { "TTD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "780", null },
                    { "TWD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "901", null },
                    { "TZS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "834", "Sh" },
                    { "UAH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "980", "₴" },
                    { "UGX", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "800", "USh" },
                    { "USD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "840", "$" },
                    { "USN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "997", null },
                    { "UYI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "940", null },
                    { "UYU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "858", null },
                    { "UYW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 4, null, null, null, false, null, null, null, "927", null },
                    { "UZS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "860", "so'm" },
                    { "VES", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "928", null },
                    { "VND", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "704", "₫" },
                    { "VUV", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "548", null },
                    { "WST", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "882", "T" },
                    { "XAF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "950", "Fr" },
                    { "XAG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "961", null },
                    { "XAU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "959", null },
                    { "XBA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "955", null },
                    { "XBB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "956", null },
                    { "XBC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "957", null },
                    { "XBD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "958", null },
                    { "XCD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "951", null },
                    { "XCG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "532", "Fr" },
                    { "XDR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "960", null },
                    { "XOF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "952", "Fr" },
                    { "XPD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "964", null },
                    { "XPF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "953", "₣" },
                    { "XPT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "962", null },
                    { "XSU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "994", null },
                    { "XTS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "963", null },
                    { "XUA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "965", null },
                    { "XXX", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 0, null, null, null, false, null, null, null, "999", null },
                    { "YER", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "886", "﷼" },
                    { "ZAR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "710", "R" },
                    { "ZMW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "967", "ZK" },
                    { "ZWL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", 2, null, null, null, false, null, null, null, "932", null }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                column: "Id",
                values: new object[]
                {
                    "F",
                    "M"
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "CreatedUserName", "DeletedDate", "DeletedUserId", "DeletedUserName", "IsDeleted", "LastUpdatedDate", "LastUpdatedUserId", "LastUpdatedUserName", "Name" },
                values: new object[,]
                {
                    { "AR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null, "العربية" },
                    { "EN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null, "English" },
                    { "FR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null, "Français" }
                });

            migrationBuilder.InsertData(
                table: "PersonBase",
                columns: new[] { "Id", "BirthCity", "BirthCountryId", "BirthCountryId1", "BirthDate", "CreatedDate", "CreatedUserId", "CreatedUserName", "DeletedDate", "DeletedUserId", "DeletedUserName", "Discriminator", "FirstName", "FullName", "GenderId", "IsDeleted", "LastName", "LastUpdatedDate", "LastUpdatedUserId", "LastUpdatedUserName", "NationalityCountryId", "NationalityId" },
                values: new object[] { 1, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, "Person", "Slim", "Slim Aissaoui", null, false, "Aissaoui", null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "PersonIdentityTypes",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "CreatedUserName", "DeletedDate", "DeletedUserId", "DeletedUserName", "IsDeleted", "LastUpdatedDate", "LastUpdatedUserId", "LastUpdatedUserName" },
                values: new object[,]
                {
                    { "DL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null },
                    { "ID", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null },
                    { "PASS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null },
                    { "RC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "PhoneTypes",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "CreatedUserName", "DeletedDate", "DeletedUserId", "DeletedUserName", "IsDeleted", "LastUpdatedDate", "LastUpdatedUserId", "LastUpdatedUserName" },
                values: new object[,]
                {
                    { "Fax", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null },
                    { "Hom", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null },
                    { "Mob", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null },
                    { "Wrk", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, null, null, false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name", "ParentRoleId" },
                values: new object[] { 1, "", "Supper Admin", null });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "CreatedUserName", "CurrencyId", "DeletedDate", "DeletedUserId", "DeletedUserName", "Id3Digit", "IsDeleted", "LastUpdatedDate", "LastUpdatedUserId", "LastUpdatedUserName", "PhoneCode" },
                values: new object[,]
                {
                    { "AD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "AND", false, null, null, null, "376" },
                    { "AE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AED", null, null, null, "ARE", false, null, null, null, "971" },
                    { "AF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AFN", null, null, null, "AFG", false, null, null, null, "93" },
                    { "AG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCD", null, null, null, "ATG", false, null, null, null, "1268" },
                    { "AI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCD", null, null, null, "AIA", false, null, null, null, "1264" },
                    { "AL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "ALL", null, null, null, "ALB", false, null, null, null, "355" },
                    { "AM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AMD", null, null, null, "ARM", false, null, null, null, "374" },
                    { "AO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AOA", null, null, null, "AGO", false, null, null, null, "244" },
                    { "AQ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "ATA", false, null, null, null, "672" },
                    { "AR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "ARS", null, null, null, "ARG", false, null, null, null, "54" },
                    { "AS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "ASM", false, null, null, null, "1684" },
                    { "AT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "AUT", false, null, null, null, "43" },
                    { "AU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AUD", null, null, null, "AUS", false, null, null, null, "61" },
                    { "AW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AWG", null, null, null, "ABW", false, null, null, null, "297" },
                    { "AZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AZN", null, null, null, "AZE", false, null, null, null, "994" },
                    { "BA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BAM", null, null, null, "BIH", false, null, null, null, "387" },
                    { "BB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BBD", null, null, null, "BRB", false, null, null, null, "1246" },
                    { "BD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BDT", null, null, null, "BGD", false, null, null, null, "880" },
                    { "BE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "BEL", false, null, null, null, "32" },
                    { "BF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XOF", null, null, null, "BFA", false, null, null, null, "226" },
                    { "BG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BGN", null, null, null, "BGR", false, null, null, null, "359" },
                    { "BH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BHD", null, null, null, "BHR", false, null, null, null, "973" },
                    { "BI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BIF", null, null, null, "BDI", false, null, null, null, "257" },
                    { "BJ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XOF", null, null, null, "BEN", false, null, null, null, "229" },
                    { "BL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "BLM", false, null, null, null, "590" },
                    { "BM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BMD", null, null, null, "BMU", false, null, null, null, "1441" },
                    { "BN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BND", null, null, null, "BRN", false, null, null, null, "673" },
                    { "BO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BOB", null, null, null, "BOL", false, null, null, null, "591" },
                    { "BR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BRL", null, null, null, "BRA", false, null, null, null, "55" },
                    { "BS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BSD", null, null, null, "BHS", false, null, null, null, "1242" },
                    { "BT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BTN", null, null, null, "BTN", false, null, null, null, "975" },
                    { "BV", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NOK", null, null, null, "BVT", false, null, null, null, "47" },
                    { "BW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BWP", null, null, null, "BWA", false, null, null, null, "267" },
                    { "BY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BYN", null, null, null, "BLR", false, null, null, null, "375" },
                    { "BZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "BZD", null, null, null, "BLZ", false, null, null, null, "501" },
                    { "CA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "CAD", null, null, null, "CAN", false, null, null, null, "1" },
                    { "CC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AUD", null, null, null, "CCK", false, null, null, null, "61" },
                    { "CD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "CDF", null, null, null, "COD", false, null, null, null, "243" },
                    { "CG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XAF", null, null, null, "COG", false, null, null, null, "242" },
                    { "CH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "CHF", null, null, null, "CHE", false, null, null, null, "41" },
                    { "CK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NZD", null, null, null, "COK", false, null, null, null, "682" },
                    { "CL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "CLP", null, null, null, "CHL", false, null, null, null, "56" },
                    { "CN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "CNY", null, null, null, "CHN", false, null, null, null, "86" },
                    { "CO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "COP", null, null, null, "COL", false, null, null, null, "57" },
                    { "CR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "CRC", null, null, null, "CRI", false, null, null, null, "506" },
                    { "CU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "CUP", null, null, null, "CUB", false, null, null, null, "53" },
                    { "CW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCG", null, null, null, "CUW", false, null, null, null, "599" },
                    { "CX", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AUD", null, null, null, "CXR", false, null, null, null, "61" },
                    { "CY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "CYP", false, null, null, null, "357" },
                    { "CZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "CZK", null, null, null, "CZE", false, null, null, null, "420" },
                    { "DE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "DEU", false, null, null, null, "49" },
                    { "DJ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "DJF", null, null, null, "DJI", false, null, null, null, "253" },
                    { "DK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "DKK", null, null, null, "DNK", false, null, null, null, "45" },
                    { "DM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCD", null, null, null, "DMA", false, null, null, null, "1767" },
                    { "DO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "DOP", null, null, null, "DOM", false, null, null, null, "1809" },
                    { "DZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "DZD", null, null, null, "DZA", false, null, null, null, "213" },
                    { "EC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "ECU", false, null, null, null, "593" },
                    { "EE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "EST", false, null, null, null, "372" },
                    { "EG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EGP", null, null, null, "EGY", false, null, null, null, "20" },
                    { "EH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MAD", null, null, null, "ESH", false, null, null, null, "212" },
                    { "ER", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "ERN", null, null, null, "ERI", false, null, null, null, "291" },
                    { "ES", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "ESP", false, null, null, null, "34" },
                    { "ET", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "ETB", null, null, null, "ETH", false, null, null, null, "251" },
                    { "FI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "FIN", false, null, null, null, "358" },
                    { "FJ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "FJD", null, null, null, "FJI", false, null, null, null, "679" },
                    { "FK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "FKP", null, null, null, "FLK", false, null, null, null, "500" },
                    { "FM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "FSM", false, null, null, null, "691" },
                    { "FO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "DKK", null, null, null, "FRO", false, null, null, null, "298" },
                    { "FR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "FRA", false, null, null, null, "33" },
                    { "GA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XAF", null, null, null, "GAB", false, null, null, null, "241" },
                    { "GB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GBP", null, null, null, "GBR", false, null, null, null, "44" },
                    { "GD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCD", null, null, null, "GRD", false, null, null, null, "1473" },
                    { "GE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GEL", null, null, null, "GEO", false, null, null, null, "995" },
                    { "GF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "GUF", false, null, null, null, "594" },
                    { "GG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GBP", null, null, null, "GGY", false, null, null, null, "44" },
                    { "GH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GHS", null, null, null, "GHA", false, null, null, null, "233" },
                    { "GI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GIP", null, null, null, "GIB", false, null, null, null, "350" },
                    { "GL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "DKK", null, null, null, "GRL", false, null, null, null, "299" },
                    { "GM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GMD", null, null, null, "GMB", false, null, null, null, "220" },
                    { "GN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GNF", null, null, null, "GIN", false, null, null, null, "224" },
                    { "GP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "GLP", false, null, null, null, "590" },
                    { "GQ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XAF", null, null, null, "GNQ", false, null, null, null, "240" },
                    { "GR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "GRC", false, null, null, null, "30" },
                    { "GS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "SGS", false, null, null, null, "500" },
                    { "GT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GTQ", null, null, null, "GTM", false, null, null, null, "502" },
                    { "GU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "GUM", false, null, null, null, "1671" },
                    { "GW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XOF", null, null, null, "GNB", false, null, null, null, "245" },
                    { "GY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GYD", null, null, null, "GUY", false, null, null, null, "592" },
                    { "HK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "HKD", null, null, null, "HKG", false, null, null, null, "852" },
                    { "HM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AUD", null, null, null, "HMD", false, null, null, null, "672" },
                    { "HN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "HNL", null, null, null, "HND", false, null, null, null, "504" },
                    { "HR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "HRV", false, null, null, null, "385" },
                    { "HT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "HTG", null, null, null, "HTI", false, null, null, null, "509" },
                    { "HU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "HUF", null, null, null, "HUN", false, null, null, null, "36" },
                    { "ID", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "IDR", null, null, null, "IDN", false, null, null, null, "62" },
                    { "IE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "IRL", false, null, null, null, "353" },
                    { "IM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GBP", null, null, null, "IMN", false, null, null, null, "44" },
                    { "IN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "INR", null, null, null, "IND", false, null, null, null, "91" },
                    { "IO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "IOT", false, null, null, null, "246" },
                    { "IQ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "IQD", null, null, null, "IRQ", false, null, null, null, "964" },
                    { "IR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "IRR", null, null, null, "IRN", false, null, null, null, "98" },
                    { "IS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "ISK", null, null, null, "ISL", false, null, null, null, "354" },
                    { "IT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "ITA", false, null, null, null, "39" },
                    { "JE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "GBP", null, null, null, "JEY", false, null, null, null, "44" },
                    { "JM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "JMD", null, null, null, "JAM", false, null, null, null, "1876" },
                    { "JO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "JOD", null, null, null, "JOR", false, null, null, null, "962" },
                    { "JP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "JPY", null, null, null, "JPN", false, null, null, null, "81" },
                    { "KE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "KES", null, null, null, "KEN", false, null, null, null, "254" },
                    { "KG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "KGS", null, null, null, "KGZ", false, null, null, null, "996" },
                    { "KI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AUD", null, null, null, "KIR", false, null, null, null, "686" },
                    { "KM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "KMF", null, null, null, "KHM", false, null, null, null, "269" },
                    { "KN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCD", null, null, null, "KNA", false, null, null, null, "1869" },
                    { "KP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "KPW", null, null, null, "PRK", false, null, null, null, "850" },
                    { "KR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "KRW", null, null, null, "KOR", false, null, null, null, "82" },
                    { "KW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "KWD", null, null, null, "KWT", false, null, null, null, "965" },
                    { "KZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "KZT", null, null, null, "KAZ", false, null, null, null, "7" },
                    { "LA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "LAK", null, null, null, "LAO", false, null, null, null, "856" },
                    { "LB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "LBP", null, null, null, "LBN", false, null, null, null, "961" },
                    { "LC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCD", null, null, null, "LCA", false, null, null, null, "1758" },
                    { "LI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "CHF", null, null, null, "LIE", false, null, null, null, "423" },
                    { "LK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "LKR", null, null, null, "LKA", false, null, null, null, "94" },
                    { "LR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "LRD", null, null, null, "LBR", false, null, null, null, "231" },
                    { "LS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "LSL", null, null, null, "LSO", false, null, null, null, "266" },
                    { "LT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "LTU", false, null, null, null, "370" },
                    { "LU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "LUX", false, null, null, null, "352" },
                    { "LV", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "LVA", false, null, null, null, "371" },
                    { "LY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "LYD", null, null, null, "LBY", false, null, null, null, "218" },
                    { "MA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MAD", null, null, null, "MAR", false, null, null, null, "212" },
                    { "MC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "MCO", false, null, null, null, "377" },
                    { "MD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MDL", null, null, null, "MDA", false, null, null, null, "373" },
                    { "ME", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "MNE", false, null, null, null, "382" },
                    { "MF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "MAF", false, null, null, null, "590" },
                    { "MG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MGA", null, null, null, "MDG", false, null, null, null, "261" },
                    { "MH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "MHL", false, null, null, null, "692" },
                    { "ML", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XOF", null, null, null, "MLI", false, null, null, null, "223" },
                    { "MM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MMK", null, null, null, "MMR", false, null, null, null, "95" },
                    { "MN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MNT", null, null, null, "MNG", false, null, null, null, "976" },
                    { "MO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MOP", null, null, null, "MAC", false, null, null, null, "853" },
                    { "MP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "MNP", false, null, null, null, "1670" },
                    { "MQ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "MTQ", false, null, null, null, "596" },
                    { "MR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MRU", null, null, null, "MRT", false, null, null, null, "222" },
                    { "MS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCD", null, null, null, "MSR", false, null, null, null, "1664" },
                    { "MT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "MLT", false, null, null, null, "356" },
                    { "MU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MUR", null, null, null, "MUS", false, null, null, null, "230" },
                    { "MV", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MVR", null, null, null, "MDV", false, null, null, null, "960" },
                    { "MW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MWK", null, null, null, "MWI", false, null, null, null, "265" },
                    { "MX", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MXN", null, null, null, "MEX", false, null, null, null, "52" },
                    { "MY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MYR", null, null, null, "MYS", false, null, null, null, "60" },
                    { "MZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "MZN", null, null, null, "MOZ", false, null, null, null, "258" },
                    { "NA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NAD", null, null, null, "NAM", false, null, null, null, "264" },
                    { "NC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XPF", null, null, null, "NCL", false, null, null, null, "687" },
                    { "NE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XOF", null, null, null, "NER", false, null, null, null, "227" },
                    { "NF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AUD", null, null, null, "NFK", false, null, null, null, "672" },
                    { "NG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NGN", null, null, null, "NGA", false, null, null, null, "234" },
                    { "NI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NIO", null, null, null, "NIC", false, null, null, null, "505" },
                    { "NL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "NLD", false, null, null, null, "31" },
                    { "NO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NOK", null, null, null, "NOR", false, null, null, null, "47" },
                    { "NP", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NPR", null, null, null, "NPL", false, null, null, null, "977" },
                    { "NR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AUD", null, null, null, "NRU", false, null, null, null, "674" },
                    { "NU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NZD", null, null, null, "NIU", false, null, null, null, "683" },
                    { "NZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NZD", null, null, null, "NZL", false, null, null, null, "64" },
                    { "OM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "OMR", null, null, null, "OMN", false, null, null, null, "968" },
                    { "PA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "PAB", null, null, null, "PAN", false, null, null, null, "507" },
                    { "PE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "PEN", null, null, null, "PER", false, null, null, null, "51" },
                    { "PF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XPF", null, null, null, "PYF", false, null, null, null, "689" },
                    { "PG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "PGK", null, null, null, "PNG", false, null, null, null, "675" },
                    { "PH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "PHP", null, null, null, "PHL", false, null, null, null, "63" },
                    { "PK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "PKR", null, null, null, "PAK", false, null, null, null, "92" },
                    { "PL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "PLN", null, null, null, "POL", false, null, null, null, "48" },
                    { "PM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "SPM", false, null, null, null, "508" },
                    { "PN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NZD", null, null, null, "PCN", false, null, null, null, "64" },
                    { "PR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "PRI", false, null, null, null, "1787" },
                    { "PS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "PSE", false, null, null, null, "970" },
                    { "PT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "PRT", false, null, null, null, "351" },
                    { "PW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "PLW", false, null, null, null, "680" },
                    { "PY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "PYG", null, null, null, "PRY", false, null, null, null, "595" },
                    { "QA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "QAR", null, null, null, "QAT", false, null, null, null, "974" },
                    { "RE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "REU", false, null, null, null, "262" },
                    { "RO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "RON", null, null, null, "ROU", false, null, null, null, "40" },
                    { "RS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "RSD", null, null, null, "SRB", false, null, null, null, "381" },
                    { "RU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "RUB", null, null, null, "RUS", false, null, null, null, "7" },
                    { "RW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "RWF", null, null, null, "RWA", false, null, null, null, "250" },
                    { "SA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SAR", null, null, null, "SAU", false, null, null, null, "966" },
                    { "SB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SBD", null, null, null, "SLB", false, null, null, null, "677" },
                    { "SC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SCR", null, null, null, "SYC", false, null, null, null, "248" },
                    { "SD", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SDG", null, null, null, "SDN", false, null, null, null, "249" },
                    { "SE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SEK", null, null, null, "SWE", false, null, null, null, "46" },
                    { "SG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SGD", null, null, null, "SGP", false, null, null, null, "65" },
                    { "SH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SHP", null, null, null, "SHN", false, null, null, null, "290" },
                    { "SI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "SVN", false, null, null, null, "386" },
                    { "SJ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NOK", null, null, null, "SJM", false, null, null, null, "47" },
                    { "SK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "SVK", false, null, null, null, "421" },
                    { "SL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SLE", null, null, null, "SLE", false, null, null, null, "232" },
                    { "SM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "SMR", false, null, null, null, "378" },
                    { "SN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XOF", null, null, null, "SEN", false, null, null, null, "221" },
                    { "SO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SOS", null, null, null, "SOM", false, null, null, null, "252" },
                    { "SR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SRD", null, null, null, "SUR", false, null, null, null, "597" },
                    { "SS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SSP", null, null, null, "SSD", false, null, null, null, "211" },
                    { "ST", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "STN", null, null, null, "STP", false, null, null, null, "239" },
                    { "SV", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "SLV", false, null, null, null, "503" },
                    { "SX", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCG", null, null, null, "SXM", false, null, null, null, "1721" },
                    { "SY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SYP", null, null, null, "SYR", false, null, null, null, "963" },
                    { "SZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "SZL", null, null, null, "SWZ", false, null, null, null, "268" },
                    { "TC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "TCA", false, null, null, null, "1649" },
                    { "TF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "ATF", false, null, null, null, "262" },
                    { "TG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XOF", null, null, null, "TGO", false, null, null, null, "228" },
                    { "TH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "THB", null, null, null, "THA", false, null, null, null, "66" },
                    { "TJ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "TJS", null, null, null, "TJK", false, null, null, null, "992" },
                    { "TK", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "NZD", null, null, null, "TKL", false, null, null, null, "690" },
                    { "TL", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "TLS", false, null, null, null, "670" },
                    { "TM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "TMT", null, null, null, "TKM", false, null, null, null, "993" },
                    { "TN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "TND", null, null, null, "TUN", false, null, null, null, "216" },
                    { "TO", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "TOP", null, null, null, "TON", false, null, null, null, "676" },
                    { "TR", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "TRY", null, null, null, "TUR", false, null, null, null, "90" },
                    { "TT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "TTD", null, null, null, "TTO", false, null, null, null, "1868" },
                    { "TV", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "AUD", null, null, null, "TUV", false, null, null, null, "688" },
                    { "TW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "TWD", null, null, null, "TWN", false, null, null, null, "886" },
                    { "TZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "TZS", null, null, null, "TZA", false, null, null, null, "255" },
                    { "UA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "UAH", null, null, null, "UKR", false, null, null, null, "380" },
                    { "UG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "UGX", null, null, null, "UGA", false, null, null, null, "256" },
                    { "UM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "UMI", false, null, null, null, "1" },
                    { "US", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "USA", false, null, null, null, "1" },
                    { "UY", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "UYU", null, null, null, "URY", false, null, null, null, "598" },
                    { "UZ", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "UZS", null, null, null, "UZB", false, null, null, null, "998" },
                    { "VA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "VAT", false, null, null, null, "379" },
                    { "VC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "XCD", null, null, null, "VCT", false, null, null, null, "1784" },
                    { "VE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "VES", null, null, null, "VEN", false, null, null, null, "58" },
                    { "VG", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "VGB", false, null, null, null, "1284" },
                    { "VI", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "USD", null, null, null, "VIR", false, null, null, null, "1340" },
                    { "VN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "VND", null, null, null, "VNM", false, null, null, null, "84" },
                    { "VU", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "VUV", null, null, null, "VUT", false, null, null, null, "678" },
                    { "WS", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "WST", null, null, null, "WSM", false, null, null, null, "685" },
                    { "YE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "YER", null, null, null, "YEM", false, null, null, null, "967" },
                    { "YT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "EUR", null, null, null, "MYT", false, null, null, null, "262" },
                    { "ZA", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "ZAR", null, null, null, "ZAF", false, null, null, null, "27" },
                    { "ZM", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "ZMW", null, null, null, "ZMB", false, null, null, null, "260" },
                    { "ZW", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system", "ZWL", null, null, null, "ZWE", false, null, null, null, "263" }
                });

            migrationBuilder.InsertData(
                table: "CurrencyNames",
                columns: new[] { "Id", "CurrencyId", "LanguageId", "Name" },
                values: new object[,]
                {
                    { "AED_AR", "AED", "AR", "درهم إماراتي" },
                    { "AED_EN", "AED", "EN", "United Arab Emirates dirham" },
                    { "AED_FR", "AED", "FR", "Dirham des Émirats arabes unis" },
                    { "AFN_AR", "AFN", "AR", "أفغاني أفغانستاني" },
                    { "AFN_EN", "AFN", "EN", "Afghan afghani" },
                    { "AFN_FR", "AFN", "FR", "Afghani afghan" },
                    { "ALL_AR", "ALL", "AR", "ليك ألباني" },
                    { "ALL_EN", "ALL", "EN", "Albanian lek" },
                    { "ALL_FR", "ALL", "FR", "Lek albanais" },
                    { "AMD_AR", "AMD", "AR", "درام أرميني" },
                    { "AMD_EN", "AMD", "EN", "Armenian dram" },
                    { "AMD_FR", "AMD", "FR", "Dram arménien" },
                    { "AOA_AR", "AOA", "AR", "كوانزا أنغولي" },
                    { "AOA_EN", "AOA", "EN", "Angolan kwanza" },
                    { "AOA_FR", "AOA", "FR", "Kwanza angolais" },
                    { "ARS_AR", "ARS", "AR", "بيزو أرجنتيني" },
                    { "ARS_EN", "ARS", "EN", "Argentine peso" },
                    { "ARS_FR", "ARS", "FR", "Peso argentin" },
                    { "AUD_AR", "AUD", "AR", "دولار أسترالي" },
                    { "AUD_EN", "AUD", "EN", "Australian dollar" },
                    { "AUD_FR", "AUD", "FR", "Dollar australien" },
                    { "AWG_AR", "AWG", "AR", "فلورن أروبي" },
                    { "AWG_EN", "AWG", "EN", "Aruban florin" },
                    { "AWG_FR", "AWG", "FR", "Florin arubais" },
                    { "AZN_AR", "AZN", "AR", "مانات أذربيجاني" },
                    { "AZN_EN", "AZN", "EN", "Azerbaijani manat" },
                    { "AZN_FR", "AZN", "FR", "Manat azerbaïdjanais" },
                    { "BAM_AR", "BAM", "AR", "مارك قابل للتحويل (البوسنة)" },
                    { "BAM_EN", "BAM", "EN", "Bosnia-Herzegovina convertible mark" },
                    { "BAM_FR", "BAM", "FR", "Mark convertible de Bosnie-Herzégovine" },
                    { "BBD_AR", "BBD", "AR", "دولار بربادوسي" },
                    { "BBD_EN", "BBD", "EN", "Barbados dollar" },
                    { "BBD_FR", "BBD", "FR", "Dollar barbadien" },
                    { "BDT_AR", "BDT", "AR", "تاكا بنغلاديشي" },
                    { "BDT_EN", "BDT", "EN", "Bangladeshi taka" },
                    { "BDT_FR", "BDT", "FR", "Taka bangladais" },
                    { "BGN_AR", "BGN", "AR", "ليف بلغاري" },
                    { "BGN_EN", "BGN", "EN", "Bulgarian lev" },
                    { "BGN_FR", "BGN", "FR", "Lev bulgare" },
                    { "BHD_AR", "BHD", "AR", "دينار بحريني" },
                    { "BHD_EN", "BHD", "EN", "Bahraini dinar" },
                    { "BHD_FR", "BHD", "FR", "Dinar bahreïni" },
                    { "BIF_AR", "BIF", "AR", "فرنك بوروندي" },
                    { "BIF_EN", "BIF", "EN", "Burundian franc" },
                    { "BIF_FR", "BIF", "FR", "Franc burundais" },
                    { "BMD_AR", "BMD", "AR", "دولار برمودي" },
                    { "BMD_EN", "BMD", "EN", "Bermuda dollar" },
                    { "BMD_FR", "BMD", "FR", "Dollar bermudien" },
                    { "BND_AR", "BND", "AR", "دولار بروناي" },
                    { "BND_EN", "BND", "EN", "Brunei dollar" },
                    { "BND_FR", "BND", "FR", "Dollar de Brunei" },
                    { "BOB_AR", "BOB", "AR", "بوليفيانو بوليفي" },
                    { "BOB_EN", "BOB", "EN", "Bolivian boliviano" },
                    { "BOB_FR", "BOB", "FR", "Boliviano bolivien" },
                    { "BOV_AR", "BOV", "AR", "إمفيدول بوليفي" },
                    { "BOV_EN", "BOV", "EN", "Bolivian Mvdol" },
                    { "BOV_FR", "BOV", "FR", "Mvdol bolivien" },
                    { "BRL_AR", "BRL", "AR", "ريال برازيلي" },
                    { "BRL_EN", "BRL", "EN", "Brazilian real" },
                    { "BRL_FR", "BRL", "FR", "Réal brésilien" },
                    { "BSD_AR", "BSD", "AR", "دولار باهامي" },
                    { "BSD_EN", "BSD", "EN", "Bahamian dollar" },
                    { "BSD_FR", "BSD", "FR", "Dollar bahaméen" },
                    { "BTN_AR", "BTN", "AR", "نولتورم بوتاني" },
                    { "BTN_EN", "BTN", "EN", "Bhutanese ngultrum" },
                    { "BTN_FR", "BTN", "FR", "Ngultrum bhoutanais" },
                    { "BWP_AR", "BWP", "AR", "بولا بوتسواني" },
                    { "BWP_EN", "BWP", "EN", "Botswana pula" },
                    { "BWP_FR", "BWP", "FR", "Pula botswanais" },
                    { "BYN_AR", "BYN", "AR", "روبل بيلاروسي" },
                    { "BYN_EN", "BYN", "EN", "Belarusian ruble" },
                    { "BYN_FR", "BYN", "FR", "Rouble biélorusse" },
                    { "BZD_AR", "BZD", "AR", "دولار بليزي" },
                    { "BZD_EN", "BZD", "EN", "Belize dollar" },
                    { "BZD_FR", "BZD", "FR", "Dollar bélizien" },
                    { "CAD_AR", "CAD", "AR", "دولار كندي" },
                    { "CAD_EN", "CAD", "EN", "Canadian dollar" },
                    { "CAD_FR", "CAD", "FR", "Dollar canadien" },
                    { "CDF_AR", "CDF", "AR", "فرنك كونغولي" },
                    { "CDF_EN", "CDF", "EN", "Congolese franc" },
                    { "CDF_FR", "CDF", "FR", "Franc congolais" },
                    { "CHE_AR", "CHE", "AR", "يوروالـ WIR" },
                    { "CHE_EN", "CHE", "EN", "WIR euro" },
                    { "CHE_FR", "CHE", "FR", "WIR euro" },
                    { "CHF_AR", "CHF", "AR", "فرنك سويسري" },
                    { "CHF_EN", "CHF", "EN", "Swiss franc" },
                    { "CHF_FR", "CHF", "FR", "Franc suisse" },
                    { "CHW_AR", "CHW", "AR", "فرنك WIR" },
                    { "CHW_EN", "CHW", "EN", "WIR franc" },
                    { "CHW_FR", "CHW", "FR", "WIR franc" },
                    { "CLF_AR", "CLF", "AR", "وحدة التنمية (UF)" },
                    { "CLF_EN", "CLF", "EN", "Unidad de Fomento" },
                    { "CLF_FR", "CLF", "FR", "Unité de fomento" },
                    { "CLP_AR", "CLP", "AR", "بيزو تشيلي" },
                    { "CLP_EN", "CLP", "EN", "Chilean peso" },
                    { "CLP_FR", "CLP", "FR", "Peso chilien" },
                    { "CNY_AR", "CNY", "AR", "يوان صيني" },
                    { "CNY_EN", "CNY", "EN", "Chinese yuan" },
                    { "CNY_FR", "CNY", "FR", "Yuan chinois" },
                    { "COP_AR", "COP", "AR", "بيزو كولومبي" },
                    { "COP_EN", "COP", "EN", "Colombian peso" },
                    { "COP_FR", "COP", "FR", "Peso colombien" },
                    { "COU_AR", "COU", "AR", "وحدة القيمة الحقيقية" },
                    { "COU_EN", "COU", "EN", "Unidad de Valor Real" },
                    { "COU_FR", "COU", "FR", "Unité de valeur réelle" },
                    { "CRC_AR", "CRC", "AR", "كولون كوستاريكي" },
                    { "CRC_EN", "CRC", "EN", "Costa Rican colón" },
                    { "CRC_FR", "CRC", "FR", "Colón costaricien" },
                    { "CUP_AR", "CUP", "AR", "بيزو كوبي" },
                    { "CUP_EN", "CUP", "EN", "Cuban peso" },
                    { "CUP_FR", "CUP", "FR", "Peso cubain" },
                    { "CVE_AR", "CVE", "AR", "إسكودو الرأس الأخضر" },
                    { "CVE_EN", "CVE", "EN", "Cabo Verde escudo" },
                    { "CVE_FR", "CVE", "FR", "Escudo cap-verdien" },
                    { "CZK_AR", "CZK", "AR", "كورونا تشيكية" },
                    { "CZK_EN", "CZK", "EN", "Czech koruna" },
                    { "CZK_FR", "CZK", "FR", "Couronne tchèque" },
                    { "DJF_AR", "DJF", "AR", "فرنك جيبوتي" },
                    { "DJF_EN", "DJF", "EN", "Djiboutian franc" },
                    { "DJF_FR", "DJF", "FR", "Franc djiboutien" },
                    { "DKK_AR", "DKK", "AR", "كرونة دانمركية" },
                    { "DKK_EN", "DKK", "EN", "Danish krone" },
                    { "DKK_FR", "DKK", "FR", "Couronne danoise" },
                    { "DOP_AR", "DOP", "AR", "بيزو دومينيكاني" },
                    { "DOP_EN", "DOP", "EN", "Dominican peso" },
                    { "DOP_FR", "DOP", "FR", "Peso dominicain" },
                    { "DZD_AR", "DZD", "AR", "دينار جزائري" },
                    { "DZD_EN", "DZD", "EN", "Algerian dinar" },
                    { "DZD_FR", "DZD", "FR", "Dinar algérien" },
                    { "EGP_AR", "EGP", "AR", "جنيه مصري" },
                    { "EGP_EN", "EGP", "EN", "Egyptian pound" },
                    { "EGP_FR", "EGP", "FR", "Livre égyptienne" },
                    { "ERN_AR", "ERN", "AR", "نكفة إريترية" },
                    { "ERN_EN", "ERN", "EN", "Eritrean nakfa" },
                    { "ERN_FR", "ERN", "FR", "Nakfa érythréen" },
                    { "ETB_AR", "ETB", "AR", "بر إثيوبي" },
                    { "ETB_EN", "ETB", "EN", "Ethiopian birr" },
                    { "ETB_FR", "ETB", "FR", "Birr éthiopien" },
                    { "EUR_AR", "EUR", "AR", "يورو" },
                    { "EUR_EN", "EUR", "EN", "Euro" },
                    { "EUR_FR", "EUR", "FR", "Euro" },
                    { "FJD_AR", "FJD", "AR", "دولار فيجي" },
                    { "FJD_EN", "FJD", "EN", "Fiji dollar" },
                    { "FJD_FR", "FJD", "FR", "Dollar fidjien" },
                    { "FKP_AR", "FKP", "AR", "جنيه جزر فوكلاند" },
                    { "FKP_EN", "FKP", "EN", "Falkland Islands pound" },
                    { "FKP_FR", "FKP", "FR", "Livre des îles Falkland" },
                    { "GBP_AR", "GBP", "AR", "الجنيه الإسترليني" },
                    { "GBP_EN", "GBP", "EN", "Great British Pound" },
                    { "GBP_FR", "GBP", "FR", "Livre sterling" },
                    { "GEL_AR", "GEL", "AR", "لاري جورجي" },
                    { "GEL_EN", "GEL", "EN", "Georgian lari" },
                    { "GEL_FR", "GEL", "FR", "Lari géorgien" },
                    { "GHS_AR", "GHS", "AR", "سيدي غاني" },
                    { "GHS_EN", "GHS", "EN", "Ghanaian cedi" },
                    { "GHS_FR", "GHS", "FR", "Cedi ghanéen" },
                    { "GIP_AR", "GIP", "AR", "جنيه جبل طارق" },
                    { "GIP_EN", "GIP", "EN", "Gibraltar pound" },
                    { "GIP_FR", "GIP", "FR", "Livre de Gibraltar" },
                    { "GMD_AR", "GMD", "AR", "دلاسي غامبي" },
                    { "GMD_EN", "GMD", "EN", "Gambian dalasi" },
                    { "GMD_FR", "GMD", "FR", "Dalasi gambien" },
                    { "GNF_AR", "GNF", "AR", "فرنك غيني" },
                    { "GNF_EN", "GNF", "EN", "Guinean franc" },
                    { "GNF_FR", "GNF", "FR", "Franc guinéen" },
                    { "GTQ_AR", "GTQ", "AR", "كتزال غواتيمالي" },
                    { "GTQ_EN", "GTQ", "EN", "Guatemalan quetzal" },
                    { "GTQ_FR", "GTQ", "FR", "Quetzal guatémaltèque" },
                    { "GYD_AR", "GYD", "AR", "دولار غياني" },
                    { "GYD_EN", "GYD", "EN", "Guyanese dollar" },
                    { "GYD_FR", "GYD", "FR", "Dollar guyanais" },
                    { "HKD_AR", "HKD", "AR", "دولار هونغ كونغ" },
                    { "HKD_EN", "HKD", "EN", "Hong Kong dollar" },
                    { "HKD_FR", "HKD", "FR", "Dollar de Hong Kong" },
                    { "HNL_AR", "HNL", "AR", "ليمبيرا هندوراسية" },
                    { "HNL_EN", "HNL", "EN", "Honduran lempira" },
                    { "HNL_FR", "HNL", "FR", "Lempira hondurien" },
                    { "HTG_AR", "HTG", "AR", "غورد هايتي" },
                    { "HTG_EN", "HTG", "EN", "Haitian gourde" },
                    { "HTG_FR", "HTG", "FR", "Gourde haïtienne" },
                    { "HUF_AR", "HUF", "AR", "فورينت هنغاري" },
                    { "HUF_EN", "HUF", "EN", "Hungarian forint" },
                    { "HUF_FR", "HUF", "FR", "Forint hongrois" },
                    { "IDR_AR", "IDR", "AR", "روبية إندونيسية" },
                    { "IDR_EN", "IDR", "EN", "Indonesian rupiah" },
                    { "IDR_FR", "IDR", "FR", "Roupie indonésienne" },
                    { "INR_AR", "INR", "AR", "روبية هندية" },
                    { "INR_EN", "INR", "EN", "Indian rupee" },
                    { "INR_FR", "INR", "FR", "Roupie indienne" },
                    { "IQD_AR", "IQD", "AR", "دينار عراقي" },
                    { "IQD_EN", "IQD", "EN", "Iraqi dinar" },
                    { "IQD_FR", "IQD", "FR", "Dinar irakien" },
                    { "IRR_AR", "IRR", "AR", "ريال إيراني" },
                    { "IRR_EN", "IRR", "EN", "Iranian rial" },
                    { "IRR_FR", "IRR", "FR", "Rial iranien" },
                    { "ISK_AR", "ISK", "AR", "كرونة آيسلندية" },
                    { "ISK_EN", "ISK", "EN", "Icelandic króna" },
                    { "ISK_FR", "ISK", "FR", "Couronne islandaise" },
                    { "JMD_AR", "JMD", "AR", "دولار جامايكي" },
                    { "JMD_EN", "JMD", "EN", "Jamaican dollar" },
                    { "JMD_FR", "JMD", "FR", "Dollar jamaïcain" },
                    { "JOD_AR", "JOD", "AR", "دينار أردني" },
                    { "JOD_EN", "JOD", "EN", "Jordanian dinar" },
                    { "JOD_FR", "JOD", "FR", "Dinar jordanien" },
                    { "JPY_AR", "JPY", "AR", "ين ياباني" },
                    { "JPY_EN", "JPY", "EN", "Japanese yen" },
                    { "JPY_FR", "JPY", "FR", "Yen japonais" },
                    { "KES_AR", "KES", "AR", "شلن كيني" },
                    { "KES_EN", "KES", "EN", "Kenyan shilling" },
                    { "KES_FR", "KES", "FR", "Shilling kényan" },
                    { "KGS_AR", "KGS", "AR", "سوم قيرغيزي" },
                    { "KGS_EN", "KGS", "EN", "Kyrgyzstani som" },
                    { "KGS_FR", "KGS", "FR", "Som kirghize" },
                    { "KHR_AR", "KHR", "AR", "ريال كمبودي" },
                    { "KHR_EN", "KHR", "EN", "Cambodian riel" },
                    { "KHR_FR", "KHR", "FR", "Riel cambodgien" },
                    { "KMF_AR", "KMF", "AR", "فرنك قمري" },
                    { "KMF_EN", "KMF", "EN", "Comorian franc" },
                    { "KMF_FR", "KMF", "FR", "Franc comorien" },
                    { "KPW_AR", "KPW", "AR", "وون كوري شمالي" },
                    { "KPW_EN", "KPW", "EN", "North Korean Won" },
                    { "KPW_FR", "KPW", "FR", "Won nord-coréen" },
                    { "KRW_AR", "KRW", "AR", "وون كوري جنوبي" },
                    { "KRW_EN", "KRW", "EN", "South Korean Won" },
                    { "KRW_FR", "KRW", "FR", "Won sud-coréen" },
                    { "KWD_AR", "KWD", "AR", "دينار كويتي" },
                    { "KWD_EN", "KWD", "EN", "Kuwaiti dinar" },
                    { "KWD_FR", "KWD", "FR", "Dinar koweïtien" },
                    { "KYD_AR", "KYD", "AR", "دولار جزر كايمان" },
                    { "KYD_EN", "KYD", "EN", "Cayman Islands dollar" },
                    { "KYD_FR", "KYD", "FR", "Dollar des îles Caïmans" },
                    { "KZT_AR", "KZT", "AR", "تينغي كازاخستاني" },
                    { "KZT_EN", "KZT", "EN", "Kazakhstani tenge" },
                    { "KZT_FR", "KZT", "FR", "Tenge kazakh" },
                    { "LAK_AR", "LAK", "AR", "كيب لاوسي" },
                    { "LAK_EN", "LAK", "EN", "Lao kip" },
                    { "LAK_FR", "LAK", "FR", "Kip laotien" },
                    { "LBP_AR", "LBP", "AR", "جنيه لبناني" },
                    { "LBP_EN", "LBP", "EN", "Lebanese pound" },
                    { "LBP_FR", "LBP", "FR", "Livre libanaise" },
                    { "LKR_AR", "LKR", "AR", "روبية سريلانكية" },
                    { "LKR_EN", "LKR", "EN", "Sri Lankan rupee" },
                    { "LKR_FR", "LKR", "FR", "Roupie srilankaise" },
                    { "LRD_AR", "LRD", "AR", "دولار ليبيري" },
                    { "LRD_EN", "LRD", "EN", "Liberian dollar" },
                    { "LRD_FR", "LRD", "FR", "Dollar libérien" },
                    { "LSL_AR", "LSL", "AR", "لوتي ليسوتي" },
                    { "LSL_EN", "LSL", "EN", "Lesotho loti" },
                    { "LSL_FR", "LSL", "FR", "Loti lesothan" },
                    { "LYD_AR", "LYD", "AR", "دينار ليبي" },
                    { "LYD_EN", "LYD", "EN", "Libyan dinar" },
                    { "LYD_FR", "LYD", "FR", "Dinar libyen" },
                    { "MAD_AR", "MAD", "AR", "درهم مغربي" },
                    { "MAD_EN", "MAD", "EN", "Moroccan dirham" },
                    { "MAD_FR", "MAD", "FR", "Dirham marocain" },
                    { "MDL_AR", "MDL", "AR", "ليو مولدوفي" },
                    { "MDL_EN", "MDL", "EN", "Moldovan leu" },
                    { "MDL_FR", "MDL", "FR", "Leu moldave" },
                    { "MGA_AR", "MGA", "AR", "أرياري ملغاشي" },
                    { "MGA_EN", "MGA", "EN", "Malagasy ariary" },
                    { "MGA_FR", "MGA", "FR", "Ariary malgache" },
                    { "MKD_AR", "MKD", "AR", "دينار مقدوني" },
                    { "MKD_EN", "MKD", "EN", "Macedonian denar" },
                    { "MKD_FR", "MKD", "FR", "Denar macédonien" },
                    { "MMK_AR", "MMK", "AR", "كيات ميانماري" },
                    { "MMK_EN", "MMK", "EN", "Myanmar kyat" },
                    { "MMK_FR", "MMK", "FR", "Kyat myanmarais" },
                    { "MNT_AR", "MNT", "AR", "توغروغ منغولي" },
                    { "MNT_EN", "MNT", "EN", "Mongolian tögrög" },
                    { "MNT_FR", "MNT", "FR", "Tögrög mongol" },
                    { "MOP_AR", "MOP", "AR", "باتاكا ماكاوية" },
                    { "MOP_EN", "MOP", "EN", "Macanese pataca" },
                    { "MOP_FR", "MOP", "FR", "Pataca macanaise" },
                    { "MRU_AR", "MRU", "AR", "أوقية موريتانية" },
                    { "MRU_EN", "MRU", "EN", "Mauritanian ouguiya" },
                    { "MRU_FR", "MRU", "FR", "Ouguiya mauritanienne" },
                    { "MUR_AR", "MUR", "AR", "روبية موريشيوسية" },
                    { "MUR_EN", "MUR", "EN", "Mauritian rupee" },
                    { "MUR_FR", "MUR", "FR", "Roupie mauricienne" },
                    { "MVR_AR", "MVR", "AR", "روفيه مالديفية" },
                    { "MVR_EN", "MVR", "EN", "Maldivian rufiyaa" },
                    { "MVR_FR", "MVR", "FR", "Rufiyaa maldivienne" },
                    { "MWK_AR", "MWK", "AR", "كواشا مالاوي" },
                    { "MWK_EN", "MWK", "EN", "Malawian kwacha" },
                    { "MWK_FR", "MWK", "FR", "Kwacha malawien" },
                    { "MXN_AR", "MXN", "AR", "بيزو مكسيكي" },
                    { "MXN_EN", "MXN", "EN", "Mexican peso" },
                    { "MXN_FR", "MXN", "FR", "Peso mexicain" },
                    { "MXV_AR", "MXV", "AR", "وحدة UDI المكسيكية" },
                    { "MXV_EN", "MXV", "EN", "Mexican UDI" },
                    { "MXV_FR", "MXV", "FR", "UDI mexicaine" },
                    { "MYR_AR", "MYR", "AR", "رينغيت ماليزي" },
                    { "MYR_EN", "MYR", "EN", "Malaysian ringgit" },
                    { "MYR_FR", "MYR", "FR", "Ringgit malaisien" },
                    { "MZN_AR", "MZN", "AR", "متكال موزمبيقي" },
                    { "MZN_EN", "MZN", "EN", "Mozambican metical" },
                    { "MZN_FR", "MZN", "FR", "Metical mozambicain" },
                    { "NAD_AR", "NAD", "AR", "دولار ناميبي" },
                    { "NAD_EN", "NAD", "EN", "Namibian dollar" },
                    { "NAD_FR", "NAD", "FR", "Dollar namibien" },
                    { "NGN_AR", "NGN", "AR", "نايرا نيجيري" },
                    { "NGN_EN", "NGN", "EN", "Nigerian naira" },
                    { "NGN_FR", "NGN", "FR", "Naira nigérian" },
                    { "NIK_AR", "NOK", "AR", "كرونة نرويجية" },
                    { "NIK_EN", "NOK", "EN", "Norwegian krone" },
                    { "NIK_FR", "NOK", "FR", "Couronne norvégienne" },
                    { "NIO_AR", "NIO", "AR", "كوردوبا نيكاراغوا" },
                    { "NIO_EN", "NIO", "EN", "Nicaraguan córdoba" },
                    { "NIO_FR", "NIO", "FR", "Córdoba nicaraguayen" },
                    { "NPR_AR", "NPR", "AR", "روبية نيبالية" },
                    { "NPR_EN", "NPR", "EN", "Nepalese rupee" },
                    { "NPR_FR", "NPR", "FR", "Roupie népalaise" },
                    { "NZD_AR", "NZD", "AR", "دولار نيوزيلندي" },
                    { "NZD_EN", "NZD", "EN", "New Zealand dollar" },
                    { "NZD_FR", "NZD", "FR", "Dollar néo-zélandais" },
                    { "OMR_AR", "OMR", "AR", "ريال عُماني" },
                    { "OMR_EN", "OMR", "EN", "Omani rial" },
                    { "OMR_FR", "OMR", "FR", "Rial omanais" },
                    { "PAB_AR", "PAB", "AR", "بالبوا بنمي" },
                    { "PAB_EN", "PAB", "EN", "Panamanian balboa" },
                    { "PAB_FR", "PAB", "FR", "Balboa panaméen" },
                    { "PEN_AR", "PEN", "AR", "سول بيروفي" },
                    { "PEN_EN", "PEN", "EN", "Peruvian sol" },
                    { "PEN_FR", "PEN", "FR", "Sol péruvien" },
                    { "PGK_AR", "PGK", "AR", "كينا بابوا غينيا الجديدة" },
                    { "PGK_EN", "PGK", "EN", "Papua New Guinean kina" },
                    { "PGK_FR", "PGK", "FR", "Kina papou-néo-guinéen" },
                    { "PHP_AR", "PHP", "AR", "بيزو فلبيني" },
                    { "PHP_EN", "PHP", "EN", "Philippine peso" },
                    { "PHP_FR", "PHP", "FR", "Peso philippin" },
                    { "PKR_AR", "PKR", "AR", "روبية باكستانية" },
                    { "PKR_EN", "PKR", "EN", "Pakistani rupee" },
                    { "PKR_FR", "PKR", "FR", "Roupie pakistanaise" },
                    { "PLN_AR", "PLN", "AR", "زلوتي بولندي" },
                    { "PLN_EN", "PLN", "EN", "Polish zloty" },
                    { "PLN_FR", "PLN", "FR", "Zloty polonais" },
                    { "PYG_AR", "PYG", "AR", "غواراني باراغواي" },
                    { "PYG_EN", "PYG", "EN", "Paraguayan guarani" },
                    { "PYG_FR", "PYG", "FR", "Guarani paraguayen" },
                    { "QAR_AR", "QAR", "AR", "ريال قطري" },
                    { "QAR_EN", "QAR", "EN", "Qatari riyal" },
                    { "QAR_FR", "QAR", "FR", "Riyal qatari" },
                    { "RON_AR", "RON", "AR", "ليو روماني" },
                    { "RON_EN", "RON", "EN", "Romanian leu" },
                    { "RON_FR", "RON", "FR", "Leu roumain" },
                    { "RSD_AR", "RSD", "AR", "دينار صربي" },
                    { "RSD_EN", "RSD", "EN", "Serbian dinar" },
                    { "RSD_FR", "RSD", "FR", "Dinar serbe" },
                    { "RUB_AR", "RUB", "AR", "روبل روسي" },
                    { "RUB_EN", "RUB", "EN", "Russian ruble" },
                    { "RUB_FR", "RUB", "FR", "Rouble russe" },
                    { "RWF_AR", "RWF", "AR", "فرنك رواندي" },
                    { "RWF_EN", "RWF", "EN", "Rwandan franc" },
                    { "RWF_FR", "RWF", "FR", "Franc rwandais" },
                    { "SAR_AR", "SAR", "AR", "ريال سعودي" },
                    { "SAR_EN", "SAR", "EN", "Saudi riyal" },
                    { "SAR_FR", "SAR", "FR", "Riyal saoudien" },
                    { "SBD_AR", "SBD", "AR", "دولار جزر سليمان" },
                    { "SBD_EN", "SBD", "EN", "Solomon Islands dollar" },
                    { "SBD_FR", "SBD", "FR", "Dollar des Îles Salomon" },
                    { "SCR_AR", "SCR", "AR", "روبية سيشيلية" },
                    { "SCR_EN", "SCR", "EN", "Seychellois rupee" },
                    { "SCR_FR", "SCR", "FR", "Roupie seychelloise" },
                    { "SDG_AR", "SDG", "AR", "جنيه سوداني" },
                    { "SDG_EN", "SDG", "EN", "Sudanese pound" },
                    { "SDG_FR", "SDG", "FR", "Livre soudanaise" },
                    { "SEK_AR", "SEK", "AR", "كرونة سويدية" },
                    { "SEK_EN", "SEK", "EN", "Swedish krona" },
                    { "SEK_FR", "SEK", "FR", "Couronne suédoise" },
                    { "SGD_AR", "SGD", "AR", "دولار سنغافوري" },
                    { "SGD_EN", "SGD", "EN", "Singapore dollar" },
                    { "SGD_FR", "SGD", "FR", "Dollar de Singapour" },
                    { "SHP_AR", "SHP", "AR", "جنيه سانت هيليني" },
                    { "SHP_EN", "SHP", "EN", "Saint Helena pound" },
                    { "SHP_FR", "SHP", "FR", "Livre de Sainte-Hélène" },
                    { "SLE_AR", "SLE", "AR", "ليون سيراليوني" },
                    { "SLE_EN", "SLE", "EN", "Sierra Leone leone" },
                    { "SLE_FR", "SLE", "FR", "Leone sierra-léonais" },
                    { "SOS_AR", "SOS", "AR", "شلن صومالي" },
                    { "SOS_EN", "SOS", "EN", "Somali shilling" },
                    { "SOS_FR", "SOS", "FR", "Shilling somalien" },
                    { "SRD_AR", "SRD", "AR", "دولار سورينامي" },
                    { "SRD_EN", "SRD", "EN", "Surinamese dollar" },
                    { "SRD_FR", "SRD", "FR", "Dollar surinamais" },
                    { "SSP_AR", "SSP", "AR", "جنيه جنوب سوداني" },
                    { "SSP_EN", "SSP", "EN", "South Sudanese pound" },
                    { "SSP_FR", "SSP", "FR", "Livre sud-soudanaise" },
                    { "STN_AR", "STN", "AR", "دوبرا ساو تومي وبرينسيب" },
                    { "STN_EN", "STN", "EN", "São Tomé and Príncipe dobra" },
                    { "STN_FR", "STN", "FR", "Dobra de Sao Tomé-et-Principe" },
                    { "SYP_AR", "SYP", "AR", "ليرة سورية" },
                    { "SYP_EN", "SYP", "EN", "Syrian pound" },
                    { "SYP_FR", "SYP", "FR", "Livre syrienne" },
                    { "SZL_AR", "SZL", "AR", "ليلانغيني إسواتيني" },
                    { "SZL_EN", "SZL", "EN", "Eswatini lilangeni" },
                    { "SZL_FR", "SZL", "FR", "Lilangeni eswatinien" },
                    { "THB_AR", "THB", "AR", "بات تايلندي" },
                    { "THB_EN", "THB", "EN", "Thai baht" },
                    { "THB_FR", "THB", "FR", "Baht thaïlandais" },
                    { "TJS_AR", "TJS", "AR", "سوموني طاجيكي" },
                    { "TJS_EN", "TJS", "EN", "Tajikistani somoni" },
                    { "TJS_FR", "TJS", "FR", "Somoni tadjik" },
                    { "TMT_AR", "TMT", "AR", "مانات تركماني" },
                    { "TMT_EN", "TMT", "EN", "Turkmenistan manat" },
                    { "TMT_FR", "TMT", "FR", "Manat turkmène" },
                    { "TND_AR", "TND", "AR", "دينار تونسي" },
                    { "TND_EN", "TND", "EN", "Tunisian dinar" },
                    { "TND_FR", "TND", "FR", "Dinar tunisien" },
                    { "TOP_AR", "TOP", "AR", "بانغا تونغي" },
                    { "TOP_EN", "TOP", "EN", "Tongan paʻanga" },
                    { "TOP_FR", "TOP", "FR", "Pa’anga tongien" },
                    { "TRY_AR", "TRY", "AR", "ليرة تركية" },
                    { "TRY_EN", "TRY", "EN", "Turkish lira" },
                    { "TRY_FR", "TRY", "FR", "Livre turque" },
                    { "TTD_AR", "TTD", "AR", "دولار ترينيداد وتوباغو" },
                    { "TTD_EN", "TTD", "EN", "Trinidad and Tobago dollar" },
                    { "TTD_FR", "TTD", "FR", "Dollar trinidadien" },
                    { "TWD_AR", "TWD", "AR", "دولار تايواني جديد" },
                    { "TWD_EN", "TWD", "EN", "New Taiwan dollar" },
                    { "TWD_FR", "TWD", "FR", "Nouveau dollar taïwanais" },
                    { "TZS_AR", "TZS", "AR", "شلن تنزاني" },
                    { "TZS_EN", "TZS", "EN", "Tanzanian shilling" },
                    { "TZS_FR", "TZS", "FR", "Shilling tanzanien" },
                    { "UAH_AR", "UAH", "AR", "هريفنيا أوكرانية" },
                    { "UAH_EN", "UAH", "EN", "Ukrainian hryvnia" },
                    { "UAH_FR", "UAH", "FR", "Hryvnia ukrainienne" },
                    { "UGX_AR", "UGX", "AR", "شلن أوغندي" },
                    { "UGX_EN", "UGX", "EN", "Ugandan shilling" },
                    { "UGX_FR", "UGX", "FR", "Shilling ougandais" },
                    { "USD_AR", "USD", "AR", "دولار أمريكي" },
                    { "USD_EN", "USD", "EN", "United States dollar" },
                    { "USD_FR", "USD", "FR", "Dollar des États-Unis" },
                    { "UYU_AR", "UYU", "AR", "بيزو أوروغواياني" },
                    { "UYU_EN", "UYU", "EN", "Uruguayan peso" },
                    { "UYU_FR", "UYU", "FR", "Peso uruguayen" },
                    { "UZS_AR", "UZS", "AR", "سوم أوزبكي" },
                    { "UZS_EN", "UZS", "EN", "Uzbekistani soʻm" },
                    { "UZS_FR", "UZS", "FR", "Soum ouzbek" },
                    { "VES_AR", "VES", "AR", "بوليفار فنزويلي" },
                    { "VES_EN", "VES", "EN", "Venezuelan bolívar" },
                    { "VES_FR", "VES", "FR", "Bolívar vénézuélien" },
                    { "VND_AR", "VND", "AR", "دونغ فيتنامي" },
                    { "VND_EN", "VND", "EN", "Vietnamese đồng" },
                    { "VND_FR", "VND", "FR", "Dong vietnamien" },
                    { "VUV_AR", "VUV", "AR", "فاتو فانواتو" },
                    { "VUV_EN", "VUV", "EN", "Vanuatu vatu" },
                    { "VUV_FR", "VUV", "FR", "Vatu vanuatais" },
                    { "WST_AR", "WST", "AR", "تالا ساموا" },
                    { "WST_EN", "WST", "EN", "Samoan tālā" },
                    { "WST_FR", "WST", "FR", "Tālā samoan" },
                    { "XAF_AR", "XAF", "AR", "فرنك إفريقي (BEAC)" },
                    { "XAF_EN", "XAF", "EN", "CFA franc BEAC" },
                    { "XAF_FR", "XAF", "FR", "Franc CFA BEAC" },
                    { "XAG_AR", "XAG", "AR", "فضة (أونصة تروي)" },
                    { "XAG_EN", "XAG", "EN", "Silver (troy ounce)" },
                    { "XAG_FR", "XAG", "FR", "Argent (once troy)" },
                    { "XAU_AR", "XAU", "AR", "ذهب (أونصة تروي)" },
                    { "XAU_EN", "XAU", "EN", "Gold (troy ounce)" },
                    { "XAU_FR", "XAU", "FR", "Or (once troy)" },
                    { "XBA_AR", "XBA", "AR", "وحدة مركبة أوروبية" },
                    { "XBA_EN", "XBA", "EN", "European composite unit" },
                    { "XBA_FR", "XBA", "FR", "Unité composite européenne" },
                    { "XBB_AR", "XBB", "AR", "وحدة نقدية أوروبية" },
                    { "XBB_EN", "XBB", "EN", "European monetary unit" },
                    { "XBB_FR", "XBB", "FR", "Unité monétaire européenne" },
                    { "XBC_AR", "XBC", "AR", "وحدة حساب أوروبية 9" },
                    { "XBC_EN", "XBC", "EN", "European unit of account 9" },
                    { "XBC_FR", "XBC", "FR", "Unité de compte européenne 9" },
                    { "XBD_AR", "XBD", "AR", "وحدة حساب أوروبية 17" },
                    { "XBD_EN", "XBD", "EN", "European unit of account 17" },
                    { "XBD_FR", "XBD", "FR", "Unité de compte européenne 17" },
                    { "XCD_AR", "XCD", "AR", "دولار شرق الكاريبي" },
                    { "XCD_EN", "XCD", "EN", "East Caribbean dollar" },
                    { "XCD_FR", "XCD", "FR", "Dollar des Caraïbes orientales" },
                    { "XCG_AR", "XCG", "AR", "فرنك إفريقي منطقة وسط أفريقيا" },
                    { "XCG_EN", "XCG", "EN", "CFA Franc BEAC" },
                    { "XCG_FR", "XCG", "FR", "Franc CFA BEAC" },
                    { "XDR_AR", "XDR", "AR", "حقوق سحب خاصة (IMF)" },
                    { "XDR_EN", "XDR", "EN", "SDR (IMF)" },
                    { "XDR_FR", "XDR", "FR", "DTS (FMI)" },
                    { "XOF_AR", "XOF", "AR", "فرنك إفريقي (BCEAO)" },
                    { "XOF_EN", "XOF", "EN", "CFA franc BCEAO" },
                    { "XOF_FR", "XOF", "FR", "Franc CFA BCEAO" },
                    { "XPD_AR", "XPD", "AR", "بالاديوم (أونصة تروي)" },
                    { "XPD_EN", "XPD", "EN", "Palladium (troy ounce)" },
                    { "XPD_FR", "XPD", "FR", "Palladium (once troy)" },
                    { "XPF_AR", "XPF", "AR", "فرنك CFP" },
                    { "XPF_EN", "XPF", "EN", "CFP franc" },
                    { "XPF_FR", "XPF", "FR", "Franc CFP" },
                    { "XPT_AR", "XPT", "AR", "بلاتين (أونصة تروي)" },
                    { "XPT_EN", "XPT", "EN", "Platinum (troy ounce)" },
                    { "XPT_FR", "XPT", "FR", "Platine (once troy)" },
                    { "XSU_AR", "XSU", "AR", "سوكر (وحدة ألبا)" },
                    { "XSU_EN", "XSU", "EN", "Sucre (ALBA unit)" },
                    { "XSU_FR", "XSU", "FR", "Sucre (unité ALBA)" },
                    { "XTS_AR", "XTS", "AR", "رمز للاختبار" },
                    { "XTS_EN", "XTS", "EN", "Code for testing" },
                    { "XTS_FR", "XTS", "FR", "Code pour tests" },
                    { "XUA_AR", "XUA", "AR", "وحدة حساب بنك التنمية الأفريقي" },
                    { "XUA_EN", "XUA", "EN", "ADB unit of account" },
                    { "XUA_FR", "XUA", "FR", "Unité de compte BAD" },
                    { "XXX_AR", "XXX", "AR", "بدون عملة" },
                    { "XXX_EN", "XXX", "EN", "No currency" },
                    { "XXX_FR", "XXX", "FR", "Aucune monnaie" },
                    { "YER_AR", "YER", "AR", "ريال يمني" },
                    { "YER_EN", "YER", "EN", "Yemeni rial" },
                    { "YER_FR", "YER", "FR", "Rial yéménite" },
                    { "ZAR_AR", "ZAR", "AR", "راند جنوب أفريقي" },
                    { "ZAR_EN", "ZAR", "EN", "South African rand" },
                    { "ZAR_FR", "ZAR", "FR", "Rand sud-africain" },
                    { "ZMW_AR", "ZMW", "AR", "كواشا زامبي" },
                    { "ZMW_EN", "ZMW", "EN", "Zambian kwacha" },
                    { "ZMW_FR", "ZMW", "FR", "Kwacha zambien" },
                    { "ZWL_AR", "ZWL", "AR", "دولار زيمبابوي" },
                    { "ZWL_EN", "ZWL", "EN", "Zimbabwean dollar" },
                    { "ZWL_FR", "ZWL", "FR", "Dollar zimbabwéen" }
                });

            migrationBuilder.InsertData(
                table: "EMails",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "CreatedUserName", "EMailAddress", "IsValid", "LastUpdatedDate", "LastUpdatedUserId", "LastUpdatedUserName", "PersonBaseId" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", "slim.aissaoui89@gmail.com", true, null, null, null, 1 });

            migrationBuilder.InsertData(
                table: "GenderNames",
                columns: new[] { "Id", "GenderId", "LanguageId", "Name" },
                values: new object[,]
                {
                    { "F_AR", "F", "AR", "أنثى" },
                    { "F_EN", "F", "EN", "Female" },
                    { "F_FR", "F", "FR", "Femme" },
                    { "M_AR", "M", "AR", "ذكر" },
                    { "M_EN", "M", "EN", "Male" },
                    { "M_FR", "M", "FR", "Homme" }
                });

            migrationBuilder.InsertData(
                table: "PersonIdentityTypeNames",
                columns: new[] { "LanguageId", "PersonIdentityTypeId", "Id", "Name" },
                values: new object[,]
                {
                    { "AR", "DL", "DL_AR", "رخصة القيادة" },
                    { "AR", "ID", "ID_AR", "بطاقة الهوية" },
                    { "AR", "PASS", "PASS_AR", "جواز السفر" },
                    { "AR", "RC", "RC_AR", "بطاقة الإقامة" },
                    { "EN", "DL", "DL_EN", "Driver's License" },
                    { "EN", "ID", "ID_EN", "Identity Card" },
                    { "EN", "PASS", "PASS_EN", "Passport" },
                    { "EN", "RC", "RC_EN", "Residence Card" },
                    { "FR", "DL", "DL_FR", "Permis de conduire" },
                    { "FR", "ID", "ID_FR", "Carte d'identité" },
                    { "FR", "PASS", "PASS_FR", "Passeport" },
                    { "FR", "RC", "RC_FR", "Carte de Séjour" }
                });

            migrationBuilder.InsertData(
                table: "PhoneTypeNames",
                columns: new[] { "Id", "LanguageId", "Name", "PhoneTypeId" },
                values: new object[,]
                {
                    { "Fax_AR", "AR", "فاكس", "Fax" },
                    { "Fax_EN", "EN", "Fax", "Fax" },
                    { "Fax_FR", "FR", "Fax", "Fax" },
                    { "Hom_AR", "AR", "المنزل", "Hom" },
                    { "Hom_EN", "EN", "Home", "Hom" },
                    { "Hom_FR", "FR", "Domicile", "Hom" },
                    { "Mob_AR", "AR", "جوال", "Mob" },
                    { "Mob_EN", "EN", "Mobile", "Mob" },
                    { "Mob_FR", "FR", "Mobile", "Mob" },
                    { "Wrk_AR", "AR", "العمل", "Wrk" },
                    { "Wrk_EN", "EN", "Work", "Wrk" },
                    { "Wrk_FR", "FR", "Travail", "Wrk" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsEnabled", "Password", "RoleId", "UserName" },
                values: new object[] { 1, false, "a491daf75263600280ccf6e3a56a576c93784039a45166f86905442334ef2888", 1, "slim.aissaoui" });

            migrationBuilder.InsertData(
                table: "CountryNames",
                columns: new[] { "Id", "CountryId", "LanguageId", "Name", "Nationality" },
                values: new object[,]
                {
                    { "AD_AR", "AD", "AR", "أندورا", "أندوري" },
                    { "AD_EN", "AD", "EN", "Andorra", "Andorran" },
                    { "AD_FR", "AD", "FR", "Andorre", "Andorran" },
                    { "AE_AR", "AE", "AR", "الإمارات العربية المتحدة", "إماراتي" },
                    { "AE_EN", "AE", "EN", "United Arab Emirates", "Emirati" },
                    { "AE_FR", "AE", "FR", "Émirats arabes unis", "Émirati" },
                    { "AF_AR", "AF", "AR", "أفغانستان", "أفغاني" },
                    { "AF_EN", "AF", "EN", "Afghanistan", "Afghan" },
                    { "AF_FR", "AF", "FR", "Afghanistan", "Afghan" },
                    { "AL_AR", "AL", "AR", "ألبانيا", "ألباني" },
                    { "AL_EN", "AL", "EN", "Albania", "Albanian" },
                    { "AL_FR", "AL", "FR", "Albanie", "Albanais" },
                    { "AM_AR", "AM", "AR", "أرمينيا", "أرميني" },
                    { "AM_EN", "AM", "EN", "Armenia", "Armenian" },
                    { "AM_FR", "AM", "FR", "Arménie", "Arménien" },
                    { "AO_AR", "AO", "AR", "أنغولا", "أنغولي" },
                    { "AO_EN", "AO", "EN", "Angola", "Angolan" },
                    { "AO_FR", "AO", "FR", "Angola", "Angolais" },
                    { "AR_AR", "AR", "AR", "الأرجنتين", "أرجنتيني" },
                    { "AR_EN", "AR", "EN", "Argentina", "Argentine" },
                    { "AR_FR", "AR", "FR", "Argentine", "Argentin" },
                    { "AT_AR", "AT", "AR", "النمسا", "نمساوي" },
                    { "AT_EN", "AT", "EN", "Austria", "Austrian" },
                    { "AT_FR", "AT", "FR", "Autriche", "Autrichien" },
                    { "AU_AR", "AU", "AR", "أستراليا", "أسترالي" },
                    { "AU_EN", "AU", "EN", "Australia", "Australian" },
                    { "AU_FR", "AU", "FR", "Australie", "Australien" },
                    { "AZ_AR", "AZ", "AR", "أذربيجان", "أذربيجاني" },
                    { "AZ_EN", "AZ", "EN", "Azerbaijan", "Azerbaijani" },
                    { "AZ_FR", "AZ", "FR", "Azerbaïdjan", "Azerbaïdjanais" },
                    { "BD_AR", "BD", "AR", "بنغلاديش", "بنغلاديشي" },
                    { "BD_EN", "BD", "EN", "Bangladesh", "Bangladeshi" },
                    { "BD_FR", "BD", "FR", "Bangladesh", "Bangladais" },
                    { "BE_AR", "BE", "AR", "بلجيكا", "بلجيكي" },
                    { "BE_EN", "BE", "EN", "Belgium", "Belgian" },
                    { "BE_FR", "BE", "FR", "Belgique", "Belge" },
                    { "BG_AR", "BG", "AR", "بلغاريا", "بلغاري" },
                    { "BG_EN", "BG", "EN", "Bulgaria", "Bulgarian" },
                    { "BG_FR", "BG", "FR", "Bulgarie", "Bulgare" },
                    { "BH_AR", "BH", "AR", "البحرين", "بحريني" },
                    { "BH_EN", "BH", "EN", "Bahrain", "Bahraini" },
                    { "BH_FR", "BH", "FR", "Bahreïn", "Bahreïni" },
                    { "BR_AR", "BR", "AR", "البرازيل", "برازيلي" },
                    { "BR_EN", "BR", "EN", "Brazil", "Brazilian" },
                    { "BR_FR", "BR", "FR", "Brésil", "Brésilien" },
                    { "CA_AR", "CA", "AR", "كندا", "كندي" },
                    { "CA_EN", "CA", "EN", "Canada", "Canadian" },
                    { "CA_FR", "CA", "FR", "Canada", "Canadien" },
                    { "CH_AR", "CH", "AR", "سويسرا", "سويسري" },
                    { "CH_EN", "CH", "EN", "Switzerland", "Swiss" },
                    { "CH_FR", "CH", "FR", "Suisse", "Suisse" },
                    { "CL_AR", "CL", "AR", "تشيلي", "تشيلي" },
                    { "CL_EN", "CL", "EN", "Chile", "Chilean" },
                    { "CL_FR", "CL", "FR", "Chili", "Chilien" },
                    { "CN_AR", "CN", "AR", "الصين", "صيني" },
                    { "CN_EN", "CN", "EN", "China", "Chinese" },
                    { "CN_FR", "CN", "FR", "Chine", "Chinois" },
                    { "CO_AR", "CO", "AR", "كولومبيا", "كولومبي" },
                    { "CO_EN", "CO", "EN", "Colombia", "Colombian" },
                    { "CO_FR", "CO", "FR", "Colombie", "Colombien" },
                    { "CR_AR", "CR", "AR", "كوستاريكا", "كوستاريكي" },
                    { "CR_EN", "CR", "EN", "Costa Rica", "Costa Rican" },
                    { "CR_FR", "CR", "FR", "Costa Rica", "Costaricien" },
                    { "CU_AR", "CU", "AR", "كوبا", "كوبي" },
                    { "CU_EN", "CU", "EN", "Cuba", "Cuban" },
                    { "CU_FR", "CU", "FR", "Cuba", "Cubain" },
                    { "DE_AR", "DE", "AR", "ألمانيا", "ألماني" },
                    { "DE_EN", "DE", "EN", "Germany", "German" },
                    { "DE_FR", "DE", "FR", "Allemagne", "Allemand" },
                    { "DJ_AR", "DJ", "AR", "جيبوتي", "جيبوتي" },
                    { "DJ_EN", "DJ", "EN", "Djibouti", "Djiboutian" },
                    { "DJ_FR", "DJ", "FR", "Djibouti", "Djiboutien" },
                    { "DK_AR", "DK", "AR", "الدنمارك", "دنماركي" },
                    { "DK_EN", "DK", "EN", "Denmark", "Danish" },
                    { "DK_FR", "DK", "FR", "Danemark", "Danois" },
                    { "DO_AR", "DO", "AR", "جمهورية الدومينيكان", "دومينيكاني" },
                    { "DO_EN", "DO", "EN", "Dominican Republic", "Dominican" },
                    { "DO_FR", "DO", "FR", "République dominicaine", "Dominicain" },
                    { "DZ_AR", "DZ", "AR", "الجزائر", "جزائري" },
                    { "DZ_EN", "DZ", "EN", "Algeria", "Algerian" },
                    { "DZ_FR", "DZ", "FR", "Algérie", "Algérien" },
                    { "EE_AR", "EE", "AR", "إستونيا", "إستوني" },
                    { "EE_EN", "EE", "EN", "Estonia", "Estonian" },
                    { "EE_FR", "EE", "FR", "Estonie", "Estonien" },
                    { "EG_AR", "EG", "AR", "مصر", "مصري" },
                    { "EG_EN", "EG", "EN", "Egypt", "Egyptian" },
                    { "EG_FR", "EG", "FR", "Égypte", "Égyptien" },
                    { "ES_AR", "ES", "AR", "إسبانيا", "إسباني" },
                    { "ES_EN", "ES", "EN", "Spain", "Spanish" },
                    { "ES_FR", "ES", "FR", "Espagne", "Espagnol" },
                    { "ET_AR", "ET", "AR", "إثيوبيا", "إثيوبي" },
                    { "ET_EN", "ET", "EN", "Ethiopia", "Ethiopian" },
                    { "ET_FR", "ET", "FR", "Éthiopie", "Éthiopien" },
                    { "FI_AR", "FI", "AR", "فنلندا", "فنلندي" },
                    { "FI_EN", "FI", "EN", "Finland", "Finnish" },
                    { "FI_FR", "FI", "FR", "Finlande", "Finlandais" },
                    { "FJ_AR", "FJ", "AR", "فيجي", "فيجي" },
                    { "FJ_EN", "FJ", "EN", "Fiji", "Fijian" },
                    { "FJ_FR", "FJ", "FR", "Fidji", "Fidjien" },
                    { "FM_AR", "FM", "AR", "ميكرونيزيا", "ميكرونيزي" },
                    { "FM_EN", "FM", "EN", "Micronesia", "Micronesian" },
                    { "FM_FR", "FM", "FR", "Micronésie", "Micronésien" },
                    { "FO_AR", "FO", "AR", "جزر فارو", "فاروي" },
                    { "FO_EN", "FO", "EN", "Faroe Islands", "Faroese" },
                    { "FO_FR", "FO", "FR", "Îles Féroé", "Féringien" },
                    { "FR_AR", "FR", "AR", "فرنسا", "فرنسي" },
                    { "FR_EN", "FR", "EN", "France", "French" },
                    { "FR_FR", "FR", "FR", "France", "Français" },
                    { "GA_AR", "GA", "AR", "الغابون", "غابوني" },
                    { "GA_EN", "GA", "EN", "Gabon", "Gabonese" },
                    { "GA_FR", "GA", "FR", "Gabon", "Gabonais" },
                    { "GB_AR", "GB", "AR", "المملكة المتحدة", "بريطاني" },
                    { "GB_EN", "GB", "EN", "United Kingdom", "British" },
                    { "GB_FR", "GB", "FR", "Royaume-Uni", "Britannique" },
                    { "GE_AR", "GE", "AR", "جورجيا", "جورجي" },
                    { "GE_EN", "GE", "EN", "Georgia", "Georgian" },
                    { "GE_FR", "GE", "FR", "Géorgie", "Géorgien" },
                    { "GH_AR", "GH", "AR", "غانا", "غاني" },
                    { "GH_EN", "GH", "EN", "Ghana", "Ghanaian" },
                    { "GH_FR", "GH", "FR", "Ghana", "Ghanéen" },
                    { "GM_AR", "GM", "AR", "غامبيا", "غامبي" },
                    { "GM_EN", "GM", "EN", "Gambia", "Gambian" },
                    { "GM_FR", "GM", "FR", "Gambie", "Gambien" },
                    { "GN_AR", "GN", "AR", "غينيا", "غيني" },
                    { "GN_EN", "GN", "EN", "Guinea", "Guinean" },
                    { "GN_FR", "GN", "FR", "Guinée", "Guinéen" },
                    { "GR_AR", "GR", "AR", "اليونان", "يوناني" },
                    { "GR_EN", "GR", "EN", "Greece", "Greek" },
                    { "GR_FR", "GR", "FR", "Grèce", "Grec" },
                    { "GT_AR", "GT", "AR", "غواتيمالا", "غواتيمالي" },
                    { "GT_EN", "GT", "EN", "Guatemala", "Guatemalan" },
                    { "GT_FR", "GT", "FR", "Guatemala", "Guatémaltèque" },
                    { "GW_AR", "GW", "AR", "غينيا بيساو", "غيني بيساوي" },
                    { "GW_EN", "GW", "EN", "Guinea-Bissau", "Bissau-Guinean" },
                    { "GW_FR", "GW", "FR", "Guinée-Bissau", "Bissau-Guinéen" },
                    { "GY_AR", "GY", "AR", "غيانا", "غياني" },
                    { "GY_EN", "GY", "EN", "Guyana", "Guyanese" },
                    { "GY_FR", "GY", "FR", "Guyana", "Guyanien" },
                    { "HN_AR", "HN", "AR", "هندوراس", "هندوراسي" },
                    { "HN_EN", "HN", "EN", "Honduras", "Honduran" },
                    { "HN_FR", "HN", "FR", "Honduras", "Hondurien" },
                    { "HT_AR", "HT", "AR", "هايتي", "هايتي" },
                    { "HT_EN", "HT", "EN", "Haiti", "Haitian" },
                    { "HT_FR", "HT", "FR", "Haïti", "Haïtien" },
                    { "HU_AR", "HU", "AR", "المجر", "مجري" },
                    { "HU_EN", "HU", "EN", "Hungary", "Hungarian" },
                    { "HU_FR", "HU", "FR", "Hongrie", "Hongrois" },
                    { "ID_AR", "ID", "AR", "إندونيسيا", "إندونيسي" },
                    { "ID_EN", "ID", "EN", "Indonesia", "Indonesian" },
                    { "ID_FR", "ID", "FR", "Indonésie", "Indonésien" },
                    { "IE_AR", "IE", "AR", "أيرلندا", "أيرلندي" },
                    { "IE_EN", "IE", "EN", "Ireland", "Irish" },
                    { "IE_FR", "IE", "FR", "Irlande", "Irlandais" },
                    { "IN_AR", "IN", "AR", "الهند", "هندي" },
                    { "IN_EN", "IN", "EN", "India", "Indian" },
                    { "IN_FR", "IN", "FR", "Inde", "Indien" },
                    { "IQ_AR", "IQ", "AR", "العراق", "عراقي" },
                    { "IQ_EN", "IQ", "EN", "Iraq", "Iraqi" },
                    { "IQ_FR", "IQ", "FR", "Irak", "Irakien" },
                    { "IR_AR", "IR", "AR", "إيران", "إيراني" },
                    { "IR_EN", "IR", "EN", "Iran", "Iranian" },
                    { "IR_FR", "IR", "FR", "Iran", "Iranien" },
                    { "IS_AR", "IS", "AR", "آيسلندا", "آيسلندي" },
                    { "IS_EN", "IS", "EN", "Iceland", "Icelandic" },
                    { "IS_FR", "IS", "FR", "Islande", "Islandais" },
                    { "IT_AR", "IT", "AR", "إيطاليا", "إيطالي" },
                    { "IT_EN", "IT", "EN", "Italy", "Italian" },
                    { "IT_FR", "IT", "FR", "Italie", "Italien" },
                    { "JM_AR", "JM", "AR", "جامايكا", "جامايكي" },
                    { "JM_EN", "JM", "EN", "Jamaica", "Jamaican" },
                    { "JM_FR", "JM", "FR", "Jamaïque", "Jamaïcain" },
                    { "JO_AR", "JO", "AR", "الأردن", "أردني" },
                    { "JO_EN", "JO", "EN", "Jordan", "Jordanian" },
                    { "JO_FR", "JO", "FR", "Jordanie", "Jordanien" },
                    { "JP_AR", "JP", "AR", "اليابان", "ياباني" },
                    { "JP_EN", "JP", "EN", "Japan", "Japanese" },
                    { "JP_FR", "JP", "FR", "Japon", "Japonais" },
                    { "KE_AR", "KE", "AR", "كينيا", "كيني" },
                    { "KE_EN", "KE", "EN", "Kenya", "Kenyan" },
                    { "KE_FR", "KE", "FR", "Kenya", "Kényan" },
                    { "KG_AR", "KG", "AR", "قرغيزستان", "قرغيزي" },
                    { "KG_EN", "KG", "EN", "Kyrgyzstan", "Kyrgyz" },
                    { "KG_FR", "KG", "FR", "Kirghizistan", "Kirghize" },
                    { "KI_AR", "KI", "AR", "كيريباس", "كيريباسي" },
                    { "KI_EN", "KI", "EN", "Kiribati", "I-Kiribati" },
                    { "KI_FR", "KI", "FR", "Kiribati", "Kiribatien" },
                    { "KN_AR", "KN", "AR", "سانت كيتس ونيفيس", "نيفيسي" },
                    { "KN_EN", "KN", "EN", "Saint Kitts and Nevis", "Kittitian / Nevisian" },
                    { "KN_FR", "KN", "FR", "Saint-Christophe-et-Niévès", "Kittitien / Névicien" },
                    { "KR_AR", "KR", "AR", "كوريا الجنوبية", "كوري جنوبي" },
                    { "KR_EN", "KR", "EN", "South Korea", "South Korean" },
                    { "KR_FR", "KR", "FR", "Corée du Sud", "Sud-Coréen" },
                    { "KW_AR", "KW", "AR", "الكويت", "كويتي" },
                    { "KW_EN", "KW", "EN", "Kuwait", "Kuwaiti" },
                    { "KW_FR", "KW", "FR", "Koweït", "Koweïtien" },
                    { "KZ_AR", "KZ", "AR", "كازاخستان", "كازاخستاني" },
                    { "KZ_EN", "KZ", "EN", "Kazakhstan", "Kazakh" },
                    { "KZ_FR", "KZ", "FR", "Kazakhstan", "Kazakh" },
                    { "LA_AR", "LA", "AR", "لاوس", "لاوسي" },
                    { "LA_EN", "LA", "EN", "Laos", "Lao" },
                    { "LA_FR", "LA", "FR", "Laos", "Laotien" },
                    { "LB_AR", "LB", "AR", "لبنان", "لبناني" },
                    { "LB_EN", "LB", "EN", "Lebanon", "Lebanese" },
                    { "LB_FR", "LB", "FR", "Liban", "Libanais" },
                    { "LC_AR", "LC", "AR", "سانت لوسيا", "لوسياني" },
                    { "LC_EN", "LC", "EN", "Saint Lucia", "Saint Lucian" },
                    { "LC_FR", "LC", "FR", "Sainte-Lucie", "Saint-Lucien" },
                    { "LI_AR", "LI", "AR", "ليختنشتاين", "ليختنشتايني" },
                    { "LI_EN", "LI", "EN", "Liechtenstein", "Liechtensteiner" },
                    { "LI_FR", "LI", "FR", "Liechtenstein", "Liechtensteinois" },
                    { "LK_AR", "LK", "AR", "سريلانكا", "سريلانكي" },
                    { "LK_EN", "LK", "EN", "Sri Lanka", "Sri Lankan" },
                    { "LK_FR", "LK", "FR", "Sri Lanka", "Sri-Lankais" },
                    { "LR_AR", "LR", "AR", "ليبيريا", "ليبيري" },
                    { "LR_EN", "LR", "EN", "Liberia", "Liberian" },
                    { "LR_FR", "LR", "FR", "Libéria", "Libérien" },
                    { "LS_AR", "LS", "AR", "ليسوتو", "ليسوتي" },
                    { "LS_EN", "LS", "EN", "Lesotho", "Basotho" },
                    { "LS_FR", "LS", "FR", "Lesotho", "Lésothien" },
                    { "LT_AR", "LT", "AR", "ليتوانيا", "ليتواني" },
                    { "LT_EN", "LT", "EN", "Lithuania", "Lithuanian" },
                    { "LT_FR", "LT", "FR", "Lituanie", "Lituanien" },
                    { "LU_AR", "LU", "AR", "لوكسمبورغ", "لوكسمبورغي" },
                    { "LU_EN", "LU", "EN", "Luxembourg", "Luxembourgish" },
                    { "LU_FR", "LU", "FR", "Luxembourg", "Luxembourgeois" },
                    { "LV_AR", "LV", "AR", "لاتفيا", "لاتفي" },
                    { "LV_EN", "LV", "EN", "Latvia", "Latvian" },
                    { "LV_FR", "LV", "FR", "Lettonie", "Letton" },
                    { "LY_AR", "LY", "AR", "ليبيا", "ليبي" },
                    { "LY_EN", "LY", "EN", "Libya", "Libyan" },
                    { "LY_FR", "LY", "FR", "Libye", "Libyen" },
                    { "MA_AR", "MA", "AR", "المغرب", "مغربي" },
                    { "MA_EN", "MA", "EN", "Morocco", "Moroccan" },
                    { "MA_FR", "MA", "FR", "Maroc", "Marocain" },
                    { "MC_AR", "MC", "AR", "موناكو", "موناكي" },
                    { "MC_EN", "MC", "EN", "Monaco", "Monegasque" },
                    { "MC_FR", "MC", "FR", "Monaco", "Monégasque" },
                    { "MD_AR", "MD", "AR", "مولدوفا", "مولدوفي" },
                    { "MD_EN", "MD", "EN", "Moldova", "Moldovan" },
                    { "MD_FR", "MD", "FR", "Moldavie", "Moldave" },
                    { "ME_AR", "ME", "AR", "الجبل الأسود", "مونتينيغري" },
                    { "ME_EN", "ME", "EN", "Montenegro", "Montenegrin" },
                    { "ME_FR", "ME", "FR", "Monténégro", "Monténégrin" },
                    { "MG_AR", "MG", "AR", "مدغشقر", "مدغشقري" },
                    { "MG_EN", "MG", "EN", "Madagascar", "Malagasy" },
                    { "MG_FR", "MG", "FR", "Madagascar", "Malgache" },
                    { "MH_AR", "MH", "AR", "جزر مارشال", "مارشالي" },
                    { "MH_EN", "MH", "EN", "Marshall Islands", "Marshallese" },
                    { "MH_FR", "MH", "FR", "Îles Marshall", "Marshallais" },
                    { "ML_AR", "ML", "AR", "مالي", "مالي" },
                    { "ML_EN", "ML", "EN", "Mali", "Malian" },
                    { "ML_FR", "ML", "FR", "Mali", "Malien" },
                    { "MM_AR", "MM", "AR", "ميانمار", "بورمي" },
                    { "MM_EN", "MM", "EN", "Myanmar", "Burmese" },
                    { "MM_FR", "MM", "FR", "Myanmar", "Birman" },
                    { "MN_AR", "MN", "AR", "منغوليا", "منغولي" },
                    { "MN_EN", "MN", "EN", "Mongolia", "Mongolian" },
                    { "MN_FR", "MN", "FR", "Mongolie", "Mongol" },
                    { "MR_AR", "MR", "AR", "موريتانيا", "موريتاني" },
                    { "MR_EN", "MR", "EN", "Mauritania", "Mauritanian" },
                    { "MR_FR", "MR", "FR", "Mauritanie", "Mauritanien" },
                    { "MT_AR", "MT", "AR", "مالطا", "مالطي" },
                    { "MT_EN", "MT", "EN", "Malta", "Maltese" },
                    { "MT_FR", "MT", "FR", "Malte", "Maltais" },
                    { "MU_AR", "MU", "AR", "موريشيوس", "موريشي" },
                    { "MU_EN", "MU", "EN", "Mauritius", "Mauritian" },
                    { "MU_FR", "MU", "FR", "Maurice", "Mauricien" },
                    { "MV_AR", "MV", "AR", "المالديف", "مالديفي" },
                    { "MV_EN", "MV", "EN", "Maldives", "Maldivian" },
                    { "MV_FR", "MV", "FR", "Maldives", "Maldivien" },
                    { "MW_AR", "MW", "AR", "ملاوي", "ملاوي" },
                    { "MW_EN", "MW", "EN", "Malawi", "Malawian" },
                    { "MW_FR", "MW", "FR", "Malawi", "Malawien" },
                    { "MX_AR", "MX", "AR", "المكسيك", "مكسيكي" },
                    { "MX_EN", "MX", "EN", "Mexico", "Mexican" },
                    { "MX_FR", "MX", "FR", "Mexique", "Mexicain" },
                    { "MY_AR", "MY", "AR", "ماليزيا", "ماليزي" },
                    { "MY_EN", "MY", "EN", "Malaysia", "Malaysian" },
                    { "MY_FR", "MY", "FR", "Malaisie", "Malaisien" },
                    { "MZ_AR", "MZ", "AR", "موزمبيق", "موزمبيقي" },
                    { "MZ_EN", "MZ", "EN", "Mozambique", "Mozambican" },
                    { "MZ_FR", "MZ", "FR", "Mozambique", "Mozambicain" },
                    { "NA_AR", "NA", "AR", "ناميبيا", "ناميبي" },
                    { "NA_EN", "NA", "EN", "Namibia", "Namibian" },
                    { "NA_FR", "NA", "FR", "Namibie", "Namibien" },
                    { "NG_AR", "NG", "AR", "نيجيريا", "نيجيري" },
                    { "NG_EN", "NG", "EN", "Nigeria", "Nigerian" },
                    { "NG_FR", "NG", "FR", "Nigéria", "Nigérian" },
                    { "NI_AR", "NI", "AR", "نيكاراغوا", "نيكاراغوي" },
                    { "NI_EN", "NI", "EN", "Nicaragua", "Nicaraguan" },
                    { "NI_FR", "NI", "FR", "Nicaragua", "Nicaraguayen" },
                    { "NL_AR", "NL", "AR", "هولندا", "هولندي" },
                    { "NL_EN", "NL", "EN", "Netherlands", "Dutch" },
                    { "NL_FR", "NL", "FR", "Pays-Bas", "Néerlandais" },
                    { "NO_AR", "NO", "AR", "النرويج", "نرويجي" },
                    { "NO_EN", "NO", "EN", "Norway", "Norwegian" },
                    { "NO_FR", "NO", "FR", "Norvège", "Norvégien" },
                    { "NP_AR", "NP", "AR", "نيبال", "نيبالي" },
                    { "NP_EN", "NP", "EN", "Nepal", "Nepali" },
                    { "NP_FR", "NP", "FR", "Népal", "Népalais" },
                    { "NZ_AR", "NZ", "AR", "نيوزيلندا", "نيوزيلندي" },
                    { "NZ_EN", "NZ", "EN", "New Zealand", "New Zealander" },
                    { "NZ_FR", "NZ", "FR", "Nouvelle-Zélande", "Néo-Zélandais" },
                    { "OM_AR", "OM", "AR", "عمان", "عماني" },
                    { "OM_EN", "OM", "EN", "Oman", "Omani" },
                    { "OM_FR", "OM", "FR", "Oman", "Omanais" },
                    { "PA_AR", "PA", "AR", "بنما", "بنمي" },
                    { "PA_EN", "PA", "EN", "Panama", "Panamanian" },
                    { "PA_FR", "PA", "FR", "Panama", "Panaméen" },
                    { "PE_AR", "PE", "AR", "بيرو", "بيروفي" },
                    { "PE_EN", "PE", "EN", "Peru", "Peruvian" },
                    { "PE_FR", "PE", "FR", "Pérou", "Péruvien" },
                    { "PG_AR", "PG", "AR", "بابوا غينيا الجديدة", "بابوي" },
                    { "PG_EN", "PG", "EN", "Papua New Guinea", "Papua New Guinean" },
                    { "PG_FR", "PG", "FR", "Papouasie-Nouvelle-Guinée", "Papou" },
                    { "PH_AR", "PH", "AR", "الفلبين", "فلبيني" },
                    { "PH_EN", "PH", "EN", "Philippines", "Filipino" },
                    { "PH_FR", "PH", "FR", "Philippines", "Philippin" },
                    { "PK_AR", "PK", "AR", "باكستان", "باكستاني" },
                    { "PK_EN", "PK", "EN", "Pakistan", "Pakistani" },
                    { "PK_FR", "PK", "FR", "Pakistan", "Pakistanais" },
                    { "PL_AR", "PL", "AR", "بولندا", "بولندي" },
                    { "PL_EN", "PL", "EN", "Poland", "Polish" },
                    { "PL_FR", "PL", "FR", "Pologne", "Polonais" },
                    { "PS_AR", "PS", "AR", "فلسطين", "فلسطيني" },
                    { "PS_EN", "PS", "EN", "Palestine", "Palestinian" },
                    { "PS_FR", "PS", "FR", "Palestine", "Palestinien" },
                    { "PT_AR", "PT", "AR", "البرتغال", "برتغالي" },
                    { "PT_EN", "PT", "EN", "Portugal", "Portuguese" },
                    { "PT_FR", "PT", "FR", "Portugal", "Portugais" },
                    { "PW_AR", "PW", "AR", "بالاو", "بالاوي" },
                    { "PW_EN", "PW", "EN", "Palau", "Palauan" },
                    { "PW_FR", "PW", "FR", "Palaos", "Paluan" },
                    { "PY_AR", "PY", "AR", "باراغواي", "باراغواياني" },
                    { "PY_EN", "PY", "EN", "Paraguay", "Paraguayan" },
                    { "PY_FR", "PY", "FR", "Paraguay", "Paraguayen" },
                    { "QA_AR", "QA", "AR", "قطر", "قطري" },
                    { "QA_EN", "QA", "EN", "Qatar", "Qatari" },
                    { "QA_FR", "QA", "FR", "Qatar", "Qatari" },
                    { "RO_AR", "RO", "AR", "رومانيا", "روماني" },
                    { "RO_EN", "RO", "EN", "Romania", "Romanian" },
                    { "RO_FR", "RO", "FR", "Roumanie", "Roumain" },
                    { "RS_AR", "RS", "AR", "صربيا", "صربي" },
                    { "RS_EN", "RS", "EN", "Serbia", "Serbian" },
                    { "RS_FR", "RS", "FR", "Serbie", "Serbe" },
                    { "RU_AR", "RU", "AR", "روسيا", "روسي" },
                    { "RU_EN", "RU", "EN", "Russia", "Russian" },
                    { "RU_FR", "RU", "FR", "Russie", "Russe" },
                    { "RW_AR", "RW", "AR", "رواندا", "رواندي" },
                    { "RW_EN", "RW", "EN", "Rwanda", "Rwandan" },
                    { "RW_FR", "RW", "FR", "Rwanda", "Rwandais" },
                    { "SA_AR", "SA", "AR", "السعودية", "سعودي" },
                    { "SA_EN", "SA", "EN", "Saudi Arabia", "Saudi" },
                    { "SA_FR", "SA", "FR", "Arabie saoudite", "Saoudien" },
                    { "SC_AR", "SC", "AR", "سيشل", "سيشلي" },
                    { "SC_EN", "SC", "EN", "Seychelles", "Seychellois" },
                    { "SC_FR", "SC", "FR", "Seychelles", "Seychellois" },
                    { "SD_AR", "SD", "AR", "السودان", "سوداني" },
                    { "SD_EN", "SD", "EN", "Sudan", "Sudanese" },
                    { "SD_FR", "SD", "FR", "Soudan", "Soudanais" },
                    { "SE_AR", "SE", "AR", "السويد", "سويدي" },
                    { "SE_EN", "SE", "EN", "Sweden", "Swedish" },
                    { "SE_FR", "SE", "FR", "Suède", "Suédois" },
                    { "SG_AR", "SG", "AR", "سنغافورة", "سنغافوري" },
                    { "SG_EN", "SG", "EN", "Singapore", "Singaporean" },
                    { "SG_FR", "SG", "FR", "Singapour", "Singapourien" },
                    { "SI_AR", "SI", "AR", "سلوفينيا", "سلوفيني" },
                    { "SI_EN", "SI", "EN", "Slovenia", "Slovenian" },
                    { "SI_FR", "SI", "FR", "Slovénie", "Slovène" },
                    { "SK_AR", "SK", "AR", "سلوفاكيا", "سلوفاكي" },
                    { "SK_EN", "SK", "EN", "Slovakia", "Slovak" },
                    { "SK_FR", "SK", "FR", "Slovaquie", "Slovaque" },
                    { "SL_AR", "SL", "AR", "سيراليون", "سيراليوني" },
                    { "SL_EN", "SL", "EN", "Sierra Leone", "Sierra Leonean" },
                    { "SL_FR", "SL", "FR", "Sierra Leone", "Sierra-Léonais" },
                    { "SM_AR", "SM", "AR", "سان مارينو", "ماريني" },
                    { "SM_EN", "SM", "EN", "San Marino", "Sammarinese" },
                    { "SM_FR", "SM", "FR", "Saint-Marin", "Saint-Marinais" },
                    { "SN_AR", "SN", "AR", "السنغال", "سنغالي" },
                    { "SN_EN", "SN", "EN", "Senegal", "Senegalese" },
                    { "SN_FR", "SN", "FR", "Sénégal", "Sénégalais" },
                    { "SO_AR", "SO", "AR", "الصومال", "صومالي" },
                    { "SO_EN", "SO", "EN", "Somalia", "Somali" },
                    { "SO_FR", "SO", "FR", "Somalie", "Somalien" },
                    { "SR_AR", "SR", "AR", "سورينام", "سورينامي" },
                    { "SR_EN", "SR", "EN", "Suriname", "Surinamese" },
                    { "SR_FR", "SR", "FR", "Suriname", "Surinamien" },
                    { "ST_AR", "ST", "AR", "ساو تومي وبرينسيب", "ساو تومي" },
                    { "ST_EN", "ST", "EN", "São Tomé and Príncipe", "São Toméan" },
                    { "ST_FR", "ST", "FR", "Sao Tomé-et-Principe", "Santoméen" },
                    { "SY_AR", "SY", "AR", "سوريا", "سوري" },
                    { "SY_EN", "SY", "EN", "Syria", "Syrian" },
                    { "SY_FR", "SY", "FR", "Syrie", "Syrien" },
                    { "TG_AR", "TG", "AR", "توجو", "توغولي" },
                    { "TG_EN", "TG", "EN", "Togo", "Togolese" },
                    { "TG_FR", "TG", "FR", "Togo", "Togolais" },
                    { "TH_AR", "TH", "AR", "تايلاند", "تايلاندي" },
                    { "TH_EN", "TH", "EN", "Thailand", "Thai" },
                    { "TH_FR", "TH", "FR", "Thaïlande", "Thaïlandais" },
                    { "TJ_AR", "TJ", "AR", "طاجيكستان", "طاجيكي" },
                    { "TJ_EN", "TJ", "EN", "Tajikistan", "Tajik" },
                    { "TJ_FR", "TJ", "FR", "Tadjikistan", "Tadjik" },
                    { "TL_AR", "TL", "AR", "تيمور الشرقية", "تيموري" },
                    { "TL_EN", "TL", "EN", "Timor-Leste", "Timorese" },
                    { "TL_FR", "TL", "FR", "Timor oriental", "Timorais" },
                    { "TN_AR", "TN", "AR", "تونس", "تونسي" },
                    { "TN_EN", "TN", "EN", "Tunisia", "Tunisian" },
                    { "TN_FR", "TN", "FR", "Tunisie", "Tunisien" },
                    { "TO_AR", "TO", "AR", "تونغا", "تونغي" },
                    { "TO_EN", "TO", "EN", "Tonga", "Tongan" },
                    { "TO_FR", "TO", "FR", "Tonga", "Tongien" },
                    { "TR_AR", "TR", "AR", "تركيا", "تركي" },
                    { "TR_EN", "TR", "EN", "Turkey", "Turkish" },
                    { "TR_FR", "TR", "FR", "Turquie", "Turc" },
                    { "TT_AR", "TT", "AR", "ترينيداد وتوباغو", "ترينيدادي" },
                    { "TT_EN", "TT", "EN", "Trinidad and Tobago", "Trinidadian / Tobagonian" },
                    { "TT_FR", "TT", "FR", "Trinité-et-Tobago", "Trinidadien / Tobagonien" },
                    { "TW_AR", "TW", "AR", "تايوان", "تايواني" },
                    { "TW_EN", "TW", "EN", "Taiwan", "Taiwanese" },
                    { "TW_FR", "TW", "FR", "Taïwan", "Taïwanais" },
                    { "TZ_AR", "TZ", "AR", "تنزانيا", "تنزاني" },
                    { "TZ_EN", "TZ", "EN", "Tanzania", "Tanzanian" },
                    { "TZ_FR", "TZ", "FR", "Tanzanie", "Tanzanien" },
                    { "UA_AR", "UA", "AR", "أوكرانيا", "أوكراني" },
                    { "UA_EN", "UA", "EN", "Ukraine", "Ukrainian" },
                    { "UA_FR", "UA", "FR", "Ukraine", "Ukrainien" },
                    { "UG_AR", "UG", "AR", "أوغندا", "أوغندي" },
                    { "UG_EN", "UG", "EN", "Uganda", "Ugandan" },
                    { "UG_FR", "UG", "FR", "Ouganda", "Ougandais" },
                    { "US_AR", "US", "AR", "الولايات المتحدة الأمريكية", "أمريكي" },
                    { "US_EN", "US", "EN", "United States", "American" },
                    { "US_FR", "US", "FR", "États-Unis", "Américain" },
                    { "UY_AR", "UY", "AR", "أوروغواي", "أوروغواياني" },
                    { "UY_EN", "UY", "EN", "Uruguay", "Uruguayan" },
                    { "UY_FR", "UY", "FR", "Uruguay", "Uruguayen" },
                    { "UZ_AR", "UZ", "AR", "أوزبكستان", "أوزبكي" },
                    { "UZ_EN", "UZ", "EN", "Uzbekistan", "Uzbek" },
                    { "UZ_FR", "UZ", "FR", "Ouzbékistan", "Ouzbek" },
                    { "VC_AR", "VC", "AR", "سانت فنسنت والغرينادين", "فنسنتي" },
                    { "VC_EN", "VC", "EN", "Saint Vincent and the Grenadines", "Vincentian" },
                    { "VC_FR", "VC", "FR", "Saint-Vincent-et-les-Grenadines", "Saint-Vincentais" },
                    { "VE_AR", "VE", "AR", "فنزويلا", "فنزويلي" },
                    { "VE_EN", "VE", "EN", "Venezuela", "Venezuelan" },
                    { "VE_FR", "VE", "FR", "Venezuela", "Vénézuélien" },
                    { "VN_AR", "VN", "AR", "فيتنام", "فيتنامي" },
                    { "VN_EN", "VN", "EN", "Vietnam", "Vietnamese" },
                    { "VN_FR", "VN", "FR", "Viêt Nam", "Vietnamien" },
                    { "VU_AR", "VU", "AR", "فانواتو", "فانواتي" },
                    { "VU_EN", "VU", "EN", "Vanuatu", "Ni-Vanuatu" },
                    { "VU_FR", "VU", "FR", "Vanuatu", "Vanuatais" },
                    { "WS_AR", "WS", "AR", "ساموا", "ساموي" },
                    { "WS_EN", "WS", "EN", "Samoa", "Samoan" },
                    { "WS_FR", "WS", "FR", "Samoa", "Samoan" },
                    { "YE_AR", "YE", "AR", "اليمن", "يمني" },
                    { "YE_EN", "YE", "EN", "Yemen", "Yemeni" },
                    { "YE_FR", "YE", "FR", "Yémen", "Yéménite" },
                    { "ZA_AR", "ZA", "AR", "جنوب أفريقيا", "جنوب أفريقي" },
                    { "ZA_EN", "ZA", "EN", "South Africa", "South African" },
                    { "ZA_FR", "ZA", "FR", "Afrique du Sud", "Sud-Africain" },
                    { "ZM_AR", "ZM", "AR", "زامبيا", "زامبي" },
                    { "ZM_EN", "ZM", "EN", "Zambia", "Zambian" },
                    { "ZM_FR", "ZM", "FR", "Zambie", "Zambien" },
                    { "ZW_AR", "ZW", "AR", "زيمبابوي", "زيمبابوي" },
                    { "ZW_EN", "ZW", "EN", "Zimbabwe", "Zimbabwean" },
                    { "ZW_FR", "ZW", "FR", "Zimbabwe", "Zimbabwéen" }
                });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "CountryId", "CreatedDate", "CreatedUserId", "CreatedUserName", "Description", "FullNumber", "IsValid", "LastUpdatedDate", "LastUpdatedUserId", "LastUpdatedUserName", "Number", "PersonBaseId", "PhoneTypeId" },
                values: new object[] { 1, "TN", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "system", null, "+21658890780", true, null, null, null, "58890780", 1, "Mob" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId1",
                table: "Addresses",
                column: "CountryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonBaseId",
                table: "Addresses",
                column: "PersonBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CurrencyId",
                table: "Countries",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Id",
                table: "Countries",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Id3Digit",
                table: "Countries",
                column: "Id3Digit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryNames_CountryId_LanguageId",
                table: "CountryNames",
                columns: new[] { "CountryId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryNames_LanguageId",
                table: "CountryNames",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Num",
                table: "Currencies",
                column: "Num",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyNames_CurrencyId_LanguageId",
                table: "CurrencyNames",
                columns: new[] { "CurrencyId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyNames_LanguageId",
                table: "CurrencyNames",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DataFieldPermissionByRoleDetail_DataFieldPermissionByRoleId_RecordId",
                table: "DataFieldPermissionByRoleDetail",
                columns: new[] { "DataFieldPermissionByRoleId", "RecordId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataFieldPermissionByRoles_DataFieldGeneralPermissionId_RoleId",
                table: "DataFieldPermissionByRoles",
                columns: new[] { "DataFieldGeneralPermissionId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataFieldPermissionByRoles_RoleId",
                table: "DataFieldPermissionByRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DataFieldPermissionByUserDetails_DataFieldPermissionByUserId_RecordId",
                table: "DataFieldPermissionByUserDetails",
                columns: new[] { "DataFieldPermissionByUserId", "RecordId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataFieldPermissionByUsers_DataFieldGeneralPermissionId_UserId",
                table: "DataFieldPermissionByUsers",
                columns: new[] { "DataFieldGeneralPermissionId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataFieldPermissionByUsers_UserId",
                table: "DataFieldPermissionByUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DataFields_DataObjectId",
                table: "DataFields",
                column: "DataObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DataObjectPermissionByRoleDetail_DataObjectPermissionByRoleId_RecordId",
                table: "DataObjectPermissionByRoleDetail",
                columns: new[] { "DataObjectPermissionByRoleId", "RecordId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataObjectPermissionByRoles_DataObjectGeneralPermissionId_RoleId",
                table: "DataObjectPermissionByRoles",
                columns: new[] { "DataObjectGeneralPermissionId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataObjectPermissionByRoles_RoleId",
                table: "DataObjectPermissionByRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DataObjectPermissionByUserDetails_DataObjectPermissionByUserId_RecordId",
                table: "DataObjectPermissionByUserDetails",
                columns: new[] { "DataObjectPermissionByUserId", "RecordId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataObjectPermissionByUsers_DataObjectGeneralPermissionId_UserId",
                table: "DataObjectPermissionByUsers",
                columns: new[] { "DataObjectGeneralPermissionId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataObjectPermissionByUsers_UserId",
                table: "DataObjectPermissionByUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalFiles_IBANId",
                table: "DigitalFiles",
                column: "IBANId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalFiles_PersonBaseId",
                table: "DigitalFiles",
                column: "PersonBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalFiles_PersonIdentityId",
                table: "DigitalFiles",
                column: "PersonIdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalFileTraces_DigitalFileId",
                table: "DigitalFileTraces",
                column: "DigitalFileId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalFileTraces_UserId",
                table: "DigitalFileTraces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EMails_PersonBaseId",
                table: "EMails",
                column: "PersonBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_GenderNames_GenderId_LanguageId",
                table: "GenderNames",
                columns: new[] { "GenderId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenderNames_LanguageId",
                table: "GenderNames",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_IBANs_CountryId1",
                table: "IBANs",
                column: "CountryId1");

            migrationBuilder.CreateIndex(
                name: "IX_IBANs_PersonBaseId",
                table: "IBANs",
                column: "PersonBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBase_BirthCountryId1",
                table: "PersonBase",
                column: "BirthCountryId1");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBase_GenderId",
                table: "PersonBase",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBase_NationalityCountryId",
                table: "PersonBase",
                column: "NationalityCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentities_FullNumber",
                table: "PersonIdentities",
                column: "FullNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentities_PersonId",
                table: "PersonIdentities",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentities_PersonIdentityTypeId",
                table: "PersonIdentities",
                column: "PersonIdentityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentityTypeNames_PersonIdentityTypeId",
                table: "PersonIdentityTypeNames",
                column: "PersonIdentityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CountryId",
                table: "Phones",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_FullNumber",
                table: "Phones",
                column: "FullNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PersonBaseId",
                table: "Phones",
                column: "PersonBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PhoneTypeId",
                table: "Phones",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneTypeNames_LanguageId",
                table: "PhoneTypeNames",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneTypeNames_PhoneTypeId",
                table: "PhoneTypeNames",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Level",
                table: "Roles",
                column: "Level",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ParentRoleId",
                table: "Roles",
                column: "ParentRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUiResourcePermissions_RoleId",
                table: "RoleUiResourcePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUiResourcePermissions_UiResourceId",
                table: "RoleUiResourcePermissions",
                column: "UiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Traces_TableName_TableId",
                table: "Traces",
                columns: new[] { "TableName", "TableId" });

            migrationBuilder.CreateIndex(
                name: "IX_UiResourceNames_LanguageId",
                table: "UiResourceNames",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UiResourceNames_UiResourceId_LanguageId",
                table: "UiResourceNames",
                columns: new[] { "UiResourceId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UiResources_Key",
                table: "UiResources",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UiResources_ParentUiResourceId",
                table: "UiResources",
                column: "ParentUiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_UiResources_UiResourceTypeId",
                table: "UiResources",
                column: "UiResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UiResourceTypeNames_LanguageId",
                table: "UiResourceTypeNames",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UiResourceTypeNames_UiResourceId",
                table: "UiResourceTypeNames",
                column: "UiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_UiResourceTypeNames_UiResourceTypeId",
                table: "UiResourceTypeNames",
                column: "UiResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserUiResourcePermissions_UiResourceId",
                table: "UserUiResourcePermissions",
                column: "UiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUiResourcePermissions_UserId",
                table: "UserUiResourcePermissions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CountryNames");

            migrationBuilder.DropTable(
                name: "CurrencyNames");

            migrationBuilder.DropTable(
                name: "DataFieldPermissionByRoleDetail");

            migrationBuilder.DropTable(
                name: "DataFieldPermissionByUserDetails");

            migrationBuilder.DropTable(
                name: "DataFields");

            migrationBuilder.DropTable(
                name: "DataObjectPermissionByRoleDetail");

            migrationBuilder.DropTable(
                name: "DataObjectPermissionByUserDetails");

            migrationBuilder.DropTable(
                name: "DigitalFiles");

            migrationBuilder.DropTable(
                name: "DigitalFileTraces");

            migrationBuilder.DropTable(
                name: "EMails");

            migrationBuilder.DropTable(
                name: "GenderNames");

            migrationBuilder.DropTable(
                name: "PersonIdentityTypeNames");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "PhoneTypeNames");

            migrationBuilder.DropTable(
                name: "RoleUiResourcePermissions");

            migrationBuilder.DropTable(
                name: "Traces");

            migrationBuilder.DropTable(
                name: "UiResourceNames");

            migrationBuilder.DropTable(
                name: "UiResourceTypeNames");

            migrationBuilder.DropTable(
                name: "UserUiResourcePermissions");

            migrationBuilder.DropTable(
                name: "DataFieldPermissionByRoles");

            migrationBuilder.DropTable(
                name: "DataFieldPermissionByUsers");

            migrationBuilder.DropTable(
                name: "DataObjects");

            migrationBuilder.DropTable(
                name: "DataObjectPermissionByRoles");

            migrationBuilder.DropTable(
                name: "DataObjectPermissionByUsers");

            migrationBuilder.DropTable(
                name: "IBANs");

            migrationBuilder.DropTable(
                name: "PersonIdentities");

            migrationBuilder.DropTable(
                name: "PhoneTypes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "UiResources");

            migrationBuilder.DropTable(
                name: "DataFieldGeneralPermissions");

            migrationBuilder.DropTable(
                name: "DataObjectGeneralPermissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PersonIdentityTypes");

            migrationBuilder.DropTable(
                name: "UiResourceTypes");

            migrationBuilder.DropTable(
                name: "PersonBase");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
