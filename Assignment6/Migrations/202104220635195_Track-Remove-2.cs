namespace Assignment3_WEB524.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrackRemove2 : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tracks", "Upload", c => c.Binary());
            AddColumn("dbo.Tracks", "UploadContentType", c => c.String(maxLength: 200));
        }
    }
}
