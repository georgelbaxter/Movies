namespace MovieDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingMTMRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieActors1", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieActors1", "Actor_Id", "dbo.Actors");
            DropIndex("dbo.MovieActors1", new[] { "Movie_Id" });
            DropIndex("dbo.MovieActors1", new[] { "Actor_Id" });
            AddColumn("dbo.Actors", "Movie_Id", c => c.Int());
            CreateIndex("dbo.Actors", "Movie_Id");
            AddForeignKey("dbo.Actors", "Movie_Id", "dbo.Movies", "Id");
            DropTable("dbo.MovieActors");
            DropTable("dbo.MovieActors1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MovieActors1",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Actor_Id });
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        ActorId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActorId, t.MovieId });
            
            DropForeignKey("dbo.Actors", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.Actors", new[] { "Movie_Id" });
            DropColumn("dbo.Actors", "Movie_Id");
            CreateIndex("dbo.MovieActors1", "Actor_Id");
            CreateIndex("dbo.MovieActors1", "Movie_Id");
            AddForeignKey("dbo.MovieActors1", "Actor_Id", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieActors1", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
        }
    }
}
