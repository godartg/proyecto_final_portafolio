namespace SistemaPortafolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablePortafolioAdded : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Documento", new[] { "curso_id" });
            CreateTable(
                "dbo.Portafolio",
                c => new
                    {
                        portafolio_id = c.Int(nullable: false, identity: true),
                        cursodocente_id = c.Int(nullable: false),
                        unidad = c.String(maxLength: 10),
                        matriculados = c.Int(nullable: false),
                        retirados = c.Int(nullable: false),
                        abandono = c.Int(nullable: false),
                        asisten = c.Int(nullable: false),
                        aprobados = c.Int(nullable: false),
                        desaprobados = c.Int(nullable: false),
                        material_digital = c.String(),
                        material_impreso = c.String(),
                        material_cantidad = c.String(),
                        recepcionado_por = c.String(),
                    })
                .PrimaryKey(t => t.portafolio_id)
                .ForeignKey("dbo.CursoDocente", t => t.cursodocente_id, cascadeDelete: true)
                .Index(t => t.cursodocente_id);
            
            AlterColumn("dbo.Documento", "curso_id", c => c.Int());
            CreateIndex("dbo.Documento", "curso_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Portafolio", "cursodocente_id", "dbo.CursoDocente");
            DropIndex("dbo.Documento", new[] { "curso_id" });
            DropIndex("dbo.Portafolio", new[] { "cursodocente_id" });
            AlterColumn("dbo.Documento", "curso_id", c => c.Int(nullable: false));
            DropTable("dbo.Portafolio");
            CreateIndex("dbo.Documento", "curso_id");
        }
    }
}
