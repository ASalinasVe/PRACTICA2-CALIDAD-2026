# Grafos de flujo de control — Microservicio de Usuarios

**Revisión del árbol fuente actual:** 2026-10-12. Los intervalos se refieren a las líneas físicas de los archivos listados, desde 1.

## Alcance y lectura

Se analizaron los cuerpos de métodos escritos en los proyectos API, App, Dom e Inf bajo MicroServices/MicroServicioUsuario.API/. El inventario principal contiene **56 métodos**, incluidas las dos funciones locales de Registration.RegisterUser. Se enumeran además **15 constructores con cuerpo** en una sección separada y el bloque de instrucciones top-level de Program.cs, que no es un método declarado.

Se excluyeron archivos generados, carpetas bin/ y obj/, el proyecto de pruebas, interfaces sin implementación, propiedades/accesores automáticos y declaraciones sin cuerpo. Las dos propiedades calculadas con cuerpo explícito se incluyen en un apéndice separado y no se suman al conteo de métodos. Las llamadas a otros métodos o bibliotecas se representan como bloques; no se expanden. Las expresiones SQL dentro de literales se tratan como datos, no como decisiones del flujo C#.

Cada grafo es de flujo normal salvo las aristas de excepción indicadas explícitamente. Los catch se conectan desde el bloque try que los contiene. Las excepciones que una llamada externa pudiera lanzar fuera de un try/catch no se predicen: su condición depende del runtime/proveedor. Las etiquetas “Sí/No” se refieren a la condición del nodo indicado. V(G) no se calcula en general; cuando un ticket requiere el análisis, sus valores se documentan en la sección del método correspondiente.

## Índice de métodos

### API — MicroServicioUsuario

| Archivo relativo | Clase.Método y firma resumida | Líneas |
|---|---|---:|
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController.ChangePasswordFirstLogin(ChangePasswordDTO) | 54–60 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController.Login(LoginDTO) | 63–75 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController.Register(RegisterDTO) | 79–83 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController.Select() | 86–101 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController.GetById(int) | 105–120 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController.Search(string) | 125–140 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController.Create(User) | 145–175 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController.Update(User) | 180–216 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController.Delete(int) | 221–250 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/WeatherForecastController.cs | WeatherForecastController.Get() | 22–31 |

### App — MicroServicioUser.App

| Archivo relativo | Clase.Método y firma resumida | Líneas |
|---|---|---:|
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs | UserService.Insert(User) | 21–29 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs | UserService.Update(User) | 31–36 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs | UserService.GetById(int) | 38–41 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs | UserService.Delete(int) | 43–46 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs | UserService.GetAll() | 48–51 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs | UserService.Search(string) | 53–56 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/RegistrationService.cs | RegistrationService.RegisterUser(string, string, string, string, int) | 25–44 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs | LoginService.ChangePassword(int, string, string) | 13–16 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs | LoginService.ChangePasswordFirstLogin(int, string, string) | 18–22 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs | LoginService.ValidateLogin(string, string) | 24–28 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/JwtService.cs | JwtService.GenerateToken(int, string) | 18–41 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/EmailService.cs | EmailService.SendEmailAsync(string, string, string, bool) | 19–22 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/EmailService.cs | EmailService.SendNewUserCredentialsAsync(string, string, string, string) | 24–88 |

### Dom — MicroServicioUser.Dom

| Archivo relativo | Clase.Método y firma resumida | Líneas |
|---|---|---:|
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Rules/PasswordValidator.cs | PasswordValidator.Validate(string) | 12–21 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs | Result.Success() | 16 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs | Result.Failure(params string[]) | 17 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs | Result&lt;T&gt;.Success(T) | 29 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs | Result&lt;T&gt;.Failure(params string[]) | 30 |

### Inf — MicroServicoUser.Inf

| Archivo relativo | Clase.Método y firma resumida | Líneas |
|---|---|---:|
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs | UserRepository.GetAll() | 21–50 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs | UserRepository.Insert(User) | 52–91 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs | UserRepository.Update(User) | 92–116 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs | UserRepository.Search(string) | 118–162 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs | UserRepository.GetById(int) | 164–194 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs | UserRepository.GetByUsername(string) | 196–225 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs | UserRepository.Delete(int) | 226–241 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Login.cs | Login.ChangePassword(int, string, string) | 14–47 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Login.cs | Login.ChangePasswordFirstLogin(int, string, string) | 49–76 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Login.cs | Login.ValidateLogin(string, string) | 78–96 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs | RegistrationHelpers.Md5Hex(string) | 15–26 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs | Registration.GeneratePassword(int) | 39–51 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs | Registration.RegisterUser(string, string, string, string, int) | 53–130 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs | Registration.RegisterUser.RemoveDiacritics(string) — función local | 58–69 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs | Registration.RegisterUser.SlugifyLetters(string) — función local | 71–80 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.CreateConnection() | 14 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.ExecuteParameterizedQuery&lt;T&gt;(string, T) | 16–20 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.ExecuteParameterizedQuery&lt;TOut,TParam&gt;(string, TParam) | 21–25 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.ExecuteQuery&lt;T&gt;(string) | 27–31 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.ExecuteParameterizedNonQuery&lt;T&gt;(string, T) | 32–36 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.ExecuteNonQuery(string) | 38–42 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.ExecuteCommand(MySqlCommand) | 44–51 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.ExecuteCommand&lt;T&gt;(MySqlCommand) | 52–58 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.LoadDataTable(MySqlCommand) | 60–72 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.OpenConnection(MySqlConnection) | 74 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager.ExecuteMySqlCommand(MySqlCommand) | 76 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/DbParameterHelper.cs | DbParameterHelper.PopulateCommandParameters&lt;T&gt;(string, T) | 10–41 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/DbMapper.cs | DbMapper.MapDataRowToModel&lt;T&gt;(DataRow) | 10–38 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/DbMapper.cs | DbMapper.MapDataTableToModelIterable&lt;T&gt;(DataTable) | 40–49 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/MySqlConnectionDB.cs | MySqlConnectionDB.GetConnection() | 20–23 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/EmailAdapters/SmtpEmailAdapter.cs | SmtpEmailAdapter.SendEmailAsync(string, string, string, bool) | 18–55 |

## Constructores con cuerpo (inventario separado)

| Archivo relativo | Clase.Constructor y firma resumida | Líneas |
|---|---|---:|
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs | UserController(IRepositoryService&lt;User&gt;, LoginService, RegistrationService, IJwtService) | 42–49 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/WeatherForecastController.cs | WeatherForecastController(ILogger&lt;WeatherForecastController&gt;) | 16–19 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs | UserService(IRepository) | 17–20 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/RegistrationService.cs | RegistrationService(IRegistration, EmailService) | 19–23 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs | LoginService(ILogin) | 8–11 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/JwtService.cs | JwtService(IConfiguration) | 16 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/EmailService.cs | EmailService(IEmailService) | 14–17 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs | Result(bool, List&lt;string&gt;) | 10–14 |
| MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs | Result&lt;T&gt;(bool, T, List&lt;string&gt;) | 24–28 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs | UserRepository(MySqlConnectionManager) | 16–19 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Login.cs | Login(IRepository) | 9–12 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs | Registration(IRepository) | 33–37 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs | MySqlConnectionManager(IConfiguration) | 10–13 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/MySqlConnectionDB.cs | MySqlConnectionDB(IConfiguration) | 15–18 |
| MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/EmailAdapters/SmtpEmailAdapter.cs | SmtpEmailAdapter(SmtpSettings) | 14–17 |


# Proyecto API — MicroServicioUsuario

## Clase UserController

### UserController.ChangePasswordFirstLogin

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** public async Task&lt;ActionResult&lt;bool&gt;&gt; ChangePasswordFirstLogin(ChangePasswordDTO cpDTO)  
**Líneas:** 54–60. Delega el cambio de contraseña inicial y envuelve ok/error en Ok.

~~~csharp
0054 | public async Task<ActionResult<bool>> ChangePasswordFirstLogin([FromBody] ChangePasswordDTO cpDTO)
0056 | var res = await this.loginService.ChangePasswordFirstLogin(cpDTO.UserId, cpDTO.CurrentPassword,
0057 |     cpDTO.NewPassword);
0059 | return Ok(new {Ok = res.ok, Error = res.error});
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await loginService.ChangePasswordFirstLogin]
    N2 --> N3[Construir Ok con Ok y Error]
    N3 --> N4([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 54–55 | Inicio | Entrada |
| N2 | 56–57 | loginService.ChangePasswordFirstLogin(...) | Obtener resultado |
| N3 | 59 | return Ok(...) | Respuesta HTTP 200 |
| N4 | 60 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 56 |
| N2 → N3 | llamada completada | 59 |
| N3 → N4 | return | 59 |

**Puntos de decisión:** ninguno.

### UserController.Login

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** public async Task&lt;ActionResult&lt;bool&gt;&gt; Login(LoginDTO loginDTO)  
**Líneas:** 63–75. Valida credenciales; genera token si son válidas y, si no, devuelve error.

~~~csharp
0066 | var obj = await this.loginService.ValidateLogin(loginDTO.Username, loginDTO.Password);
0067 | if (obj.ok)
0069 |     var result = await jwtService.GenerateToken((int)obj.userId, obj.role);
0070 |     return Ok(new LoginResponse(){Error = "", Ok = true, Token = result.Value, TokenType = "Bearer"});
0074 | return Ok(new LoginResponse(){ Error = obj.error, Ok = false });
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await ValidateLogin]
    N2 --> N3{obj.ok?}
    N3 -- Sí --> N4[Await jwtService.GenerateToken]
    N4 --> N5[return Ok LoginResponse con token]
    N5 --> N7([Salida])
    N3 -- No --> N6[return Ok LoginResponse de error]
    N6 --> N7
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 63–65 | Inicio | Entrada |
| N2 | 66 | loginService.ValidateLogin(...) | Autenticar |
| N3 | 67 | obj.ok | Elegir respuesta |
| N4 | 69 | jwtService.GenerateToken(...) | Crear token si válido |
| N5 | 70–72 | return Ok(LoginResponse con token) | Éxito |
| N6 | 74 | return Ok(LoginResponse con error) | Credenciales inválidas |
| N7 | 75 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 66 |
| N2 → N3 | llamada completada | 67 |
| N3 → N4 | Sí | 67 |
| N3 → N6 | No | 74 |
| N4 → N5 | token generado | 70 |
| N5 → N7 | return | 70–72 |
| N6 → N7 | return | 74 |

**Puntos de decisión:** if en línea 67. No hay catch local para las llamadas await.

### UserController.Register

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** public async Task&lt;ActionResult&lt;bool&gt;&gt; Register(RegisterDTO registerDto)  
**Líneas:** 79–83. Delega los datos de registro y responde con ok/error.

~~~csharp
0081 | var obj = await this.registrationService.RegisterUser(registerDto.FirstName, registerDto.LastName, registerDto.Email, registerDto.Role, registerDto.CreatedBy);
0082 | return Ok(new {Ok=obj.ok, Error=obj.error});
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await registrationService.RegisterUser]
    N2 --> N3[return Ok con Ok y Error]
    N3 --> N4([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 79–80 | Inicio | Entrada |
| N2 | 81 | registrationService.RegisterUser(...) | Delegar |
| N3 | 82 | return Ok(...) | Respuesta |
| N4 | 83 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 81 |
| N2 → N3 | llamada completada | 82 |
| N3 → N4 | return | 82 |

**Puntos de decisión:** ninguno.


### UserController.Select

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** public async Task&lt;ActionResult&lt;List&lt;User&gt;&gt;&gt; Select()  
**Líneas:** 86–101. Consulta los usuarios y responde Ok o StatusCode 500.

~~~csharp
0088 | var categories = await service.GetAll();
0089 | if (categories.IsSuccess)
0091 |     return Ok(categories.Value);
0093 | else
0095 |     return StatusCode(500, new { message = "Error al obtener los usuarios", error = categories.Errors });
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await service.GetAll]
    N2 --> N3{categories.IsSuccess?}
    N3 -- Sí --> N4[return Ok categories.Value]
    N3 -- No --> N5[return StatusCode 500 con errores]
    N4 --> N6([Salida])
    N5 --> N6
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 86–87 | Inicio | Entrada |
| N2 | 88 | service.GetAll() | Consultar usuarios |
| N3 | 89 | categories.IsSuccess | Elegir resultado |
| N4 | 91 | return Ok(categories.Value) | Éxito |
| N5 | 93–99 | else y StatusCode(500, ...) | Error |
| N6 | 101 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 88 |
| N2 → N3 | llamada completada | 89 |
| N3 → N4 | Sí | 89 |
| N3 → N5 | No / else | 93 |
| N4 → N6 | return | 91 |
| N5 → N6 | return | 95–99 |

**Puntos de decisión:** if/else, líneas 89–93.

### UserController.GetById

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** public async Task&lt;ActionResult&lt;User&gt;&gt; GetById(int id)  
**Líneas:** 105–120. Consulta por id y responde Ok o StatusCode 500.

~~~csharp
0107 | var category = await service.GetById(id);
0108 | if (category.IsSuccess)
0110 |     return Ok(category.Value);
0112 | else
0114 |     return StatusCode(500, new { message = "Error al obtener el usuario", error = category.Errors });
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await service.GetById]
    N2 --> N3{category.IsSuccess?}
    N3 -- Sí --> N4[return Ok category.Value]
    N3 -- No --> N5[return StatusCode 500 con errores]
    N4 --> N6([Salida])
    N5 --> N6
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 105–106 | Inicio | Entrada |
| N2 | 107 | service.GetById(id) | Consultar por id |
| N3 | 108 | category.IsSuccess | Elegir resultado |
| N4 | 110 | return Ok(category.Value) | Éxito |
| N5 | 112–118 | else y StatusCode(500, ...) | Error |
| N6 | 120 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 107 |
| N2 → N3 | llamada completada | 108 |
| N3 → N4 | Sí | 108 |
| N3 → N5 | No / else | 112 |
| N4 → N6 | return | 110 |
| N5 → N6 | return | 114–118 |

**Puntos de decisión:** if/else, líneas 108–112.

### UserController.Search

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** public async Task&lt;ActionResult&lt;List&lt;User&gt;&gt;&gt; Search(string property)  
**Líneas:** 125–140. Delega búsqueda y responde con resultado o error HTTP 500.

~~~csharp
0127 | var categories = await service.Search(property);
0128 | if (categories.IsSuccess)
0130 |     return Ok(categories.Value);
0132 | else
0134 |     return StatusCode(500, new { message = "Error al buscar usuario", error = categories.Errors });
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await service.Search]
    N2 --> N3{categories.IsSuccess?}
    N3 -- Sí --> N4[return Ok categories.Value]
    N3 -- No --> N5[return StatusCode 500 con errores]
    N4 --> N6([Salida])
    N5 --> N6
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 125–126 | Inicio | Entrada |
| N2 | 127 | service.Search(property) | Buscar usuarios |
| N3 | 128 | categories.IsSuccess | Elegir resultado |
| N4 | 130 | return Ok(categories.Value) | Éxito |
| N5 | 132–138 | else y StatusCode(500, ...) | Error |
| N6 | 140 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 127 |
| N2 → N3 | llamada completada | 128 |
| N3 → N4 | Sí | 128 |
| N3 → N5 | No / else | 132 |
| N4 → N6 | return | 130 |
| N5 → N6 | return | 134–138 |

**Puntos de decisión:** if/else, líneas 128–132.


### UserController.Create

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** public async Task&lt;ActionResult&gt; Create(User User)  
**Líneas:** 145–175. Rechaza ModelState inválido; inserta y responde CreatedAtAction o 500.

~~~csharp
0147 | if (!ModelState.IsValid)
0149 |     return BadRequest(ModelState);
0152 | var result = await service.Insert(User);
0154 | if (result.IsSuccess)
0156 |     return CreatedAtAction(nameof(Select), new { id = result.Value }, new { ... });
0167 | else
0169 |     return StatusCode(500, new { message = "Error al crear el usuario", error = result.Errors });
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2{ModelState.IsValid?}
    N2 -- No --> N3[return BadRequest]
    N2 -- Sí --> N4[Await service.Insert]
    N4 --> N5{result.IsSuccess?}
    N5 -- Sí --> N6[return CreatedAtAction]
    N5 -- No --> N7[return StatusCode 500]
    N3 --> N8([Salida])
    N6 --> N8
    N7 --> N8
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 145–146 | Inicio | Entrada |
| N2 | 147 | !ModelState.IsValid | Validar modelo |
| N3 | 149 | return BadRequest(ModelState) | Retorno anticipado |
| N4 | 152 | service.Insert(User) | Insertar |
| N5 | 154 | result.IsSuccess | Elegir respuesta |
| N6 | 156–165 | return CreatedAtAction(...) | Éxito |
| N7 | 167–173 | else; StatusCode(500, ...) | Error |
| N8 | 175 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 147 |
| N2 → N3 | No válido | 147–149 |
| N2 → N4 | Válido | 147–152 |
| N3 → N8 | return anticipado | 149 |
| N4 → N5 | llamada completada | 154 |
| N5 → N6 | Sí | 154 |
| N5 → N7 | No / else | 167 |
| N6 → N8 | return | 156–165 |
| N7 → N8 | return | 169–173 |

**Puntos de decisión:** if de validación, línea 147; if/else de resultado, líneas 154–167.

