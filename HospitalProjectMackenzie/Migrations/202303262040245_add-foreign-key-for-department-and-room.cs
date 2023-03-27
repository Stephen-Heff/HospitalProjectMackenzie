namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeyfordepartmentandroom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Department_DepartmentID", c => c.Int());
            AddColumn("dbo.Rooms", "DepartmentID", c => c.Int(nullable: false));
            AddColumn("dbo.Departments", "SiteID", c => c.Int(nullable: false));
            AddColumn("dbo.Volunteers", "Department_DepartmentID", c => c.Int());
            CreateIndex("dbo.Doctors", "Department_DepartmentID");
            CreateIndex("dbo.Rooms", "DepartmentID");
            CreateIndex("dbo.Departments", "SiteID");
            CreateIndex("dbo.Volunteers", "Department_DepartmentID");
            AddForeignKey("dbo.Doctors", "Department_DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.Departments", "SiteID", "dbo.Sites", "SiteID", cascadeDelete: true);
            AddForeignKey("dbo.Volunteers", "Department_DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.Rooms", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Volunteers", "Department_DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Doctors", "Department_DepartmentID", "dbo.Departments");
            DropIndex("dbo.Volunteers", new[] { "Department_DepartmentID" });
            DropIndex("dbo.Departments", new[] { "SiteID" });
            DropIndex("dbo.Rooms", new[] { "DepartmentID" });
            DropIndex("dbo.Doctors", new[] { "Department_DepartmentID" });
            DropColumn("dbo.Volunteers", "Department_DepartmentID");
            DropColumn("dbo.Departments", "SiteID");
            DropColumn("dbo.Rooms", "DepartmentID");
            DropColumn("dbo.Doctors", "Department_DepartmentID");
        }
    }
}
