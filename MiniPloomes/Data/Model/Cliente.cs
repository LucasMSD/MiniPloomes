﻿namespace MiniPloomes.Data.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int UsuarioId { get; set;}
        public DateTime Created { get; set; }
    }
}
