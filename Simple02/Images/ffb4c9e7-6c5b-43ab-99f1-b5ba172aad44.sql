CREATE TABLE [dbo].[Posts] (
    [PID]                INT              IDENTITY (1, 1) NOT NULL,
    [Poster_Id]          NVARCHAR (128)   NULL,
    [ToEqry_EID]         UNIQUEIDENTIFIER NULL,
    [content]            NVARCHAR (MAX)   NULL,
    [type]               NVARCHAR (MAX)   NULL,
    [postingTime]        DATETIME         NOT NULL DEFAULT (getdate()),
    [posterDisplay]      NVARCHAR (MAX)   NULL,
    [forvotes]           INT              CONSTRAINT [DF__Posts__forvotes__01142BA1] DEFAULT ((0)) NOT NULL,
    [RlyWhich_PID]       INT              NULL,
    [ApplicationUser_Id] NVARCHAR (128)   NULL,
    [ToAsmt_AID]         INT              NULL,
    CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED ([PID] ASC),
    CONSTRAINT [FK_dbo.Posts_dbo.AspNetUsers_ApplicationUser_Id] FOREIGN KEY ([ApplicationUser_Id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Posts_dbo.Answers_ToAsmt_AID] FOREIGN KEY ([ToAsmt_AID]) REFERENCES [dbo].[Answers] ([AID]),
    CONSTRAINT [FK_dbo.Posts_dbo.AspNetUsers_Poster_Id] FOREIGN KEY ([Poster_Id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Posts_dbo.Enquiries_ToEqry_EID] FOREIGN KEY ([ToEqry_EID]) REFERENCES [dbo].[Enquiries] ([Enquiry_id]),
    CONSTRAINT [FK_dbo.Posts_dbo.Posts_RlyWhich_PID] FOREIGN KEY ([RlyWhich_PID]) REFERENCES [dbo].[Posts] ([PID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Poster_Id]
    ON [dbo].[Posts]([Poster_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ToEqry_EID]
    ON [dbo].[Posts]([ToEqry_EID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationUser_Id]
    ON [dbo].[Posts]([ApplicationUser_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RlyWhich_PID]
    ON [dbo].[Posts]([RlyWhich_PID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ToAsmt_AID]
    ON [dbo].[Posts]([ToAsmt_AID] ASC);

