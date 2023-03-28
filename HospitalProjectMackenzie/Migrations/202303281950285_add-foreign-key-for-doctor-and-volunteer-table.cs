namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeyfordoctorandvolunteertable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "Department_DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Volunteers", "Department_DepartmentID", "dbo.Departments");
            DropIndex("dbo.Doctors", new[] { "Department_DepartmentID" });
            DropIndex("dbo.Volunteers", new[] { "Department_DepartmentID" });
            RenameColumn(table: "dbo.Doctors", name: "Department_DepartmentID", newName: "DepartmentID");
            RenameColumn(table: "dbo.Volunteers", name: "Department_DepartmentID", newName: "DepartmentID");
            AlterColumn("dbo.Doctors", "DepartmentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Volunteers", "DepartmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Doctors", "DepartmentID");
            CreateIndex("dbo.Volunteers", "DepartmentID");
            AddForeignKey("dbo.Doctors", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: false);
            AddForeignKey("dbo.Volunteers", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Volunteers", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Doctors", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Volunteers", new[] { "DepartmentID" });
            DropIndex("dbo.Doctors", new[] { "DepartmentID" });
            AlterColumn("dbo.Volunteers", "DepartmentID", c => c.Int());
            AlterColumn("dbo.Doctors", "DepartmentID", c => c.Int());
            RenameColumn(table: "dbo.Volunteers", name: "DepartmentID", newName: "Department_DepartmentID");
            RenameColumn(table: "dbo.Doctors", name: "DepartmentID", newName: "Department_DepartmentID");
            CreateIndex("dbo.Volunteers", "Department_DepartmentID");
            CreateIndex("dbo.Doctors", "Department_DepartmentID");
            AddForeignKey("dbo.Volunteers", "Department_DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.Doctors", "Department_DepartmentID", "dbo.Departments", "DepartmentID");
        }
    }
}
