using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AribONE.Data.Migrations.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class SyncCurrentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyProductCosts");

            migrationBuilder.DropTable(
                name: "ProductTransactions");

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "TreasuriesTransactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "RevenueExpenses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StockAccountId",
                table: "Products",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SalesCostAccountId",
                table: "Products",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SalesAccountId",
                table: "Products",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "ProductCode",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseAccountId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "InventoryAdjustments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "EWalletTransactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "CustomerTransactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DynamicBalanceModeEnabled",
                table: "Branches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShiftModeEnabled",
                table: "Branches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowCurrentBalanceOnReceipt",
                table: "Branches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            // Bills.ShiftId was a dead pre-GUID int (always 0); Postgres can't cast
            // integer -> uuid automatically. Mirror the SQL Server migration
            // (20260630154459_AddShiftManagement): drop the meaningless int and add
            // the nullable Guid afresh (all existing bills become ShiftId = NULL =
            // "pre-shift era").
            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "Bills");

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "Bills",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankId",
                table: "Bills",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BankMoney",
                table: "Bills",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EndingBalance",
                table: "Bills",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PreviousBalance",
                table: "Bills",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Bills",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WalletMoney",
                table: "Bills",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Barcodes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "BankTransactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    DedupKey = table.Column<string>(type: "text", nullable: false),
                    ReferenceType = table.Column<string>(type: "text", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    GroupKey = table.Column<string>(type: "text", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    AlertSeq = table.Column<int>(type: "integer", nullable: false),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    MembersSignature = table.Column<string>(type: "text", nullable: true),
                    IsResolved = table.Column<bool>(type: "boolean", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillId = table.Column<Guid>(type: "uuid", nullable: false),
                    CashRegNum = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillPayments_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Num = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceBillId = table.Column<Guid>(type: "uuid", nullable: true),
                    Principal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IntervalMonths = table.Column<int>(type: "integer", nullable: false),
                    RoundingMode = table.Column<int>(type: "integer", nullable: false),
                    RoundingStep = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    RemainderTarget = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallmentPlans_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryBatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    BatchNumber = table.Column<string>(type: "text", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    InitialQty = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    RemainingQty = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    SourceRegNum = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryBatches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryBatches_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryBatches_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Dealing = table.Column<int>(type: "integer", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InQty = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    InPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    InTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OutQty = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    OutPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OutTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegNum = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Pc = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiryEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    FinanceEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    SystemEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiryDaysAhead = table.Column<int>(type: "integer", nullable: false),
                    InstallmentDueSoonDays = table.Column<int>(type: "integer", nullable: false),
                    BackupStaleDays = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Num = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    TreasuryId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkstationId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    OpenedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OpenedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OpeningCash = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OpenNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ClosedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClosedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpectedCash = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ActualCash = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Difference = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CloseNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsForceClosed = table.Column<bool>(type: "boolean", nullable: false),
                    ExpectedBank = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ExpectedWallet = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ExpectedCredit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SalesTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    RefundsTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shifts_Users_OpenedByUserId",
                        column: x => x.OpenedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeightedAverageCosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Qty = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightedAverageCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightedAverageCosts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeightedAverageCosts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeightedAverageCosts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationReadStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NotificationId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReadSeq = table.Column<int>(type: "integer", nullable: true),
                    DismissedSeq = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationReadStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationReadStates_AppNotifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "AppNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstallmentPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PaidAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallmentItems_InstallmentPlans_InstallmentPlanId",
                        column: x => x.InstallmentPlanId,
                        principalTable: "InstallmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryBatchConsumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegNum = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Qty = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBatchConsumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryBatchConsumptions_InventoryBatches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "InventoryBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstallmentPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstallmentItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CashRegNum = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallmentPayments_InstallmentItems_InstallmentItemId",
                        column: x => x.InstallmentItemId,
                        principalTable: "InstallmentItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountOperands",
                columns: new[] { "Operand", "AccountId", "LabelAr", "LabelEn" },
                values: new object[,]
                {
                    { "PurchasedServices", new Guid("00000001-0000-7000-a000-000000000156"), "خدمات مشتراة", "Purchased Services" },
                    { "SoldServices", new Guid("00000001-0000-7000-a000-000000000218"), "خدمات مباعة", "Sold Services" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000003-0000-7000-a000-000000000043"), "يمكنه عرض خطط وجداول الأقساط", "عرض الأقساط" },
                    { new Guid("00000003-0000-7000-a000-000000000044"), "يمكنه إنشاء خطة أقساط جديدة للعميل", "إنشاء أقساط" },
                    { new Guid("00000003-0000-7000-a000-000000000045"), "يمكنه تحصيل دفعة على قسط مستحق", "تحصيل الأقساط" },
                    { new Guid("00000003-0000-7000-a000-000000000046"), "يمكنه إلغاء خطة أقساط نشطة", "إلغاء الأقساط" },
                    { new Guid("00000003-0000-7000-a000-000000000047"), "يمكنه إغلاق ورديه كاشير آخر (إغلاق إجباري)", "اغلاق الورديه إجباري" },
                    { new Guid("00000003-0000-7000-a000-000000000048"), "يمكنه عرض ورديات وتقارير الكاشيرين الآخرين", "عرض ورديات الكاشيرين الآخرين" },
                    { new Guid("00000003-0000-7000-a000-000000000049"), "يمكنه تسجيل حركات نقدية من خزنة الفرع مباشرة بدلاً من درج الكاشير", "التعامل مع نقدية الخزنة الرئيسية" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-7000-a000-000000000072"), new Guid("00000003-0000-7000-a000-000000000043"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000073"), new Guid("00000003-0000-7000-a000-000000000044"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000074"), new Guid("00000003-0000-7000-a000-000000000045"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000075"), new Guid("00000003-0000-7000-a000-000000000046"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000076"), new Guid("00000003-0000-7000-a000-000000000047"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000077"), new Guid("00000003-0000-7000-a000-000000000048"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000078"), new Guid("00000003-0000-7000-a000-000000000049"), new Guid("00000002-0000-7000-a000-000000000001") },
                    { new Guid("00000004-0000-7000-a000-000000000079"), new Guid("00000003-0000-7000-a000-000000000049"), new Guid("00000002-0000-7000-a000-000000000002") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreasuriesTransactions_ShiftId",
                table: "TreasuriesTransactions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueExpenses_ShiftId",
                table: "RevenueExpenses",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustments_ShiftId",
                table: "InventoryAdjustments",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_EWalletTransactions_ShiftId",
                table: "EWalletTransactions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTransactions_ShiftId",
                table: "CustomerTransactions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ShiftId",
                table: "Bills",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_Code",
                table: "Barcodes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_ShiftId",
                table: "BankTransactions",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_AppNotifications_BranchId_IsResolved_CreatedAt",
                table: "AppNotifications",
                columns: new[] { "BranchId", "IsResolved", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AppNotifications_DedupKey",
                table: "AppNotifications",
                column: "DedupKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillPayments_BillId",
                table: "BillPayments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillPayments_ShiftId",
                table: "BillPayments",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentItems_InstallmentPlanId",
                table: "InstallmentItems",
                column: "InstallmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentPayments_InstallmentItemId",
                table: "InstallmentPayments",
                column: "InstallmentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentPlans_CustomerId",
                table: "InstallmentPlans",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatchConsumptions_BatchId",
                table: "InventoryBatchConsumptions",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatchConsumptions_RegNum",
                table: "InventoryBatchConsumptions",
                column: "RegNum");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_BranchId",
                table: "InventoryBatches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_ExpiryDate",
                table: "InventoryBatches",
                column: "ExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_ProductId_WarehouseId_RemainingQty",
                table: "InventoryBatches",
                columns: new[] { "ProductId", "WarehouseId", "RemainingQty" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_SourceRegNum",
                table: "InventoryBatches",
                column: "SourceRegNum");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatches_WarehouseId",
                table: "InventoryBatches",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_CustomerId",
                table: "InventoryMovements",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_ProductId",
                table: "InventoryMovements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_WarehouseId",
                table: "InventoryMovements",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationReadStates_NotificationId_UserId",
                table: "NotificationReadStates",
                columns: new[] { "NotificationId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_BranchId",
                table: "NotificationSettings",
                column: "BranchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_BranchId_WorkstationId_Status",
                table: "Shifts",
                columns: new[] { "BranchId", "WorkstationId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_OpenedByUserId",
                table: "Shifts",
                column: "OpenedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightedAverageCosts_BranchId",
                table: "WeightedAverageCosts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightedAverageCosts_ProductId",
                table: "WeightedAverageCosts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightedAverageCosts_WarehouseId",
                table: "WeightedAverageCosts",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPayments");

            migrationBuilder.DropTable(
                name: "InstallmentPayments");

            migrationBuilder.DropTable(
                name: "InventoryBatchConsumptions");

            migrationBuilder.DropTable(
                name: "InventoryMovements");

            migrationBuilder.DropTable(
                name: "NotificationReadStates");

            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "WeightedAverageCosts");

            migrationBuilder.DropTable(
                name: "InstallmentItems");

            migrationBuilder.DropTable(
                name: "InventoryBatches");

            migrationBuilder.DropTable(
                name: "AppNotifications");

            migrationBuilder.DropTable(
                name: "InstallmentPlans");

            migrationBuilder.DropIndex(
                name: "IX_TreasuriesTransactions_ShiftId",
                table: "TreasuriesTransactions");

            migrationBuilder.DropIndex(
                name: "IX_RevenueExpenses_ShiftId",
                table: "RevenueExpenses");

            migrationBuilder.DropIndex(
                name: "IX_InventoryAdjustments_ShiftId",
                table: "InventoryAdjustments");

            migrationBuilder.DropIndex(
                name: "IX_EWalletTransactions_ShiftId",
                table: "EWalletTransactions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTransactions_ShiftId",
                table: "CustomerTransactions");

            migrationBuilder.DropIndex(
                name: "IX_Bills_ShiftId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Barcodes_Code",
                table: "Barcodes");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_ShiftId",
                table: "BankTransactions");

            migrationBuilder.DeleteData(
                table: "AccountOperands",
                keyColumn: "Operand",
                keyValue: "PurchasedServices");

            migrationBuilder.DeleteData(
                table: "AccountOperands",
                keyColumn: "Operand",
                keyValue: "SoldServices");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000072"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000073"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000074"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000075"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000076"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000077"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000078"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-7000-a000-000000000079"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-7000-a000-000000000049"));

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "TreasuriesTransactions");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "RevenueExpenses");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchaseAccountId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "InventoryAdjustments");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "EWalletTransactions");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "CustomerTransactions");

            migrationBuilder.DropColumn(
                name: "DynamicBalanceModeEnabled",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ShiftModeEnabled",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ShowCurrentBalanceOnReceipt",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BankMoney",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "EndingBalance",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PreviousBalance",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "WalletMoney",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "BankTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "StockAccountId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SalesCostAccountId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SalesAccountId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "Bills",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Barcodes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "DailyProductCosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    BatchNumber = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Qty = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProductCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyProductCosts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyProductCosts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyProductCosts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Dealing = table.Column<int>(type: "integer", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    InQty = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    InTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OutPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OutQty = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    OutTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Pc = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    RegNum = table.Column<Guid>(type: "uuid", nullable: false),
                    Unit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTransactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTransactions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTransactions_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyProductCosts_BranchId",
                table: "DailyProductCosts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProductCosts_ProductId",
                table: "DailyProductCosts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyProductCosts_WarehouseId",
                table: "DailyProductCosts",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransactions_CustomerId",
                table: "ProductTransactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransactions_ProductId",
                table: "ProductTransactions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransactions_WarehouseId",
                table: "ProductTransactions",
                column: "WarehouseId");
        }
    }
}
