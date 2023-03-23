namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentsroomspatientsdoctors : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Patients", "AppointmentID", "dbo.Appointments");
            //DropIndex("dbo.Patients", new[] { "AppointmentID" });
            AddColumn("dbo.Appointments", "PatientID", c => c.Int(nullable: false));
            AddColumn("dbo.Appointments", "DoctorID", c => c.Int(nullable: false));
            AddColumn("dbo.Appointments", "RoomID", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "PatientID");
            CreateIndex("dbo.Appointments", "DoctorID");
            CreateIndex("dbo.Appointments", "RoomID");
            AddForeignKey("dbo.Appointments", "DoctorID", "dbo.Appointments", "AppointmentID");
            AddForeignKey("dbo.Appointments", "PatientID", "dbo.Appointments", "AppointmentID");
            AddForeignKey("dbo.Appointments", "RoomID", "dbo.Appointments", "AppointmentID");
            //DropColumn("dbo.Patients", "AppointmentID");
        }
        
        public override void Down()
        {
           // AddColumn("dbo.Patients", "AppointmentID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Appointments", "RoomID", "dbo.Appointments");
            DropForeignKey("dbo.Appointments", "PatientID", "dbo.Appointments");
            DropForeignKey("dbo.Appointments", "DoctorID", "dbo.Appointments");
            DropIndex("dbo.Appointments", new[] { "RoomID" });
            DropIndex("dbo.Appointments", new[] { "DoctorID" });
            DropIndex("dbo.Appointments", new[] { "PatientID" });
            DropColumn("dbo.Appointments", "RoomID");
            DropColumn("dbo.Appointments", "DoctorID");
            DropColumn("dbo.Appointments", "PatientID");
           // CreateIndex("dbo.Patients", "AppointmentID");
            //AddForeignKey("dbo.Patients", "AppointmentID", "dbo.Appointments", "AppointmentID", cascadeDelete: true);
        }
    }
}
