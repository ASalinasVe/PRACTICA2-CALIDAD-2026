# V(G) y casos unitarios — LoginService.ValidateLogin(string, string)

**Archivo:** MicroServices/MicroServicioUsuario.API/MicroServicioUser.App/Services/LoginService.cs  
**Líneas:** 24–28  
**Grafo de referencia:** docs/testing/usuarios/control-flow-graphs.md, sección "LoginService.ValidateLogin" (verificado contra el código actual: una sola llamada `await _login.ValidateLogin(username, plainPassword)` y un retorno, sin decisiones).  
**Pruebas:** MicroServices/MicroServicioUsuario.API/MicroServicioUsuario.Tests/LoginServiceValidateLoginTests.cs

## Grafo de flujo de control

Nodos: ENTRY/EXIT y un bloque de instrucciones. No hay decisiones, bucles, retornos anticipados ni `try/catch`.

```mermaid
flowchart TD
    ENTRY([Entrada]) --> B1[await _login.ValidateLogin]
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

**Verificación con Coverlet:** la clase de estado async `LoginService/<ValidateLogin>d__4` reporta `complexity="1"`, igual que V(G).

## Caminos independientes (conjunto base)

| Camino | Recorrido | Caso |
|---|---|---|
| P1 | ENTRY → B1 → EXIT | CP1 |

Único camino. Cubre las dos aristas del grafo.

## Casos de prueba y relación con V(G)

| ID | Prueba | Camino / justificación |
|---|---|---|
| CP1 | ValidateLogin_WhenSuccessful_ReturnsSameTupleAndForwardsArguments | P1; verifica que la tupla completa (`ok`, `userId`, `role`, `error`, `isFirstLogin`) llega sin cambios y que `username` y `plainPassword` se reenvían una sola vez. |
| CA2 | ValidateLogin_WhenRejected_ReturnsSameError | Sin camino nuevo: mismo P1 con un login rechazado; verifica que el error y los campos nulos pasan sin modificar. |

**Total de casos unitarios: 2.**

**Relación con V(G):** 2 = V(G) (1) + 1 caso adicional. CP1 cubre el único camino independiente. CA2 no abre ramas: solo verifica que el servicio no transforma la respuesta de rechazo.

**Nota sobre el alcance:** `LoginValidateLoginTests` prueba la clase `Login` de la capa Inf, que sí tiene decisiones (estado, contraseña y rol). Esta prueba cubre solo el delegado de la capa App, que es lo que el ticket pide.

## Cobertura (Coverlet, suite completa)

- Líneas 26–28 de `ValidateLogin` (clase `<ValidateLogin>d__4`): todas cubiertas.
- Ramas: 100 % (no hay ramas que cubrir).

## Ejecución

Suite completa en la rama `fix/PEB26`: 146 de 146 pruebas pasan (144 previas + 2 nuevas).
