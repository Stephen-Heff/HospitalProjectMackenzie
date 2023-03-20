namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        PaymentAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payments");
        }
    }
}
