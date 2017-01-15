namespace MovieDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMovieActorModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovieActors", newName: "MovieActors1");
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        ActorId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActorId, t.MovieId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovieActors");
            RenameTable(name: "dbo.MovieActors1", newName: "MovieActors");
        }
    }
}
