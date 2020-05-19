using System;

namespace Domain.Loggable.Attributes
{
    /// <summary>
    /// Utilizar esse atribuito nas propriedade na qual seja um idenficador unico.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class LoggableIdentifierAttribute : Attribute
    {
    }
}
