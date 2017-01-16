namespace MovieDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableMovieId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Actors", name: "Movie_Id", newName: "MovieId");
            RenameIndex(table: "dbo.Actors", name: "IX_Movie_Id", newName: "IX_MovieId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Actors", name: "IX_MovieId", newName: "IX_Movie_Id");
            RenameColumn(table: "dbo.Actors", name: "MovieId", newName: "Movie_Id");
        }
    }
}
