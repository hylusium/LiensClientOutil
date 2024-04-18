CREATE TABLE Client(
   IdClient INT IDENTITY,
   NomClient VARCHAR(50) NOT NULL,
   PRIMARY KEY(IdClient)
);

CREATE TABLE Outil(
   IdOutil INT IDENTITY,
   NomOutil VARCHAR(50) NOT NULL,
   VersionOutil VARCHAR(50) NOT NULL,
   DateMaj DATE NOT NULL,
   état VARCHAR(50) NOT NULL,
   Commentaire VARCHAR(300),
   PRIMARY KEY(IdOutil)
);

CREATE TABLE utillisateur(
   IdUser INT IDENTITY,
   username VARCHAR(50) NOT NULL,
   Password VARCHAR(50) NOT NULL,
   Rôle INT NOT NULL,
   PRIMARY KEY(IdUser)
);

CREATE TABLE Possede(
   IdClient INT,
   IdOutil INT,
   PRIMARY KEY(IdClient, IdOutil),
   FOREIGN KEY(IdClient) REFERENCES Client(IdClient),
   FOREIGN KEY(IdOutil) REFERENCES Outil(IdOutil)
);
