﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulos
    {
        public int Id { get; set; }

        [DisplayName("Código")] //se usa para cambiarle el nombre a la columna en el data grid view
        public string Codigo { get; set; }

        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Imagen")]
        public string ImagenUrl{ get; set; }

        public float Precio{ get; set; }
        [DisplayName("Marca")]
        public Marcas IdMarca{ get; set; }
        [DisplayName("Categoría")]
        public Categorias IdCategoria{ get; set; }
    }
}