### UserController.Update

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** public async Task&lt;ActionResult&gt; Update(User User)  
**Líneas:** 180–216. Valida, actualiza y distingue éxito, id inexistente y otros errores.

~~~csharp
0183 | if (!ModelState.IsValid) return BadRequest(ModelState);
0188 | var result = await service.Update(User);
0190 | if (result.IsSuccess) return Ok(new { message = "...", data = User });
0200 | if (result.Errors.Any(e => e.Contains("No se encontró")))
0202 |     return NotFound(new { message = ... });
0207 | else
0209 |     return StatusCode(500, new { message = "...", error = result.Errors });
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2{ModelState.IsValid?}
    N2 -- No --> N3[return BadRequest]
    N2 -- Sí --> N4[Await service.Update]
    N4 --> N5{result.IsSuccess?}
    N5 -- Sí --> N6[return Ok con usuario]
    N5 -- No --> N7{Any error contiene No se encontró?}
    N7 -- Sí --> N8[return NotFound]
    N7 -- No --> N9[return StatusCode 500]
    N3 --> N10([Salida])
    N6 --> N10
    N8 --> N10
    N9 --> N10
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 180–182 | Inicio | Entrada |
| N2 | 183 | !ModelState.IsValid | Validar |
| N3 | 185 | return BadRequest(ModelState) | Retorno anticipado |
| N4 | 188 | service.Update(User) | Actualizar |
| N5 | 190 | result.IsSuccess | Éxito o fallo |
| N6 | 192–196 | return Ok(...) | Éxito |
| N7 | 200 | result.Errors.Any(e => e.Contains(...)) | Clasificar; Any y Contains no se expanden |
| N8 | 202–205 | return NotFound(...) | Usuario inexistente |
| N9 | 207–213 | else; StatusCode(500, ...) | Otro error |
| N10 | 216 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 183 |
| N2 → N3 | No válido | 183–185 |
| N2 → N4 | Válido | 183–188 |
| N3 → N10 | return anticipado | 185 |
| N4 → N5 | llamada completada | 190 |
| N5 → N6 | Sí | 190 |
| N5 → N7 | No / else | 198–200 |
| N6 → N10 | return | 192–196 |
| N7 → N8 | Sí | 200 |
| N7 → N9 | No / else | 207 |
| N8 → N10 | return | 202–205 |
| N9 → N10 | return | 209–213 |

**Puntos de decisión:** if de validación, línea 183; if/else de éxito, líneas 190–198; if/else de clasificación, líneas 200–207. Any/Contains se trata como llamada única, sin expandir su implementación.

### UserController.Delete

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** public async Task&lt;ActionResult&gt; Delete(int id)  
**Líneas:** 221–250. Elimina y distingue éxito, usuario no encontrado y otros errores.

~~~csharp
0223 | var result = await service.Delete(id);
0225 | if (result.IsSuccess) return Ok(new { message = ... });
0234 | if (result.Errors.Any(e => e.Contains("No se encontró")))
0236 |     return NotFound(new { message = ... });
0241 | else
0243 |     return StatusCode(500, new { message = "...", error = result.Errors });
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await service.Delete]
    N2 --> N3{result.IsSuccess?}
    N3 -- Sí --> N4[return Ok]
    N3 -- No --> N5{Any error contiene No se encontró?}
    N5 -- Sí --> N6[return NotFound]
    N5 -- No --> N7[return StatusCode 500]
    N4 --> N8([Salida])
    N6 --> N8
    N7 --> N8
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 221–222 | Inicio | Entrada |
| N2 | 223 | service.Delete(id) | Delegar eliminación |
| N3 | 225 | result.IsSuccess | Éxito o error |
| N4 | 227–230 | return Ok(...) | Éxito |
| N5 | 234 | result.Errors.Any(e => e.Contains(...)) | Clasificar error |
| N6 | 236–239 | return NotFound(...) | Usuario no encontrado |
| N7 | 241–247 | else; return StatusCode(500, ...) | Otro error |
| N8 | 250 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 223 |
| N2 → N3 | llamada completada | 225 |
| N3 → N4 | Sí | 225 |
| N3 → N5 | No / else | 232–234 |
| N4 → N8 | return | 227–230 |
| N5 → N6 | Sí | 234 |
| N5 → N7 | No / else | 241 |
| N6 → N8 | return | 236–239 |
| N7 → N8 | return | 243–247 |

**Puntos de decisión:** if/else de resultado, líneas 225–232; if/else de clasificación, líneas 234–241. Any/Contains se representa sin expandir.


## Clase WeatherForecastController

### WeatherForecastController.Get

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/WeatherForecastController.cs  
**Firma:** public IEnumerable&lt;WeatherForecast&gt; Get()  
**Líneas:** 22–31. Usa Range/Select para crear cinco pronósticos y ToArray para materializarlos.

~~~csharp
0022 | public IEnumerable<WeatherForecast> Get()
0024 | return Enumerable.Range(1, 5).Select(index => new WeatherForecast
0026 |     Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
0027 |     TemperatureC = Random.Shared.Next(-20, 55),
0028 |     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
0030 | }).ToArray();
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Range y Select con callback index => WeatherForecast]
    N2 --> N3[ToArray]
    N3 --> N4([Salida con arreglo])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 22–23 | Entrada a Get | Entrada |
| N2 | 24–29 | Range(...).Select(callback) | Crear secuencia y cada elemento |
| N3 | 30 | ToArray() | Materializar |
| N4 | 31 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 24 |
| N2 → N3 | secuencia construida | 30 |
| N3 → N4 | retorno de expresión | 24–30 |

**Puntos de decisión:** ninguno escrito en el método. La iteración interna de Range/Select no se expande.

# Proyecto App — MicroServicioUser.App

## Clase UserService

Los seis métodos de esta clase delegan al repositorio; Insert y Update actualizan campos antes de la llamada.

### UserService.Insert

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs  
**Firma:** public async Task&lt;Result&lt;int&gt;&gt; Insert(User t)  
**Líneas:** 21–29.

~~~csharp
0021 | public async Task<Result<int>> Insert(User t)
0023 | t.CreatedDate = DateTime.Now;
0024 | t.LastUpdate = DateTime.Now;
0025 | t.Status = true;
0026 | t.FirstLogin = 1;
0028 | return await userRepository.Insert(t);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar CreatedDate, LastUpdate, Status y FirstLogin]
    N2 --> N3[Await userRepository.Insert]
    N3 --> N4([Salida con resultado])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 21–22 | Entrada | Entrada |
| N2 | 23–26 | Asignaciones a t | Preparar usuario |
| N3 | 28 | userRepository.Insert(t) | Delegar inserción |
| N4 | 29 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 23 |
| N2 → N3 | secuencia | 28 |
| N3 → N4 | return await | 28 |

**Puntos de decisión:** ninguno.

### UserService.Update

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs  
**Firma:** public async Task&lt;Result&lt;int&gt;&gt; Update(User t)  
**Líneas:** 31–36.

~~~csharp
0031 | public async Task<Result<int>> Update(User t)
0033 | t.LastUpdate = DateTime.Now;
0035 | return await userRepository.Update(t);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar LastUpdate]
    N2 --> N3[Await userRepository.Update]
    N3 --> N4([Salida con resultado])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 31–32 | Entrada | Entrada |
| N2 | 33 | t.LastUpdate = DateTime.Now | Actualizar fecha |
| N3 | 35 | userRepository.Update(t) | Delegar actualización |
| N4 | 36 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 33 |
| N2 → N3 | secuencia | 35 |
| N3 → N4 | return await | 35 |

**Puntos de decisión:** ninguno.

### UserService.GetById

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs  
**Firma:** public async Task&lt;Result&lt;User&gt;&gt; GetById(int id)  
**Líneas:** 38–41.

~~~csharp
0038 | public async Task<Result<User>> GetById(int id)
0040 | return await userRepository.GetById(id);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await userRepository.GetById]
    N2 --> N3([Salida con resultado])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 38–39 | Entrada | Entrada |
| N2 | 40 | userRepository.GetById(id) | Delegar consulta |
| N3 | 41 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 40 |
| N2 → N3 | return await | 40 |

**Puntos de decisión:** ninguno.

### UserService.Delete

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs  
**Firma:** public async Task&lt;Result&lt;int&gt;&gt; Delete(int id)  
**Líneas:** 43–46.

~~~csharp
0043 | public async Task<Result<int>> Delete(int id)
0045 | return await userRepository.Delete(id);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await userRepository.Delete]
    N2 --> N3([Salida con resultado])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 43–44 | Entrada | Entrada |
| N2 | 45 | userRepository.Delete(id) | Delegar eliminación |
| N3 | 46 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 45 |
| N2 → N3 | return await | 45 |

**Puntos de decisión:** ninguno.


### UserService.GetAll

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs  
**Firma:** public async Task&lt;Result&lt;IEnumerable&lt;User&gt;&gt;&gt; GetAll()  
**Líneas:** 48–51.

~~~csharp
0048 | public async Task<Result<IEnumerable<User>>> GetAll()
0050 | return await userRepository.GetAll();
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await userRepository.GetAll]
    N2 --> N3([Salida con resultado])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 48–49 | Entrada | Entrada |
| N2 | 50 | userRepository.GetAll() | Delegar consulta |
| N3 | 51 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 50 |
| N2 → N3 | return await | 50 |

**Puntos de decisión:** ninguno.

### UserService.Search

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs  
**Firma:** public async Task&lt;Result&lt;IEnumerable&lt;User&gt;&gt;&gt; Search(string property)  
**Líneas:** 53–56.

~~~csharp
0053 | public async Task<Result<IEnumerable<User>>> Search(string property)
0055 | return await userRepository.Search(property);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await userRepository.Search]
    N2 --> N3([Salida con resultado])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 53–54 | Entrada | Entrada |
| N2 | 55 | userRepository.Search(property) | Delegar búsqueda |
| N3 | 56 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 55 |
| N2 → N3 | return await | 55 |

**Puntos de decisión:** ninguno.

## Clase LoginService

Los métodos siguientes delegan a ILogin; no agregan bifurcaciones.

### LoginService.ChangePassword

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs  
**Firma:** public async Task&lt;(bool ok, string? error)&gt; ChangePassword(int userId, string currentPassword, string newPassword)  
**Líneas:** 13–16.

~~~csharp
0013 | public async Task<(bool ok, string? error)> ChangePassword(int userId, string currentPassword, string newPassword)
0015 | return await _login.ChangePassword(userId, currentPassword, newPassword);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await _login.ChangePassword]
    N2 --> N3([Salida con tupla])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 13–14 | Entrada | Entrada |
| N2 | 15 | _login.ChangePassword(...) | Delegar |
| N3 | 16 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 15 |
| N2 → N3 | return await | 15 |

**Puntos de decisión:** ninguno.

### LoginService.ChangePasswordFirstLogin

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs  
**Firma:** public async Task&lt;(bool ok, string? error)&gt; ChangePasswordFirstLogin(int userId, string currentPassword, string newPassword)  
**Líneas:** 18–22.

~~~csharp
0018 | public async Task<(bool ok, string? error)> ChangePasswordFirstLogin(int userId, string currentPassword,
0019 |     string newPassword)
0021 | return await _login.ChangePasswordFirstLogin(userId, currentPassword, newPassword);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await _login.ChangePasswordFirstLogin]
    N2 --> N3([Salida con tupla])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 18–20 | Entrada | Entrada |
| N2 | 21 | _login.ChangePasswordFirstLogin(...) | Delegar |
| N3 | 22 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 21 |
| N2 → N3 | return await | 21 |

**Puntos de decisión:** ninguno.

### LoginService.ValidateLogin

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs  
**Firma:** public async Task&lt;(bool ok, int? userId, string? role, string? error, bool isFirstLogin)&gt; ValidateLogin(string username, string plainPassword)  
**Líneas:** 24–28.

~~~csharp
0024 | public async Task<(bool ok, int? userId, string? role, string? error, bool isFirstLogin)> ValidateLogin(
0025 |     string username, string plainPassword)
0027 | return await _login.ValidateLogin(username, plainPassword);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await _login.ValidateLogin]
    N2 --> N3([Salida con tupla])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 24–26 | Entrada | Entrada |
| N2 | 27 | _login.ValidateLogin(...) | Delegar validación |
| N3 | 28 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 27 |
| N2 → N3 | return await | 27 |

**Puntos de decisión:** ninguno.


## Clase RegistrationService

### RegistrationService.RegisterUser

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/RegistrationService.cs  
**Firma:** public async Task&lt;(bool ok, string? generatedUsername, string? generatedPassword, string? error)&gt; RegisterUser(string firstName, string lastName, string email, string role, int createdBy)  
**Líneas:** 25–44. Delega el registro, devuelve fallo de inmediato si ok es falso, y si es verdadero agenda un envío de correo en una tarea que no espera.

~~~csharp
0027 | var t = false;
0028 | (...) = await _registration.RegisterUser(...);
0029 | if (!ok)
0030 |     return (false, null, null, error);
0032 | _ = Task.Run(async () =>
0034 |     try
0036 |         await _emailService.SendNewUserCredentialsAsync(...);
0038 |     catch (Exception ex)
0040 |         System.Console.WriteLine(...);
0043 | return (true, generatedUsername, generatedPassword, null);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar t y await _registration.RegisterUser]
    N2 --> N3{!ok?}
    N3 -- Sí --> N4[return false con error]
    N4 --> N10([Salida del método])
    N3 -- No --> N5[Agendar Task.Run sin esperarla]
    N5 --> N9[return true con usuario y contraseña]
    N9 --> N10
    N5 -. callback asíncrono .-> N6[try: await SendNewUserCredentialsAsync]
    N6 -- normal --> N8([Fin del callback])
    N6 -- excepción --> N7[catch Exception: WriteLine]
    N7 --> N8
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 25–26 | Entrada | Entrada |
| N2 | 27–28 | Inicializar t y llamar registro | Obtener resultado |
| N3 | 29 | !ok | Decidir fallo/éxito |
| N4 | 30 | return (false, null, null, error) | Retorno anticipado |
| N5 | 32–42 | Task.Run(async callback) | Agendar tarea sin esperarla |
| N6 | 34–37 | try y envío await | Enviar credenciales |
| N7 | 38–41 | catch Exception y WriteLine | Capturar error del envío |
| N8 | 42 | Fin de callback | Fin independiente |
| N9 | 43 | return true con credenciales | Salida exitosa |
| N10 | 44 | Fin del cuerpo | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 27–28 |
| N2 → N3 | llamada completada | 29 |
| N3 → N4 | Sí | 29–30 |
| N3 → N5 | No | 32 |
| N4 → N10 | return | 30 |
| N5 → N9 | el método continúa sin esperar callback | 32–43 |
| N9 → N10 | return | 43 |
| N5 → N6 | callback agendado; ejecución separada | 32–34 |
| N6 → N8 | sin excepción | 36–37 |
| N6 → N7 | excepción capturada | 38 |
| N7 → N8 | catch completado | 40–41 |

**Puntos de decisión:** if en línea 29; catch(Exception) del callback en línea 38. El callback corre aparte: el método devuelve en línea 43 sin esperar su terminación.

## Clase JwtService

### JwtService.GenerateToken

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/JwtService.cs  
**Firma:** public async Task&lt;Result&lt;string&gt;&gt; GenerateToken(int userId, string role)  
**Líneas:** 18–41. Lee configuración JWT, crea clave, claims y token, y devuelve la serialización.

~~~csharp
0020 | IConfigurationSection jwt = _config.GetSection("Jwt");
0021 | SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
0022 | SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
0024 | List<Claim> claims = new List<Claim> { ... };
0031 | JwtSecurityToken token = new JwtSecurityToken(...);
0036 |     expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpireMinutes"])),
0040 | return Result<string>.Success(new JwtSecurityTokenHandler().WriteToken(token));
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Leer Jwt y crear clave/firma]
    N2 --> N3[Crear claims]
    N3 --> N4[Crear JwtSecurityToken]
    N4 --> N5[WriteToken y Result.Success]
    N5 --> N6([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 18–19 | Entrada | Entrada |
| N2 | 20–22 | Configuración, clave y firma | Preparar firma |
| N3 | 24–29 | Claims NameId, Role y Jti | Preparar identidad |
| N4 | 31–38 | new JwtSecurityToken(...) | Crear token y vencimiento |
| N5 | 40 | WriteToken y Result&lt;string&gt;.Success | Serializar y devolver |
| N6 | 41 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 20 |
| N2 → N3 | secuencia | 24 |
| N3 → N4 | secuencia | 31 |
| N4 → N5 | secuencia | 40 |
| N5 → N6 | return | 40 |

**Puntos de decisión:** ninguno. Las conversiones/llamadas de configuración pueden fallar en runtime y no hay catch local.

## Clase EmailService

### EmailService.SendEmailAsync

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/EmailService.cs  
**Firma:** public async Task&lt;bool&gt; SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)  
**Líneas:** 19–22.

~~~csharp
0019 | public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
0021 | return await _emailService.SendEmailAsync(toEmail, subject, body, isHtml);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await _emailService.SendEmailAsync]
    N2 --> N3([Salida bool])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 19–20 | Entrada | Entrada |
| N2 | 21 | _emailService.SendEmailAsync(...) | Delegar envío |
| N3 | 22 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 21 |
| N2 → N3 | return await | 21 |

**Puntos de decisión:** ninguno.

