namespace BitSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "Iscanceled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Iscanceled", c => c.Boolean(nullable: false));
        }
    }
}
