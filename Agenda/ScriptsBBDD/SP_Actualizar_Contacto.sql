CREATE PROCEDURE [dbo].[SP_Actualizar_Contacto]
	@idContacto int,
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

	update Contacto
	set ApellidoNombre = @ApellidoNombre,
		genero = @Genero,
		idPais = @IdPais,
		Localidad = @Localidad,
		ContactoInterno = @ContInterno,
		Organizacion = @Organizacion,
		IdArea = @IdArea,
		Activo = @Activo,
		Direccion = @Direccion,
		TelefonoFijo = @TelFijo,
		TelefonoCelular = @TelCel,
		Email = @Email,
		CuentaSkype = @Skype
	where idContacto = @idContacto;

END 