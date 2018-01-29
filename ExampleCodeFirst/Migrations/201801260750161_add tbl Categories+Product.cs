namespace ExampleCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblCategoriesProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProducts", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblProducts", "CategoryId");
            AddForeignKey("dbo.tblProducts", "CategoryId", "dbo.tblCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProducts", "CategoryId", "dbo.tblCategories");
            DropIndex("dbo.tblProducts", new[] { "CategoryId" });
            DropColumn("dbo.tblProducts", "CategoryId");
        }
    }
}
