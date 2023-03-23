namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BillAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "AppointmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Bills", "AppointmentID");
            AddForeignKey("dbo.Bills", "AppointmentID", "dbo.Appointments", "AppointmentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "AppointmentID", "dbo.Appointments");
            DropIndex("dbo.Bills", new[] { "AppointmentID" });
            DropColumn("dbo.Bills", "AppointmentID");
        }
    }
}
