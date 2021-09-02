using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AltalizaApi.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