### EmailService.SendNewUserCredentialsAsync

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/EmailService.cs  
**Firma:** public async Task&lt;bool&gt; SendNewUserCredentialsAsync(string toEmail, string userName, string firstName, string password)  
**Líneas:** 24–88. Compone un asunto y una plantilla HTML, luego delega el envío con isHtml true.

~~~csharp
0024 | public async Task<bool> SendNewUserCredentialsAsync(string toEmail, string userName, string firstName, string password)
0026 | var subject = "Bienvenido al Sistema de Pagos de Aranceles";
0028 | var body = $@"... HTML con firstName, userName y password ...";
0087 | return await SendEmailAsync(toEmail, subject, body, isHtml: true);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar subject]
    N2 --> N3[Construir body HTML]
    N3 --> N4[Await SendEmailAsync con isHtml true]
    N4 --> N5([Salida bool])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 24–25 | Entrada | Entrada |
| N2 | 26 | Asunto | Preparar correo |
| N3 | 28–85 | Plantilla HTML interpolada | Formar cuerpo |
| N4 | 87 | SendEmailAsync(...) | Delegar envío |
| N5 | 88 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 26 |
| N2 → N3 | secuencia | 28 |
| N3 → N4 | secuencia | 87 |
| N4 → N5 | return await | 87 |

**Puntos de decisión:** ninguno.


# Proyecto Dom — MicroServicioUser.Dom

## Clase PasswordValidator

### PasswordValidator.Validate

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Rules/PasswordValidator.cs  
**Firma:** public static (bool ok, string? error) Validate(string password)  
**Líneas:** 12–21. Rechaza cadena vacía/blanca, luego rechaza una contraseña que no coincide con Strong.

~~~csharp
0012 | public static (bool ok, string? error) Validate(string password)
0014 | if (string.IsNullOrWhiteSpace(password))
0015 |     return (false, "La contraseña no puede estar vacía.");
0017 | if (!Strong.IsMatch(password))
0018 |     return (false, "La contraseña debe tener al menos 8 caracteres e incluir al menos una mayúscula, una minúscula, un número y un carácter especial.");
0020 | return (true, null);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2{IsNullOrWhiteSpace?}
    N2 -- Sí --> N3[return false: vacía]
    N2 -- No --> N4{!Strong.IsMatch?}
    N4 -- Sí --> N5[return false: no cumple regla]
    N4 -- No --> N6[return true]
    N3 --> N7([Salida])
    N5 --> N7
    N6 --> N7
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 12–13 | Entrada | Entrada |
| N2 | 14 | string.IsNullOrWhiteSpace(password) | Validar vacío |
| N3 | 15 | return false, error de vacía | Retorno anticipado |
| N4 | 17 | !Strong.IsMatch(password) | Validar regla de complejidad |
| N5 | 18 | return false, error de regla | Rechazo |
| N6 | 20 | return (true, null) | Aceptación |
| N7 | 21 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 14 |
| N2 → N3 | Sí | 14–15 |
| N2 → N4 | No | 14–17 |
| N3 → N7 | return | 15 |
| N4 → N5 | Sí | 17–18 |
| N4 → N6 | No | 17–20 |
| N5 → N7 | return | 18 |
| N6 → N7 | return | 20 |

**Puntos de decisión:** dos if con retornos anticipados, líneas 14–18.

## Clase Result

### Result.Success

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs  
**Firma:** public static Result Success()  
**Línea:** 16. Construye un resultado exitoso con lista vacía de errores.

~~~csharp
0016 | public static Result Success() => new Result(true, new List<string>());
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear Result exitoso con errores vacíos]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 16 | Entrada de expresión | Entrada |
| N2 | 16 | new Result(true, new List&lt;string&gt;()) | Construir valor |
| N3 | 16 | Resultado de expresión | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | evaluar expresión | 16 |
| N2 → N3 | expresión completada | 16 |

**Puntos de decisión:** ninguno.

### Result.Failure

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs  
**Firma:** public static Result Failure(params string[] errors)  
**Línea:** 17. Construye un resultado fallido con los errores recibidos.

~~~csharp
0017 | public static Result Failure(params string[] errors) => new Result(false, new List<string>(errors));
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear Result fallido con List de errors]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 17 | Entrada de expresión | Entrada |
| N2 | 17 | new Result(false, new List&lt;string&gt;(errors)) | Construir valor |
| N3 | 17 | Resultado de expresión | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | evaluar expresión | 17 |
| N2 → N3 | expresión completada | 17 |

**Puntos de decisión:** ninguno.

### Result&lt;T&gt;.Success

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs  
**Firma:** public static Result&lt;T&gt; Success(T value)  
**Línea:** 29. Construye un resultado genérico exitoso, conservando value.

~~~csharp
0029 | public static Result<T> Success(T value) => new Result<T>(true, value, new List<string>());
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear resultado genérico exitoso con value]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 29 | Entrada de expresión | Entrada |
| N2 | 29 | new Result&lt;T&gt;(true, value, lista vacía) | Construir valor |
| N3 | 29 | Resultado de expresión | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | evaluar expresión | 29 |
| N2 → N3 | expresión completada | 29 |

**Puntos de decisión:** ninguno.

### Result&lt;T&gt;.Failure

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs  
**Firma:** public static Result&lt;T&gt; Failure(params string[] errors)  
**Línea:** 30. Construye un resultado genérico fallido con default(T) y la lista de errores.

~~~csharp
0030 | public static Result<T> Failure(params string[] errors) => new Result<T>(false, default(T)!, new List<string>(errors));
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear resultado genérico fallido con default y errors]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 30 | Entrada de expresión | Entrada |
| N2 | 30 | new Result&lt;T&gt;(false, default(T), lista de errors) | Construir valor |
| N3 | 30 | Resultado de expresión | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | evaluar expresión | 30 |
| N2 → N3 | expresión completada | 30 |

**Puntos de decisión:** ninguno.


# Proyecto Inf — MicroServicoUser.Inf

## Clase UserRepository

Las consultas SQL se representan como asignaciones de datos; los AND/OR que aparecen dentro del texto SQL no son decisiones del flujo C#. En cada método, la excepción que ocurra dentro de try conduce al catch local.

### UserRepository.GetAll

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs  
**Firma:** public async Task&lt;Result&lt;IEnumerable&lt;User&gt;&gt;&gt; GetAll()  
**Líneas:** 21–50. Prepara SELECT de usuarios activos; devuelve colección o Error.

~~~csharp
0021 | public async Task<Result<IEnumerable<User>>> GetAll()
0023–0039 | string query = "... SELECT ... WHERE status = TRUE ...";
0040 | try
0043 | return Result<IEnumerable<User>>.Success(_dbConnectionManager.ExecuteQuery<User>(query));
0045 | catch(Exception ex)
0047 | return Result<IEnumerable<User>>.Failure(ex.Message);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Construir query]
    N2 --> N3[try: ExecuteQuery y Result.Success]
    N3 -- normal --> N5[return resultado exitoso]
    N3 -- excepción --> N4[catch Exception]
    N4 --> N6[return Result.Failure]
    N5 --> N7([Salida])
    N6 --> N7
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 21–22 | Entrada | Entrada |
| N2 | 23–39 | Asignación query SELECT | Preparar consulta |
| N3 | 40–44 | try y ExecuteQuery | Ejecutar consulta y envolver resultado |
| N4 | 45–46 | catch(Exception ex) | Capturar excepción del try |
| N5 | 43 | return Result.Success(...) | Salida normal |
| N6 | 47–48 | return Result.Failure(ex.Message) | Salida de error |
| N7 | 50 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 23 |
| N2 → N3 | secuencia | 40 |
| N3 → N5 | sin excepción | 43 |
| N3 → N4 | excepción | 45 |
| N4 → N6 | catch | 47 |
| N5 → N7 | return | 43 |
| N6 → N7 | return | 47 |

**Puntos de decisión:** catch(Exception), línea 45.

### UserRepository.Insert

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs  
**Firma:** public async Task&lt;Result&lt;int&gt;&gt; Insert(User model)  
**Líneas:** 52–91. Prepara INSERT parametrizado y devuelve filas afectadas o Error.

~~~csharp
0052 | public async Task<Result<int>> Insert(User model)
0054–0082 | string query = "... INSERT INTO user ...";
0083 | try
0085 | return Result<int>.Success(_dbConnectionManager.ExecuteParameterizedNonQuery(query, model));
0087 | catch (Exception e)
0089 | return Result<int>.Failure(e.Message);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Construir query INSERT]
    N2 --> N3[try: ExecuteParameterizedNonQuery y Success]
    N3 -- normal --> N5[return Success]
    N3 -- excepción --> N4[catch Exception]
    N4 --> N6[return Failure]
    N5 --> N7([Salida])
    N6 --> N7
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 52–53 | Entrada | Entrada |
| N2 | 54–82 | Asignación query INSERT | Preparar consulta |
| N3 | 83–86 | try y ExecuteParameterizedNonQuery | Ejecutar e interpretar |
| N4 | 87–88 | catch(Exception e) | Capturar excepción |
| N5 | 85 | return Result.Success(...) | Salida normal |
| N6 | 89–90 | return Result.Failure(e.Message) | Salida de error |
| N7 | 91 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 54 |
| N2 → N3 | secuencia | 83 |
| N3 → N5 | sin excepción | 85 |
| N3 → N4 | excepción | 87 |
| N4 → N6 | catch | 89 |
| N5 → N7 | return | 85 |
| N6 → N7 | return | 89 |

**Puntos de decisión:** catch(Exception), línea 87.

### UserRepository.Update

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs  
**Firma:** public async Task&lt;Result&lt;int&gt;&gt; Update(User model)  
**Líneas:** 92–116. Prepara UPDATE parametrizado y devuelve filas afectadas o Error.

~~~csharp
0092 | public async Task<Result<int>> Update(User model)
0094–0106 | string query = "... UPDATE user ... WHERE id = @Id";
0107 | try
0109 | return Result<int>.Success(_dbConnectionManager.ExecuteParameterizedNonQuery(query, model));
0111 | catch (Exception e)
0113 | return Result<int>.Failure(e.Message);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Construir query UPDATE]
    N2 --> N3[try: ExecuteParameterizedNonQuery y Success]
    N3 -- normal --> N5[return Success]
    N3 -- excepción --> N4[catch Exception]
    N4 --> N6[return Failure]
    N5 --> N7([Salida])
    N6 --> N7
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 92–93 | Entrada | Entrada |
| N2 | 94–106 | Asignación query UPDATE | Preparar consulta |
| N3 | 107–110 | try y ExecuteParameterizedNonQuery | Ejecutar e interpretar |
| N4 | 111–112 | catch(Exception e) | Capturar excepción |
| N5 | 109 | return Result.Success(...) | Salida normal |
| N6 | 113–114 | return Result.Failure(e.Message) | Salida de error |
| N7 | 116 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 94 |
| N2 → N3 | secuencia | 107 |
| N3 → N5 | sin excepción | 109 |
| N3 → N4 | excepción | 111 |
| N4 → N6 | catch | 113 |
| N5 → N7 | return | 109 |
| N6 → N7 | return | 113 |

**Puntos de decisión:** catch(Exception), línea 111.


### UserRepository.Search

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs  
**Firma:** public async Task&lt;Result&lt;IEnumerable&lt;User&gt;&gt;&gt; Search(string property)  
**Líneas:** 118–162. Construye un probe con property en campos buscables, luego ejecuta consulta parametrizada.

~~~csharp
0118 | public async Task<Result<IEnumerable<User>>> Search(string property)
0120–0129 | var probe = new User { Username = property, FirstName = property, ... };
0131–0153 | const string query = "... WHERE status = TRUE AND (... OR ...) ...";
0154 | try
0156 | return Result<IEnumerable<User>>.Success(_dbConnectionManager.ExecuteParameterizedQuery<User>(query, probe));
0158 | catch (Exception e)
0160 | return Result<IEnumerable<User>>.Failure(e.Message);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear probe]
    N2 --> N3[Asignar query SQL como texto]
    N3 --> N4[try: ExecuteParameterizedQuery y Success]
    N4 -- normal --> N6[return Success]
    N4 -- excepción --> N5[catch Exception]
    N5 --> N7[return Failure]
    N6 --> N8([Salida])
    N7 --> N8
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 118–119 | Entrada | Entrada |
| N2 | 120–129 | Inicializar User probe | Preparar valores |
| N3 | 131–153 | Asignar query | SQL OR es texto de consulta, no bifurcación C# |
| N4 | 154–157 | try y ExecuteParameterizedQuery | Ejecutar consulta |
| N5 | 158–159 | catch(Exception e) | Capturar excepción |
| N6 | 156 | return Result.Success(...) | Salida normal |
| N7 | 160–161 | return Result.Failure(e.Message) | Salida de error |
| N8 | 162 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 120 |
| N2 → N3 | secuencia | 131 |
| N3 → N4 | secuencia | 154 |
| N4 → N6 | sin excepción | 156 |
| N4 → N5 | excepción | 158 |
| N5 → N7 | catch | 160 |
| N6 → N8 | return | 156 |
| N7 → N8 | return | 160 |

**Puntos de decisión:** catch(Exception), línea 158. AND/OR dentro del literal SQL no son operadores C# evaluados por este método.

### UserRepository.GetById

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs  
**Firma:** public async Task&lt;Result&lt;User?&gt;&gt; GetById(int id)  
**Líneas:** 164–194. Busca usuario activo; toma FirstOrDefault y lo envuelve en Success.

~~~csharp
0164 | public async Task<Result<User?>> GetById(int id)
0166–0182 | string query = "... WHERE id = @Id AND status = TRUE LIMIT 1";
0184 | var model = new User { Id = id };
0185 | try
0187 | var res = Result<User?>.Success(_dbConnectionManager.ExecuteParameterizedQuery<User>(query, model).FirstOrDefault());
0188 | return res;
0190 | catch (Exception e)
0192 | return Result<User?>.Failure(e.Message);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar query y model]
    N2 --> N3[try: ExecuteParameterizedQuery, FirstOrDefault y Success]
    N3 -- normal --> N5[return res]
    N3 -- excepción --> N4[catch Exception]
    N4 --> N6[return Failure]
    N5 --> N7([Salida])
    N6 --> N7
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 164–165 | Entrada | Entrada |
| N2 | 166–184 | Query y User con Id | Preparar búsqueda |
| N3 | 185–187 | try; llamada y FirstOrDefault | Consultar y envolver |
| N4 | 190–191 | catch(Exception e) | Capturar excepción |
| N5 | 188 | return res | Salida normal |
| N6 | 192–193 | return Failure(e.Message) | Salida de error |
| N7 | 194 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 166 |
| N2 → N3 | secuencia | 185 |
| N3 → N5 | sin excepción | 187–188 |
| N3 → N4 | excepción | 190 |
| N4 → N6 | catch | 192 |
| N5 → N7 | return | 188 |
| N6 → N7 | return | 192 |

**Puntos de decisión:** catch(Exception), línea 190.


### UserRepository.GetByUsername

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs  
**Firma:** public async Task&lt;Result&lt;User?&gt;&gt; GetByUsername(string username)  
**Líneas:** 196–225. Consulta por username y envuelve FirstOrDefault.

~~~csharp
0196 | public async Task<Result<User?>> GetByUsername(string username)
0198–0214 | const string sql = "... WHERE username = @Username LIMIT 1";
0215 | var probe = new User { Username = username };
0216 | try
0218 | return Result<User?>.Success(_dbConnectionManager.ExecuteParameterizedQuery<User>(sql, probe).FirstOrDefault());
0221 | catch (Exception e)
0223 | return Result<User?>.Failure(e.Message);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar sql y probe]
    N2 --> N3[try: ExecuteParameterizedQuery, FirstOrDefault y Success]
    N3 -- normal --> N5[return Success]
    N3 -- excepción --> N4[catch Exception]
    N4 --> N6[return Failure]
    N5 --> N7([Salida])
    N6 --> N7
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 196–197 | Entrada | Entrada |
| N2 | 198–215 | Asignar SQL y probe | Preparar consulta |
| N3 | 216–219 | try y llamada parametrizada | Consultar y envolver |
| N4 | 221–222 | catch(Exception e) | Capturar excepción |
| N5 | 218 | return Result.Success(...) | Salida normal; el valor puede ser null |
| N6 | 223–224 | return Result.Failure(e.Message) | Salida de error |
| N7 | 225 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 198 |
| N2 → N3 | secuencia | 216 |
| N3 → N5 | sin excepción | 218 |
| N3 → N4 | excepción | 221 |
| N4 → N6 | catch | 223 |
| N5 → N7 | return | 218 |
| N6 → N7 | return | 223 |

**Puntos de decisión:** catch(Exception), línea 221.

### UserRepository.Delete

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs  
**Firma:** public async Task&lt;Result&lt;int&gt;&gt; Delete(int id)  
**Líneas:** 226–241. Realiza borrado lógico (status false) y devuelve filas afectadas o Error.

