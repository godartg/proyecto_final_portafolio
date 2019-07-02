namespace SistemaPortafolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablasPruebaEntradaAgregadas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PruebaEntrada",
                c => new
                    {
                        pruebaentrada_id = c.Int(nullable: false, identity: true),
                        cursodocente_id = c.Int(nullable: false),
                        matriculados = c.Int(nullable: false),
                        evaluados = c.Int(nullable: false),
                        medidas_correctivas = c.String(maxLength: 150),
                        recomendaciones = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.pruebaentrada_id)
                .ForeignKey("dbo.CursoDocente", t => t.cursodocente_id, cascadeDelete: true)
                .Index(t => t.cursodocente_id);
            
            CreateTable(
                "dbo.PruebaEntradaDetalle",
                c => new
                    {
                        pruebaentradadetalle_id = c.Int(nullable: false, identity: true),
                        pruebaentrada_id = c.Int(nullable: false),
                        conocimiento = c.String(maxLength: 250),
                        deficiente = c.Int(nullable: false),
                        suficiente = c.Int(nullable: false),
                        bueno = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.pruebaentradadetalle_id)
                .ForeignKey("dbo.PruebaEntrada", t => t.pruebaentrada_id)
                .Index(t => t.pruebaentrada_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PruebaEntradaDetalle", "pruebaentrada_id", "dbo.PruebaEntrada");
            DropForeignKey("dbo.PruebaEntrada", "cursodocente_id", "dbo.CursoDocente");
            DropIndex("dbo.PruebaEntradaDetalle", new[] { "pruebaentrada_id" });
            DropIndex("dbo.PruebaEntrada", new[] { "cursodocente_id" });
            DropTable("dbo.PruebaEntradaDetalle");
            DropTable("dbo.PruebaEntrada");
        }
    }
}
