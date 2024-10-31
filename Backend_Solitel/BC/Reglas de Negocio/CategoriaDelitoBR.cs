using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BC.Reglas_de_Negocio
{
    public static class CategoriaDelitoBR
    {
        // Método para validar que el nombre no sea vacío, ni contenga solo espacios, ni tenga caracteres especiales
        // Además, que tenga una longitud máxima de 50 caracteres
        public static void ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío o contener solo espacios.");
            }

            // Verificar que la longitud del nombre no exceda los 50 caracteres
            if (nombre.Length > 50)
            {
                throw new ArgumentException("El nombre no puede tener más de 50 caracteres.");
            }

            // Expresión regular que permite letras (incluyendo tildes), espacios y guiones
            string patron = @"^[a-zA-Z0-9À-ÿ\s'-]+$";

            if (!Regex.IsMatch(nombre, patron))
            {
                throw new ArgumentException("El nombre contiene caracteres inválidos. Solo se permiten letras, espacios, tildes y guiones.");
            }
        }

        // Método para validar que la descripción no esté vacía ni consista únicamente en espacios
        // Además, que tenga una longitud máxima de 255 caracteres
        public static void ValidarDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("La descripción no puede estar vacía o contener solo espacios.");
            }

            // Verificar que la longitud de la descripción no exceda los 255 caracteres
            if (descripcion.Length > 255)
            {
                throw new ArgumentException("La descripción no puede tener más de 255 caracteres.");
            }
        }

        // Método general para validar toda la clase CategoriaDelito
        public static void ValidarCategoriaDelito(CategoriaDelito categoriaDelito)
        {
            if (categoriaDelito == null)
            {
                throw new ArgumentNullException(nameof(categoriaDelito), "El objeto CategoriaDelito no puede ser nulo.");
            }

            // Validar el nombre
            ValidarNombre(categoriaDelito.Nombre);

            // Validar la descripción
            ValidarDescripcion(categoriaDelito.Descripcion);
        }
    }
}