~~~csharp
0226 | public async Task<Result<int>> Delete(int id)
0228–0232 | string query = "... UPDATE user SET ... status = FALSE WHERE id = @Id";
0233 | try
0235 | return Result<int>.Success(_dbConnectionManager.ExecuteParameterizedNonQuery(query, new IdDto() { Id = id}));
0237 | catch (Exception e)
0239 | return Result<int>.Failure(e.Message);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Construir query y DTO]
    N2 --> N3[try: ExecuteParameterizedNonQuery y Success]
    N3 -- normal --> N5[return Success]
    N3 -- excepción --> N4[catch Exception]
    N4 --> N6[return Failure]
    N5 --> N7([Salida])
    N6 --> N7
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 226–227 | Entrada | Entrada |
| N2 | 228–232 | Query UPDATE status FALSE | Preparar borrado lógico |
| N3 | 233–236 | try y ExecuteParameterizedNonQuery | Ejecutar e interpretar |
| N4 | 237–238 | catch(Exception e) | Capturar excepción |
| N5 | 235 | return Result.Success(...) | Salida normal |
| N6 | 239–240 | return Result.Failure(e.Message) | Salida de error |
| N7 | 241 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 228 |
| N2 → N3 | secuencia | 233 |
| N3 → N5 | sin excepción | 235 |
| N3 → N4 | excepción | 237 |
| N4 → N6 | catch | 239 |
| N5 → N7 | return | 235 |
| N6 → N7 | return | 239 |

**Puntos de decisión:** catch(Exception), línea 237.


## Clase Login

### Login.ChangePassword

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Login.cs  
**Firma:** public async Task&lt;(bool ok, string? error)&gt; ChangePassword(int userId, string currentPassword, string newPassword)  
**Líneas:** 14–47. Busca el usuario, verifica la contraseña actual y reglas de la nueva, evita repetir la contraseña y persiste.

~~~csharp
0017 | var userResult = await _userService.GetById(userId);
0018 | if (!userResult.IsSuccess || userResult.Value is null)
0019 |     return (false, "Usuario no encontrado.");
0024 | var currentHash = RegistrationHelpers.Md5Hex(currentPassword);
0025 | if (!string.Equals(user.PasswordHash, currentHash, StringComparison.OrdinalIgnoreCase))
0026 |     return (false, "La contraseña actual no es correcta.");
0029 | var pwCheck = PasswordValidator.Validate(newPassword);
0030 | if (!pwCheck.ok) return (false, pwCheck.error);
0034 | var newHash = RegistrationHelpers.Md5Hex(newPassword);
0035 | if (string.Equals(user.PasswordHash, newHash, StringComparison.OrdinalIgnoreCase))
0036 |     return (false, "La nueva contraseña debe ser diferente a la actual.");
0039–0042 | Asignar hash/fecha y await _userService.Update(user)
0043 | if (!update.IsSuccess)
0044 |     return (false, string.Join("; ", update.Errors));
0046 | return (true, null);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await GetById]
    N2 --> N3{!IsSuccess? primer operando de ||}
    N3 -- Sí --> N5[return usuario no encontrado]
    N3 -- No; evaluar segundo operando --> N4{Value is null?}
    N4 -- Sí --> N5
    N4 -- No --> N6[Obtener user y llamar RegistrationHelpers.Md5Hex(currentPassword)]
    N6 --> N7{Hash actual distinto?}
    N7 -- Sí --> N8[return contraseña actual incorrecta]
    N7 -- No --> N9[PasswordValidator.Validate]
    N9 --> N10{!pwCheck.ok?}
    N10 -- Sí --> N11[return error de validación]
    N10 -- No --> N12[RegistrationHelpers.Md5Hex(newPassword)]
    N12 --> N13{Hash nuevo igual al actual?}
    N13 -- Sí --> N14[return nueva contraseña repetida]
    N13 -- No --> N15[Actualizar campos y await Update]
    N15 --> N16{!update.IsSuccess?}
    N16 -- Sí --> N17[return errores de Update]
    N16 -- No --> N18[return true]
    N5 --> N19([Salida])
    N8 --> N19
    N11 --> N19
    N14 --> N19
    N17 --> N19
    N18 --> N19
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 14–16 | Entrada | Entrada |
| N2 | 17 | _userService.GetById(userId) | Buscar usuario |
| N3 | 18 | !userResult.IsSuccess | Primer operando de OR |
| N4 | 18 | userResult.Value is null | Segundo operando, solo si primero es falso |
| N5 | 19 | return Usuario no encontrado | Retorno anticipado |
| N6 | 21–24 | Tomar user y llamar RegistrationHelpers.Md5Hex(currentPassword) | Preparar validación |
| N7 | 25 | !string.Equals(...) | Comparar contraseña actual |
| N8 | 26 | return contraseña incorrecta | Rechazo |
| N9 | 29 | PasswordValidator.Validate(newPassword) | Validar regla de contraseña |
| N10 | 30 | !pwCheck.ok | Seleccionar resultado |
| N11 | 31 | return pwCheck.error | Rechazo |
| N12 | 34 | RegistrationHelpers.Md5Hex(newPassword) | Preparar comparación |
| N13 | 35 | string.Equals(user.PasswordHash, newHash, ...) | Evitar reutilización |
| N14 | 36 | return nueva contraseña igual | Rechazo |
| N15 | 39–42 | Asignar hash/fecha y llamar Update | Persistir |
| N16 | 43 | !update.IsSuccess | Comprobar persistencia |
| N17 | 44 | return errores unidos | Error de persistencia |
| N18 | 46 | return (true, null) | Éxito |
| N19 | 47 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 17 |
| N2 → N3 | llamada completada | 18 |
| N3 → N5 | Sí | 18–19 |
| N3 → N4 | No; continúa evaluación de OR | 18 |
| N4 → N5 | Sí | 18–19 |
| N4 → N6 | No | 18–21 |
| N5 → N19 | return anticipado | 19 |
| N6 → N7 | secuencia | 25 |
| N7 → N8 | Sí | 25–26 |
| N7 → N9 | No | 25–29 |
| N8 → N19 | return | 26 |
| N9 → N10 | llamada completada | 30 |
| N10 → N11 | Sí | 30–31 |
| N10 → N12 | No | 30–34 |
| N11 → N19 | return | 31 |
| N12 → N13 | secuencia | 35 |
| N13 → N14 | Igual | 35–36 |
| N13 → N15 | Distinto | 35–42 |
| N14 → N19 | return | 36 |
| N15 → N16 | Update completado | 43 |
| N16 → N17 | Sí | 43–44 |
| N16 → N18 | No | 43–46 |
| N17 → N19 | return | 44 |
| N18 → N19 | return | 46 |

**Puntos de decisión:** if con OR cortocircuitado en línea 18; if de hash en línea 25; if de validación en línea 30; if de repetición en línea 35; if de persistencia en línea 43. Cada return asociado es un retorno anticipado. La segunda parte del OR solo se evalúa si la primera es falsa.


### Login.ChangePasswordFirstLogin

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Login.cs  
**Firma:** public async Task&lt;(bool ok, string? error)&gt; ChangePasswordFirstLogin(int userId, string currentPassword, string newPassword)  
**Líneas:** 49–76. Además de verificar usuario/contraseña, exige FirstLogin == 1 y lo cambia a 0 al guardar.

~~~csharp
0051 | var userResult = await _userService.GetById(userId);
0052 | if (!userResult.IsSuccess || userResult.Value is null)
0053 |     return (false, "Usuario no encontrado.");
0057 | if (user.FirstLogin != 1)
0058 |     return (false, "Este usuario ya ha cambiado su contraseña inicial.");
0060–0062 | Calcular currentHash; si no coincide, return false
0065–0067 | Calcular newHash; si coincide con hash actual, return false
0068–0072 | Asignar PasswordHash, FirstLogin=0, LastUpdate y await Update
0073 | if (!update.IsSuccess)
0074 |     return (false, string.Join("; ", update.Errors));
0075 | return (true, null);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await GetById]
    N2 --> N3{!IsSuccess? primer operando de ||}
    N3 -- Sí --> N5[return usuario no encontrado]
    N3 -- No; evaluar segundo operando --> N4{Value is null?}
    N4 -- Sí --> N5
    N4 -- No --> N6[Obtener user]
    N6 --> N7{FirstLogin distinto de 1?}
    N7 -- Sí --> N8[return ya cambió contraseña inicial]
    N7 -- No --> N9[RegistrationHelpers.Md5Hex(currentPassword)]
    N9 --> N10{Contraseña actual distinta?}
    N10 -- Sí --> N11[return contraseña actual incorrecta]
    N10 -- No --> N12[RegistrationHelpers.Md5Hex(newPassword)]
    N12 --> N13{Hash nuevo igual al actual?}
    N13 -- Sí --> N14[return nueva contraseña repetida]
    N13 -- No --> N15[Asignar hash, FirstLogin=0, fecha; await Update]
    N15 --> N16{!update.IsSuccess?}
    N16 -- Sí --> N17[return errores de Update]
    N16 -- No --> N18[return true]
    N5 --> N19([Salida])
    N8 --> N19
    N11 --> N19
    N14 --> N19
    N17 --> N19
    N18 --> N19
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 49–50 | Entrada | Entrada |
| N2 | 51 | _userService.GetById(userId) | Buscar usuario |
| N3 | 52 | !userResult.IsSuccess | Primer operando de OR |
| N4 | 52 | userResult.Value is null | Segundo operando |
| N5 | 53 | return Usuario no encontrado | Retorno anticipado |
| N6 | 55 | Obtener user | Continuar validaciones |
| N7 | 57 | user.FirstLogin != 1 | Exigir primer inicio |
| N8 | 58 | return ya cambió contraseña inicial | Rechazo |
| N9 | 60 | RegistrationHelpers.Md5Hex(currentPassword) | Preparar comparación |
| N10 | 61 | !string.Equals(...) | Comparar hash actual |
| N11 | 62 | return contraseña incorrecta | Rechazo |
| N12 | 65 | RegistrationHelpers.Md5Hex(newPassword) | Preparar comparación |
| N13 | 66 | string.Equals(...) | Detectar reutilización |
| N14 | 67 | return nueva contraseña igual | Rechazo |
| N15 | 68–72 | Actualizar contraseña, flag, fecha; llamar Update | Persistir |
| N16 | 73 | !update.IsSuccess | Comprobar resultado |
| N17 | 74 | return errores unidos | Error de persistencia |
| N18 | 75 | return (true, null) | Éxito |
| N19 | 76 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 51 |
| N2 → N3 | llamada completada | 52 |
| N3 → N5 | Sí | 52–53 |
| N3 → N4 | No; evaluar segundo operando de OR | 52 |
| N4 → N5 | Sí | 52–53 |
| N4 → N6 | No | 52–55 |
| N5 → N19 | return | 53 |
| N6 → N7 | secuencia | 57 |
| N7 → N8 | Sí | 57–58 |
| N7 → N9 | No | 57–60 |
| N8 → N19 | return | 58 |
| N9 → N10 | secuencia | 61 |
| N10 → N11 | Sí | 61–62 |
| N10 → N12 | No | 61–65 |
| N11 → N19 | return | 62 |
| N12 → N13 | secuencia | 66 |
| N13 → N14 | Igual | 66–67 |
| N13 → N15 | Distinto | 66–72 |
| N14 → N19 | return | 67 |
| N15 → N16 | Update completado | 73 |
| N16 → N17 | Sí | 73–74 |
| N16 → N18 | No | 73–75 |
| N17 → N19 | return | 74 |
| N18 → N19 | return | 75 |

**Puntos de decisión:** if con OR cortocircuitado en línea 52; if FirstLogin en línea 57; if de hash actual en línea 61; if de repetición en línea 66; if de persistencia en línea 73. La segunda expresión del OR solo se evalúa cuando la primera es falsa.


### Login.ValidateLogin

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Login.cs  
**Firma:** public async Task&lt;(bool ok, int? userId, string? role, string? error, bool isFirstLogin)&gt; ValidateLogin(string username, string plainPassword)  
**Líneas:** 78–96. Busca usuario, verifica estado y hash, normaliza un rol numérico a nombre y devuelve el indicador de primer login.

~~~csharp
0081 | var result = await _userService.GetByUsername(username);
0082 | if (result.IsFailure || !result.Value.Status) return (false, null, null, result.Errors.First(), false);
0084 | var user = result.Value;
0085 | var givenHash = RegistrationHelpers.Md5Hex(plainPassword);
0086 | if (!string.Equals(user.PasswordHash, givenHash, StringComparison.OrdinalIgnoreCase))
0087 |     return (false, null, null, "Contraseña incorrecta.", false);
0090 | var roleValue = user.Role;
0091 | if (int.TryParse(roleValue, out var code) && AuthenticationRoles.CodeToRole.ContainsKey(code))
0093 |     roleValue = AuthenticationRoles.CodeToRole[code];
0095 | return (true, user.Id, roleValue, null, user.FirstLogin == 0);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Await GetByUsername]
    N2 --> N3{result.IsFailure? primer operando de ||}
    N3 -- Sí --> N8[return fallo con Errors.First]
    N3 -- No; evaluar segundo operando --> N4{!result.Value.Status?}
    N4 -- Sí --> N8
    N4 -- No --> N6[Obtener user y llamar RegistrationHelpers.Md5Hex(plainPassword)]
    N4 -- Value null al leer Status --> N5[Excepción no capturada en línea 82]
    N5 --> N15([Salida por excepción])
    N6 --> N7{Hash distinto?}
    N7 -- Sí --> N9[return contraseña incorrecta]
    N7 -- No --> N10[Asignar roleValue]
    N10 --> N11{int.TryParse? primer operando de &&}
    N11 -- No; cortocircuita --> N13[return éxito con roleValue]
    N11 -- Sí; evaluar segundo operando --> N12{CodeToRole.ContainsKey?}
    N12 -- Sí --> N14[Convertir código a nombre de rol]
    N12 -- No --> N13
    N14 --> N13
    N8 --> N16([Salida])
    N9 --> N16
    N13 --> N16
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 78–80 | Entrada | Entrada |
| N2 | 81 | _userService.GetByUsername(username) | Buscar |
| N3 | 82 | result.IsFailure | Primer operando de OR |
| N4 | 82 | !result.Value.Status | Segundo operando de OR; solo se evalúa si IsFailure es falso |
| N5 | 82 | Acceso Value.Status si Value es null | Puede lanzar; no existe guard ni catch |
| N6 | 84–85 | Obtener user y llamar RegistrationHelpers.Md5Hex(plainPassword) | Preparar comparación |
| N7 | 86 | !string.Equals(...) | Validar contraseña |
| N8 | 82 | return con Errors.First() | Usuario no disponible/inactivo |
| N9 | 87 | return contraseña incorrecta | Rechazo |
| N10 | 90 | var roleValue = user.Role | Preparar rol |
| N11 | 91 | int.TryParse(...) | Primer operando de && |
| N12 | 91 | CodeToRole.ContainsKey(code) | Segundo operando de && |
| N13 | 95 | return éxito; isFirstLogin se calcula con FirstLogin == 0 | Salida exitosa |
| N14 | 93 | Asignar nombre desde CodeToRole | Conversión del rol |
| N15 | 82 | Excepción no capturada posible por dereferencia nula | Salida excepcional |
| N16 | 96 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 81 |
| N2 → N3 | llamada completada | 82 |
| N3 → N8 | Sí; OR omite el segundo operando | 82 |
| N3 → N4 | No; evaluar segundo operando | 82 |
| N4 → N8 | Sí | 82 |
| N4 → N6 | No | 82–85 |
| N4 → N5 | Value es null al acceder a Status | 82 |
| N5 → N15 | excepción sin catch | 82 |
| N6 → N7 | secuencia | 86 |
| N7 → N9 | Sí | 86–87 |
| N7 → N10 | No | 86–90 |
| N8 → N16 | return | 82 |
| N9 → N16 | return | 87 |
| N10 → N11 | secuencia | 91 |
| N11 → N13 | No; && cortocircuita | 91 |
| N11 → N12 | Sí; evaluar segundo operando | 91 |
| N12 → N14 | Sí | 91–93 |
| N12 → N13 | No | 91–95 |
| N14 → N13 | rol convertido | 93 |
| N13 → N16 | return | 95 |

**Puntos de decisión:** if con OR en línea 82 (segundo operando cortocircuitado); posible dereferencia nula en línea 82; if con AND en línea 91 (ContainsKey solo se evalúa si TryParse es true); if de comparación de hash en línea 86. La comparación FirstLogin == 0 de línea 95 determina el valor booleano devuelto, no selecciona otra ruta del método.


## Clases RegistrationHelpers y Registration

### RegistrationHelpers.Md5Hex

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs  
**Firma:** public static string Md5Hex(string input)  
**Líneas:** 15–26. Calcula MD5 de UTF-8 y convierte cada byte a dos dígitos hexadecimales.

