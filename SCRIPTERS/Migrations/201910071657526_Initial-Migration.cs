namespace SCRIPTERS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionTime = c.DateTime(nullable: false),
                        User = c.String(),
                        TransactionType = c.String(),
                        TransactionDetails = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Code = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                        ItemCategoryId = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookCategories", t => t.ItemCategoryId, cascadeDelete: true)
                .Index(t => t.ItemCategoryId);
            
            CreateTable(
                "dbo.PurchaseDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        OutletId = c.Int(nullable: false),
                        PurchaseNumber = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        Remarks = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Outlets", t => t.OutletId)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.OutletId)
                .Index(t => t.EmployeeId)
                .Index(t => t.SupplierId)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(),
                        OutletId = c.Int(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        Image = c.Binary(nullable: false),
                        ContactNo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ReferenceId = c.Int(),
                        EmerContactNo = c.String(nullable: false),
                        NationalId = c.String(),
                        FathersName = c.String(),
                        MothersName = c.String(),
                        PresentAddress = c.String(),
                        PermanentAddress = c.String(nullable: false),
                        RoleTyeId = c.Int(nullable: false),
                        RoleType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ReferenceId)
                .ForeignKey("dbo.Outlets", t => t.OutletId, cascadeDelete: true)
                .ForeignKey("dbo.RoleTypes", t => t.RoleType_Id)
                .Index(t => t.OutletId)
                .Index(t => t.ReferenceId)
                .Index(t => t.RoleType_Id);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        OutletId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        ExpenseDate = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Due = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Outlets", t => t.OutletId, cascadeDelete: true)
                .Index(t => t.OutletId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ExpenseDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        ExpenseId = c.Int(nullable: false),
                        ExpenseItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(maxLength: 1000),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Expenses", t => t.ExpenseId, cascadeDelete: true)
                .ForeignKey("dbo.ExpenseItems", t => t.ExpenseItemId, cascadeDelete: true)
                .Index(t => t.ExpenseId)
                .Index(t => t.ExpenseItemId);
            
            CreateTable(
                "dbo.ExpenseItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                        Image = c.Binary(),
                        ExpenseCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseCategories", t => t.ExpenseCategoryId, cascadeDelete: true)
                .Index(t => t.ExpenseCategoryId);
            
            CreateTable(
                "dbo.ExpenseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Outlets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        ContactNo = c.String(),
                        Address = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventorySales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        OutletId = c.Int(nullable: false),
                        SaleNumber = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        CusContractNo = c.String(nullable: false),
                        CusName = c.String(nullable: false),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Outlets", t => t.OutletId)
                .Index(t => t.OutletId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.InventorySalesDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        InventoryId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQuantity = c.Int(nullable: false),
                        InventorySaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .ForeignKey("dbo.InventorySales", t => t.InventorySaleId, cascadeDelete: true)
                .Index(t => t.InventoryId)
                .Index(t => t.InventorySaleId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Code = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                        ItemCategoryId = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InventoryCategories", t => t.ItemCategoryId, cascadeDelete: true)
                .Index(t => t.ItemCategoryId);
            
            CreateTable(
                "dbo.InventoryCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inventories", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        OutletId = c.Int(nullable: false),
                        OrderNumber = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        Remarks = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Outlets", t => t.OutletId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.OutletId)
                .Index(t => t.EmployeeId)
                .Index(t => t.SupplierId)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(),
                        ContactNo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Image = c.Binary(),
                        Address = c.String(maxLength: 1000),
                        SupplierType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        OutletId = c.Int(nullable: false),
                        SaleNumber = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        CusContractNo = c.String(nullable: false),
                        CusName = c.String(nullable: false),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DueAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Outlets", t => t.OutletId)
                .Index(t => t.OutletId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.SalesDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQuantity = c.Int(nullable: false),
                        SaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.RoleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CanOrder = c.Boolean(nullable: false),
                        CanPurchase = c.Boolean(nullable: false),
                        CanSale = c.Boolean(nullable: false),
                        CanHr = c.Boolean(nullable: false),
                        CanManageInventory = c.Boolean(nullable: false),
                        CanManageInventoryType = c.Boolean(nullable: false),
                        CanManageBooks = c.Boolean(nullable: false),
                        CanManageBookType = c.Boolean(nullable: false),
                        CanManageReports = c.Boolean(nullable: false),
                        CanManageCustomers = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusinessRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VAT = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(),
                        ContactNo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Image = c.Binary(),
                        Address = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToEmail = c.String(nullable: false),
                        EMailBody = c.String(),
                        EmailSubject = c.String(),
                        EmailCC = c.String(),
                        EmailBCC = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        ThemeColor = c.String(),
                        IsFullDay = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.IncomeVms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalesTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasesTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalIncome = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventoryIncomeVms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalesTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrdersTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalIncome = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SmsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToNo = c.String(nullable: false),
                        SmsBody = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Purchases", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.PurchaseDetails", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "OutletId", "dbo.Outlets");
            DropForeignKey("dbo.Employees", "RoleType_Id", "dbo.RoleTypes");
            DropForeignKey("dbo.Purchases", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.SalesDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.SalesDetails", "BookId", "dbo.Books");
            DropForeignKey("dbo.Sales", "OutletId", "dbo.Outlets");
            DropForeignKey("dbo.Sales", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.InventorySales", "OutletId", "dbo.Outlets");
            DropForeignKey("dbo.InventorySalesDetails", "InventorySaleId", "dbo.InventorySales");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Orders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Orders", "OutletId", "dbo.Outlets");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.OrderDetails", "ItemId", "dbo.Inventories");
            DropForeignKey("dbo.Inventories", "ItemCategoryId", "dbo.InventoryCategories");
            DropForeignKey("dbo.InventorySalesDetails", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.InventorySales", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Expenses", "OutletId", "dbo.Outlets");
            DropForeignKey("dbo.Employees", "OutletId", "dbo.Outlets");
            DropForeignKey("dbo.ExpenseDetails", "ExpenseItemId", "dbo.ExpenseItems");
            DropForeignKey("dbo.ExpenseItems", "ExpenseCategoryId", "dbo.ExpenseCategories");
            DropForeignKey("dbo.ExpenseDetails", "ExpenseId", "dbo.Expenses");
            DropForeignKey("dbo.Expenses", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "ReferenceId", "dbo.Employees");
            DropForeignKey("dbo.PurchaseDetails", "ItemId", "dbo.Books");
            DropForeignKey("dbo.Books", "ItemCategoryId", "dbo.BookCategories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SalesDetails", new[] { "SaleId" });
            DropIndex("dbo.SalesDetails", new[] { "BookId" });
            DropIndex("dbo.Sales", new[] { "EmployeeId" });
            DropIndex("dbo.Sales", new[] { "OutletId" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Orders", new[] { "SupplierId" });
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
            DropIndex("dbo.Orders", new[] { "OutletId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ItemId" });
            DropIndex("dbo.Inventories", new[] { "ItemCategoryId" });
            DropIndex("dbo.InventorySalesDetails", new[] { "InventorySaleId" });
            DropIndex("dbo.InventorySalesDetails", new[] { "InventoryId" });
            DropIndex("dbo.InventorySales", new[] { "EmployeeId" });
            DropIndex("dbo.InventorySales", new[] { "OutletId" });
            DropIndex("dbo.ExpenseItems", new[] { "ExpenseCategoryId" });
            DropIndex("dbo.ExpenseDetails", new[] { "ExpenseItemId" });
            DropIndex("dbo.ExpenseDetails", new[] { "ExpenseId" });
            DropIndex("dbo.Expenses", new[] { "EmployeeId" });
            DropIndex("dbo.Expenses", new[] { "OutletId" });
            DropIndex("dbo.Employees", new[] { "RoleType_Id" });
            DropIndex("dbo.Employees", new[] { "ReferenceId" });
            DropIndex("dbo.Employees", new[] { "OutletId" });
            DropIndex("dbo.Purchases", new[] { "Customer_Id" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.Purchases", new[] { "EmployeeId" });
            DropIndex("dbo.Purchases", new[] { "OutletId" });
            DropIndex("dbo.PurchaseDetails", new[] { "PurchaseId" });
            DropIndex("dbo.PurchaseDetails", new[] { "ItemId" });
            DropIndex("dbo.Books", new[] { "ItemCategoryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SmsModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.InventoryIncomeVms");
            DropTable("dbo.IncomeVms");
            DropTable("dbo.Events");
            DropTable("dbo.EmailModels");
            DropTable("dbo.Customers");
            DropTable("dbo.BusinessRules");
            DropTable("dbo.RoleTypes");
            DropTable("dbo.SalesDetails");
            DropTable("dbo.Sales");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.InventoryCategories");
            DropTable("dbo.Inventories");
            DropTable("dbo.InventorySalesDetails");
            DropTable("dbo.InventorySales");
            DropTable("dbo.Outlets");
            DropTable("dbo.ExpenseCategories");
            DropTable("dbo.ExpenseItems");
            DropTable("dbo.ExpenseDetails");
            DropTable("dbo.Expenses");
            DropTable("dbo.Employees");
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchaseDetails");
            DropTable("dbo.Books");
            DropTable("dbo.BookCategories");
            DropTable("dbo.Audits");
        }
    }
}
