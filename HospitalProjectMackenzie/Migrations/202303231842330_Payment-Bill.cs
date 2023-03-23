namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentBill : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "BillID", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "BillID");
            AddForeignKey("dbo.Payments", "BillID", "dbo.Bills", "BillID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "BillID", "dbo.Bills");
            DropIndex("dbo.Payments", new[] { "BillID" });
            DropColumn("dbo.Payments", "BillID");
        }
    }
}
