namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                        Phone = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ClassifiedAds", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "ContactInfo_Id", c => c.Int());
            AlterColumn("dbo.ClassifiedAds", "Expires", c => c.DateTime(nullable: false));
            CreateIndex("dbo.AspNetUsers", "ContactInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "ContactInfo_Id", "dbo.ContactInfoes", "Id");
            DropColumn("dbo.AspNetUsers", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String(maxLength: 10, fixedLength: true, unicode: false));
            DropForeignKey("dbo.AspNetUsers", "ContactInfo_Id", "dbo.ContactInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "ContactInfo_Id" });
            AlterColumn("dbo.ClassifiedAds", "Expires", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "ContactInfo_Id");
            DropColumn("dbo.ClassifiedAds", "Created");
            DropTable("dbo.ContactInfoes");
        }
    }
}
