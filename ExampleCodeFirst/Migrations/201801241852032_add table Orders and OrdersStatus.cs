namespace ExampleCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableOrdersandOrdersStatus : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblOrder", "UserProfileId", "dbo.tblUserProfiles");
            DropForeignKey("dbo.tblOrder", "OrderStatusId", "dbo.tblOrderStatuses");
            DropIndex("dbo.tblOrder", new[] { "OrderStatusId" });
            DropIndex("dbo.tblOrder", new[] { "UserProfileId" });
            DropTable("dbo.tblOrderStatuses");
            DropTable("dbo.tblOrder");
        }
    }
}
