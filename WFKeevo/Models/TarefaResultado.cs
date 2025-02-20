namespace WFKeevo.Models
{
    public class TarefaResultado
    {
        public Tarefa Tarefa { get; set; }
        public string MensagemErro { get; set; }
        public bool Sucesso => Tarefa != null;
    }
}
