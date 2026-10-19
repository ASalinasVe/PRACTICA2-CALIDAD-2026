# V(G) y casos unitarios — LoginService.ChangePassword(int, string, string)

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs  
**Líneas:** 13–16  
**Grafo de referencia:** docs/testing/usuarios/control-flow-graphs.md, sección "LoginService.ChangePassword" (verificado contra el código actual: una sola llamada `await _login.ChangePassword(...)` y un retorno, sin decisiones).  
**Pruebas:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario.Tests/LoginServiceChangePasswordTests.cs

## Grafo de flujo de control

Nodos: ENTRY/EXIT y un bloque de instrucciones. No hay decisiones, bucles, retornos anticipados ni `try/catch`.

```mermaid
flowchart TD
    ENTRY([Entrada]) --> B1[await _login.ChangePassword]
    B1 --> EXIT([Salida con tupla])
```

## Valores de V(G) por los tres métodos

Grafo completo con ENTRY y EXIT, un único componente conexo (P = 1).

| Magnitud | Valor | Cálculo |
|---|---:|---|
| N (nodos) | 3 | ENTRY, B1, EXIT |
| E (aristas) | 2 | ENTRY→B1, B1→EXIT |
| P (componentes) | 1 | — |
| Regiones | 1 | Sin ciclos: solo la región externa |
| Nodos predicado | 0 | Ninguno; predicados + 1 = 1 |
| **V(G) = E − N + 2P** | **1** | 2 − 3 + 2 |
| **V(G) = regiones** | **1** | |
| **V(G) = predicados + 1** | **1** | 0 + 1 |

**Discrepancias:** ninguna. Los tres cálculos coinciden en 1.

**Verificación con Coverlet:** la clase de estado async `LoginService/<ChangePassword>d__2` reporta `complexity="1"`, igual que V(G).

## Caminos independientes (conjunto base)

| Camino | Recorrido | Caso |
|---|---|---|
| P1 | ENTRY → B1 → EXIT | CP1 |

Único camino. Cubre las dos aristas del grafo.

## Casos de prueba y relación con V(G)

| ID | Prueba | Camino / justificación |
|---|---|---|
| CP1 | ChangePassword_WhenSuccessful_ReturnsSuccessAndForwardsArguments | P1; verifica que el resultado llega sin cambios y que `userId`, contraseña actual y nueva se reenvían tal cual, una sola vez. |
| CA2 | ChangePassword_WhenItFails_ReturnsSameError | Sin camino nuevo: mismo P1 con `ILogin` fallido; verifica que el mensaje de error pasa sin modificar. |

**Total de casos unitarios: 2.**

**Relación con V(G):** 2 = V(G) (1) + 1 caso adicional. CP1 cubre el único camino independiente. CA2 no abre ramas: solo verifica que el servicio no transforma el error del repositorio de login.

## Cobertura (Coverlet, suite completa)

- Líneas 14–16 de `ChangePassword` (clase `<ChangePassword>d__2`): todas cubiertas.
- Ramas: 100 % (no hay ramas que cubrir).

## Ejecución

Suite completa en la rama `fix/PEB24`: 116 de 116 pruebas pasan (114 previas + 2 nuevas).
