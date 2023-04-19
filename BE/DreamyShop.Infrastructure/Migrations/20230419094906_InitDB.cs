using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamyShop.EntityFrameworkCore.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsVisibility = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsUnique = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverPicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVisibility = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CoverPicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVisibility = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SeoMetaDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequireUseCouponCode = table.Column<bool>(type: "bit", nullable: false),
                    ValidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscountAmount = table.Column<double>(type: "float", nullable: false),
                    LimitedUsageTimes = table.Column<bool>(type: "bit", nullable: false),
                    MaximumDiscountAmount = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    GenderType = table.Column<bool>(type: "bit", nullable: true),
                    Dob = table.Column<DateTime>(type: "date", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdentityID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StoredSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApproverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTickets_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManufacturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeoMetaDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailPicture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionCategories_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionManufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManufactureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionManufacturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionManufacturers_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionProducts_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleType = table.Column<byte>(type: "tinyint", nullable: false),
                    ProfileUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTicketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventionTicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTicketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTicketItems_InventoryTickets_InventionTicketId",
                        column: x => x.InventionTicketId,
                        principalTable: "InventoryTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => new { x.ProductId, x.AttributeId });
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => new { x.ProductId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVisibility = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailPicture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantValues",
                columns: table => new
                {
                    ProductVariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductAttributeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantValues", x => new { x.ProductVariantId, x.ProductId, x.AttributeId, x.ProductAttributeValueId });
                    table.ForeignKey(
                        name: "FK_ProductVariantValues_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariantValues_ProductAttributeValues_ProductAttributeValueId",
                        column: x => x.ProductAttributeValueId,
                        principalTable: "ProductAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductVariantValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariantValues_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "Code", "DateCreated", "DateUpdated", "IsActive", "IsUnique", "IsVisibility", "Name", "Note", "SortOrder", "StatusID" },
                values: new object[,]
                {
                    { new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), "COLOR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, true, "COLOR", "Product color", 1, (byte)0 },
                    { new Guid("827ca5b7-0087-4256-bec0-399199a518d9"), "MATERIAL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, true, "MATERIAL", "", 3, (byte)0 },
                    { new Guid("f9885dfb-02a8-4065-a4aa-18b29e48ee89"), "SIZE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, true, "SIZE", "", 2, (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Code", "Country", "CoverPicture", "IsActive", "IsVisibility", "Name", "Slug" },
                values: new object[,]
                {
                    { new Guid("41c2c299-ea5f-4c23-992d-e6f043f1b26f"), "GCCI", "Italy", "", true, true, "Gucci", "gucci" },
                    { new Guid("57a5f678-43f0-4648-92d8-16bd09d7143e"), "SN", "Japan", "", true, true, "Sony", "sony" },
                    { new Guid("69d0372b-dbf5-4b70-9beb-0e4ea77f243a"), "asus", "Taiwan", "", true, true, "ASUS", "asus" },
                    { new Guid("80cad838-29c7-4a02-81c0-9ebe78a0a273"), "IPAPL", "US", "", true, true, "Apple", "apple" },
                    { new Guid("b9be517b-72aa-46f1-9a98-a0b993cd2cf7"), "dell", "Texas-USA", "", true, true, "DELL", "dell" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Code", "CoverPicture", "DateCreated", "DateUpdated", "IsActive", "IsVisibility", "Name", "ParentId", "SeoMetaDescription", "Slug", "SortOrder", "StatusID" },
                values: new object[,]
                {
                    { new Guid("2ed8e62d-2f2e-4957-ae81-8a07b0bcd443"), "LP", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Laptop", null, "", "laptop", 3, (byte)0 },
                    { new Guid("7375fab5-4ff3-43d0-a707-a56062e161be"), "JLY", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Jewelry", null, "", "jly", 4, (byte)0 },
                    { new Guid("96bff1b2-3715-4f10-90d3-aaabb332e0e9"), "CMR", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Camera", null, "", "camera", 1, (byte)0 },
                    { new Guid("efd560a8-c65b-439c-af43-765da733f3c1"), "IP", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Iphone", null, "", "iphone", 2, (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Code", "DateCreated", "DateUpdated", "Description", "ManufacturerId", "Name", "ProductType", "SeoMetaDescription", "Slug", "SortOrder", "StatusID", "ThumbnailPicture" },
                values: new object[,]
                {
                    { new Guid("1747cdf9-3acb-4001-8f52-ee7f387f8efb"), new Guid("96bff1b2-3715-4f10-90d3-aaabb332e0e9"), "CMRSKS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Security camera, surveillance camera, wireless camera, wifi camera, high resolution, motion sensor, remote monitoring", new Guid("57a5f678-43f0-4648-92d8-16bd09d7143e"), "Camera-SKS", 1, "Security camera, surveillance camera, wireless camera, wifi camera, high resolution, motion sensor, remote monitoring", "camera-sks", 1, (byte)0, "" },
                    { new Guid("215e9dee-1d6c-40f4-9233-bb810509adaa"), new Guid("2ed8e62d-2f2e-4957-ae81-8a07b0bcd443"), "DELLDEMON", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Buy genuine Laptop at our store with best quality and affordable price. We supply laptop products from reputable brands. Order now to get a free laptop backpack!", new Guid("b9be517b-72aa-46f1-9a98-a0b993cd2cf7"), "Laptop DELL DEMON", 2, "", "dell-demon", 4, (byte)0, "" },
                    { new Guid("30299235-6937-41b7-a76d-14584f5f856a"), new Guid("efd560a8-c65b-439c-af43-765da733f3c1"), "IP14XSM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("80cad838-29c7-4a02-81c0-9ebe78a0a273"), "Iphone 14 XSMax", 2, "Find out about Apple's latest line of iPhones at Apple Store Vietnam. Order online and get instant deals.", "ip14-xsmax", 3, (byte)0, "" },
                    { new Guid("85f8b0c3-cb8d-4ccb-9544-19daad6ef352"), new Guid("96bff1b2-3715-4f10-90d3-aaabb332e0e9"), "CMRUFG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "XYZ wireless security camera with high resolution.", new Guid("57a5f678-43f0-4648-92d8-16bd09d7143e"), "Camera-UFG", 1, "XYZ wireless security camera with high resolution, built-in motion sensor, supports wifi connection, helps you observe your family, home, shop, office whenever and wherever.", "camera-ufg", 2, (byte)0, "" },
                    { new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new Guid("7375fab5-4ff3-43d0-a707-a56062e161be"), "CLBGCCI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("41c2c299-ea5f-4c23-992d-e6f043f1b26f"), "Crocodile leather bag", 2, "", "clbcci", 4, (byte)0, "" }
                });

            migrationBuilder.InsertData(
                table: "ProductAttributeValues",
                columns: new[] { "Id", "AttributeId", "DateCreated", "DateUpdated", "ProductId", "Value" },
                values: new object[,]
                {
                    { new Guid("2c2fcf7c-fa5a-4b6f-ac30-48a2a6bdb4b5"), new Guid("827ca5b7-0087-4256-bec0-399199a518d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), "Cotton" },
                    { new Guid("3d369a2c-6bcb-468c-b296-64d96a84258a"), new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), "Blue" },
                    { new Guid("3d44b2ac-0bd6-433b-9858-abb84d74ea2e"), new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), "Red" },
                    { new Guid("4267feee-9a5b-4156-90b0-9e3585a8af22"), new Guid("827ca5b7-0087-4256-bec0-399199a518d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), "Crocodile skin" },
                    { new Guid("59327bba-3a1d-40ad-82c2-99a019e9d3f6"), new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("30299235-6937-41b7-a76d-14584f5f856a"), "White" },
                    { new Guid("64e5d43e-a34c-4eab-8672-f7162dd63803"), new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("30299235-6937-41b7-a76d-14584f5f856a"), "Black" },
                    { new Guid("6bb0a02b-d294-496a-b496-994d3dfaa6f2"), new Guid("f9885dfb-02a8-4065-a4aa-18b29e48ee89"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), "L" },
                    { new Guid("a2ee0ac5-663d-4d97-b449-ed8fe48fada3"), new Guid("827ca5b7-0087-4256-bec0-399199a518d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), "Leather" },
                    { new Guid("dfdbe11d-c978-4cc4-9a9e-cafc19805ac8"), new Guid("f9885dfb-02a8-4065-a4aa-18b29e48ee89"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), "M" }
                });

            migrationBuilder.InsertData(
                table: "ProductAttributes",
                columns: new[] { "AttributeId", "ProductId", "DateCreated", "DateUpdated", "StatusID" },
                values: new object[,]
                {
                    { new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("827ca5b7-0087-4256-bec0-399199a518d9"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("f9885dfb-02a8-4065-a4aa-18b29e48ee89"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Description", "IsActive", "IsVisibility", "Price", "ProductId", "Quantity", "SKU", "StatusID", "ThumbnailPicture" },
                values: new object[,]
                {
                    { new Guid("0a134c80-0493-458a-9f02-16361f0df5c7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", true, true, 1250.0, new Guid("30299235-6937-41b7-a76d-14584f5f856a"), 20, "IP14-B", (byte)0, "" },
                    { new Guid("35b39a36-5970-42aa-b996-55170555f85b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", true, true, 4500.0, new Guid("e914fd7b-9af8-403e-9f32-803346659264"), 15, "BAGGUCCI-B", (byte)0, "" },
                    { new Guid("afd19304-3a45-4304-b2bb-1040f000c369"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", true, true, 5000.0, new Guid("e914fd7b-9af8-403e-9f32-803346659264"), 10, "BAGGUCCI-A", (byte)0, "" },
                    { new Guid("f2ba4ab5-46c4-4ce9-baaf-2c98972d45b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", true, true, 9500.0, new Guid("e914fd7b-9af8-403e-9f32-803346659264"), 5, "BAGGUCCI-VIP", (byte)0, "" },
                    { new Guid("fc364f29-fcb9-44b7-8854-dfce09824c35"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", true, true, 1200.0, new Guid("30299235-6937-41b7-a76d-14584f5f856a"), 12, "IP14-A", (byte)0, "" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariantValues",
                columns: new[] { "AttributeId", "ProductAttributeValueId", "ProductId", "ProductVariantId", "DateCreated", "DateUpdated", "StatusID" },
                values: new object[,]
                {
                    { new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new Guid("64e5d43e-a34c-4eab-8672-f7162dd63803"), new Guid("30299235-6937-41b7-a76d-14584f5f856a"), new Guid("0a134c80-0493-458a-9f02-16361f0df5c7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new Guid("3d369a2c-6bcb-468c-b296-64d96a84258a"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new Guid("35b39a36-5970-42aa-b996-55170555f85b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("827ca5b7-0087-4256-bec0-399199a518d9"), new Guid("2c2fcf7c-fa5a-4b6f-ac30-48a2a6bdb4b5"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new Guid("35b39a36-5970-42aa-b996-55170555f85b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("f9885dfb-02a8-4065-a4aa-18b29e48ee89"), new Guid("6bb0a02b-d294-496a-b496-994d3dfaa6f2"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new Guid("35b39a36-5970-42aa-b996-55170555f85b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new Guid("3d44b2ac-0bd6-433b-9858-abb84d74ea2e"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new Guid("afd19304-3a45-4304-b2bb-1040f000c369"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("827ca5b7-0087-4256-bec0-399199a518d9"), new Guid("a2ee0ac5-663d-4d97-b449-ed8fe48fada3"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new Guid("afd19304-3a45-4304-b2bb-1040f000c369"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("f9885dfb-02a8-4065-a4aa-18b29e48ee89"), new Guid("dfdbe11d-c978-4cc4-9a9e-cafc19805ac8"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new Guid("afd19304-3a45-4304-b2bb-1040f000c369"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new Guid("3d369a2c-6bcb-468c-b296-64d96a84258a"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new Guid("f2ba4ab5-46c4-4ce9-baaf-2c98972d45b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("827ca5b7-0087-4256-bec0-399199a518d9"), new Guid("4267feee-9a5b-4156-90b0-9e3585a8af22"), new Guid("e914fd7b-9af8-403e-9f32-803346659264"), new Guid("f2ba4ab5-46c4-4ce9-baaf-2c98972d45b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 },
                    { new Guid("03b9545d-09bd-4b83-808d-de2208e9d26a"), new Guid("59327bba-3a1d-40ad-82c2-99a019e9d3f6"), new Guid("30299235-6937-41b7-a76d-14584f5f856a"), new Guid("fc364f29-fcb9-44b7-8854-dfce09824c35"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTicketItems_InventionTicketId",
                table: "InventoryTicketItems",
                column: "InventionTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTickets_InventoryId",
                table: "InventoryTickets",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeId",
                table: "ProductAttributes",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_AttributeId",
                table: "ProductAttributeValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId",
                table: "ProductAttributeValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagId",
                table: "ProductTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantValues_AttributeId",
                table: "ProductVariantValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantValues_ProductAttributeValueId",
                table: "ProductVariantValues",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantValues_ProductId",
                table: "ProductVariantValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCategories_PromotionId",
                table: "PromotionCategories",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionManufacturers_PromotionId",
                table: "PromotionManufacturers",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionProducts_PromotionId",
                table: "PromotionProducts",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserID",
                table: "Roles",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryTicketItems");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "ProductVariantValues");

            migrationBuilder.DropTable(
                name: "PromotionCategories");

            migrationBuilder.DropTable(
                name: "PromotionManufacturers");

            migrationBuilder.DropTable(
                name: "PromotionProducts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "InventoryTickets");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
