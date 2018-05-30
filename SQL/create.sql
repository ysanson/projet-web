/*------------------------------------------------------------
*        Script SQLSERVER 
------------------------------------------------------------*/


/*------------------------------------------------------------
-- Table: UTILISATEUR
------------------------------------------------------------*/
CREATE TABLE UTILISATEUR(
	IdUtilisateur INT IDENTITY (1,1) NOT NULL ,
	identifiant   VARCHAR (50)  ,
	password      VARCHAR (100) NOT NULL ,
	nom           VARCHAR (25) NOT NULL ,
	prenom        VARCHAR (25)  ,
	tel           VARCHAR (25)  ,
	mail          VARCHAR (50)  ,
	adresse       VARCHAR (25)  ,
	ville         VARCHAR (25)  ,
	codePostal    INT   ,
	estVeto       bit  NOT NULL ,
	CONSTRAINT prk_constraint_UTILISATEUR PRIMARY KEY NONCLUSTERED (IdUtilisateur)
);


/*------------------------------------------------------------
-- Table: ANIMAL
------------------------------------------------------------*/
CREATE TABLE ANIMAL(
	IdAnimal      INT IDENTITY (1,1) NOT NULL ,
	nom           VARCHAR (25)  ,
	sexe          VARCHAR(10) CHECK (sexe IN ('male','femelle'))  ,
	commentaire   TEXT   ,
	dateNaissance DATETIME  ,
	IdUtilisateur INT  NOT NULL ,
	idEspece      INT  NOT NULL ,
	CONSTRAINT prk_constraint_ANIMAL PRIMARY KEY NONCLUSTERED (IdAnimal)
);


/*------------------------------------------------------------
-- Table: OPERATION
------------------------------------------------------------*/
CREATE TABLE OPERATION(
	IdOperation INT IDENTITY (1,1) NOT NULL ,
	label       VARCHAR (50) NOT NULL ,
	CONSTRAINT prk_constraint_OPERATION PRIMARY KEY NONCLUSTERED (IdOperation)
);


/*------------------------------------------------------------
-- Table: ESPECE
------------------------------------------------------------*/
CREATE TABLE ESPECE(
	idEspece INT IDENTITY (1,1) NOT NULL ,
	espece   VARCHAR (25) NOT NULL ,
	race     VARCHAR (25)  ,
	CONSTRAINT prk_constraint_ESPECE PRIMARY KEY NONCLUSTERED (idEspece)
);


/*------------------------------------------------------------
-- Table: CONSULTATION
------------------------------------------------------------*/
CREATE TABLE CONSULTATION(
	idObservation INT IDENTITY (1,1) NOT NULL ,
	description   TEXT NOT NULL ,
	dateObs       DATETIME  ,
	IdAnimal      INT  NOT NULL ,
	IdUtilisateur INT  NOT NULL ,
	CONSTRAINT prk_constraint_CONSULTATION PRIMARY KEY NONCLUSTERED (idObservation)
);


/*------------------------------------------------------------
-- Table: MEDICAMENT
------------------------------------------------------------*/
CREATE TABLE MEDICAMENT(
	idMedicament  INT IDENTITY (1,1) NOT NULL ,
	nomMedicament VARCHAR (25) NOT NULL ,
	posologieMax  VARCHAR (25)  ,
	CONSTRAINT prk_constraint_MEDICAMENT PRIMARY KEY NONCLUSTERED (idMedicament)
);


/*------------------------------------------------------------
-- Table: REMARQUES
------------------------------------------------------------*/
CREATE TABLE REMARQUES(
	idRemarques     INT IDENTITY (1,1) NOT NULL ,
	dateRemarque    DATETIME  ,
	contenuRemarque VARCHAR (500)  ,
	IdAnimal        INT  NOT NULL ,
	CONSTRAINT prk_constraint_REMARQUES PRIMARY KEY NONCLUSTERED (idRemarques)
);


/*------------------------------------------------------------
-- Table: SUIVI_PDS
------------------------------------------------------------*/
CREATE TABLE SUIVI_PDS(
	idSuivi   INT IDENTITY (1,1) NOT NULL ,
	dateSuivi DATETIME  ,
	poids     FLOAT   ,
	IdAnimal  INT  NOT NULL ,
	CONSTRAINT prk_constraint_SUIVI_PDS PRIMARY KEY NONCLUSTERED (idSuivi)
);


