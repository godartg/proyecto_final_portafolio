namespace SistemaPortafolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HojaDeVida : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HojaVidaDocenteActividadesDesarrolloProfesional",
                c => new
                    {
                        idHojaVidaDocenteActividadesDesarrolloProfesional = c.Int(nullable: false, identity: true),
                        hojavida_id = c.Int(nullable: false),
                        institucion = c.String(),
                        actividad = c.String(),
                        fechainicio = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        fechafin = c.DateTime(nullable: false, storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.idHojaVidaDocenteActividadesDesarrolloProfesional)
                .ForeignKey("dbo.HojaVida", t => t.hojavida_id, cascadeDelete: true)
                .Index(t => t.hojavida_id);
            
            CreateTable(
                "dbo.HojaVidaDocenteActividadServicios",
                c => new
                    {
                        idHojaVidaDocenteActividadServicios = c.Int(nullable: false, identity: true),
                        hojavida_id = c.Int(nullable: false),
                        institucion = c.String(),
                        servicio = c.String(),
                        fechainicio = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        fechafin = c.DateTime(nullable: false, storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.idHojaVidaDocenteActividadServicios)
                .ForeignKey("dbo.HojaVida", t => t.hojavida_id, cascadeDelete: true)
                .Index(t => t.hojavida_id);
            
            CreateTable(
                "dbo.HojaVidaDocenteHonoresPremios",
                c => new
                    {
                        idHojaVidaDocenteHonoresPremios = c.Int(nullable: false, identity: true),
                        hojavida_id = c.Int(nullable: false),
                        institucion = c.String(nullable: false, maxLength: 150),
                        titulo = c.String(nullable: false, maxLength: 150),
                        fecha = c.DateTime(nullable: false, storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.idHojaVidaDocenteHonoresPremios)
                .ForeignKey("dbo.HojaVida", t => t.hojavida_id, cascadeDelete: true)
                .Index(t => t.hojavida_id);
            
            CreateTable(
                "dbo.HojaVidaDocenteMembresia",
                c => new
                    {
                        idHojaVidaDocenteMembresia = c.Int(nullable: false, identity: true),
                        hojavida_id = c.Int(nullable: false),
                        institucion = c.String(nullable: false, maxLength: 150),
                        funcion = c.String(nullable: false, maxLength: 150),
                        ano = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.idHojaVidaDocenteMembresia)
                .ForeignKey("dbo.HojaVida", t => t.hojavida_id, cascadeDelete: true)
                .Index(t => t.hojavida_id);
            
            CreateTable(
                "dbo.HojaVidaDocentePublicaciones",
                c => new
                    {
                        idHojaVidaDocentePublicaciones = c.Int(nullable: false, identity: true),
                        hojavida_id = c.Int(nullable: false),
                        tipoProduccion = c.String(maxLength: 150),
                        titulo = c.String(maxLength: 150),
                        primerAutor = c.String(nullable: false, unicode: false, storeType: "text"),
                        coAutores = c.String(unicode: false, storeType: "text"),
                        ano = c.String(maxLength: 50),
                        doi = c.String(unicode: false, storeType: "text"),
                        fuente = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.idHojaVidaDocentePublicaciones)
                .ForeignKey("dbo.HojaVida", t => t.hojavida_id, cascadeDelete: true)
                .Index(t => t.hojavida_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HojaVidaDocentePublicaciones", "hojavida_id", "dbo.HojaVida");
            DropForeignKey("dbo.HojaVidaDocenteMembresia", "hojavida_id", "dbo.HojaVida");
            DropForeignKey("dbo.HojaVidaDocenteHonoresPremios", "hojavida_id", "dbo.HojaVida");
            DropForeignKey("dbo.HojaVidaDocenteActividadServicios", "hojavida_id", "dbo.HojaVida");
            DropForeignKey("dbo.HojaVidaDocenteActividadesDesarrolloProfesional", "hojavida_id", "dbo.HojaVida");
            DropIndex("dbo.HojaVidaDocentePublicaciones", new[] { "hojavida_id" });
            DropIndex("dbo.HojaVidaDocenteMembresia", new[] { "hojavida_id" });
            DropIndex("dbo.HojaVidaDocenteHonoresPremios", new[] { "hojavida_id" });
            DropIndex("dbo.HojaVidaDocenteActividadServicios", new[] { "hojavida_id" });
            DropIndex("dbo.HojaVidaDocenteActividadesDesarrolloProfesional", new[] { "hojavida_id" });
            DropTable("dbo.HojaVidaDocentePublicaciones");
            DropTable("dbo.HojaVidaDocenteMembresia");
            DropTable("dbo.HojaVidaDocenteHonoresPremios");
            DropTable("dbo.HojaVidaDocenteActividadServicios");
            DropTable("dbo.HojaVidaDocenteActividadesDesarrolloProfesional");
        }
    }
}
