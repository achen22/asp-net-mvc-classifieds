namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassifieds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassifiedAds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TypeId = c.Byte(nullable: false),
                        Title = c.String(maxLength: 128),
                        Description = c.String(storeType: "ntext"),
                        Expires = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassifiedTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.ClassifiedTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(maxLength: 8, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String(maxLength: 10, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassifiedAds", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassifiedAds", "TypeId", "dbo.ClassifiedTypes");
            DropIndex("dbo.ClassifiedAds", new[] { "TypeId" });
            DropIndex("dbo.ClassifiedAds", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "Phone");
            DropTable("dbo.ClassifiedTypes");
            DropTable("dbo.ClassifiedAds");
        }
    }
}
