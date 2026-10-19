# V(G) y casos unitarios — UserRepository.GetById(int)

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicoUser.Inf/Repository/UserRepository.cs  
**Líneas:** 164–194  
**Grafo de referencia:** docs/testing/usuarios/control-flow-graphs.md, sección "UserRepository.GetById" (verificado contra el código actual: query con `WHERE id = @Id AND status = TRUE LIMIT 1`, `try` que envuelve la consulta y `FirstOrDefault`, `catch (Exception)` y los dos retornos).  
**Pruebas:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario.Tests/UserRepositoryGetByIdTests.cs

## Grafo de flujo de control

Nodos: ENTRY/EXIT, bloque de preparación de la consulta, el `try` como nodo de decisión (normal o excepción), el retorno normal y el `catch`. El `catch` se conecta desde el `try`, como indica la convención del documento de referencia para excepciones. La condición `status = TRUE` vive en SQL, no en C#, así que no es una decisión del grafo.

```mermaid
flowchart TD
    ENTRY([Entrada]) --> B1[Asignar query y model con Id]
    B1 --> TRY{try: ExecuteParameterizedQuery, FirstOrDefault; ¿excepción?}
    TRY -- Normal --> OK[return Success res]
    TRY -- Excepción --> CATCH[catch Exception: return Failure e.Message]
    OK --> EXIT([Salida])
    CATCH --> EXIT
```

## Valores de V(G) por los tres métodos

Grafo completo con ENTRY y EXIT, un único componente conexo (P = 1).

| Magnitud | Valor | Cálculo |
|---|---:|---|
| N (nodos) | 6 | ENTRY, B1, TRY, OK, CATCH, EXIT |
| E (aristas) | 6 | ENTRY→B1, B1→TRY, TRY→OK, TRY→CATCH, OK→EXIT, CATCH→EXIT |
| P (componentes) | 1 | — |
| Regiones | 2 | E − N + 2 = 2 caras: externa y una interna sin ciclos cerrados, delimitada por TRY-OK-EXIT-CATCH-TRY |
| Nodos predicado | 1 | TRY (normal vs. excepción); predicados + 1 = 2 |
| **V(G) = E − N + 2P** | **2** | 6 − 6 + 2 |
| **V(G) = regiones** | **2** | |
| **V(G) = predicados + 1** | **2** | 1 + 1 |

**Discrepancias:** ninguna entre los tres cálculos de fuente. Coverlet reporta `complexity="1"` para la clase de estado `<GetById>d__6`: no cuenta el `catch` como ramificación. Es la misma diferencia de herramienta que en `RegistrationService.RegisterUser`, documentada allí.

## Caminos independientes (conjunto base)

| Camino | Recorrido | Caso |
|---|---|---|
| P1 | ENTRY → B1 → TRY(normal) → OK → EXIT | CP1 |
| P2 | ENTRY → B1 → TRY(excepción) → CATCH → EXIT | CP2 |

Los dos caminos cubren las seis aristas del grafo.

## Casos de prueba y relación con V(G)

| ID | Prueba | Camino / justificación |
|---|---|---|
| CP1 | GetById_WhenActiveUserExists_ReturnsSuccessWithUser | P1; verifica que el resultado envuelve el mismo usuario, que la consulta contiene `WHERE id = @Id AND status = TRUE` y que el id llega en el modelo. |
| CP2 | GetById_WhenQueryThrows_ReturnsFailureWithExceptionMessage | P2; verifica que la excepción no escapa y que su mensaje se devuelve como error. |
| CA3 | GetById_WhenNoActiveUserMatches_ReturnsSuccessWithNull | Sin camino nuevo: mismo P1 con la consulta sin filas; `FirstOrDefault` devuelve `null` y el resultado sigue siendo éxito. |

**Total de casos unitarios: 3.**

**Relación con V(G):** 3 = V(G) (2) + 1 caso adicional. CP1 y CP2 cubren los dos caminos independientes. CA3 no abre ramas: el método no tiene decisión para "no encontrado", y la ausencia de filas se resuelve dentro de `FirstOrDefault`.

**Nota sobre el alcance:** la prueba usa un `IMySqlConnectionManager` falso. No se prueba el SQL real ni el mapeo de columnas, que pertenecen a la base de datos y a `DbMapper`.

## Cobertura (Coverlet, suite completa)

- Líneas 165–194 de `GetById` (clase `<GetById>d__6`): todas cubiertas, incluido el `catch` (líneas 190–192, 1 acierto).
- Ramas: 100 % (no hay ramas de C# que cubrir; el `catch` queda acreditado por cobertura de líneas).

## Ejecución

Suite completa en la rama `fix/PEB39`: 123 de 123 pruebas pasan (120 previas + 3 nuevas).
