using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Persona_.Commands.NewPersona
{
    public record class NewPersonaVM : IRequest<Iresult>
    {
        public int I_STEP { get; set; }// 1 = STEP 1, 2 = STEP 2..
        /// <summary>
        /// insert tabla documento_persona        
        /// </summary>
        /// <param name="I_TYPE_DOC_ID"></param>
        /// <param name="V_NUMBER_DOCUMENT"></param>
        public int I_TYPE_DOC_ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El número de documento de la persona es requerida.")]
        [RequiredNull(ErrorMessage = "El número de documento de la persona es requerida.")]
        [RegularExpression(@"^[0-9]{8,15}$", ErrorMessage = "El número de documento no es válido (letras, caracteres especiales, espacios o debe contener solo 8 a 15 digitos).")]
        public string V_NUMBER_DOCUMENT { get; set; }
        /// <summary>
        /// Insert tabla persona
        /// </summary>    
        [Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida.")]
        [RequiredNull(ErrorMessage = "La llave foránea es requerida.")]
        public string I_UBIGEO_ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida.")]
        [RequiredNull(ErrorMessage = "La llave foránea es requerida.")]
        public string I_SEX_ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de la persona es requerido.")]
        [RequiredNull(ErrorMessage = "El nombre de la persona es requerido.")]
        [RegularExpression(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]+$", ErrorMessage = "El nombre de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_FIRST_NAME { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$", ErrorMessage = "El nombre de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_SECOND_NAME { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El apellido paterno de la persona es requerido.")]
        [RequiredNull(ErrorMessage = "El apellido paterno de la persona es requerido.")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El apellido paterno de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_PATERNAL_LAST_NAME { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El apellido materno de la persona es requerido.")]
        [RequiredNull(ErrorMessage = "El apellido materno de la persona es requerido.")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El apellido materno de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_MOTHER_LAST_NAME { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha de nacimiento de la persona es requerido.")]
        [RequiredNull(ErrorMessage = "La fecha de nacimiento de la persona es requerido.")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$", ErrorMessage = "La fecha de nacimiento no es valido (más de un espacio, fecha incorrecta, espacios al inicio o al final de la data de entrada).")]
        public string D_BIRTH_DATE { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La dirección de domicilio de la persona es requerido.")]
        [RequiredNull(ErrorMessage = "La dirección de domicilio de la persona es requerido.")]
        [RegularExpression(@"^[^\s].*[^\s]$", ErrorMessage = "La dirección de domicilio de la persona no es válida (espacios al inicio o al final de la data de entrada).")]
        public string V_ADDRESS_HOME { get; set; }

        [RegularExpression(@"^(?:[^\s].*)?", ErrorMessage = "La dirección del trabajo de la persona no es válida (espacios al inicio o al final de la data de entrada).")]
        public string? V_ADDRESS_WORK { get; set; }
        /// <summary>
        /// insert tabla Contacto_Emergencia
        /// </summary>
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El nombre del parentesco no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_NAME_RELATIONSHIP { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]+$", ErrorMessage = "El parentesco no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_RELATIONSHIP { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El teléfono no es válido (letras, caracteres especiales o espacios).")]
        public string? V_MOVIL_PHONE_RELATIONSHIP { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El teléfono no es válido (letras, caracteres especiales o espacios).")]
        public string? V_PHONE_RELATIONSHIP { get; set; }

        /// <summary>
        /// insert tabla contacto
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El teléfono no es válido (letras, caracteres especiales o espacios).")]
        public string? V_PHONE { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El celular de la persona es requerida.")]
        [RequiredNull(ErrorMessage = "El celular de la persona es requerida.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El celular no es válido (letras, caracteres especiales o espacios).")]
        public string V_MOVIL_PHONE { get; set; }

        [RegularExpression(@"^(?:[^\s].*)?", ErrorMessage = "El correo eletrónico de la persona no es válida (espacios al inicio o al final de la data de entrada).")]
        public string? V_EMAIL { get; set; }


    }
}
