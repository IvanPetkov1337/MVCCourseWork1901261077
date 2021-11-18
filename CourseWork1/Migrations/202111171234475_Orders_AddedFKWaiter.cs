namespace CourseWork1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders_AddedFKWaiter : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Waiter_Id", "dbo.Waiters");
            DropIndex("dbo.Orders", new[] { "Waiter_Id" });
            RenameColumn(table: "dbo.Orders", name: "Waiter_Id", newName: "WaiterId");
            AlterColumn("dbo.Orders", "WaiterId", c => c.Int(nullable: true));
            CreateIndex("dbo.Orders", "WaiterId");
            AddForeignKey("dbo.Orders", "WaiterId", "dbo.Waiters", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "WaiterId", "dbo.Waiters");
            DropIndex("dbo.Orders", new[] { "WaiterId" });
            AlterColumn("dbo.Orders", "WaiterId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "WaiterId", newName: "Waiter_Id");
            CreateIndex("dbo.Orders", "Waiter_Id");
            AddForeignKey("dbo.Orders", "Waiter_Id", "dbo.Waiters", "Id");
        }
    }
}
