namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSiteAndAmenity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Amenities",
                c => new
                    {
                        AmenityID = c.Int(nullable: false, identity: true),
                        SiteId = c.Int(nullable: false),
                        AmenityName = c.String(),
                        AmenityLocation = c.String(),
                        AmenityType = c.String(),
                    })
                .PrimaryKey(t => t.AmenityID)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        SiteID = c.Int(nullable: false, identity: true),
                        SiteName = c.String(),
                        SiteDescription = c.String(),
                        SiteAddress = c.String(),
                        SiteNumber = c.String(),
                        SiteImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.SiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Amenities", "SiteId", "dbo.Sites");
            DropIndex("dbo.Amenities", new[] { "SiteId" });
            DropTable("dbo.Sites");
            DropTable("dbo.Amenities");
        }
    }
}
