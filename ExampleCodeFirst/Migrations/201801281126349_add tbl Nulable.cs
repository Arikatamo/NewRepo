namespace ExampleCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblNulable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tblCategories", new[] { "ParentID" });
            AddColumn("dbo.tblCategories", "IsHead", c => c.Boolean(nullable: false));
            AlterColumn("dbo.tblCategories", "ParentID", c => c.Int());
            CreateIndex("dbo.tblCategories", "ParentID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tblCategories", new[] { "ParentID" });
            AlterColumn("dbo.tblCategories", "ParentID", c => c.Int(nullable: false));
            DropColumn("dbo.tblCategories", "IsHead");
            CreateIndex("dbo.tblCategories", "ParentID");
        }
    }
}
