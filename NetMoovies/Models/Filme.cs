using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetMoovies.Models
{

    public class Filme
    {

        public int Id { get; set; }

        [StringLength(60, MinimumLength =3)]
        [Required]
        public string Titulo { get; set; }

        [Display(Name = "Data de Lançamento")]
        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }


        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        [Display(Name = "Gênero")]
        public string Genero { get; set; }

        [Range(1,100)]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(5)]
        [Required]
        [Display(Name = "Classificação")]
        public string Classificacao { get; set; }

    }
}
