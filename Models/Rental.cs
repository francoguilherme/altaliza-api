using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AltalizaApi.Models
{
    public partial class Rental
    {
        public int Id { get; set; }
        public int IdPersonagem { get; set; }
        public int IdVeiculo { get; set; }
        public DateTime DataExpiracao { get; set; }
    }

    public partial class RentalRequest
    {
        public int IdVeiculo { get; set; }
        public DateTime DataExpiracao { get; set; }
        public int Preco { get; set; }
    }

    public partial class RenewRentalRequest
    {
        public int Dias { get; set; }
        public int Preco { get; set; }
    }
}
