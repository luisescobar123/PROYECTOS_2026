using System;

namespace BibliotecaApp_Evaluacion
{
    public class Libro
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public decimal PrecioAlquiler { get; set; }
        public int CopiasDisponibles { get; set; }
        public string Estado { get; set; }
        public int AutorId { get; set; }
        public int VecesPrestado { get; set; }

        public override string ToString()
        {
            return $"{Codigo} - {Titulo} | Género: {Genero} | Precio alquiler: {PrecioAlquiler:C} | Copias: {CopiasDisponibles} | Estado: {Estado} | VecesPrestado: {VecesPrestado}";
        }
    }
}
