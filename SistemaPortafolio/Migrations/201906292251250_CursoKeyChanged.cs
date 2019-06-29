namespace SistemaPortafolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CursoKeyChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CursoAlumno", "curso_cod", "dbo.Curso");
            DropForeignKey("dbo.CursoDocente", "curso_cod", "dbo.Curso");
            DropForeignKey("dbo.Documento", "curso_cod", "dbo.Curso");
            DropForeignKey("dbo.MetadataDocumento", "cod_curso", "dbo.Curso");
            DropIndex("dbo.CursoAlumno", new[] { "curso_cod" });
            DropIndex("dbo.CursoDocente", new[] { "curso_cod" });
            DropIndex("dbo.Documento", new[] { "curso_cod" });
            DropIndex("dbo.MetadataDocumento", new[] { "cod_curso" });
            RenameColumn(table: "dbo.CursoAlumno", name: "curso_cod", newName: "curso_id");
            RenameColumn(table: "dbo.CursoDocente", name: "curso_cod", newName: "curso_id");
            RenameColumn(table: "dbo.Documento", name: "curso_cod", newName: "curso_id");
            RenameColumn(table: "dbo.MetadataDocumento", name: "cod_curso", newName: "curso_id");
            DropPrimaryKey("dbo.Curso");
            AddColumn("dbo.Curso", "curso_id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Curso", "tipo_curso", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Curso", "curso_cod", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.CursoAlumno", "curso_id", c => c.Int(nullable: false));
            AlterColumn("dbo.CursoDocente", "curso_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Documento", "curso_id", c => c.Int(nullable: false));
            AlterColumn("dbo.MetadataDocumento", "curso_id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Curso", "curso_id");
            CreateIndex("dbo.CursoAlumno", "curso_id");
            CreateIndex("dbo.CursoDocente", "curso_id");
            CreateIndex("dbo.Documento", "curso_id");
            CreateIndex("dbo.MetadataDocumento", "curso_id");
            AddForeignKey("dbo.CursoAlumno", "curso_id", "dbo.Curso", "curso_id");
            AddForeignKey("dbo.CursoDocente", "curso_id", "dbo.Curso", "curso_id");
            AddForeignKey("dbo.Documento", "curso_id", "dbo.Curso", "curso_id");
            AddForeignKey("dbo.MetadataDocumento", "curso_id", "dbo.Curso", "curso_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MetadataDocumento", "curso_id", "dbo.Curso");
            DropForeignKey("dbo.Documento", "curso_id", "dbo.Curso");
            DropForeignKey("dbo.CursoDocente", "curso_id", "dbo.Curso");
            DropForeignKey("dbo.CursoAlumno", "curso_id", "dbo.Curso");
            DropIndex("dbo.MetadataDocumento", new[] { "curso_id" });
            DropIndex("dbo.Documento", new[] { "curso_id" });
            DropIndex("dbo.CursoDocente", new[] { "curso_id" });
            DropIndex("dbo.CursoAlumno", new[] { "curso_id" });
            DropPrimaryKey("dbo.Curso");
            AlterColumn("dbo.MetadataDocumento", "curso_id", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Documento", "curso_id", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.CursoDocente", "curso_id", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.CursoAlumno", "curso_id", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Curso", "curso_cod", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropColumn("dbo.Curso", "tipo_curso");
            DropColumn("dbo.Curso", "curso_id");
            AddPrimaryKey("dbo.Curso", "curso_cod");
            RenameColumn(table: "dbo.MetadataDocumento", name: "curso_id", newName: "cod_curso");
            RenameColumn(table: "dbo.Documento", name: "curso_id", newName: "curso_cod");
            RenameColumn(table: "dbo.CursoDocente", name: "curso_id", newName: "curso_cod");
            RenameColumn(table: "dbo.CursoAlumno", name: "curso_id", newName: "curso_cod");
            CreateIndex("dbo.MetadataDocumento", "cod_curso");
            CreateIndex("dbo.Documento", "curso_cod");
            CreateIndex("dbo.CursoDocente", "curso_cod");
            CreateIndex("dbo.CursoAlumno", "curso_cod");
            AddForeignKey("dbo.MetadataDocumento", "cod_curso", "dbo.Curso", "curso_cod");
            AddForeignKey("dbo.Documento", "curso_cod", "dbo.Curso", "curso_cod");
            AddForeignKey("dbo.CursoDocente", "curso_cod", "dbo.Curso", "curso_cod");
            AddForeignKey("dbo.CursoAlumno", "curso_cod", "dbo.Curso", "curso_cod");
        }
    }
}
