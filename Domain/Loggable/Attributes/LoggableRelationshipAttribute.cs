using System;

namespace Domain.Loggable.Attributes
{
    /// <summary>
    /// Utilizar esse atributo para referenciar uma propriedade do tipo relacionamento 1:N
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class LoggableRelationshipAttribute : Attribute
    {
        /// <summary>
        /// Texto que será utilizado antes do Type da classe na qual o atributo está. Será usada tanto na criação quanto na alteração
        /// Ex: 'para o Usuário', propriedade Type da classe User        
        /// </summary>
        public string Preposition { get; set; }

        /// <summary>
        /// Nome da propriedade que deverá ser utilizado o valor
        /// Ex: Name, valor dessa propriedade
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Texto que será colocado no lugar do padrão (somente no log de alteração)
        /// </summary>
        public string PrepositionOnUpdate { get; set; }

        public string PropertyType { get; set; }
    }
}
