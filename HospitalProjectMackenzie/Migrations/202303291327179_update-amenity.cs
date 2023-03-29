namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateamenity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Amenities", "AmenityDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Amenities", "AmenityDescription");
        }
    }
}
