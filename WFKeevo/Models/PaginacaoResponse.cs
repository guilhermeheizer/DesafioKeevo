using System.Collections.Generic;

namespace WFKeevo.Models
{
    // Classe genérica para resposta de paginação
    public class PaginacaoResponse<T> where T : class
    {
        // Dados vai conter a listagem de dados retornada pela consulta
        public IEnumerable<T> Dados { get; set; }

        // Quantidade total de linhas que atendem ao critério da consulta
        public long TotalLinhas { get; set; }

        // Quantidade de itens que foram pulados
        public int Skip { get; set; }

        // Quantidade de itens retornados na página
        public int Take { get; set; }

        // Construtor para inicializar a classe com dados de paginação
        public PaginacaoResponse(IEnumerable<T> dados, long totalLinhas, int skip, int take)
        {
            Dados = dados;
            TotalLinhas = totalLinhas;
            Skip = skip;
            Take = take;
        }
    }
}
