namespace CourseWork1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: true, identity: true),
                        ItemName = c.String(nullable: false, maxLength: 32),
                        IsVegan = c.Boolean(nullable: false),
                        Price = c.Double(nullable: true),
                        Calories = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: true, identity: true),
                        FirstNameOfClient = c.String(nullable: false, maxLength: 25),
                        LastNameOfClient = c.String(maxLength: 25),
                        Table = c.Int(nullable: false),
                        TimeOfOrder = c.DateTime(nullable: false),
                        Notes = c.String(maxLength: 128),
                        Tip = c.Double(nullable: false),
                        Rating = c.Double(nullable: false),
                        Menu_Id = c.Int(),
                        Waiter_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.Menu_Id)
                .ForeignKey("dbo.Waiters", t => t.Waiter_Id)
                .Index(t => t.Menu_Id)
                .Index(t => t.Waiter_Id);
            
            CreateTable(
                "dbo.Waiters",
                c => new
                    {
                        Id = c.Int(nullable: true, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(maxLength: 25),
                        IsVaccinated = c.Boolean(nullable: false),
                        Experience = c.Int(nullable: false),
                        Salary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Waiter_Id", "dbo.Waiters");
            DropForeignKey("dbo.Orders", "Menu_Id", "dbo.Menus");
            DropIndex("dbo.Orders", new[] { "Waiter_Id" });
            DropIndex("dbo.Orders", new[] { "Menu_Id" });
            DropTable("dbo.Waiters");
            DropTable("dbo.Orders");
            DropTable("dbo.Menus");
        }
    }
}
