CREATE PROCEDURE [dbo].[SP_Insertar_Contacto]
	@ApellidoNombre varchar(100),
	@Genero varchar(4),
	@IdPais int,
	@Localidad varchar(100),
	@ContInterno varchar(100),
	@Organizacion varchar(100),
	@IdArea	int,
	@Activo varchar(10),
	@Direccion varchar(100),
	@TelFijo varchar(50),
	@TelCel varchar(50),
	@Email varchar(50),
	@Skype varchar(50)
AS
	
BEGIN 


	insert into Contacto(ApellidoNombre, genero, idpais, localidad, ContactoInterno, organizacion, idarea, activo, direccion, TelefonoFijo, TelefonoCelular, email, CuentaSkype, FechaIngreso)
	values (@ApellidoNombre, @Genero, @IdPais,@Localidad, @ContInterno, @Organizacion, @IdArea, @Activo, @Direccion,@TelFijo, @TelCel, @Email, @Skype, GETDATE());
   


END 