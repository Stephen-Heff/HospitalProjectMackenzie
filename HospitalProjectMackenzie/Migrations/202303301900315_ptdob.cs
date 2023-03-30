namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ptdob : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "PatientDateOfBirth", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "PatientDateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
