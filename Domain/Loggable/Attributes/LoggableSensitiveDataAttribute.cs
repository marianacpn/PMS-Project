using System;

namespace Domain.Loggable.Attributes
{
    /// <summary>
    /// Utilizar este atributo nas propriedades que deseja não exibir o seu valor no log, porém, ainda assim, deve ser avisado que houve alteração.
    /// Obs: Necessário utilizar em conjunto com o Atributo [AutoLogAttribute]
    /// </summary>
    public class LoggableSensitiveDataAttribute : Attribute
    {
        /// <summary>
        /// Texto utilizado no lugar do valor da propriedade      
        /// Ex.: "para" (alterou o valor da senha)
        /// É possivel utilizar a chave ({0}) para colocar o valor do AutoLogAttribute.Name no texto
        /// Ex.: MessageOnUpdate = "alterou o valor da {0}"
        /// </summary>
        public string MessageOnUpdate { get; set; }
    }
}
