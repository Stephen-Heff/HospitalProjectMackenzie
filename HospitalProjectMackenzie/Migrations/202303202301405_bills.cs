namespace HospitalProjectMackenzie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillID = c.Int(nullable: false, identity: true),
                        BillAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bills");
        }
    }
}
