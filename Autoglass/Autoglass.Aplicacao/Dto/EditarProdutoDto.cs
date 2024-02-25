using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Aplicacao.Dto
{
    public class EditarProdutoDto : IValidatableObject
    {
        [Required]
        public string Descricao { get; set; }
        public DateOnly? DataFabricacao { get; set; }
        public DateOnly? DataValidade { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataFabricacao >= DataValidade)
            {
                yield return new ValidationResult(
                    $"A data de validade deve ser mais recente que a data de fabricação.");
            }
        }
    }
}
