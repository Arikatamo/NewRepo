namespace ExampleCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCategories", t => t.ParentID)
                .Index(t => t.ParentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblCategories", "ParentID", "dbo.tblCategories");
            DropIndex("dbo.tblCategories", new[] { "ParentID" });
            DropTable("dbo.tblCategories");
        }
    }
}
