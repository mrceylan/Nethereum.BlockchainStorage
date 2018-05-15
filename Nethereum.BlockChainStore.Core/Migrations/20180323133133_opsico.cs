using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OpsICO.Core.Migrations
{
    public partial class opsico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    CampaignID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AirdropAmount = table.Column<double>(nullable: false),
                    ApprovalStatus = table.Column<int>(nullable: false),
                    CampaignStatus = table.Column<int>(nullable: false),
                    CardImage = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: true),
                    Currency = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    HardCap = table.Column<double>(nullable: false),
                    MaxAmount = table.Column<double>(nullable: false),
                    MinAmount = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RaisedAmount = table.Column<double>(nullable: false),
                    RecordState = table.Column<int>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: true),
                    SoftCap = table.Column<double>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    TokenName = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.CampaignID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSetting",
                columns: table => new
                {
                    NotificationSettingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Finance = table.Column<bool>(nullable: false),
                    FollowedCampaigns = table.Column<bool>(nullable: false),
                    InviteLinks = table.Column<bool>(nullable: false),
                    Messages = table.Column<bool>(nullable: false),
                    NewCampaigns = table.Column<bool>(nullable: false),
                    News = table.Column<bool>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSetting", x => x.NotificationSettingID);
                    table.ForeignKey(
                        name: "FK_NotificationSetting_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                columns: table => new
                {
                    ReferenceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CampaignID = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => x.ReferenceID);
                    table.ForeignKey(
                        name: "FK_Reference_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDetail",
                columns: table => new
                {
                    UserDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetail", x => x.UserDetailID);
                    table.ForeignKey(
                        name: "FK_UserDetail_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserWallet",
                columns: table => new
                {
                    UserWalletID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWallet", x => x.UserWalletID);
                    table.ForeignKey(
                        name: "FK_UserWallet_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    VerificationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovalStatus = table.Column<int>(nullable: false),
                    DocumentType = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verification", x => x.VerificationID);
                    table.ForeignKey(
                        name: "FK_Verification_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampaignDetail",
                columns: table => new
                {
                    CampaignDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignID = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    LinkType = table.Column<int>(nullable: false),
                    RecordState = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignDetail", x => x.CampaignDetailID);
                    table.ForeignKey(
                        name: "FK_CampaignDetail_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignUpdate",
                columns: table => new
                {
                    CampaignUpdateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignID = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: true),
                    RecordState = table.Column<int>(nullable: false),
                    UpdateDetail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignUpdate", x => x.CampaignUpdateID);
                    table.ForeignKey(
                        name: "FK_CampaignUpdate_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignID = table.Column<int>(nullable: false),
                    CommentState = table.Column<int>(nullable: false),
                    CommentText = table.Column<string>(nullable: true),
                    CommentTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comment_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Follow",
                columns: table => new
                {
                    FollowID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignID = table.Column<int>(nullable: false),
                    FollowTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follow", x => x.FollowID);
                    table.ForeignKey(
                        name: "FK_Follow_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Follow_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Investment",
                columns: table => new
                {
                    InvestmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    CampaignID = table.Column<int>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    TxID = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment", x => x.InvestmentID);
                    table.ForeignKey(
                        name: "FK_Investment_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Investment_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignID = table.Column<int>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    MessageText = table.Column<string>(nullable: true),
                    MessageTime = table.Column<DateTime>(nullable: false),
                    To = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Message_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    PricingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignID = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    MaxAmount = table.Column<double>(nullable: false),
                    MinAmount = table.Column<double>(nullable: false),
                    PricePerCurrency = table.Column<double>(nullable: false),
                    RecordState = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.PricingID);
                    table.ForeignKey(
                        name: "FK_Pricing_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignID = table.Column<int>(nullable: false),
                    RatingValue = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_Rating_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WhiteListMember",
                columns: table => new
                {
                    WhitelistMemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignID = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: true),
                    MaxAmount = table.Column<double>(nullable: false),
                    MinAmount = table.Column<double>(nullable: false),
                    RecordState = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    WhitelistStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiteListMember", x => x.WhitelistMemberID);
                    table.ForeignKey(
                        name: "FK_WhiteListMember_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WhiteListMember_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceDetail",
                columns: table => new
                {
                    ReferenceDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ReferenceID = table.Column<int>(nullable: true),
                    ReferenceStatus = table.Column<int>(nullable: false),
                    TargetUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceDetail", x => x.ReferenceDetailID);
                    table.ForeignKey(
                        name: "FK_ReferenceDetail_Reference_ReferenceID",
                        column: x => x.ReferenceID,
                        principalTable: "Reference",
                        principalColumn: "ReferenceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignDetail_CampaignID",
                table: "CampaignDetail",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignUpdate_CampaignID",
                table: "CampaignUpdate",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CampaignID",
                table: "Comment",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserID",
                table: "Comment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_CampaignID",
                table: "Follow",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_UserID",
                table: "Follow",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Investment_CampaignID",
                table: "Investment",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_Investment_UserID",
                table: "Investment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_CampaignID",
                table: "Message",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSetting_UserID",
                table: "NotificationSetting",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pricing_CampaignID",
                table: "Pricing",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_CampaignID",
                table: "Rating",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserID",
                table: "Rating",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_ApplicationUserId",
                table: "Reference",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceDetail_ReferenceID",
                table: "ReferenceDetail",
                column: "ReferenceID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_UserID",
                table: "UserDetail",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserWallet_UserID",
                table: "UserWallet",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_UserID",
                table: "Verification",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WhiteListMember_CampaignID",
                table: "WhiteListMember",
                column: "CampaignID");

            migrationBuilder.CreateIndex(
                name: "IX_WhiteListMember_UserID",
                table: "WhiteListMember",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CampaignDetail");

            migrationBuilder.DropTable(
                name: "CampaignUpdate");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Follow");

            migrationBuilder.DropTable(
                name: "Investment");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "NotificationSetting");

            migrationBuilder.DropTable(
                name: "Pricing");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "ReferenceDetail");

            migrationBuilder.DropTable(
                name: "UserDetail");

            migrationBuilder.DropTable(
                name: "UserWallet");

            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropTable(
                name: "WhiteListMember");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
