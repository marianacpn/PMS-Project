using System;

namespace Domain.Loggable.Attributes
{
    /// <summary>
    /// Utilizar esse atribuito nas propriedades nas quais deseja realizar o auto log e validar de alteração de valor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class LoggableAttribute : Attribute
    {
        /// <summary>
        /// Texto que será utilizado para referencias a propriedade. 
        /// Ex log de criação: (Nome: José),
        /// Ex log de alteração: de (Nome: José) para (Nome: Zé)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// texto que será utilizando antes do valor da propriedade
        /// </summary>
        public string PrefixText { get; set; }

        /// <summary>
        /// texto que será utilizando depois do valor da propriedade.
        /// </summary>
        public string SufixText { get; set; }

    }
}
