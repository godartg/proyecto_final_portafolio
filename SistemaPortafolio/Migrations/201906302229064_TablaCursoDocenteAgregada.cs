namespace SistemaPortafolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablaCursoDocenteAgregada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Silabo",
                c => new
                    {
                        silabo_id = c.Int(nullable: false, identity: true),
                        cursodocente_id = c.Int(nullable: false),
                        descripcion = c.String(unicode: false, storeType: "text"),
                        bibliografia = c.String(unicode: false, storeType: "text"),
                        competencias_curso = c.String(unicode: false, storeType: "text"),
                        temas = c.String(unicode: false, storeType: "text"),
                        resultados = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.silabo_id)
                .ForeignKey("dbo.CursoDocente", t => t.cursodocente_id)
                .Index(t => t.cursodocente_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Silabo", "cursodocente_id", "dbo.CursoDocente");
            DropIndex("dbo.Silabo", new[] { "cursodocente_id" });
            DropTable("dbo.Silabo");
        }
    }
}
