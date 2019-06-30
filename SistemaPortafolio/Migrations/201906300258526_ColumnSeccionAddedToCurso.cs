namespace SistemaPortafolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnSeccionAddedToCurso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Curso", "seccion", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Curso", "seccion");
        }
    }
}
