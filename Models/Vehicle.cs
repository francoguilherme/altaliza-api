using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AltalizaApi.Models
{
    public partial class Vehicle
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public int Estoque { get; set; }
        public string Imagem { get; set; }
        public int Preco1Dia { get; set; }
        public int Preco7Dia { get; set; }
        public int Preco15Dia { get; set; }
    }

    public partial class UserVehicle
    {
        public int RentalId { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public int Preco1Dia { get; set; }
        public int Preco7Dia { get; set; }
        public int Preco15Dia { get; set; }
        public bool Disponivel { get; set; }
    }
}
