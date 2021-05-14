using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMoovies.Models
{
    public class FilmeGeneroViewModel
    {
        public List<Filme> filmes;
        public SelectList generos;
        public string filmeGenero { get; set; }

    }
}
