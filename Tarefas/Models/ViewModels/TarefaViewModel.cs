using System.Collections.Generic;

namespace Tarefas.Models.ViewModels
{
    public class TarefaViewModel
    {
        public List<TarefaItem> TarefaList { get; set; }
        public TarefaItem Tarefa { get; set; }
    }
}
