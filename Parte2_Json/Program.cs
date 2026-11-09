using System;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

// Paso 3: Crear objeto con datos
var persona = new
{
    Nombre = "Cristell",
    Edad = 20,
    Carrera = "Ingeniería en Sistemas"
};

// Paso 4: Convertir objeto a cadena JSON
string json = JsonConvert.SerializeObject(persona);

Console.WriteLine(json);
