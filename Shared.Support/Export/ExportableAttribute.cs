using System;

namespace Shared.Support.Export
{
    /// <summary>
    /// Utilizar atributo para marcar propriedade para Export
    /// </summary>
    public class ExportableAttribute : Attribute
    {
        /// <summary>
        /// Texto que será utilizado para nomear a coluna
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Numero de ordem para as colunas no Export
        /// </summary>
        public int ColumnOrder { get; set; }

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