~~~csharp
0015 | public static string Md5Hex(string input)
0017 | using var md5 = MD5.Create();
0018–0020 | Convertir input a bytes, calcular hash y crear StringBuilder
0021 | foreach (var b in hashBytes)
0023 | sb.Append(b.ToString("x2"));
0025 | return sb.ToString();
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear MD5, bytes, hash y StringBuilder]
    N2 --> N3{¿Queda byte en hashBytes?}
    N3 -- Sí --> N4[Append byte en formato x2]
    N4 --> N3
    N3 -- No --> N5[return sb.ToString]
    N5 --> N6([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 15–16 | Entrada | Entrada |
| N2 | 17–20 | Crear MD5, calcular hash y StringBuilder | Preparación |
| N3 | 21 | foreach sobre hashBytes | Iterar |
| N4 | 23 | Append(b.ToString("x2")) | Agregar byte |
| N5 | 25 | return sb.ToString() | Devolver hexadecimal |
| N6 | 26 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 17 |
| N2 → N3 | secuencia | 21 |
| N3 → N4 | quedan bytes | 21 |
| N3 → N5 | colección agotada | 21 |
| N4 → N3 | siguiente byte | 23 |
| N5 → N6 | return | 25 |

**Puntos de decisión:** foreach en línea 21.

**Complejidad ciclomática y pruebas de PEB-45:** el grafo tiene 6 nodos (N = 6), 6 aristas (E = 6) y un componente conectado (P = 1), por lo que `V(G) = E - N + 2P = 6 - 6 + 2(1) = 2`. También hay dos regiones (R = 2) y un nodo predicado (D = 1), así que `D + 1 = 2`; los tres métodos coinciden. La base teórica contempla el camino sin iteraciones y el camino con iteraciones. En este método, `ComputeHash` siempre produce 16 bytes, por lo que el camino de cero iteraciones no es alcanzable con una entrada válida y solo hay un camino factible. El ticket pide dos entradas para verificar dos resultados conocidos, implementadas como dos pruebas individuales en `RegistrationHelpersMd5HexTests`:

| Caso | Entrada → resultado esperado | Prueba | Cobertura ejercitada |
|---|---|---|---|
| 1 | `""` → `d41d8cd98f00b204e9800998ecf8427e` | `Md5Hex_WithEmptyInput_ReturnsKnownMd5Digest` | Calcula y formatea los 16 bytes del hash de una cadena vacía. |
| 2 | `"abc"` → `900150983cd24fb0d6963f7d28e17f72` | `Md5Hex_WithAbcInput_ReturnsKnownMd5Digest` | Calcula y formatea los 16 bytes del hash de texto UTF-8. |

Los dos casos cubren la iteración y la salida del `foreach`; ambos ejercitan el mismo camino factible, pero validan resultados diferentes. No se agrega un tercer caso artificial para intentar producir un hash vacío.

### Registration.GeneratePassword

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs  
**Firma:** public async Task&lt;string&gt; GeneratePassword(int length)  
**Líneas:** 39–51. Toma bytes aleatorios y mapea cada byte al alfabeto de contraseña.

~~~csharp
0039 | public async Task<string> GeneratePassword(int length)
0041 | const string chars = "...";
0042–0045 | Crear RNG, buffer length, llenarlo y crear StringBuilder
0046 | for (int i = 0; i < length; i++)
0048 | sb.Append(chars[bytes[i] % chars.Length]);
0050 | return sb.ToString();
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear alfabeto, RNG, buffer y StringBuilder]
    N2 --> N3{i menor que length? línea 46}
    N3 -- Sí --> N4[Agregar chars bytes[i] módulo chars.Length]
    N4 --> N3
    N3 -- No --> N5[return sb.ToString]
    N5 --> N6([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 39–40 | Entrada | Entrada |
| N2 | 41–45 | Inicializar alfabeto, RNG, bytes y StringBuilder | Preparación |
| N3 | 46 | i &lt; length | Control del for |
| N4 | 48 | Agregar carácter derivado del byte | Cuerpo del bucle |
| N5 | 50 | return sb.ToString() | Devolver contraseña |
| N6 | 51 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 41 |
| N2 → N3 | inicialización del for | 46 |
| N3 → N4 | Sí | 46 |
| N4 → N3 | incremento y reevaluación | 46–48 |
| N3 → N5 | No | 46 |
| N5 → N6 | return | 50 |

**Puntos de decisión:** condición del for en línea 46. La asignación del buffer en línea 43 puede fallar para un length no válido; el método no tiene catch local.

**Complejidad ciclomática y caminos independientes:** el grafo tiene 6 nodos (N = 6), 6 aristas (E = 6) y un componente conectado (P = 1). Por aristas y nodos, `V(G) = E - N + 2P = 6 - 6 + 2(1) = 2`. Tiene dos regiones (R = 2) y un nodo predicado (D = 1), por lo que `D + 1 = 2`. Los tres métodos coinciden: `V(G) = 2`; los caminos independientes son cero iteraciones (`N1 → N2 → N3 → N5 → N6`) y una iteración antes de salir (`N1 → N2 → N3 → N4 → N3 → N5 → N6`). Los cubren `RegistrationGeneratePasswordTests.GeneratePassword_WithZeroLength_ReturnsEmptyString` y `RegistrationGeneratePasswordTests.GeneratePassword_WithLengthOne_ReturnsCharacterFromAllowedAlphabet`, respectivamente. La segunda prueba comprueba longitud y pertenencia al alfabeto, sin depender del carácter aleatorio generado.


### Registration.RegisterUser

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs  
**Firma:** public async Task&lt;(bool ok, string? generatedUsername, string? generatedPassword, string? error)&gt; RegisterUser(string firstName, string lastName, string email, string role, int createdBy)  
**Líneas:** 53–130. Valida campos obligatorios, forma username, busca sufijo libre, genera contraseña, mapea rol, inserta usuario y devuelve resultado. Las funciones locales se grafican por separado; su declaración no ejecuta sus cuerpos.

Fragmento fuente:

~~~csharp
0055 | if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
0056 |     return (false, null, null, "Faltan datos obligatorios.");
0058–0080 | Declaraciones locales RemoveDiacritics y SlugifyLetters
0082–0083 | var firstSlug = SlugifyLetters(firstName?.Trim() ?? "");
0085 | string baseUser;
0086 | if (!string.IsNullOrEmpty(firstSlug) && !string.IsNullOrEmpty(lastSlug))
0088 |     baseUser = $"{firstSlug.Substring(0, 1)}_{lastSlug}";
0090 | else
0092–0094 | Usar segmento del email; si baseUser vacío, asignar "user"
0097–0098 | var candidate = baseUser; int suffix = 2;
0099 | while ((await _userRepository.GetByUsername(candidate)).Value != null)
0101–0102 |     candidate = baseUser + suffix.ToString(); suffix++;
0105 | var pwd = await GeneratePassword(10);
0108 | var roleCode = RoleToCode.ContainsKey(role) ? RoleToCode[role] : 0;
0110–0123 | Crear User; PasswordHash usa RegistrationHelpers.Md5Hex
0125 | var result = await _userRepository.Insert(user);
0126 | if (!result.IsSuccess)
0127 |     return (false, null, null, result.Errors.First());
0129 | return (true, candidate, pwd, null);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2{firstName en blanco? línea 55}
    N2 -- Sí --> N5[return faltan datos]
    N2 -- No; evaluar OR --> N3{lastName en blanco?}
    N3 -- Sí --> N5
    N3 -- No; evaluar OR --> N4{email en blanco?}
    N4 -- Sí --> N5
    N4 -- No --> N6[Slugify firstName y lastName]
    N5 --> N25([Salida])
    N6 --> N7{firstSlug no vacío? primer operando de &&}
    N7 -- No; cortocircuita --> N10[Fallback: slug de email]
    N7 -- Sí; evaluar segundo operando --> N8{lastSlug no vacío?}
    N8 -- Sí --> N9[baseUser = inicial_nombre]
    N8 -- No --> N10
    N9 --> N12[Asignar candidate y suffix]
    N10 --> N11{baseUser vacío?}
    N11 -- Sí --> N13[Asignar baseUser = user]
    N11 -- No --> N12
    N13 --> N12
    N12 --> N14{GetByUsername(candidate).Value != null?}
    N14 -- Sí --> N15[Actualizar candidate y suffix]
    N15 --> N14
    N14 -- No --> N16[Await GeneratePassword]
    N16 --> N17{RoleToCode contiene role? operador ?:}
    N17 -- Sí --> N18[roleCode = RoleToCode role]
    N17 -- No --> N19[roleCode = 0]
    N18 --> N20[Crear User y llamar RegistrationHelpers.Md5Hex(pwd)]
    N19 --> N20
    N20 --> N21[Await _userRepository.Insert]
    N21 --> N22{!result.IsSuccess?}
    N22 -- Sí --> N23[return false con Errors.First]
    N22 -- No --> N24[return true con candidate y pwd]
    N23 --> N25
    N24 --> N25
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 53–54 | Entrada | Entrada |
| N2 | 55 | IsNullOrWhiteSpace(firstName) | Primer operando de OR |
| N3 | 55 | IsNullOrWhiteSpace(lastName) | Segundo operando, solo si firstName no está en blanco |
| N4 | 55 | IsNullOrWhiteSpace(email) | Tercer operando, solo si los anteriores son falsos |
| N5 | 56 | return faltan datos | Retorno anticipado |
| N6 | 82–83 | SlugifyLetters de nombre/apellido recortados | Crear slugs |
| N7 | 86 | !IsNullOrEmpty(firstSlug) | Primer operando de AND |
| N8 | 86 | !IsNullOrEmpty(lastSlug) | Segundo operando de AND |
| N9 | 88 | baseUser a partir de inicial y apellido | Ruta de nombres no vacíos |
| N10 | 92–93 | Extraer parte local del email y slugify | Ruta fallback |
| N11 | 94 | IsNullOrEmpty(baseUser) | Comprobar fallback vacío |
| N12 | 94, 97–98 | Normalizar baseUser; inicializar candidate/suffix | Preparar colisión |
| N13 | 94 | baseUser = "user" | Default cuando slug de email queda vacío |
| N14 | 99 | await GetByUsername(candidate)).Value != null | Condición del while |
| N15 | 101–102 | Actualizar candidate y suffix | Buscar otro username |
| N16 | 105 | GeneratePassword(10) | Crear contraseña |
| N17 | 108 | RoleToCode.ContainsKey(role) | Condición del ?: |
| N18 | 108 | RoleToCode[role] | Rama true |
| N19 | 108 | 0 | Rama false |
| N20 | 110–123 | Crear User y llamar RegistrationHelpers.Md5Hex(pwd) para PasswordHash | Preparar inserción |
| N21 | 125 | _userRepository.Insert(user) | Persistir |
| N22 | 126 | !result.IsSuccess | Comprobar inserción |
| N23 | 127 | return false con primer error | Rechazo |
| N24 | 129 | return true con datos generados | Éxito |
| N25 | 130 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 55 |
| N2 → N5 | Sí | 55–56 |
| N2 → N3 | No; evaluar siguiente operando de OR | 55 |
| N3 → N5 | Sí | 55–56 |
| N3 → N4 | No; evaluar siguiente operando de OR | 55 |
| N4 → N5 | Sí | 55–56 |
| N4 → N6 | No | 55–58 |
| N5 → N25 | return | 56 |
| N6 → N7 | slugs calculados | 82–86 |
| N7 → N10 | No; AND cortocircuita | 86 |
| N7 → N8 | Sí; evaluar segundo operando | 86 |
| N8 → N9 | Sí | 86–88 |
| N8 → N10 | No / else | 90–93 |
| N9 → N12 | baseUser asignado | 88 |
| N10 → N11 | slug del email calculado | 92–94 |
| N11 → N13 | Sí | 94 |
| N11 → N12 | No | 94, 97 |
| N13 → N12 | fallback completado | 94, 97 |
| N12 → N14 | candidate inicializado | 97–99 |
| N14 → N15 | Sí: ya existe | 99–102 |
| N15 → N14 | repetir búsqueda | 101–102 |
| N14 → N16 | No: valor null/no existe | 99, 105 |
| N16 → N17 | contraseña generada | 105–108 |
| N17 → N18 | Sí | 108 |
| N17 → N19 | No | 108 |
| N18 → N20 | roleCode obtenido | 108–110 |
| N19 → N20 | roleCode por defecto | 108–110 |
| N20 → N21 | usuario listo | 123–125 |
| N21 → N22 | inserción completada | 126 |
| N22 → N23 | Sí | 126–127 |
| N22 → N24 | No | 126–129 |
| N23 → N25 | return | 127 |
| N24 → N25 | return | 129 |

**Puntos de decisión:** condición OR de tres operandos en línea 55 (cada operando se evalúa solo si los anteriores son falsos); condición AND en línea 86 (segundo operando solo si el primero es verdadero); if en línea 94; while en línea 99; operador condicional ?: en línea 108; if de inserción y retorno temprano en líneas 126–127. firstName/lastName/email fueron validados en línea 55 antes de las expresiones ?. / ?? de líneas 82–83 y 92; por el flujo normal, sus fallback por null no son alcanzables.


### Función local Registration.RegisterUser.RemoveDiacritics

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs  
**Firma local:** static string RemoveDiacritics(string text), declarada dentro de RegisterUser  
**Líneas:** 58–69. Normaliza a FormD, omite marcas no espaciadoras y vuelve a normalizar a FormC.

~~~csharp
0058 | static string RemoveDiacritics(string text)
0060 | if (string.IsNullOrWhiteSpace(text)) return string.Empty;
0061–0062 | Normalizar FormD y crear StringBuilder
0063 | foreach (var c in normalized)
0065 | var uc = CharUnicodeInfo.GetUnicodeCategory(c);
0066 | if (uc != UnicodeCategory.NonSpacingMark) sb.Append(c);
0068 | return sb.ToString().Normalize(NormalizationForm.FormC);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2{IsNullOrWhiteSpace?}
    N2 -- Sí --> N3[return string.Empty]
    N2 -- No --> N4[Normalizar FormD y crear StringBuilder]
    N4 --> N5{¿Queda carácter?}
    N5 -- Sí --> N6[Obtener categoría Unicode]
    N6 --> N7{Categoría distinta de NonSpacingMark?}
    N7 -- Sí --> N8[Append carácter]
    N7 -- No --> N5
    N8 --> N5
    N5 -- No --> N9[return texto normalizado FormC]
    N3 --> N10([Salida])
    N9 --> N10
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 58–59 | Entrada local | Entrada |
| N2 | 60 | string.IsNullOrWhiteSpace(text) | Validar entrada |
| N3 | 60 | return string.Empty | Retorno anticipado |
| N4 | 61–62 | Normalize(FormD), StringBuilder | Preparar recorrido |
| N5 | 63 | foreach normalized | Iterar caracteres |
| N6 | 65 | GetUnicodeCategory(c) | Obtener categoría |
| N7 | 66 | uc != NonSpacingMark | Decidir conservar |
| N8 | 66 | sb.Append(c) | Conservar carácter |
| N9 | 68 | return string normalizado FormC | Resultado |
| N10 | 69 | Fin local | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 60 |
| N2 → N3 | Sí | 60 |
| N2 → N4 | No | 60–62 |
| N3 → N10 | return | 60 |
| N4 → N5 | secuencia | 63 |
| N5 → N6 | hay carácter | 63 |
| N5 → N9 | colección agotada | 63 |
| N6 → N7 | categoría obtenida | 65 |
| N7 → N8 | Sí | 66 |
| N7 → N5 | No | 66 |
| N8 → N5 | siguiente carácter | 66 |
| N9 → N10 | return | 68 |

**Puntos de decisión:** if en línea 60; foreach en línea 63; if de categoría Unicode en línea 66.

**Complejidad ciclomática y pruebas de PEB-48:** el grafo tiene 10 nodos (N = 10), 12 aristas (E = 12) y un componente conectado (P = 1), por lo que `V(G) = E - N + 2P = 12 - 10 + 2(1) = 4`. También tiene tres nodos predicado (D = 3), así que `D + 1 = 4`; ambas medidas coinciden. Los cuatro casos del comentario se implementan como cuatro pruebas individuales en `RegistrationRemoveDiacriticsTests`:

| Caso | Entrada → resultado esperado | Prueba | Ramas ejercitadas |
|---|---|---|---|
| 1 | `" "` → `""` | `RemoveDiacritics_WithWhitespace_ReturnsEmptyString` | La guarda de texto en blanco retorna de inmediato. |
| 2 | `"a"` → `"a"` | `RemoveDiacritics_WithUnaccentedCharacter_PreservesCharacter` | Continúa tras la guarda; conserva un carácter y termina el recorrido. |
| 3 | `"á"` → `"a"` | `RemoveDiacritics_WithAccentedCharacter_RemovesDiacritic` | Conserva la letra base y omite la marca combinante producida por FormD. |
| 4 | `"\u0301"` → `""` | `RemoveDiacritics_WithStandaloneCombiningMark_RemovesCharacter` | La entrada pasa la guarda, pero la marca no se agrega al resultado. |

Las cuatro pruebas cubren las dos salidas de la guarda y las dos salidas de la condición de categoría Unicode. El recorrido del `foreach` ejecuta su cuerpo y luego sale en los casos no blancos. La arista de salida inmediata del `foreach` antes de cualquier iteración no tiene un caso realizable con una entrada no blanca que produzca una cadena normalizada vacía; por eso no se añade un quinto caso artificial. Cobertura Cobertura reporta 100 % de líneas y 100 % de ramas para `RemoveDiacritics`. Como la función es local, las pruebas invocan su método privado generado mediante reflexión y no cambian la API de producción.

### Función local Registration.RegisterUser.SlugifyLetters

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs  
**Firma local:** static string SlugifyLetters(string text), declarada dentro de RegisterUser  
**Líneas:** 71–80. Quita diacríticos, convierte a minúsculas y conserva solo letras ASCII a-z.

~~~csharp
0071 | static string SlugifyLetters(string text)
0073 | text = RemoveDiacritics(text).ToLowerInvariant();
0074 | var sb = new StringBuilder(text.Length);
0075 | foreach (var ch in text)
0077 |     if (ch is >= 'a' and <= 'z') sb.Append(ch);
0079 | return sb.ToString();
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[RemoveDiacritics y ToLowerInvariant]
    N2 --> N3{¿Queda carácter?}
    N3 -- Sí --> N4{ch entre 'a' y 'z'?}
    N4 -- No --> N3
    N4 -- Sí --> N6[Append ch]
    N6 --> N3
    N3 -- No --> N7[return sb.ToString]
    N7 --> N8([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 71–72 | Entrada local | Entrada |
| N2 | 73–74 | RemoveDiacritics(text), lowercase y StringBuilder | Preparar texto |
| N3 | 75 | foreach text | Iterar caracteres |
| N4 | 77 | ch is >= 'a' and <= 'z' | Condición completa del patrón |
| N6 | 77 | sb.Append(ch) | Conservar letra ASCII |
| N7 | 79 | return sb.ToString() | Resultado |
| N8 | 80 | Fin local | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 73 |
| N2 → N3 | texto procesado | 75 |
| N3 → N4 | hay carácter | 75 |
| N3 → N7 | no quedan caracteres | 75 |
| N4 → N3 | No | 77 |
| N4 → N6 | Sí | 77 |
| N6 → N3 | siguiente carácter | 77 |
| N7 → N8 | return | 79 |

**Puntos de decisión:** foreach en línea 75 e if con el patrón relacional completo ch is >= 'a' and <= 'z' en línea 77.

**Complejidad ciclomática y pruebas de PEB-49:** el grafo tiene 7 nodos (N = 7), 8 aristas (E = 8) y un componente conectado (P = 1): `V(G) = E - N + 2P = 8 - 7 + 2(1) = 3`. Tiene tres regiones (R = 3) y dos nodos predicado (D = 2), así que `D + 1 = 3`; los tres cálculos coinciden. Hay tres caminos independientes: cero iteraciones; una iteración que descarta un carácter; y una iteración que conserva un carácter. Los casos del comentario se implementan como tres pruebas individuales en `RegistrationSlugifyLettersTests`:

| Caso | Entrada → resultado esperado | Prueba | Camino/ramas ejercitadas |
|---|---|---|---|
| 1 | `""` → `""` | `SlugifyLetters_WithEmptyText_ReturnsEmptyString` | El `foreach` no entra y retorna vacío. |
| 2 | `"123!"` → `""` | `SlugifyLetters_WithNumbersAndSymbols_ReturnsEmptyString` | El `foreach` entra y descarta los caracteres fuera de `a-z`. |
| 3 | `"ÁBC"` → `"abc"` | `SlugifyLetters_WithAccentedUppercaseText_ReturnsLowercaseAsciiLetters` | Quita el diacrítico, convierte a minúsculas y conserva las letras válidas. |

`SlugifyLetters` es local a `RegisterUser`; las pruebas invocan su método privado generado mediante reflexión, sin cambiar la API de producción. En la ejecución con Cobertura, `SlugifyLetters` reportó 100 % de líneas y 100 % de ramas.


## Clase MySqlConnectionManager

### MySqlConnectionManager.CreateConnection

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs  
**Firma:** private MySqlConnection CreateConnection()  
**Línea:** 14. Crea la conexión usando la cadena almacenada.

~~~csharp
0014 | private MySqlConnection CreateConnection() => new(_connectionString);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[new MySqlConnection(_connectionString)]
    N2 --> N3([Salida con conexión])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 14 | Entrada de expresión | Entrada |
| N2 | 14 | new MySqlConnection(_connectionString) | Crear objeto |
| N3 | 14 | Resultado de expresión | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | evaluar expresión | 14 |
| N2 → N3 | construcción completada | 14 |

**Puntos de decisión:** ninguno.

### MySqlConnectionManager.ExecuteParameterizedQuery&lt;T&gt;

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs  
**Firma:** public IEnumerable&lt;T&gt; ExecuteParameterizedQuery&lt;T&gt;(string query, T model) where T : new()  
**Líneas:** 16–20. Llena parámetros y delega la ejecución de lectura.

~~~csharp
0016 | public IEnumerable<T> ExecuteParameterizedQuery<T>(string query, T model) where T : new()
0018 | using MySqlCommand command = DbParameterHelper.PopulateCommandParameters(query, model);
0019 | return ExecuteCommand<T>(command);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[PopulateCommandParameters]
    N2 --> N3[return ExecuteCommand genérico]
    N3 --> N4([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 16–17 | Entrada | Entrada |
| N2 | 18 | DbParameterHelper.PopulateCommandParameters(...) | Crear comando parametrizado |
| N3 | 19 | ExecuteCommand&lt;T&gt;(command) | Ejecutar lectura y devolver |
| N4 | 20 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 18 |
| N2 → N3 | comando creado | 19 |
| N3 → N4 | return | 19 |

**Puntos de decisión:** ninguno en este cuerpo. El flujo tiene 4 nodos (N = 4), 3 aristas (E = 3) y un componente conectado (P = 1): `V(G) = E - N + 2P = 3 - 4 + 2(1) = 1`; R = 1, D = 0 y D + 1 = 1. Hay un camino independiente, cubierto por `MySqlConnectionManagerExecuteParameterizedQueryTests.ExecuteParameterizedQuery_WithMatchingParameter_ReturnsMappedRow`. La prueba verifica la creación del parámetro `@Id`, el valor enviado y el mapeo de la fila de respuesta. No se expanden `PopulateCommandParameters` ni `ExecuteCommand` en este grafo.

### MySqlConnectionManager.ExecuteParameterizedQuery&lt;TOut,TParam&gt;

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs  
**Firma:** public IEnumerable&lt;TOut&gt; ExecuteParameterizedQuery&lt;TOut,TParam&gt;(string query, TParam model) where TOut : new()  
**Líneas:** 21–25. Llena parámetros con TParam y delega el mapeo a TOut.

~~~csharp
0021 | public IEnumerable<TOut> ExecuteParameterizedQuery<TOut, TParam>(string query, TParam model) where TOut : new()
0023 | using MySqlCommand command = DbParameterHelper.PopulateCommandParameters(query, model);
0024 | return ExecuteCommand<TOut>(command);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[PopulateCommandParameters]
    N2 --> N3[return ExecuteCommand con TOut]
    N3 --> N4([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 21–22 | Entrada | Entrada |
| N2 | 23 | PopulateCommandParameters(query, model) | Crear comando |
| N3 | 24 | ExecuteCommand&lt;TOut&gt;(command) | Ejecutar lectura |
| N4 | 25 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 23 |
| N2 → N3 | comando creado | 24 |
| N3 → N4 | return | 24 |

**Puntos de decisión:** ninguno en este cuerpo. El flujo tiene 4 nodos (N = 4), 3 aristas (E = 3) y un componente conectado (P = 1): `V(G) = E - N + 2P = 3 - 4 + 2(1) = 1`; R = 1, D = 0 y D + 1 = 1. Hay un camino independiente, cubierto por `MySqlConnectionManagerExecuteParameterizedQueryWithSeparateTypesTests.ExecuteParameterizedQuery_WithSeparateParameterAndResultTypes_ReturnsMappedRow`. La prueba usa un modelo `TParam` distinto de `TOut`, comprueba el parámetro `@Id` y el mapeo del resultado. No se expanden las llamadas.

### MySqlConnectionManager.ExecuteQuery&lt;T&gt;

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs  
**Firma:** public IEnumerable&lt;T&gt; ExecuteQuery&lt;T&gt;(string query) where T : new()  
**Líneas:** 27–31. Crea MySqlCommand sin parámetros y delega ejecución.

~~~csharp
0027 | public IEnumerable<T> ExecuteQuery<T>(string query) where T : new()
0029 | MySqlCommand command = new(query);
0030 | return ExecuteCommand<T>(command);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear MySqlCommand]
    N2 --> N3[return ExecuteCommand genérico]
    N3 --> N4([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 27–28 | Entrada | Entrada |
| N2 | 29 | new MySqlCommand(query) | Crear comando |
| N3 | 30 | ExecuteCommand&lt;T&gt;(command) | Ejecutar lectura |
| N4 | 31 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 29 |
| N2 → N3 | comando creado | 30 |
| N3 → N4 | return | 30 |

**Puntos de decisión:** ninguno. ExecuteCommand no se expande.

**Complejidad ciclomática y caminos independientes:** el grafo tiene 4 nodos (N = 4), 3 aristas (E = 3) y un componente conectado (P = 1). Por aristas y nodos, `V(G) = E - N + 2P = 3 - 4 + 2(1) = 1`. El grafo tiene una región (R = 1) y cero nodos predicado (D = 0), por lo que `D + 1 = 1`. Los tres métodos coinciden: `V(G) = 1`; existe un camino independiente, `N1 → N2 → N3 → N4`, cubierto por `MySqlConnectionManagerExecuteQueryTests.ExecuteQuery_WithValidQuery_ReturnsMappedRows`. El cálculo `2 - 3 + 2 = 1` anotado en el comentario del ticket no corresponde a los nodos y aristas de este grafo; se corrige aquí con los conteos reales.


### MySqlConnectionManager.ExecuteParameterizedNonQuery&lt;T&gt;

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs  
**Firma:** public int ExecuteParameterizedNonQuery&lt;T&gt;(string query, T model) where T : new()  
**Líneas:** 32–36. Crea comando parametrizado y delega ejecución no-query.

~~~csharp
0032 | public int ExecuteParameterizedNonQuery<T>(string query, T model) where T : new()
0034 | using MySqlCommand command = DbParameterHelper.PopulateCommandParameters(query, model);
0035 | return ExecuteCommand(command);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[PopulateCommandParameters]
    N2 --> N3[return ExecuteCommand]
    N3 --> N4([Salida con int])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 32–33 | Entrada | Entrada |
| N2 | 34 | Crear comando con parámetros | Preparación |
| N3 | 35 | ExecuteCommand(command) | Ejecutar y devolver filas |
| N4 | 36 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 34 |
| N2 → N3 | comando listo | 35 |
| N3 → N4 | return | 35 |

**Puntos de decisión:** ninguno en este cuerpo. Hay 4 nodos (N = 4), 3 aristas (E = 3) y un componente conectado (P = 1), así que `V(G) = E - N + 2P = 3 - 4 + 2(1) = 1`. El grafo tiene una región (R = 1), cero nodos predicado (D = 0) y `D + 1 = 1`; los tres métodos coinciden. El único camino independiente crea el comando parametrizado y devuelve las filas afectadas; lo cubre `MySqlConnectionManagerExecuteParameterizedNonQueryTests.ExecuteParameterizedNonQuery_WithExistingId_ReturnsAffectedRowCount`. No hay catch local; fallos de llamadas se propagan.

### MySqlConnectionManager.ExecuteNonQuery

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs  
**Firma:** public int ExecuteNonQuery(string query)  
**Líneas:** 38–42. Crea comando no parametrizado y delega ejecución.

~~~csharp
0038 | public int ExecuteNonQuery(string query)
0040 | using MySqlCommand command = new(query);
0041 | return ExecuteCommand(command);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear MySqlCommand]
    N2 --> N3[return ExecuteCommand]
    N3 --> N4([Salida con int])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 38–39 | Entrada | Entrada |
| N2 | 40 | new MySqlCommand(query) | Crear comando |
| N3 | 41 | ExecuteCommand(command) | Ejecutar |
| N4 | 42 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 40 |
| N2 → N3 | comando creado | 41 |
| N3 → N4 | return | 41 |

**Puntos de decisión:** ninguno. No hay catch local.

**Complejidad ciclomática y caminos independientes:** el grafo tiene 4 nodos (N = 4), 3 aristas (E = 3) y un componente conectado (P = 1). Por aristas y nodos, `V(G) = E - N + 2P = 3 - 4 + 2(1) = 1`. Tiene una región (R = 1) y cero nodos predicado (D = 0), por lo que `D + 1 = 1`. Los tres métodos coinciden: `V(G) = 1`; hay un camino independiente que crea el comando y devuelve el conteo delegado, cubierto por `MySqlConnectionManagerExecuteNonQueryTests.ExecuteNonQuery_WithUpdateQuery_ReturnsAffectedRowCount`. El comentario del ticket anotó `2 - 3 + 2 = 1`, pero esos conteos no corresponden al grafo; aquí se registran los valores reales. La prueba sustituye el ejecutor para no depender de un servidor MySQL.

### MySqlConnectionManager.ExecuteCommand(MySqlCommand)

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs  
**Firma:** protected virtual int ExecuteCommand(MySqlCommand command)
**Líneas:** 44–51. Abre conexión, ejecuta ExecuteNonQuery y devuelve filas afectadas.

~~~csharp
0044 | protected virtual int ExecuteCommand(MySqlCommand command)
0046 | using MySqlConnection connection = CreateConnection();
0047 | command.Connection = connection;
0048 | OpenConnection(connection);
0049 | int affectedRows = ExecuteMySqlCommand(command);
0050 | return affectedRows;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear conexión]
    N2 --> N3[Asignar connection al command]
    N3 --> N4[Open]
    N4 --> N5[ExecuteNonQuery]
    N5 --> N6[return affectedRows]
    N6 --> N7([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 44–45 | Entrada | Entrada |
| N2 | 46 | CreateConnection() | Crear conexión |
| N3 | 47 | command.Connection = connection | Asociar |
| N4 | 48 | connection.Open() | Abrir |
| N5 | 49 | ExecuteNonQuery() | Ejecutar |
| N6 | 50 | return affectedRows | Devolver conteo |
| N7 | 51 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 46 |
| N2 → N3 | secuencia | 47 |
| N3 → N4 | secuencia | 48 |
| N4 → N5 | secuencia | 49 |
| N5 → N6 | ejecución completada | 50 |
| N6 → N7 | return | 50 |

**Puntos de decisión:** ninguno en este cuerpo. Hay 7 nodos (N = 7), 6 aristas (E = 6) y un componente conectado (P = 1), por tanto `V(G) = E - N + 2P = 6 - 7 + 2(1) = 1`. El grafo tiene una región (R = 1), cero nodos predicado (D = 0) y `D + 1 = 1`; los tres métodos coinciden. El único camino independiente crea y asigna la conexión, la abre, ejecuta el comando y devuelve el conteo afectado. Lo cubre `MySqlConnectionManagerExecuteCommandNonQueryTests.ExecuteCommand_WithParameterizedUpdate_ReturnsAffectedRowCount`, que invoca este método a través de `ExecuteParameterizedNonQuery` y sustituye la apertura y la ejecución del driver para no requerir un servidor MySQL. Las excepciones del proveedor no se capturan en este cuerpo.

### MySqlConnectionManager.ExecuteCommand&lt;T&gt;

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs  
**Firma:** protected virtual IEnumerable&lt;T&gt; ExecuteCommand&lt;T&gt;(MySqlCommand command) where T : new()
**Líneas:** 52–58. Obtiene el DataTable y mapea sus filas al tipo T.

~~~csharp
0052 | protected virtual IEnumerable<T> ExecuteCommand<T>(MySqlCommand command) where T : new()
0054 | DataTable dataTable = LoadDataTable(command);
0056 | IEnumerable<T> results = DbMapper.MapDataTableToModelIterable<T>(dataTable);
0057 | return results;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[LoadDataTable]
    N2 --> N3[DbMapper.MapDataTableToModelIterable]
    N3 --> N4[return results]
    N4 --> N5([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 52–53 | Entrada | Entrada |
| N2 | 54 | LoadDataTable(command) | Obtener filas consultadas |
| N3 | 56 | DbMapper.MapDataTableToModelIterable&lt;T&gt; | Mapear filas |
| N4 | 57 | return results | Devolver colección |
| N5 | 58 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 54 |
| N2 → N3 | tabla cargada | 56 |
| N3 → N4 | mapeo completado | 57 |
| N4 → N5 | return | 57 |

**Puntos de decisión:** ninguno. El flujo lineal tiene 5 nodos (N = 5), 4 aristas (E = 4) y un componente conectado (P = 1). `V(G) = E - N + 2P = 4 - 5 + 2(1) = 1`; regiones R = 1; nodos predicado D = 0, por lo que D + 1 = 1. Los tres métodos coinciden. Hay un camino independiente que cubre la llamada de carga, el mapeo y el retorno; lo cubre `MySqlConnectionManagerExecuteCommandTests.ExecuteQuery_WithCompatibleDatabaseRow_ReturnsMappedModel` al invocar este método mediante `ExecuteQuery<T>`. El comentario del ticket anotó `2 - 3 + 2 = 1`, pero esos conteos no corresponden al grafo; aquí quedan registrados los conteos reales.

### MySqlConnectionManager.LoadDataTable

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs
**Firma:** protected virtual DataTable LoadDataTable(MySqlCommand command)
**Líneas:** 60–72. Ejecuta el comando con MySQL y devuelve la tabla cargada. Se deja virtual para sustituir solo el acceso externo cuando se prueba ExecuteCommand.

~~~csharp
0060 | protected virtual DataTable LoadDataTable(MySqlCommand command)
0062 | using MySqlConnection connection = CreateConnection();
0064–0065 | Asociar comando y abrir conexión
0067–0068 | Crear MySqlDataAdapter y DataTable
0069 | adapter.Fill(dataTable);
0071 | return dataTable;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear conexión]
    N2 --> N3[Asociar comando y abrir conexión]
    N3 --> N4[Crear adapter y DataTable]
    N4 --> N5[Fill]
    N5 --> N6[return dataTable]
    N6 --> N7([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 60–61 | Entrada | Entrada |
| N2 | 62 | CreateConnection() | Crear conexión |
| N3 | 64–65 | Asociar comando y abrir conexión | Preparar acceso |
| N4 | 67–68 | Crear adapter y DataTable | Preparar lectura |
| N5 | 69 | adapter.Fill(dataTable) | Cargar filas |
| N6 | 71 | return dataTable | Devolver tabla |
| N7 | 72 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 62 |
| N2 → N3 | conexión creada | 64 |
| N3 → N4 | conexión abierta | 67 |
| N4 → N5 | objetos creados | 69 |
| N5 → N6 | Fill completado | 71 |
| N6 → N7 | return | 71 |

**Puntos de decisión:** ninguno escrito. Hay 7 nodos, 6 aristas y P = 1, por tanto `V(G) = 6 - 7 + 2 = 1`; R = 1 y D + 1 = 1.

### MySqlConnectionManager.OpenConnection

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs
**Firma:** protected virtual void OpenConnection(MySqlConnection connection)
**Línea:** 74. Delegación de la apertura al proveedor; se puede sustituir en pruebas unitarias.

~~~csharp
0074 | protected virtual void OpenConnection(MySqlConnection connection) => connection.Open();
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[connection.Open]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 74 | Entrada | Entrada |
| N2 | 74 | connection.Open() | Abrir conexión |
| N3 | 74 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 74 |
| N2 → N3 | apertura completada | 74 |

**Puntos de decisión:** ninguno. `V(G) = 2 - 3 + 2(1) = 1`; R = 1 y D + 1 = 1.

### MySqlConnectionManager.ExecuteMySqlCommand

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs
**Firma:** protected virtual int ExecuteMySqlCommand(MySqlCommand command)
**Línea:** 76. Delegación de la ejecución al proveedor; se puede sustituir en pruebas unitarias.

~~~csharp
0076 | protected virtual int ExecuteMySqlCommand(MySqlCommand command) => command.ExecuteNonQuery();
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[command.ExecuteNonQuery]
    N2 --> N3([Salida con filas afectadas])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 76 | Entrada | Entrada |
| N2 | 76 | command.ExecuteNonQuery() | Ejecutar |
| N3 | 76 | Resultado de la expresión | Devolver conteo |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 76 |
| N2 → N3 | ejecución completada | 76 |

**Puntos de decisión:** ninguno. `V(G) = 2 - 3 + 2(1) = 1`; R = 1 y D + 1 = 1.


## Clase DbParameterHelper

### DbParameterHelper.PopulateCommandParameters&lt;T&gt;

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/DbParameterHelper.cs  
**Firma:** public static MySqlCommand PopulateCommandParameters&lt;T&gt;(string query, T model)  
**Líneas:** 10–41. Extrae nombres @param de query, crea comando, omite reflexión si model es null, y agrega parámetro cuando encuentra propiedad.

~~~csharp
0012–0016 | Regex.Matches, Cast/Select/Distinct/ToList para obtener parámetros
0018 | MySqlCommand command = new(query);
0020 | if (model == null)
0021 |     return command;
0023 | PropertyInfo[] modelProperties = typeof(T).GetProperties();
0025 | foreach (string paramName in parameters)
0027–0028 | Derivar modelPropName y buscar propiedad con FirstOrDefault
0030 | if (property != null)
0032 | object value = property.GetValue(model) ?? DBNull.Value;
0033 | command.Parameters.Add(new MySqlParameter(paramName, value));
0035–0037 | else vacío
0040 | return command;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Obtener nombres de parámetro y crear command]
    N2 --> N3{model == null?}
    N3 -- Sí --> N4[return command]
    N3 -- No --> N5[Obtener propiedades de T]
    N5 --> N6{¿Queda paramName?}
    N6 -- Sí --> N7[Derivar nombre y buscar propiedad]
    N7 --> N8{property != null?}
    N8 -- No --> N6
    N8 -- Sí --> N9{GetValue devuelve null? operador ??}
    N9 -- Sí --> N10[value = DBNull.Value]
    N9 -- No --> N11[Usar value recibido]
    N10 --> N12[Agregar MySqlParameter]
    N11 --> N12
    N12 --> N6
    N6 -- No --> N13[return command]
    N4 --> N14([Salida])
    N13 --> N14
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 10–11 | Entrada | Entrada |
| N2 | 12–18 | Recolectar nombres y crear command | Preparar |
| N3 | 20 | model == null | Elegir retorno |
| N4 | 21 | return command | Retorno anticipado |
| N5 | 23 | typeof(T).GetProperties() | Preparar reflexión |
| N6 | 25 | foreach parameters | Iterar nombres |
| N7 | 27–28 | Obtener modelPropName; FirstOrDefault | Buscar propiedad |
| N8 | 30 | property != null | Verificar coincidencia |
| N9 | 32 | property.GetValue(model) ?? DBNull.Value | Coalescencia nula |
| N10 | 32 | Rama null: usar DBNull.Value | Valor de parámetro |
| N11 | 32 | Rama no-null: conservar valor | Valor de parámetro |
| N12 | 33 | command.Parameters.Add(...) | Agregar parámetro |
| N13 | 40 | return command | Resultado final |
| N14 | 41 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 12 |
| N2 → N3 | command creado | 20 |
| N3 → N4 | Sí | 20–21 |
| N3 → N5 | No | 20–23 |
| N4 → N14 | return anticipado | 21 |
| N5 → N6 | propiedades obtenidas | 25 |
| N6 → N7 | quedan parámetros | 25–28 |
| N6 → N13 | colección agotada | 25 |
| N7 → N8 | búsqueda completada | 28–30 |
| N8 → N9 | Sí; leer valor y evaluar ?? | 30–32 |
| N8 → N6 | No; else vacío | 30, 35–38 |
| N9 → N10 | valor null; evaluar fallback DBNull | 32 |
| N9 → N11 | valor no-null; se omite fallback | 32 |
| N10 → N12 | fallback elegido | 32–33 |
| N11 → N12 | valor conservado | 32–33 |
| N12 → N6 | siguiente parámetro | 33 |
| N13 → N14 | return | 40 |

**Puntos de decisión:** if de null en línea 20; foreach en línea 25; if de propiedad en línea 30; operador ?? en línea 32. La expresión lambda de FirstOrDefault se mantiene dentro de la llamada referenciada.


## Clase DbMapper

### DbMapper.MapDataRowToModel&lt;T&gt;

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/DbMapper.cs  
**Firma:** public static T MapDataRowToModel&lt;T&gt;(DataRow row) where T : new()  
**Líneas:** 10–38. Recorre propiedades de T, ignora columnas ausentes, maneja DBNull según nulabilidad y convierte valores presentes.

~~~csharp
0012–0013 | Crear model y obtener propiedades
0014 | foreach (PropertyInfo property in propertyInfo)
0016 | if (row.Table.Columns.Contains(property.Name))
0018 | if (row[property.Name] == DBNull.Value)
0020 | if (Nullable.GetUnderlyingType(property.PropertyType) != null)
0022 |     property.SetValue(model, null);
0026 |     property.SetValue(model, Activator.CreateInstance(property.PropertyType));
0031 | Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
0032–0033 | Convert.ChangeType y property.SetValue
0037 | return model;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear model y obtener propiedades]
    N2 --> N3{¿Queda propiedad? foreach}
    N3 -- No --> N13[return model]
    N3 -- Sí --> N4{Columns.Contains(property.Name)?}
    N4 -- No --> N3
    N4 -- Sí --> N5{row[property.Name] == DBNull?}
    N5 -- Sí --> N6{Nullable.GetUnderlyingType != null?}
    N6 -- Sí --> N7[SetValue null]
    N6 -- No --> N8[Activar tipo valor predeterminado y SetValue]
    N7 --> N3
    N8 --> N3
    N5 -- No --> N9{UnderlyingType no-null? operador ??}
    N9 -- Sí --> N10[Usar underlying type]
    N9 -- No --> N11[Usar property.PropertyType]
    N10 --> N12[Convert.ChangeType y SetValue]
    N11 --> N12
    N12 --> N3
    N13 --> N14([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 10–11 | Entrada | Entrada |
| N2 | 12–13 | new T() y GetProperties() | Preparación |
| N3 | 14 | foreach propertyInfo | Iterar propiedades |
| N4 | 16 | Columns.Contains(property.Name) | Columna presente |
| N5 | 18 | row[property.Name] == DBNull.Value | Valor nulo de BD |
| N6 | 20 | Nullable.GetUnderlyingType(...) != null | Tipo nullable |
| N7 | 22 | SetValue(model, null) | Asignar null |
| N8 | 26 | Activator.CreateInstance y SetValue | Default de tipo no nullable |
| N9 | 31 | Nullable.GetUnderlyingType(...) ?? property.PropertyType | Resolver tipo destino |
| N10 | 31 | Rama izquierda no-null del ?? | Usar tipo subyacente |
| N11 | 31 | Rama fallback del ?? | Usar tipo declarado |
| N12 | 32–33 | Convert.ChangeType; SetValue | Convertir y asignar |
| N13 | 37 | return model | Salida normal |
| N14 | 38 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 12 |
| N2 → N3 | propiedades obtenidas | 14 |
| N3 → N4 | queda una propiedad | 14 |
| N3 → N13 | no quedan propiedades | 14 |
| N4 → N3 | No; columna ausente | 16 |
| N4 → N5 | Sí | 16–18 |
| N5 → N6 | Sí; DBNull | 18–20 |
| N5 → N9 | No; valor presente | 18–31 |
| N6 → N7 | Sí | 20–22 |
| N6 → N8 | No | 20–26 |
| N7 → N3 | asignación terminada | 22 |
| N8 → N3 | asignación terminada | 26 |
| N9 → N10 | UnderlyingType no-null; ?? usa izquierda | 31 |
| N9 → N11 | UnderlyingType null; ?? usa fallback | 31 |
| N10 → N12 | tipo elegido | 31–32 |
| N11 → N12 | tipo elegido | 31–32 |
| N12 → N3 | valor asignado | 32–33 |
| N13 → N14 | return | 37 |

**Puntos de decisión:** foreach en línea 14; if de columna en línea 16; if de DBNull en línea 18; if de tipo nullable en línea 20; operador ?? en línea 31. No hay catch: fallos de reflexión/conversión se propagan y su disparador concreto depende de los datos/runtime.

### DbMapper.MapDataTableToModelIterable&lt;T&gt;

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/DbMapper.cs  
**Firma:** public static IEnumerable&lt;T&gt; MapDataTableToModelIterable&lt;T&gt;(DataTable dataTable) where T : new()  
**Líneas:** 40–49. Mapea cada DataRow y agrega el resultado a una lista.

~~~csharp
0040 | public static IEnumerable<T> MapDataTableToModelIterable<T>(DataTable dataTable) where T : new()
0042 | List<T> resultList = [];
0043 | foreach (DataRow row in dataTable.Rows)
0045 |     T mappedObject = MapDataRowToModel<T>(row);
0046 |     resultList.Add(mappedObject);
0048 | return resultList;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear resultList]
    N2 --> N3{¿Queda DataRow? foreach}
    N3 -- Sí --> N4[MapDataRowToModel]
    N4 --> N5[Agregar mappedObject]
    N5 --> N3
    N3 -- No --> N6[return resultList]
    N6 --> N7([Salida])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 40–41 | Entrada | Entrada |
| N2 | 42 | List&lt;T&gt; resultList = [] | Inicializar |
| N3 | 43 | foreach dataTable.Rows | Iterar filas |
| N4 | 45 | MapDataRowToModel&lt;T&gt;(row) | Mapear una fila |
| N5 | 46 | resultList.Add(mappedObject) | Acumular |
| N6 | 48 | return resultList | Resultado |
| N7 | 49 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 42 |
| N2 → N3 | secuencia | 43 |
| N3 → N4 | quedan filas | 43 |
| N3 → N6 | filas agotadas | 43 |
| N4 → N5 | mapeo completado | 45–46 |
| N5 → N3 | siguiente fila | 46 |
| N6 → N7 | return | 48 |

**Puntos de decisión:** foreach en línea 43.


## Clase MySqlConnectionDB

### MySqlConnectionDB.GetConnection

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/MySqlConnectionDB.cs  
**Firma:** public MySqlConnection GetConnection()  
**Líneas:** 20–23. Construye una conexión con connectionString.

~~~csharp
0020 | public MySqlConnection GetConnection()
0022 | return new MySqlConnection(connectionString);
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Crear MySqlConnection]
    N2 --> N3([Salida con conexión])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 20–21 | Entrada | Entrada |
| N2 | 22 | new MySqlConnection(connectionString) | Construir conexión |
| N3 | 23 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 22 |
| N2 → N3 | return | 22 |

**Puntos de decisión:** ninguno.

## Clase SmtpEmailAdapter

### SmtpEmailAdapter.SendEmailAsync

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/EmailAdapters/SmtpEmailAdapter.cs  
**Firma:** public async Task&lt;bool&gt; SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)  
**Líneas:** 18–55. Valida host/usuario, construye MailMessage, configura SmtpClient, envía y convierte cualquier excepción del try en false.

~~~csharp
0020 | try
0022–0027 | Leer configuración SMTP
0029 | if (string.IsNullOrWhiteSpace(smtpHost) || string.IsNullOrWhiteSpace(smtpUser))
0032 |     return false;
0035–0044 | Construir MailMessage y configurar SmtpClient
0036 | message.From = new MailAddress(fromEmail ?? smtpUser, fromName);
0046 | await smtpClient.SendMailAsync(message);
0048 | return true;
0050 | catch (Exception ex)
0053 | return false;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[try: leer configuración]
    N2 --> N3{smtpHost en blanco? primer operando de ||}
    N3 -- Sí --> N4[return false]
    N3 -- No; evaluar segundo operando --> N5{smtpUser en blanco?}
    N5 -- Sí --> N4
    N5 -- No --> N6{fromEmail == null? operador ??}
    N6 -- Sí --> N7[Usar smtpUser como remitente]
    N6 -- No --> N8[Usar fromEmail]
    N7 --> N9[Crear MailMessage y configurar SmtpClient]
    N8 --> N9
    N9 --> N10[Await SendMailAsync]
    N10 -- normal --> N11[return true]
    N11 --> N14([Salida])
    N4 --> N14
    N10 -- excepción --> N12[catch Exception]
    N12 --> N13[return false]
    N13 --> N14
    N2 -. excepción en try .-> N12
    N3 -. excepción en try .-> N12
    N5 -. excepción en try .-> N12
    N6 -. excepción en try .-> N12
    N9 -. excepción en try .-> N12
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 18–19 | Entrada | Entrada |
| N2 | 20–27 | try y lecturas SMTP | Preparar configuración |
| N3 | 29 | IsNullOrWhiteSpace(smtpHost) | Primer operando de OR |
| N4 | 32 | return false | Salida si falta configuración |
| N5 | 29 | IsNullOrWhiteSpace(smtpUser) | Segundo operando; solo si host no está en blanco |
| N6 | 36 | fromEmail ?? smtpUser | Elegir remitente por null-coalescing |
| N7 | 36 | Rama null de fromEmail | Usar smtpUser |
| N8 | 36 | Rama no-null de fromEmail | Usar fromEmail |
| N9 | 35–44 | Crear/configurar mensaje y cliente | Preparar envío |
| N10 | 46 | await SendMailAsync(message) | Enviar |
| N11 | 48 | return true | Envío completado |
| N12 | 50–51 | catch(Exception ex) | Capturar excepción del try |
| N13 | 53 | return false | Fallo de envío/configuración |
| N14 | 55 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 20 |
| N2 → N3 | lectura completada | 29 |
| N3 → N4 | Sí; OR omite segundo operando | 29–32 |
| N3 → N5 | No; evaluar segundo operando de OR | 29 |
| N5 → N4 | Sí | 29–32 |
| N5 → N6 | No | 29, 36 |
| N6 → N7 | fromEmail null; evaluar fallback | 36 |
| N6 → N8 | fromEmail no-null | 36 |
| N7 → N9 | remitente elegido | 36–42 |
| N8 → N9 | remitente elegido | 36–42 |
| N9 → N10 | configuración completada | 46 |
| N10 → N11 | sin excepción | 46–48 |
| N10 → N12 | excepción | 50 |
| N11 → N14 | return | 48 |
| N4 → N14 | return | 32 |
| N12 → N13 | catch | 53 |
| N13 → N14 | return | 53 |
| N2 → N12 | excepción dentro de try | 20–27 |
| N3 → N12 | excepción dentro de try | 29 |
| N5 → N12 | excepción dentro de try | 29 |
| N6 → N12 | excepción dentro de try | 36 |
| N9 → N12 | excepción dentro de try | 35–44 |

**Puntos de decisión:** if con OR cortocircuitado en línea 29; operador ?? en línea 36; catch(Exception) en línea 50. Si host está en blanco, smtpUser no se evalúa. La excepción de cualquier operación representada dentro de try converge en catch.


## Bloque top-level de Program.cs (fuera del inventario de métodos)

Program.cs contiene instrucciones top-level y el compilador genera el punto de entrada; no hay una declaración de método con nombre/firma fuente para indexar. Se incluye el flujo para no omitir la condición de Development. El intervalo fuente relevante es MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Program.cs, líneas 12–110.

Fragmento de decisiones:

~~~csharp
0030 | var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
0034 | var fromName = _configuration["Email:FromName"] ?? "Sistema de Pagos";
0067 | var keyBytes = Encoding.UTF8.GetBytes(jwtSection["Key"]);
0097 | if (app.Environment.IsDevelopment())
0099–0100 | app.UseSwagger(); app.UseSwaggerUI();
0103–0110 | Middlewares, MapControllers y app.Run()
~~~

~~~mermaid
flowchart TD
    N1([Inicio top-level]) --> N2[Crear builder y registrar servicios iniciales; leer Email:SmtpPort]
    N2 --> N3{SmtpPort es null? operador ??}
    N3 -- Sí --> N4[Elegir "587"]
    N3 -- No --> N5[Elegir valor configurado]
    N4 --> N6[int.Parse]
    N5 --> N6
    N6 -- parse correcto --> N7[Leer demás Email settings]
    N6 -- formato inválido --> N20([Excepción no capturada en línea 30])
    N7 --> N8{FromName es null? operador ??}
    N8 -- Sí --> N9[Elegir "Sistema de Pagos"]
    N8 -- No --> N10[Elegir FromName configurado]
    N9 --> N11[Crear adapter/client y registrar servicios]
    N10 --> N11
    N11 --> N12[Leer Jwt y configurar autenticación/autorización]
    N12 --> N13[builder.Build]
    N13 --> N14{app.Environment.IsDevelopment?}
    N14 -- Sí --> N15[UseSwagger y UseSwaggerUI]
    N14 -- No --> N16[Omitir Swagger]
    N15 --> N17[Configurar HTTPS, Authentication, Authorization y Controllers]
    N16 --> N17
    N17 --> N18[app.Run]
    N18 --> N19([Ejecución iniciada])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 12 | Inicio de top-level statements | Entrada |
| N2 | 12–29 | Crear builder, registrar servicios y leer smtpPort | Preparación |
| N3 | 30 | Configuración SmtpPort es null | Operador ?? |
| N4 | 30 | Fallback "587" | Valor predeterminado |
| N5 | 30 | Valor configurado | Valor existente |
| N6 | 30 | int.Parse(...) | Convertir puerto |
| N7 | 31–33 | Leer otras claves SMTP | Preparar SMTP |
| N8 | 34 | FromName es null | Operador ?? |
| N9 | 34 | Fallback "Sistema de Pagos" | Valor predeterminado |
| N10 | 34 | FromName configurado | Conservar configuración |
| N11 | 35–63 | Crear adapter, cliente HTTP y registrar servicios/Swagger | Configuración DI |
| N12 | 66–92 | Leer JWT y configurar authn/authz | Seguridad |
| N13 | 94 | builder.Build() | Crear aplicación |
| N14 | 97 | app.Environment.IsDevelopment() | Elegir documentación Swagger |
| N15 | 99–100 | UseSwagger y UseSwaggerUI | Rama Development |
| N16 | 97–101 | Saltar bloque Swagger | Rama no Development |
| N17 | 103–108 | HTTPS, autenticación, autorización y controllers | Pipeline |
| N18 | 110 | app.Run() | Ejecutar |
| N19 | 110 | La ejecución queda en el host | Salida normal |
| N20 | 30 | int.Parse falla con valor no entero; no hay catch | Salida excepcional conocida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 12–29 |
| N2 → N3 | evaluar default de puerto | 30 |
| N3 → N4 | sí: valor null | 30 |
| N3 → N5 | no: conservar valor | 30 |
| N4 → N6 | puerto predeterminado | 30 |
| N5 → N6 | puerto configurado | 30 |
| N6 → N7 | parse correcto | 30–31 |
| N6 → N20 | formato de puerto inválido | 30 |
| N7 → N8 | secuencia | 34 |
| N8 → N9 | sí: usar fallback | 34 |
| N8 → N10 | no: conservar valor | 34 |
| N9 → N11 | default elegido | 34–35 |
| N10 → N11 | valor elegido | 34–35 |
| N11 → N12 | registro terminado | 66 |
| N12 → N13 | configuración lista | 94 |
| N13 → N14 | aplicación construida | 97 |
| N14 → N15 | Sí | 97–100 |
| N14 → N16 | No | 97–101 |
| N15 → N17 | Swagger configurado | 103 |
| N16 → N17 | Swagger omitido | 103 |
| N17 → N18 | pipeline configurado | 110 |
| N18 → N19 | Run invocado | 110 |

**Puntos de decisión:** coalescencia ?? de SmtpPort en línea 30; coalescencia ?? de FromName en línea 34; if de Environment.IsDevelopment en línea 97. int.Parse tiene salida excepcional si el valor no es entero. El resto de fallos de inicialización depende de llamadas/configuración en runtime y no tiene catch en este archivo.


# Constructores con cuerpo — grafos lineales

Se muestran separados de los 56 métodos porque los constructores son miembros ejecutables distintos en C#. Cada constructor tiene un grafo propio. Solo el constructor de Result incluye una decisión de coalescencia nula.

## API

### UserController.UserController

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Firma:** UserController(IRepositoryService&lt;User&gt; service, LoginService loginService, RegistrationService registrationService, IJwtService jwtService)  
**Líneas:** 42–49.

~~~csharp
0042 | public UserController(IRepositoryService<User> service, LoginService loginService, RegistrationService registrationService,
0043 |     IJwtService jwtService)
0045–0048 | Asignar service, loginService, registrationService y jwtService a campos
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar cuatro dependencias]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 42–44 | Parámetros e inicio | Entrada |
| N2 | 45–48 | Asignaciones a campos | Guardar dependencias |
| N3 | 49 | Fin del constructor | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 45–48 |
| N2 → N3 | cuerpo completado | 49 |

**Puntos de decisión:** ninguno.

### WeatherForecastController.WeatherForecastController

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/WeatherForecastController.cs  
**Firma:** WeatherForecastController(ILogger&lt;WeatherForecastController&gt; logger)  
**Líneas:** 16–19.

~~~csharp
0016 | public WeatherForecastController(ILogger<WeatherForecastController> logger)
0018 | _logger = logger;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar _logger]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 16–17 | Entrada | Entrada |
| N2 | 18 | _logger = logger | Guardar dependencia |
| N3 | 19 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 18 |
| N2 → N3 | cuerpo completado | 19 |

**Puntos de decisión:** ninguno.

## App

### UserService.UserService

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/UserService.cs  
**Firma:** UserService(IRepository userRepository)  
**Líneas:** 17–20.

~~~csharp
0017 | public UserService(IRepository userRepository)
0019 | this.userRepository = userRepository;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar userRepository]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 17–18 | Entrada | Entrada |
| N2 | 19 | Asignar dependencia | Guardar repositorio |
| N3 | 20 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 19 |
| N2 → N3 | cuerpo completado | 20 |

**Puntos de decisión:** ninguno.

### RegistrationService.RegistrationService

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/RegistrationService.cs  
**Firma:** RegistrationService(IRegistration registration, EmailService emailService)  
**Líneas:** 19–23.

~~~csharp
0019 | public RegistrationService(IRegistration registration, EmailService emailService)
0021–0022 | Asignar _registration y _emailService
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar dos dependencias]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 19–20 | Entrada | Entrada |
| N2 | 21–22 | Asignar registration y emailService | Guardar dependencias |
| N3 | 23 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 21–22 |
| N2 → N3 | cuerpo completado | 23 |

**Puntos de decisión:** ninguno.

### LoginService.LoginService

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs  
**Firma:** LoginService(ILogin login)  
**Líneas:** 8–11.

~~~csharp
0008 | public LoginService(ILogin login)
0010 | _login = login;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar _login]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 8–9 | Entrada | Entrada |
| N2 | 10 | _login = login | Guardar dependencia |
| N3 | 11 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 10 |
| N2 → N3 | cuerpo completado | 11 |

**Puntos de decisión:** ninguno.

### JwtService.JwtService

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/JwtService.cs  
**Firma:** JwtService(IConfiguration config)  
**Línea:** 16.

~~~csharp
0016 | public JwtService(IConfiguration config) => _config = config;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar _config]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 16 | Entrada de expresión | Entrada |
| N2 | 16 | _config = config | Asignación |
| N3 | 16 | Expresión completada | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | evaluar expresión | 16 |
| N2 → N3 | expresión completada | 16 |

**Puntos de decisión:** ninguno.

### EmailService.EmailService

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/EmailService.cs  
**Firma:** EmailService(IEmailService emailService)  
**Líneas:** 14–17.

~~~csharp
0014 | public EmailService(IEmailService emailService)
0016 | _emailService = emailService;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar _emailService]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 14–15 | Entrada | Entrada |
| N2 | 16 | Guardar dependencia | Asignar servicio |
| N3 | 17 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 16 |
| N2 → N3 | cuerpo completado | 17 |

**Puntos de decisión:** ninguno.


## Dom

### Result.Result

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs  
**Firma:** protected Result(bool isSuccess, List&lt;string&gt; errors)  
**Líneas:** 10–14.

~~~csharp
0010 | protected Result(bool isSuccess, List<string> errors)
0012 | IsSuccess = isSuccess;
0013 | Errors = errors ?? new List<string>();
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar IsSuccess]
    N2 --> N3{errors == null? operador ??}
    N3 -- Sí --> N4[Crear List vacía]
    N3 -- No --> N5[Conservar errors]
    N4 --> N6[Asignar Errors]
    N5 --> N6
    N6 --> N7([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 10–11 | Entrada | Entrada |
| N2 | 12 | IsSuccess = isSuccess | Asignar estado |
| N3 | 13 | errors ?? new List&lt;string&gt;() | Seleccionar lista |
| N4 | 13 | Rama null | Crear lista vacía |
| N5 | 13 | Rama no-null | Usar lista recibida |
| N6 | 13 | Errors = valor elegido | Asignar |
| N7 | 14 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 12 |
| N2 → N3 | secuencia | 13 |
| N3 → N4 | null | 13 |
| N3 → N5 | no-null | 13 |
| N4 → N6 | fallback creado | 13 |
| N5 → N6 | valor recibido | 13 |
| N6 → N7 | cuerpo completado | 14 |

**Puntos de decisión:** operador ?? en línea 13.

### Result&lt;T&gt;.Result

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs  
**Firma:** private Result(bool isSuccess, T value, List&lt;string&gt; errors)  
**Líneas:** 24–28.

~~~csharp
0024 | private Result(bool isSuccess, T value, List<string> errors)
0025 |     : base(isSuccess, errors)
0027 | Value = value;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Invocar constructor base]
    N2 --> N3[Asignar Value]
    N3 --> N4([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 24–26 | Entrada y base(isSuccess, errors) | Inicializar parte base |
| N2 | 25 | base(isSuccess, errors) | Llamada al constructor base |
| N3 | 27 | Value = value | Guardar valor |
| N4 | 28 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | constructor iniciado | 25 |
| N2 → N3 | base completado | 27 |
| N3 → N4 | cuerpo completado | 28 |

**Puntos de decisión:** ninguno en este cuerpo. La llamada base se representa sin expandirse.

## Inf

### UserRepository.UserRepository

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs  
**Firma:** UserRepository(MySqlConnectionManager dbConnectionManager)  
**Líneas:** 16–19.

~~~csharp
0016 | public UserRepository(MySqlConnectionManager dbConnectionManager)
0018 | _dbConnectionManager = dbConnectionManager;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar _dbConnectionManager]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 16–17 | Entrada | Entrada |
| N2 | 18 | Guardar dependencia | Asignación |
| N3 | 19 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 18 |
| N2 → N3 | cuerpo completado | 19 |

**Puntos de decisión:** ninguno.

### Login.Login

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Login.cs  
**Firma:** Login(IRepository repository)  
**Líneas:** 9–12.

~~~csharp
0009 | public Login(IRepository repository)
0011 | _userService = repository;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar _userService]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 9–10 | Entrada | Entrada |
| N2 | 11 | _userService = repository | Guardar dependencia |
| N3 | 12 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 11 |
| N2 → N3 | cuerpo completado | 12 |

**Puntos de decisión:** ninguno.

### Registration.Registration

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/Registration.cs  
**Firma:** Registration(IRepository userRepository)  
**Líneas:** 33–37.

~~~csharp
0033 | public Registration(IRepository userRepository)
0036 | _userRepository = userRepository;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar _userRepository]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 33–35 | Entrada | Entrada |
| N2 | 36 | Guardar dependencia | Asignación |
| N3 | 37 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 36 |
| N2 → N3 | cuerpo completado | 37 |

**Puntos de decisión:** ninguno.


### MySqlConnectionManager.MySqlConnectionManager

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/Database/MySqlConnectionManager.cs  
**Firma:** MySqlConnectionManager(IConfiguration configuration)  
**Líneas:** 10–13.

~~~csharp
0010 | public MySqlConnectionManager(IConfiguration configuration)
0012 | _connectionString = configuration.GetConnectionString("MysqlUserServicioDB");
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[GetConnectionString]
    N2 --> N3[Asignar _connectionString]
    N3 --> N4([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 10–11 | Entrada | Entrada |
| N2 | 12 | configuration.GetConnectionString(...) | Leer configuración |
| N3 | 12 | Asignar _connectionString | Guardar valor |
| N4 | 13 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 12 |
| N2 → N3 | valor obtenido | 12 |
| N3 → N4 | cuerpo completado | 13 |

**Puntos de decisión:** ninguno.

### MySqlConnectionDB.MySqlConnectionDB

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Persistence/MySqlConnectionDB.cs  
**Firma:** MySqlConnectionDB(IConfiguration configuration)  
**Líneas:** 15–18.

~~~csharp
0015 | public MySqlConnectionDB(IConfiguration configuration)
0017 | connectionString = configuration.GetConnectionString("MysqlUserServicioDB");
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[GetConnectionString]
    N2 --> N3[Asignar connectionString]
    N3 --> N4([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 15–16 | Entrada | Entrada |
| N2 | 17 | configuration.GetConnectionString(...) | Leer configuración |
| N3 | 17 | Asignar connectionString | Guardar valor |
| N4 | 18 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 17 |
| N2 → N3 | valor obtenido | 17 |
| N3 → N4 | cuerpo completado | 18 |

**Puntos de decisión:** ninguno.

### SmtpEmailAdapter.SmtpEmailAdapter

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/EmailAdapters/SmtpEmailAdapter.cs  
**Firma:** SmtpEmailAdapter(SmtpSettings smtpSettings)  
**Líneas:** 14–17.

~~~csharp
0014 | public SmtpEmailAdapter(SmtpSettings smtpSettings)
0016 | _smtpSettings = smtpSettings;
~~~

~~~mermaid
flowchart TD
    N1([Entrada]) --> N2[Asignar _smtpSettings]
    N2 --> N3([Salida])
~~~

| Nodo | Líneas | Fragmento | Acción |
|---|---:|---|---|
| N1 | 14–15 | Entrada | Entrada |
| N2 | 16 | _smtpSettings = smtpSettings | Guardar configuración |
| N3 | 17 | Fin | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | secuencia | 16 |
| N2 → N3 | cuerpo completado | 17 |

**Puntos de decisión:** ninguno.


# Propiedades calculadas con cuerpo explícito

Estas dos propiedades contienen expresiones ejecutables escritas por el equipo. Se incluyen para documentar esos cuerpos; no forman parte del conteo de 56 métodos.

## WeatherForecast.TemperatureF

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/WeatherForecast.cs  
**Miembro:** public int TemperatureF { get; } — expresión calculada  
**Línea:** 9. Convierte Celsius a Fahrenheit mediante suma, división y conversión entera.

~~~csharp
0009 | public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
~~~

~~~mermaid
flowchart TD
    N1([Entrada al getter]) --> N2[Evaluar 32 + cast TemperatureC dividido por 0.5556]
    N2 --> N3([Salida con int])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 9 | Inicio del getter calculado | Entrada |
| N2 | 9 | 32 + (int)(TemperatureC / 0.5556) | Calcular valor |
| N3 | 9 | Valor de expresión | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | evaluar expresión | 9 |
| N2 → N3 | expresión completada | 9 |

**Puntos de decisión:** ninguno; la expresión no bifurca.

## Result.IsFailure

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.Dom/Patterns/Result.cs  
**Miembro:** public bool IsFailure { get; } — expresión calculada  
**Línea:** 8. Devuelve la negación de IsSuccess.

~~~csharp
0008 | public bool IsFailure => !IsSuccess;
~~~

~~~mermaid
flowchart TD
    N1([Entrada al getter]) --> N2[Evaluar negación de IsSuccess]
    N2 --> N3([Salida bool])
~~~

| Nodo | Líneas | Fragmento o instrucción | Condición o acción |
|---|---:|---|---|
| N1 | 8 | Inicio del getter calculado | Entrada |
| N2 | 8 | !IsSuccess | Negar valor |
| N3 | 8 | Valor de expresión | Salida |

| Origen → destino | Etiqueta | Línea |
|---|---|---:|
| N1 → N2 | evaluar expresión | 8 |
| N2 → N3 | expresión completada | 8 |

**Puntos de decisión:** ninguno; la negación no selecciona una ruta del getter.

## Límites de la representación

Las implementaciones de las interfaces y todos los métodos con cuerpo de API/App/Dom/Inf están incluidos. Las funciones locales de RegisterUser tienen grafo propio y también aparecen como llamadas referenciadas en el método contenedor. No se abren los cuerpos de métodos llamados ni los callbacks de LINQ. No hay métodos cuyo flujo explícito quede indeterminado al leer estos archivos; las excepciones de proveedores/bibliotecas sin catch dependen de runtime, como se señala junto a los métodos afectados.








