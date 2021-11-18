namespace CourseWork1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuID_VariableType_Change : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.Orders", "WaiterId", "dbo.Waiters");
            DropIndex("dbo.Orders", new[] { "MenuId" });
            DropIndex("dbo.Orders", new[] { "WaiterId" });
            AlterColumn("dbo.Orders", "MenuId", c => c.Int());
            AlterColumn("dbo.Orders", "WaiterId", c => c.Int());
            CreateIndex("dbo.Orders", "MenuId");
            CreateIndex("dbo.Orders", "WaiterId");
            AddForeignKey("dbo.Orders", "MenuId", "dbo.Menus", "Id");
            AddForeignKey("dbo.Orders", "WaiterId", "dbo.Waiters", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "WaiterId", "dbo.Waiters");
            DropForeignKey("dbo.Orders", "MenuId", "dbo.Menus");
            DropIndex("dbo.Orders", new[] { "WaiterId" });
            DropIndex("dbo.Orders", new[] { "MenuId" });
            AlterColumn("dbo.Orders", "WaiterId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "MenuId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "WaiterId");
            CreateIndex("dbo.Orders", "MenuId");
            AddForeignKey("dbo.Orders", "WaiterId", "dbo.Waiters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "MenuId", "dbo.Menus", "Id", cascadeDelete: true);
        }
    }
}
