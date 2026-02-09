namespace SampleMcpServer.Models
{
    public class CepResponse
    {
        /// <summary>
        /// Cep consultado
        /// </summary>
        public string Cep { get; set; }

        /// <summary>
        /// Logradouro do endereço
        /// </summary>
        public string Logradouro { get; set; }
        
        /// <summary>
        /// Complemento do endereço
        /// </summary>
        public string Complemento { get; set; }
        
        /// <summary>
        /// Bairro do endereço
        /// </summary>
        public string Bairro { get; set; }
        
        /// <summary>
        /// Cidade do endereço
        /// </summary>
        public string Cidade { get; set; }
        
        /// <summary>
        /// Unidade federativa (estado) do endereço
        /// </summary>
        public string Uf { get; set; }

        /// <summary>
        /// Código DDD do telefone
        /// </summary>
        public string Ddd { get; set; }

        /// <summary>
        /// Indica se a resposta foi obtida do cache
        /// </summary>
        public bool Cached { get; set; }
    }
}