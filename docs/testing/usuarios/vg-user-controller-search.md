# V(G) y casos unitarios — UserController.Search(string)

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario/Controllers/UserController.cs  
**Líneas:** 125–140  
**Grafo de referencia:** docs/testing/usuarios/control-flow-graphs.md, sección "UserController.Search" (verificado contra el código actual: coincide en la llamada a `service.Search(property)`, la decisión `IsSuccess` y los dos retornos).  
**Pruebas:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario.Tests/UserControllerSearchTests.cs

## Grafo de flujo de control

Nodos: ENTRY/EXIT, bloques de instrucciones y una decisión. No hay bucles, retornos anticipados ni `try/catch`.

```mermaid
flowchart TD
    ENTRY([Entrada]) --> B1[await service.Search property]
    B1 --> D1{categories.IsSuccess?}
    D1 -- Sí --> B2[return Ok categories.Value]
    D1 -- No --> B3[return StatusCode 500 con message y errors]
    B2 --> EXIT([Salida])
    B3 --> EXIT
```

## Valores de V(G) por los tres métodos

Grafo completo con ENTRY y EXIT, un único componente conexo (P = 1).

| Magnitud | Valor | Cálculo |
|---|---:|---|
| N (nodos) | 6 | ENTRY, B1, D1, B2, B3, EXIT |
| E (aristas) | 6 | ENTRY→B1, B1→D1, D1→B2, D1→B3, B2→EXIT, B3→EXIT |
| P (componentes) | 1 | — |
| Regiones | 2 | 1 interna (D1-B2-EXIT-B3) + 1 externa |
| Nodos predicado | 1 | D1; predicados + 1 = 2 |
| **V(G) = E − N + 2P** | **2** | 6 − 6 + 2 |
| **V(G) = regiones** | **2** | |
| **V(G) = predicados + 1** | **2** | 1 + 1 |

**Discrepancias:** ninguna. Los tres cálculos coinciden en 2.

**Verificación con Coverlet:** la clase de estado async `UserController/<Search>d__10` reporta `complexity="2"`, igual que V(G).

## Caminos independientes (conjunto base)

| Camino | Recorrido | Caso |
|---|---|---|
| P1 | ENTRY → B1 → D1(Sí) → B2 → EXIT | CP1 |
| P2 | ENTRY → B1 → D1(No) → B3 → EXIT | CP2 |

Cada arista aparece en al menos un camino. Con 2 caminos, V(G) = 2.

## Casos de prueba y relación con V(G)

| ID | Prueba | Camino / justificación |
|---|---|---|
| CP1 | Search_WhenServiceSucceeds_ReturnsOkWithUsers | P1; además verifica que el término llega al servicio. |
| CP2 | Search_WhenServiceFails_ReturnsInternalServerErrorWithMessageAndErrors | P2 |
| CA3 | Search_WhenServiceFailsWithSeveralErrors_ReturnsAllErrorsInOrder | Sin camino nuevo: mismo camino P2 con varios errores; verifica que se conservan todos en orden. |
| CA4 | Search_WhenTermHasSpaces_ForwardsTermUnchanged | Sin camino nuevo: mismo camino P1; caracteriza que el término no se recorta ni normaliza. |

**Total de casos unitarios: 4.**

**Relación con V(G):** 4 = V(G) (2) + 2 casos adicionales. Los dos primeros cubren los dos caminos independientes. Los adicionales no abren ramas nuevas: verifican la transmisión de errores y el tratamiento del término de búsqueda.

## Cobertura (Coverlet, suite completa)

- Líneas de `Search` (clase `<Search>d__10`): todas las líneas ejecutables cubiertas.
- Ramas: 100 % (2/2) en la línea 128 (`IsSuccess`).

## Ejecución

Suite completa en la rama `test/usuarios-api-usercontroller-search-vg`: 99 de 99 pruebas pasan.
