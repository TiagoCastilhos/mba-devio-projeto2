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
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] varchar(100) NOT NULL,
        [Name] varchar(100) NULL,
        [NormalizedName] varchar(100) NULL,
        [ConcurrencyStamp] varchar(100) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] varchar(100) NOT NULL,
        [UserName] varchar(100) NULL,
        [NormalizedUserName] varchar(100) NULL,
        [Email] varchar(100) NULL,
        [NormalizedEmail] varchar(100) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] varchar(100) NULL,
        [SecurityStamp] varchar(100) NULL,
        [ConcurrencyStamp] varchar(100) NULL,
        [PhoneNumber] varchar(100) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [CATEGORIAS] (
        [Id] uniqueidentifier NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Descricao] VARCHAR(500) NOT NULL,
        [Ativo] BIT NOT NULL,
        CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [CLIENTES] (
        [Id] uniqueidentifier NOT NULL,
        [Ativo] BIT NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Email] VARCHAR(100) NOT NULL,
        [Senha] VARCHAR(256) NOT NULL,
        CONSTRAINT [PK_CLIENTES] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [VENDEDORES] (
        [Id] uniqueidentifier NOT NULL,
        [Ativo] BIT NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Email] VARCHAR(100) NOT NULL,
        [Senha] VARCHAR(256) NOT NULL,
        CONSTRAINT [PK_VENDEDORES] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] varchar(100) NOT NULL,
        [ClaimType] varchar(100) NULL,
        [ClaimValue] varchar(100) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] varchar(100) NOT NULL,
        [ClaimType] varchar(100) NULL,
        [ClaimValue] varchar(100) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] varchar(100) NOT NULL,
        [ProviderKey] varchar(100) NOT NULL,
        [ProviderDisplayName] varchar(100) NULL,
        [UserId] varchar(100) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] varchar(100) NOT NULL,
        [RoleId] varchar(100) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] varchar(100) NOT NULL,
        [LoginProvider] varchar(100) NOT NULL,
        [Name] varchar(100) NOT NULL,
        [Value] varchar(100) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE TABLE [PRODUTOS] (
        [Id] uniqueidentifier NOT NULL,
        [Estoque] INT NOT NULL,
        [Preco] DECIMAL(18,2) NOT NULL,
        [Nome] VARCHAR(100) NOT NULL,
        [Descricao] VARCHAR(500) NOT NULL,
        [Imagem] VARCHAR(500) NULL,
        [CategoriaId] uniqueidentifier NOT NULL,
        [VendedorId] uniqueidentifier NOT NULL,
        [Ativo] BIT NOT NULL,
        CONSTRAINT [PK_PRODUTOS] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PRODUTO_CATEGORIAID] FOREIGN KEY ([CategoriaId]) REFERENCES [CATEGORIAS] ([Id]),
        CONSTRAINT [FK_PRODUTO_VENDEDORID] FOREIGN KEY ([VendedorId]) REFERENCES [VENDEDORES] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250629183422_FavoritoMappingAndSeed'
)
BEGIN
    CREATE TABLE [FAVORITOS] (
        [Id] uniqueidentifier NOT NULL,
        [ClienteId] uniqueidentifier NOT NULL,
        [ProdutoId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_FAVORITOS] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FAVORITOS_CLIENTES_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [CLIENTES] ([Id]),
        CONSTRAINT [FK_FAVORITOS_PRODUTOS_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [PRODUTOS] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621002904_DefaultAppUsers'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (''1'', ''2c5e174e-3b0e-446f-86af-483d56fd7210'', ''Administrator'', ''ADMINISTRADOR''),
    (''2'', ''16aacd76-5c6d-418a-884c-116871ca2fe0'', ''Vendedor'', ''VENDEDOR''),
    (''3'', ''bd1f5f5b-77e4-4ac3-b101-1f3053f4ee6c'', ''Cliente'', ''CLIENTE'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621002904_DefaultAppUsers'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] ON;
    EXEC(N'INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
    VALUES (''f96e5735-7f8a-49a7-8fe1-64304e70257b'', 0, ''f1aef7e9-db61-4442-a01a-ea58d7609d21'', ''admin@teste.com'', CAST(1 AS bit), CAST(1 AS bit), NULL, ''ADMIN@TESTE.COM'', ''ADMIN@TESTE.COM'', ''AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg =='', NULL, CAST(0 AS bit), ''fdb857cc-1f49-484f-bd6b-bfbba7fedfab'', CAST(0 AS bit), ''admin@teste.com''),
    (''f96e5735-7f8a-49a7-8fe1-64304e70257c'', 0, ''f1aef7e9-db61-4442-a01a-ea58d7609d21'', ''cliente@teste.com'', CAST(1 AS bit), CAST(1 AS bit), NULL, ''CLIENTE@TESTE.COM'', ''CLIENTE@TESTE.COM'', ''AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg =='', NULL, CAST(0 AS bit), ''fdb857cc-1f49-484f-bd6b-bfbba7fedfab'', CAST(0 AS bit), ''cliente@teste.com''),
    (''f96e5735-7f8a-49a7-8fe1-64304e70257d'', 0, ''f1aef7e9-db61-4442-a01a-ea58d7609d21'', ''vendedor@teste.com'', CAST(1 AS bit), CAST(1 AS bit), NULL, ''VENDEDOR@TESTE.COM'', ''VENDEDOR@TESTE.COM'', ''AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg =='', NULL, CAST(0 AS bit), ''fdb857cc-1f49-484f-bd6b-bfbba7fedfab'', CAST(0 AS bit), ''vendedor@teste.com'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621002026_FirstSeed'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'Descricao', N'Nome') AND [object_id] = OBJECT_ID(N'[CATEGORIAS]'))
        SET IDENTITY_INSERT [CATEGORIAS] ON;
    EXEC(N'INSERT INTO [CATEGORIAS] ([Id], [Ativo], [Descricao], [Nome])
    VALUES (''2ce8ce71-e766-41ee-839a-f0824f7fd3b8'', CAST(1 AS BIT), ''Categoria destinada a vestuário'', ''Vestuário''),
    (''63cb29c3-db97-4744-b01d-def53fc1cccb'', CAST(0 AS BIT), ''Comidas em geral'', ''Alimentação''),
    (''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', CAST(1 AS BIT), ''Eletrônicos e eletrodomésticos'', ''Eletrônicos'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'Descricao', N'Nome') AND [object_id] = OBJECT_ID(N'[CATEGORIAS]'))
        SET IDENTITY_INSERT [CATEGORIAS] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621002904_DefaultAppUsers'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'Email', N'Nome', N'Senha') AND [object_id] = OBJECT_ID(N'[CLIENTES]'))
        SET IDENTITY_INSERT [CLIENTES] ON;
    EXEC(N'INSERT INTO [CLIENTES] ([Id], [Ativo], [Email], [Nome], [Senha])
    VALUES (''f96e5735-7f8a-49a7-8fe1-64304e70257c'', CAST(1 AS BIT), ''cliente@teste.com'', ''cliente@teste.com'', ''AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg =='')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'Email', N'Nome', N'Senha') AND [object_id] = OBJECT_ID(N'[CLIENTES]'))
        SET IDENTITY_INSERT [CLIENTES] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621002904_DefaultAppUsers'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'Email', N'Nome', N'Senha') AND [object_id] = OBJECT_ID(N'[VENDEDORES]'))
        SET IDENTITY_INSERT [VENDEDORES] ON;
    EXEC(N'INSERT INTO [VENDEDORES] ([Id], [Ativo], [Email], [Nome], [Senha])
    VALUES (''f96e5735-7f8a-49a7-8fe1-64304e70257d'', CAST(1 AS BIT), ''vendedor@teste.com'', ''vendedor@teste.com'', ''AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg =='')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'Email', N'Nome', N'Senha') AND [object_id] = OBJECT_ID(N'[VENDEDORES]'))
        SET IDENTITY_INSERT [VENDEDORES] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621002904_DefaultAppUsers'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] ON;
    EXEC(N'INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
    VALUES (''1'', ''f96e5735-7f8a-49a7-8fe1-64304e70257b''),
    (''3'', ''f96e5735-7f8a-49a7-8fe1-64304e70257c''),
    (''2'', ''f96e5735-7f8a-49a7-8fe1-64304e70257d'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621002026_FirstSeed'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'CategoriaId', N'Descricao', N'Estoque', N'Imagem', N'Nome', N'Preco', N'VendedorId') AND [object_id] = OBJECT_ID(N'[PRODUTOS]'))
        SET IDENTITY_INSERT [PRODUTOS] ON;
    EXEC(N'INSERT INTO [PRODUTOS] ([Id], [Ativo], [CategoriaId], [Descricao], [Estoque], [Imagem], [Nome], [Preco], [VendedorId])
    VALUES (''26361398-ab18-4efd-879f-1f0ad1bb6d9e'', CAST(1 AS BIT), ''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', ''teclado mecânico'', 15, ''00000000-0000-0000-0000-000000000000_imagem.jpg'', ''Teclado'', 100.0, ''f96e5735-7f8a-49a7-8fe1-64304e70257d''),
    (''5fa99536-a7c8-403d-a0a0-373f30773054'', CAST(1 AS BIT), ''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', ''mouse com fio'', 20, ''00000000-0000-0000-0000-000000000000_imagem.jpg'', ''Mouse'', 60.0, ''f96e5735-7f8a-49a7-8fe1-64304e70257d''),
    (''6fa552cd-bdbf-4f4d-b298-987c3a140275'', CAST(0 AS BIT), ''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', ''Monitor curso 27'', 28, ''00000000-0000-0000-0000-000000000000_imagem.jpg'', ''Monitor'', 780.0, ''f96e5735-7f8a-49a7-8fe1-64304e70257d''),
    (''f5dd84d8-ccda-43e8-96cf-be0ccff0de3b'', CAST(1 AS BIT), ''7b87817f-f13c-4a68-87c5-0fc28eda22ce'', ''Personal Computer'', 100, ''00000000-0000-0000-0000-000000000000_imagem.jpg'', ''Computador'', 5000.0, ''f96e5735-7f8a-49a7-8fe1-64304e70257d'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ativo', N'CategoriaId', N'Descricao', N'Estoque', N'Imagem', N'Nome', N'Preco', N'VendedorId') AND [object_id] = OBJECT_ID(N'[PRODUTOS]'))
        SET IDENTITY_INSERT [PRODUTOS] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250629183422_FavoritoMappingAndSeed'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClienteId', N'ProdutoId') AND [object_id] = OBJECT_ID(N'[FAVORITOS]'))
        SET IDENTITY_INSERT [FAVORITOS] ON;
    EXEC(N'INSERT INTO [FAVORITOS] ([Id], [ClienteId], [ProdutoId])
    VALUES (''099cb44e-44d8-45f2-960c-47139b38bc52'', ''f96e5735-7f8a-49a7-8fe1-64304e70257c'', ''6fa552cd-bdbf-4f4d-b298-987c3a140275''),
    (''115a7dde-7803-4836-9799-49046e1d7fb1'', ''f96e5735-7f8a-49a7-8fe1-64304e70257c'', ''5fa99536-a7c8-403d-a0a0-373f30773054''),
    (''4f45533c-1f36-46e5-acdb-fbb7e56254ac'', ''f96e5735-7f8a-49a7-8fe1-64304e70257c'', ''26361398-ab18-4efd-879f-1f0ad1bb6d9e''),
    (''7f5c5026-518c-4ea2-abe5-8934920d1a27'', ''f96e5735-7f8a-49a7-8fe1-64304e70257c'', ''f5dd84d8-ccda-43e8-96cf-be0ccff0de3b'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClienteId', N'ProdutoId') AND [object_id] = OBJECT_ID(N'[FAVORITOS]'))
        SET IDENTITY_INSERT [FAVORITOS] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_NOME_CATEGORIA] ON [CATEGORIAS] ([Nome]) WITH (FILLFACTOR = 80);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_CLIENTE_NOME] ON [CLIENTES] ([Nome]) WITH (FILLFACTOR = 80);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_CLIENTE_EMAIL] ON [CLIENTES] ([Email]) WITH (FILLFACTOR = 80);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_FAVORITOS_ProdutoId] ON [FAVORITOS] ([ProdutoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_FAVORITO_CLIENTEID_PRODUTOID] ON [FAVORITOS] ([ClienteId], [ProdutoId]) WITH (FILLFACTOR = 80);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_PRODUTO_NOME] ON [PRODUTOS] ([Nome]) WITH (FILLFACTOR = 80);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_PRODUTOS_CategoriaId] ON [PRODUTOS] ([CategoriaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_PRODUTOS_VendedorId] ON [PRODUTOS] ([VendedorId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_PRODUTO_NOME_CATEGORIAID_VENDEDORID] ON [PRODUTOS] ([Nome], [CategoriaId], [VendedorId]) WITH (FILLFACTOR = 80);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE INDEX [IX_VENDEDOR_NOME] ON [VENDEDORES] ([Nome]) WITH (FILLFACTOR = 80);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    CREATE UNIQUE INDEX [UQ_VENDEDOR_EMAIL] ON [VENDEDORES] ([Email]) WITH (FILLFACTOR = 80);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250621001941_FirstMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250621001941_FirstMigration', N'9.0.6');
END;

COMMIT;
GO

