namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "DoctorID", "dbo.Appointments");
            DropForeignKey("dbo.Appointments", "PatientID", "dbo.Appointments");
            DropForeignKey("dbo.Appointments", "RoomID", "dbo.Appointments");
            AddForeignKey("dbo.Appointments", "DoctorID", "dbo.Doctors", "DoctorID", cascadeDelete: true);
            AddForeignKey("dbo.Appointments", "PatientID", "dbo.Patients", "PatientID", cascadeDelete: true);
            AddForeignKey("dbo.Appointments", "RoomID", "dbo.Rooms", "RoomID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.Appointments", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "DoctorID", "dbo.Doctors");
            AddForeignKey("dbo.Appointments", "RoomID", "dbo.Appointments", "AppointmentID");
            AddForeignKey("dbo.Appointments", "PatientID", "dbo.Appointments", "AppointmentID");
            AddForeignKey("dbo.Appointments", "DoctorID", "dbo.Appointments", "AppointmentID");
        }
    }
}
