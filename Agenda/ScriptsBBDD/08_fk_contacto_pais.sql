
ALTER TABLE Contacto
ADD CONSTRAINT FK_PaisContacto
FOREIGN KEY (IdPais) REFERENCES Pais(IdPais);