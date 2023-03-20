namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtablesDoctorVolunteer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        DoctorFirstName = c.String(),
                        DoctorLastName = c.String(),
                        DoctorEmployeeNumber = c.String(),
                    })
                .PrimaryKey(t => t.DoctorID);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        VolunteerID = c.Int(nullable: false, identity: true),
                        VolunteerFirstName = c.String(),
                        VolunteerLastName = c.String(),
                    })
                .PrimaryKey(t => t.VolunteerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Volunteers");
            DropTable("dbo.Doctors");
        }
    }
}
