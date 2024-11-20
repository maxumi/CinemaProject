IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE TABLE [CinemaHalls] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Capacity] int NOT NULL,
        CONSTRAINT [PK_CinemaHalls] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE TABLE [Discounts] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [DiscountPercentage] decimal(18,2) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [IsGeneral] bit NOT NULL,
        [UsageLimit] int NULL,
        [UsageCount] int NOT NULL,
        CONSTRAINT [PK_Discounts] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE TABLE [Movies] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Genre] nvarchar(max) NOT NULL,
        [DurationMinutes] int NOT NULL,
        [ReleaseDate] datetime2 NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Movies] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Role] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE TABLE [Seats] (
        [Id] int NOT NULL IDENTITY,
        [SeatNumber] nvarchar(max) NOT NULL,
        [CinemaHallId] int NOT NULL,
        CONSTRAINT [PK_Seats] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Seats_CinemaHalls_CinemaHallId] FOREIGN KEY ([CinemaHallId]) REFERENCES [CinemaHalls] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE TABLE [Reservations] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [NumberOfTickets] int NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        [MovieId] nvarchar(max) NOT NULL,
        [MovieId1] int NOT NULL,
        [PaymentDetailId] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reservations_Movies_MovieId1] FOREIGN KEY ([MovieId1]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Reservations_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE TABLE [Reviews] (
        [Id] int NOT NULL IDENTITY,
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        [Comment] nvarchar(max) NOT NULL,
        [Rating] int NOT NULL,
        [ReviewDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reviews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE TABLE [PaymentDetails] (
        [Id] int NOT NULL IDENTITY,
        [Price] decimal(18,2) NOT NULL,
        [Date] datetime2 NOT NULL,
        [ReservationId] int NOT NULL,
        CONSTRAINT [PK_PaymentDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PaymentDetails_Reservations_ReservationId] FOREIGN KEY ([ReservationId]) REFERENCES [Reservations] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE TABLE [ReservationSeats] (
        [ReservationsId] int NOT NULL,
        [SeatsId] int NOT NULL,
        CONSTRAINT [PK_ReservationSeats] PRIMARY KEY ([ReservationsId], [SeatsId]),
        CONSTRAINT [FK_ReservationSeats_Reservations_ReservationsId] FOREIGN KEY ([ReservationsId]) REFERENCES [Reservations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ReservationSeats_Seats_SeatsId] FOREIGN KEY ([SeatsId]) REFERENCES [Seats] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE UNIQUE INDEX [IX_PaymentDetails_ReservationId] ON [PaymentDetails] ([ReservationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE INDEX [IX_Reservations_MovieId1] ON [Reservations] ([MovieId1]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE INDEX [IX_Reservations_UserId] ON [Reservations] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE INDEX [IX_ReservationSeats_SeatsId] ON [ReservationSeats] ([SeatsId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE INDEX [IX_Reviews_MovieId] ON [Reviews] ([MovieId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    CREATE INDEX [IX_Seats_CinemaHallId] ON [Seats] ([CinemaHallId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120115526_ManyToManyTest'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241120115526_ManyToManyTest', N'9.0.0');
END;

COMMIT;
GO