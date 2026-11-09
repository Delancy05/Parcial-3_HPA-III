using System;

// ===== 1. CLASE ABSTRACTA (molde obligatorio) =====
abstract class Figura
{
    public abstract double CalcularArea();
    public abstract double CalcularPerimetro();
}

// ===== 2. CLASES DERIVADAS (desarrollan los métodos abstractos) =====
class Rectangulo : Figura
{
    private double _base, _altura;

    public Rectangulo(double b, double h)
    {
        _base = b;
        _altura = h;
    }

    public override double CalcularArea() => _base * _altura;
    public override double CalcularPerimetro() => 2 * (_base + _altura);
}

class Cuadrado : Figura
{
    private double _lado;

    public Cuadrado(double lado)
    {
        _lado = lado;
    }

    public override double CalcularArea() => _lado * _lado;
    public override double CalcularPerimetro() => 4 * _lado;
}

class Triangulo : Figura
{
    private double _a, _b, _c;

    public Triangulo(double a, double b, double c)
    {
        _a = a;
        _b = b;
        _c = c;
    }

    // Fórmula de Herón
    public override double CalcularArea()
    {
        double s = CalcularPerimetro() / 2;
        return Math.Sqrt(s * (s - _a) * (s - _b) * (s - _c));
    }

    public override double CalcularPerimetro() => _a + _b + _c;
}

class Program
{
    static void Main()
    {
        // ===== 3. ARREGLO UNIDIMENSIONAL DE TIPO PADRE =====
        Figura[] figuras = new Figura[3];
        figuras[0] = new Rectangulo(4, 5);
        figuras[1] = new Cuadrado(3);
        figuras[2] = new Triangulo(3, 4, 5);

        // ===== 4. RECORRIDO POLIMÓRFICO =====
        Console.WriteLine("=== FIGURAS GEOMÉTRICAS ===\n");
        for (int i = 0; i < figuras.Length; i++)
        {
            Figura f = figuras[i]; // variable del padre, objeto hijo
            Console.WriteLine($"Clase:     {f.GetType().Name}");
            Console.WriteLine($"Área:      {f.CalcularArea():F2}");
            Console.WriteLine($"Perímetro: {f.CalcularPerimetro():F2}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
