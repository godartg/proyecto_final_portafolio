namespace SistemaPortafolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableInformeFinalAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InformeFinal",
                c => new
                    {
                        informefinal_id = c.Int(nullable: false, identity: true),
                        cursodocente_id = c.Int(nullable: false),
                        porcentaje_silabo = c.Int(nullable: false),
                        practicas = c.Int(nullable: false),
                        laboratorios = c.Int(nullable: false),
                        proyectos = c.Int(nullable: false),
                        matriculados = c.Int(nullable: false),
                        retiro = c.Int(nullable: false),
                        abandono = c.Int(nullable: false),
                        asisten = c.Int(nullable: false),
                        aprobados = c.Int(nullable: false),
                        desaprobados = c.Int(nullable: false),
                        nota_alta = c.Int(nullable: false),
                        nota_promedio = c.Int(nullable: false),
                        nota_baja = c.Int(nullable: false),
                        motivo = c.String(unicode: false, storeType: "text"),
                        observacion_estudiantes = c.String(unicode: false, storeType: "text"),
                        observacion_asistencia = c.String(unicode: false, storeType: "text"),
                        observacion_silabo = c.String(unicode: false, storeType: "text"),
                        observacion_aulavirtual = c.String(unicode: false, storeType: "text"),
                        observacion_administrativas = c.String(unicode: false, storeType: "text"),
                        observacion_competencias = c.String(unicode: false, storeType: "text"),
                        observacion_mejora = c.String(unicode: false, storeType: "text"),
                        observacion_docente = c.String(unicode: false, storeType: "text"),
                        observacion_recomendaciones = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.informefinal_id)
                .ForeignKey("dbo.CursoDocente", t => t.cursodocente_id, cascadeDelete: true)
                .Index(t => t.cursodocente_id);
            
            CreateTable(
                "dbo.InformeFinalDetalle",
                c => new
                    {
                        informefinaldetalle_id = c.Int(nullable: false, identity: true),
                        informefinal_id = c.Int(nullable: false),
                        capacidad = c.String(unicode: false, storeType: "text"),
                        nivel_alcanzado = c.String(),
                    })
                .PrimaryKey(t => t.informefinaldetalle_id)
                .ForeignKey("dbo.InformeFinal", t => t.informefinal_id)
                .Index(t => t.informefinal_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InformeFinalDetalle", "informefinal_id", "dbo.InformeFinal");
            DropForeignKey("dbo.InformeFinal", "cursodocente_id", "dbo.CursoDocente");
            DropIndex("dbo.InformeFinalDetalle", new[] { "informefinal_id" });
            DropIndex("dbo.InformeFinal", new[] { "cursodocente_id" });
            DropTable("dbo.InformeFinalDetalle");
            DropTable("dbo.InformeFinal");
        }
    }
}
