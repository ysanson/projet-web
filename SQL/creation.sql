USE [VeterinaryClinic]
GO
/****** Object:  Table [dbo].[Animal]    Script Date: 28/05/2018 23:14:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Animal](
	[IdAnimal] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](25) NULL,
	[sexe] [varchar](10) NULL,
	[commentaire] [text] NULL,
	[dateNaissance] [datetime] NULL,
	[IdUtilisateur] [int] NOT NULL,
	[idEspece] [int] NOT NULL,
 CONSTRAINT [prk_constraint_ANIMAL] PRIMARY KEY NONCLUSTERED 
(
	[IdAnimal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consultation]    Script Date: 28/05/2018 23:14:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consultation](
	[idObservation] [int] IDENTITY(1,1) NOT NULL,
	[description] [text] NOT NULL,
	[dateObs] [datetime] NULL,
	[IdAnimal] [int] NOT NULL,
	[IdUtilisateur] [int] NOT NULL,
	[examenClinique] [text] NULL,
	[diagnostic] [text] NULL,
 CONSTRAINT [prk_constraint_CONSULTATION] PRIMARY KEY NONCLUSTERED 
(
	[idObservation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Espece]    Script Date: 28/05/2018 23:14:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Espece](
	[idEspece] [int] IDENTITY(1,1) NOT NULL,
	[espece] [varchar](25) NOT NULL,
	[race] [varchar](25) NULL,
	[esprace] [varchar](50) NULL,
 CONSTRAINT [prk_constraint_ESPECE] PRIMARY KEY NONCLUSTERED 
(
	[idEspece] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicament]    Script Date: 28/05/2018 23:14:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicament](
	[idMedicament] [int] IDENTITY(1,1) NOT NULL,
	[nomMedicament] [varchar](25) NOT NULL,
	[posologieMax] [varchar](25) NULL,
 CONSTRAINT [prk_constraint_MEDICAMENT] PRIMARY KEY NONCLUSTERED 
(
	[idMedicament] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 28/05/2018 23:14:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operation](
	[IdOperation] [int] IDENTITY(1,1) NOT NULL,
	[label] [varchar](50) NOT NULL,
 CONSTRAINT [prk_constraint_OPERATION] PRIMARY KEY NONCLUSTERED 
(
	[IdOperation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Passe]    Script Date: 28/05/2018 23:14:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Passe](
	[DateOp] [datetime] NULL,
	[CompteRenduOP] [text] NULL,
	[IdAnimal] [int] NOT NULL,
	[IdOperation] [int] NOT NULL,
	[IdUtilisateur] [int] NOT NULL,
 CONSTRAINT [prk_constraint_PASSE] PRIMARY KEY NONCLUSTERED 
(
	[IdAnimal] ASC,
	[IdOperation] ASC,
	[IdUtilisateur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrescConsultation]    Script Date: 28/05/2018 23:14:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrescConsultation](
	[posologie] [varchar](25) NULL,
	[idObservation] [int] NOT NULL,
	[idMedicament] [int] NOT NULL,
 CONSTRAINT [prk_constraint_PRESC_CONS] PRIMARY KEY NONCLUSTERED 
(
	[idObservation] ASC,
	[idMedicament] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Remarques]    Script Date: 28/05/2018 23:14:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Remarques](
	[idRemarques] [int] IDENTITY(1,1) NOT NULL,
	[dateRemarque] [datetime] NULL,
	[contenuRemarque] [text] NOT NULL,
	[IdAnimal] [int] NOT NULL,
 CONSTRAINT [prk_constraint_REMARQUES] PRIMARY KEY NONCLUSTERED 
(
	[idRemarques] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuiviPoids]    Script Date: 28/05/2018 23:14:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuiviPoids](
	[idSuivi] [int] IDENTITY(1,1) NOT NULL,
	[dateSuivi] [datetime] NULL,
	[poids] [float] NULL,
	[IdAnimal] [int] NOT NULL,
 CONSTRAINT [prk_constraint_SUIVI_PDS] PRIMARY KEY NONCLUSTERED 
(
	[idSuivi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utilisateur]    Script Date: 28/05/2018 23:14:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utilisateur](
	[IdUtilisateur] [int] IDENTITY(1,1) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[nom] [varchar](25) NOT NULL,
	[prenom] [varchar](25) NULL,
	[tel] [varchar](25) NULL,
	[mail] [varchar](25) NOT NULL,
	[adresse] [varchar](150) NULL,
	[ville] [varchar](25) NULL,
	[codePostal] [int] NULL,
	[estVeto] [bit] NOT NULL,
	[salt] [varchar](20) NULL,
 CONSTRAINT [prk_constraint_UTILISATEUR] PRIMARY KEY NONCLUSTERED 
(
	[IdUtilisateur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_ANIMAL_idEspece] FOREIGN KEY([idEspece])
REFERENCES [dbo].[Espece] ([idEspece])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_ANIMAL_idEspece]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_ANIMAL_IdUtilisateur] FOREIGN KEY([IdUtilisateur])
REFERENCES [dbo].[Utilisateur] ([IdUtilisateur])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_ANIMAL_IdUtilisateur]
GO
ALTER TABLE [dbo].[Consultation]  WITH CHECK ADD  CONSTRAINT [FK_CONSULTATION_IdAnimal] FOREIGN KEY([IdAnimal])
REFERENCES [dbo].[Animal] ([IdAnimal])
GO
ALTER TABLE [dbo].[Consultation] CHECK CONSTRAINT [FK_CONSULTATION_IdAnimal]
GO
ALTER TABLE [dbo].[Consultation]  WITH CHECK ADD  CONSTRAINT [FK_CONSULTATION_IdUtilisateur] FOREIGN KEY([IdUtilisateur])
REFERENCES [dbo].[Utilisateur] ([IdUtilisateur])
GO
ALTER TABLE [dbo].[Consultation] CHECK CONSTRAINT [FK_CONSULTATION_IdUtilisateur]
GO
ALTER TABLE [dbo].[Passe]  WITH CHECK ADD  CONSTRAINT [FK_PASSE_IdAnimal] FOREIGN KEY([IdAnimal])
REFERENCES [dbo].[Animal] ([IdAnimal])
GO
ALTER TABLE [dbo].[Passe] CHECK CONSTRAINT [FK_PASSE_IdAnimal]
GO
ALTER TABLE [dbo].[Passe]  WITH CHECK ADD  CONSTRAINT [FK_PASSE_IdOperation] FOREIGN KEY([IdOperation])
REFERENCES [dbo].[Operation] ([IdOperation])
GO
ALTER TABLE [dbo].[Passe] CHECK CONSTRAINT [FK_PASSE_IdOperation]
GO
ALTER TABLE [dbo].[Passe]  WITH CHECK ADD  CONSTRAINT [FK_PASSE_IdUtilisateur] FOREIGN KEY([IdUtilisateur])
REFERENCES [dbo].[Utilisateur] ([IdUtilisateur])
GO
ALTER TABLE [dbo].[Passe] CHECK CONSTRAINT [FK_PASSE_IdUtilisateur]
GO
ALTER TABLE [dbo].[PrescConsultation]  WITH CHECK ADD  CONSTRAINT [FK_PRESC_CONS_idMedicament] FOREIGN KEY([idMedicament])
REFERENCES [dbo].[Medicament] ([idMedicament])
GO
ALTER TABLE [dbo].[PrescConsultation] CHECK CONSTRAINT [FK_PRESC_CONS_idMedicament]
GO
ALTER TABLE [dbo].[PrescConsultation]  WITH CHECK ADD  CONSTRAINT [FK_PRESC_CONS_idObservation] FOREIGN KEY([idObservation])
REFERENCES [dbo].[Consultation] ([idObservation])
GO
ALTER TABLE [dbo].[PrescConsultation] CHECK CONSTRAINT [FK_PRESC_CONS_idObservation]
GO
ALTER TABLE [dbo].[Remarques]  WITH CHECK ADD  CONSTRAINT [FK_REMARQUES_IdAnimal] FOREIGN KEY([IdAnimal])
REFERENCES [dbo].[Animal] ([IdAnimal])
GO
ALTER TABLE [dbo].[Remarques] CHECK CONSTRAINT [FK_REMARQUES_IdAnimal]
GO
ALTER TABLE [dbo].[SuiviPoids]  WITH CHECK ADD  CONSTRAINT [FK_SUIVI_PDS_IdAnimal] FOREIGN KEY([IdAnimal])
REFERENCES [dbo].[Animal] ([IdAnimal])
GO
ALTER TABLE [dbo].[SuiviPoids] CHECK CONSTRAINT [FK_SUIVI_PDS_IdAnimal]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD CHECK  (([sexe]='femelle' OR [sexe]='male'))
GO
