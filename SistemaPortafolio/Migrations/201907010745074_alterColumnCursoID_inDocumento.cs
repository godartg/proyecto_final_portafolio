namespace SistemaPortafolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterColumnCursoID_inDocumento : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Documento", new[] { "curso_id" });
            AlterColumn("dbo.Documento", "curso_id", c => c.Int());
            CreateIndex("dbo.Documento", "curso_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Documento", new[] { "curso_id" });
            AlterColumn("dbo.Documento", "curso_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Documento", "curso_id");
        }
    }
}
