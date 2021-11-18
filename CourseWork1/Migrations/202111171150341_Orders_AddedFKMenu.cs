namespace CourseWork1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders_AddedFKMenu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Menu_Id", "dbo.Menusm");
            DropIndex("dbo.Orders", new[] { "Menu_Id" });
            RenameColumn(table: "dbo.Orders", name: "Menu_Id", newName: "MenuId");
            AlterColumn("dbo.Orders", "MenuId", c => c.Int(nullable: true));
            CreateIndex("dbo.Orders", "MenuId");
            AddForeignKey("dbo.Orders", "MenuId", "dbo.Menus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "MenuId", "dbo.Menus");
            DropIndex("dbo.Orders", new[] { "MenuId" });
            AlterColumn("dbo.Orders", "MenuId", c => c.Int(nullable: true));
            RenameColumn(table: "dbo.Orders", name: "MenuId", newName: "Menu_Id");
            CreateIndex("dbo.Orders", "Menu_Id");
            AddForeignKey("dbo.Orders", "Menu_Id", "dbo.Menus", "Id");
        }
    }
}
