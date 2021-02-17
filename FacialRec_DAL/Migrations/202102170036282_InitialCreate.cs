namespace FacialRec_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FaceRec.Persoon",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Voornaam = c.String(),
                        Achternaam = c.String(),
                        Foto = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("FaceRec.Persoon");
        }
    }
}
