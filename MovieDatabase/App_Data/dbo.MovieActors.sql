CREATE TABLE [dbo].[MovieActors] (
    [MovieId] INT NOT NULL,
    [ActorId] INT NOT NULL,
    CONSTRAINT [PK_dbo.MovieActors] PRIMARY KEY CLUSTERED ([MovieId] ASC, [ActorId] ASC),
    CONSTRAINT [FK_dbo.MovieActors_dbo.Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.MovieActors_dbo.Actors_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [dbo].[Actors] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_MovieId]
    ON [dbo].[MovieActors]([MovieId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ActorId]
    ON [dbo].[MovieActors]([ActorId] ASC);

