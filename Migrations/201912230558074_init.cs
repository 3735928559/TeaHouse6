namespace TeaHouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        OrderTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        OrderNum = c.Int(nullable: false),
                        SelectedFood_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.SelectedFood_Id)
                .Index(t => t.SelectedFood_Id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FoodType = c.String(),
                        Detail = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        OrderTime = c.DateTime(nullable: false),
                        SelectedChoice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Choices", t => t.SelectedChoice_Id)
                .Index(t => t.SelectedChoice_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderModels", "SelectedChoice_Id", "dbo.Choices");
            DropForeignKey("dbo.Choices", "SelectedFood_Id", "dbo.Foods");
            DropIndex("dbo.OrderModels", new[] { "SelectedChoice_Id" });
            DropIndex("dbo.Choices", new[] { "SelectedFood_Id" });
            DropTable("dbo.OrderModels");
            DropTable("dbo.Foods");
            DropTable("dbo.Choices");
        }
    }
}
