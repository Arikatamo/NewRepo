namespace ExampleCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtableFilters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblFilterName",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.tblFilterValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Quantity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblFilters", "ProductId", "dbo.tblProducts");
            DropForeignKey("dbo.tblFilters", "FilterValueId", "dbo.tblFilterValue");
            DropForeignKey("dbo.tblFilters", "FilterNameId", "dbo.tblFilterName");
            DropIndex("dbo.tblFilters", new[] { "ProductId" });
            DropIndex("dbo.tblFilters", new[] { "FilterValueId" });
            DropIndex("dbo.tblFilters", new[] { "FilterNameId" });
            DropTable("dbo.tblProducts");
            DropTable("dbo.tblFilterValue");
            DropTable("dbo.tblFilters");
            DropTable("dbo.tblFilterName");
        }
    }
}