/*------------------------------------------------------------
-- Table: PASSE
------------------------------------------------------------*/
CREATE TABLE PASSE(
	DateOp        DATETIME  ,
	CompteRenduOP TEXT  ,
	IdAnimal      INT  NOT NULL ,
	IdOperation   INT  NOT NULL ,
	IdUtilisateur INT  NOT NULL ,
	CONSTRAINT prk_constraint_PASSE PRIMARY KEY NONCLUSTERED (IdAnimal,IdOperation,IdUtilisateur)
);


/*------------------------------------------------------------
-- Table: PRESC_CONS
------------------------------------------------------------*/
CREATE TABLE PRESC_CONS(
	posologie     VARCHAR (25)  ,
	idObservation INT  NOT NULL ,
	idMedicament  INT  NOT NULL ,
	CONSTRAINT prk_constraint_PRESC_CONS PRIMARY KEY NONCLUSTERED (idObservation,idMedicament)
);


/*------------------------------------------------------------
-- Table: PRESC_OP
------------------------------------------------------------*/
CREATE TABLE PRESC_OP(
	posologie    VARCHAR (25)  ,
	IdOperation  INT  NOT NULL ,
	idMedicament INT  NOT NULL ,
	CONSTRAINT prk_constraint_PRESC_OP PRIMARY KEY NONCLUSTERED (IdOperation,idMedicament)
);


/*------------------------------------------------------------
-- Table: SUIT
------------------------------------------------------------*/
CREATE TABLE SUIT(
	IdUtilisateur INT  NOT NULL ,
	IdAnimal      INT  NOT NULL ,
	CONSTRAINT prk_constraint_SUIT PRIMARY KEY NONCLUSTERED (IdUtilisateur,IdAnimal)
);



ALTER TABLE ANIMAL ADD CONSTRAINT FK_ANIMAL_IdUtilisateur FOREIGN KEY (IdUtilisateur) REFERENCES UTILISATEUR(IdUtilisateur);
ALTER TABLE ANIMAL ADD CONSTRAINT FK_ANIMAL_idEspece FOREIGN KEY (idEspece) REFERENCES ESPECE(idEspece);
ALTER TABLE CONSULTATION ADD CONSTRAINT FK_CONSULTATION_IdAnimal FOREIGN KEY (IdAnimal) REFERENCES ANIMAL(IdAnimal);
ALTER TABLE CONSULTATION ADD CONSTRAINT FK_CONSULTATION_IdUtilisateur FOREIGN KEY (IdUtilisateur) REFERENCES UTILISATEUR(IdUtilisateur);
ALTER TABLE REMARQUES ADD CONSTRAINT FK_REMARQUES_IdAnimal FOREIGN KEY (IdAnimal) REFERENCES ANIMAL(IdAnimal);
ALTER TABLE SUIVI_PDS ADD CONSTRAINT FK_SUIVI_PDS_IdAnimal FOREIGN KEY (IdAnimal) REFERENCES ANIMAL(IdAnimal);
ALTER TABLE PASSE ADD CONSTRAINT FK_PASSE_IdAnimal FOREIGN KEY (IdAnimal) REFERENCES ANIMAL(IdAnimal);
ALTER TABLE PASSE ADD CONSTRAINT FK_PASSE_IdOperation FOREIGN KEY (IdOperation) REFERENCES OPERATION(IdOperation);
ALTER TABLE PASSE ADD CONSTRAINT FK_PASSE_IdUtilisateur FOREIGN KEY (IdUtilisateur) REFERENCES UTILISATEUR(IdUtilisateur);
ALTER TABLE PRESC_CONS ADD CONSTRAINT FK_PRESC_CONS_idObservation FOREIGN KEY (idObservation) REFERENCES CONSULTATION(idObservation);
ALTER TABLE PRESC_CONS ADD CONSTRAINT FK_PRESC_CONS_idMedicament FOREIGN KEY (idMedicament) REFERENCES MEDICAMENT(idMedicament);
ALTER TABLE PRESC_OP ADD CONSTRAINT FK_PRESC_OP_IdOperation FOREIGN KEY (IdOperation) REFERENCES OPERATION(IdOperation);
ALTER TABLE PRESC_OP ADD CONSTRAINT FK_PRESC_OP_idMedicament FOREIGN KEY (idMedicament) REFERENCES MEDICAMENT(idMedicament);
ALTER TABLE SUIT ADD CONSTRAINT FK_SUIT_IdUtilisateur FOREIGN KEY (IdUtilisateur) REFERENCES UTILISATEUR(IdUtilisateur);
ALTER TABLE SUIT ADD CONSTRAINT FK_SUIT_IdAnimal FOREIGN KEY (IdAnimal) REFERENCES ANIMAL(IdAnimal);
