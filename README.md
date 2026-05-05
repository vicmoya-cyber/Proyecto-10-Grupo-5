Voz Comunitaria

Modulo: Programación II (SIS – 0122)

Universidad: Universidad Privado Domingo Savio 

Gestión: 2026

Integrantes:

-Juan Andres Anagua Tarquino

-Joneyli Rous Condarco Luna 

-Sebastian Israel Corrales Mattos

-Victor Jose Moya Oliver

-Adamari Eliana Villena Lascano

Docente: Lic. Andres Grover Albino Chambi

Fecha de entrega: 07 de mayo de 2026

Planteamiento del problema

En muchas comunidades persiste una desigualdad en el acceso a la información y una falta de mecanismos organizados de participación ciudadana. Los procesos de propuestas, votaciones y reportes suelen realizarse de manera manual, lo que genera desconfianza, desorganización y poca transparencia en la toma de decisiones colectivas.  

Actualmente, los ciudadanos no cuentan con una herramienta digital que les permita registrarse de forma única y segura, presentar propuestas categorizadas, emitir votos verificables y consultar el estado de las iniciativas de manera accesible. Esta carencia provoca que las decisiones comunitarias se vean limitadas por la desinformación, duplicidad de registros y ausencia de estadísticas confiables sobre la participación. 

El problema central radica en que la comunidad carece de un sistema que automatice y organice la gestión de la participación ciudadana, garantizando la validez de los datos, la transparencia en los resultados y la accesibilidad para todos los actores involucrados (ciudadanos, administradores y comunidad en general).

Objetivos

Objetivo general

Implementar un sistema de gestión de participación ciudadana denominada “Voz-Comunitaria” donde permite realizar registros, votaciones y hacer un seguimiento de propuestas sociales de manera organizada.

Objetivo especifico

1.	Establecer el uso de estructuras para mantener coherencia en el programa a desarrollar de manera lógica y accesible.
   
2.	Realizar matrices y arreglos unidimensionales para organizar las ideas de las propuestas ciudadanas verificando el estado en que se encuentren.
   
3.	Validar la entrada de datos mediante el guardado en registros para evitar confusiones y posibles ediciones ilegitimas.

Desarrollo del proyecto
 
Tecnologías utilizadas

•	Lenguaje: C# 14

•	Framework: .NET 10

•	IDE: Visual Studio 2022 / VS Code

•	Control de Versiones: Git + GitLab

•	Persistencia: Archivos CSV y TXT

Estructura de datos implementada

Records/structs

public record Ciudadano(

    string Id,
    
    string NombreCompleto,
    
    string CI,

    string ZonaBarrio,
    
    string Telefono,
    
    DateTime FechaRegistro
    
);

public record Propuesta(

    string NumeroPropuesta,
    
    string Titulo,
    
    string Descripcion,
    
    string Categoria, // "Infraestructura", "Seguridad", "Educación", "Salud", "Medio Ambiente"
    
    string AutorId,
    
    DateTime FechaPresentacion,
    
    string Estado, // "Pendiente", "En Evaluación", "Aprobada", "Rechazada", "En Ejecución"
    
    int VotosFavor,
    
    int VotosContra,
    
    DateTime? FechaResolucion
    
);

public record ConsultaPublica(

    string CodigoConsulta,
    
    string Tema,
    
    DateTime FechaInicio,
    
    DateTime FechaFin,
    
    int TotalParticipantes,
    
    string Resultado
    
);
