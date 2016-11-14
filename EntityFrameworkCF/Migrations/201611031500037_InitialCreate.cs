namespace EntityFrameworkCF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.UserID);

            CreateTable(
                "dbo.Products",
                c => new
                {
                    ProductID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Price = c.Double(nullable: false),
                    Description = c.String(),
                    Image = c.String(),       
                })
                .PrimaryKey(t => t.ProductID);



        }

        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Products");
        }
    }
}
