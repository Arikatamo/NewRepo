namespace ExampleCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addALL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCart",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserProfileId)
                .ForeignKey("dbo.tblUserProfiles", t => t.UserProfileId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.tblUserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Image = c.String(nullable: false, maxLength: 128),
                        Telephone = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreate = c.DateTime(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblOrderStatuses", t => t.OrderStatusId, cascadeDelete: true)
                .ForeignKey("dbo.tblUserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.UserProfileId)
                .Index(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.tblOrderStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(),
                        Name = c.String(nullable: false, maxLength: 250),
                        IsHead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCategories", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.tblProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Quantity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.tblFilters",
                c => new
                    {
                        FilterNameId = c.Int(nullable: false),
                        FilterValueId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterNameId, t.FilterValueId, t.ProductId })
                .ForeignKey("dbo.tblFilterName", t => t.FilterNameId, cascadeDelete: true)
                .ForeignKey("dbo.tblFilterValue", t => t.FilterValueId, cascadeDelete: true)
                .ForeignKey("dbo.tblProducts", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FilterNameId)
                .Index(t => t.FilterValueId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tblFilterName",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblFilterValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblFilterNameGroups",
                c => new
                    {
                        FilterNameId = c.Int(nullable: false),
                        FilterValueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterNameId, t.FilterValueId })
                .ForeignKey("dbo.tblFilterName", t => t.FilterNameId, cascadeDelete: true)
                .ForeignKey("dbo.tblFilterValue", t => t.FilterValueId, cascadeDelete: true)
                .Index(t => t.FilterNameId)
                .Index(t => t.FilterValueId);
            
           
            
            CreateTable(
                "dbo.RoleUserProfiles",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        UserProfile_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.UserProfile_Id })
                .ForeignKey("dbo.tblRoles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.tblUserProfiles", t => t.UserProfile_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.UserProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblFilterNameGroups", "FilterValueId", "dbo.tblFilterValue");
            DropForeignKey("dbo.tblFilterNameGroups", "FilterNameId", "dbo.tblFilterName");
            DropForeignKey("dbo.tblFilters", "ProductId", "dbo.tblProducts");
            DropForeignKey("dbo.tblFilters", "FilterValueId", "dbo.tblFilterValue");
            DropForeignKey("dbo.tblFilters", "FilterNameId", "dbo.tblFilterName");
            DropForeignKey("dbo.tblProducts", "CategoryId", "dbo.tblCategories");
            DropForeignKey("dbo.tblCategories", "ParentID", "dbo.tblCategories");
            DropForeignKey("dbo.tblCart", "UserProfileId", "dbo.tblUserProfiles");
            DropForeignKey("dbo.RoleUserProfiles", "UserProfile_Id", "dbo.tblUserProfiles");
            DropForeignKey("dbo.RoleUserProfiles", "Role_Id", "dbo.tblRoles");
            DropForeignKey("dbo.tblOrder", "UserProfileId", "dbo.tblUserProfiles");
            DropForeignKey("dbo.tblOrder", "OrderStatusId", "dbo.tblOrderStatuses");
            DropIndex("dbo.RoleUserProfiles", new[] { "UserProfile_Id" });
            DropIndex("dbo.RoleUserProfiles", new[] { "Role_Id" });
            DropIndex("dbo.tblFilterNameGroups", new[] { "FilterValueId" });
            DropIndex("dbo.tblFilterNameGroups", new[] { "FilterNameId" });
            DropIndex("dbo.tblFilters", new[] { "ProductId" });
            DropIndex("dbo.tblFilters", new[] { "FilterValueId" });
            DropIndex("dbo.tblFilters", new[] { "FilterNameId" });
            DropIndex("dbo.tblProducts", new[] { "CategoryId" });
            DropIndex("dbo.tblCategories", new[] { "ParentID" });
            DropIndex("dbo.tblOrder", new[] { "OrderStatusId" });
            DropIndex("dbo.tblOrder", new[] { "UserProfileId" });
            DropIndex("dbo.tblCart", new[] { "UserProfileId" });
            DropTable("dbo.RoleUserProfiles");
            DropTable("dbo.tblFilterNameGroups");
            DropTable("dbo.tblFilterValue");
            DropTable("dbo.tblFilterName");
            DropTable("dbo.tblFilters");
            DropTable("dbo.tblProducts");
            DropTable("dbo.tblCategories");
            DropTable("dbo.tblRoles");
            DropTable("dbo.tblOrderStatuses");
            DropTable("dbo.tblOrder");
            DropTable("dbo.tblUserProfiles");
            DropTable("dbo.tblCart");
        }
    }
}
