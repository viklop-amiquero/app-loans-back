using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Persona_.Commands.UpdatePersona
{
    public record class UpdatePersonaVM : IRequest<Iresult>
    {

        public int I_STEP { get; set; }
        /// <summary>
        /// insert tabla documento_persona        
        /// </summary>
        public int I_TYPE_DOC_ID { get; set; }

        [RequiredNull(ErrorMessage = "El número de documento de la persona es requerida.")]
        [RegularExpression(@"^[0-9]{8,15}$|^$", ErrorMessage = "El número de documento no es válido (letras, caracteres especiales, espacios o debe contener solo 8 a 15 dígitos).")]
        public string V_NUMBER_DOCUMENT { get; set; }

        /// <summary>
        /// update tabla persona
        /// </summary>
        public int I_PERSON_ID { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El ID de la llave foránea no es válido (letras, caracteres especiales o espacios en blanco)\r\nDATOS NUMERICOS")]
        public string? V_UBIGEO_ID { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El ID de la llave foránea no es válido (letras, caracteres especiales o espacios en blanco)\r\nDATOS NUMERICOS")]
        public string? V_SEX_ID { get; set; }

        [RequiredNull(ErrorMessage = "El nombre de la persona es requerido.")]
        [RegularExpression(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$", ErrorMessage = "El nombre de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_FIRST_NAME { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$", ErrorMessage = "El nombre de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_SECOND_NAME { get; set; }

        [RequiredNull(ErrorMessage = "El apellido paterno de la persona es requerido.")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El apellido paterno de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_PATERNAL_LAST_NAME { get; set; }

        [RequiredNull(ErrorMessage = "El apellido materno de la persona es requerido.")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El apellido materno de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_MOTHER_LAST_NAME { get; set; }

        [RequiredNull(ErrorMessage = "La fecha de nacimiento de la persona es requerido.")]
        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$|^$", ErrorMessage = "La fecha de nacimiento no es valido (más de un espacio, fecha incorrecta, espacios al inicio o al final de la data de entrada).")]
        public string? D_BIRTH_DATE { get; set; }
        
        [RequiredNull(ErrorMessage = "La dirección de domicilio de la persona es requerido.")]
        [RegularExpression(@"^(?:[^\s].*)?$", ErrorMessage = "La dirección de domicilio de la persona no es válida (espacios al inicio o al final de la data de entrada).")]
        public string? V_ADDRESS_HOME { get; set; }

        [RegularExpression(@"^(?:[^\s].*)?$", ErrorMessage = "La dirección del trabajo de la persona no es válida (espacios al inicio o al final de la data de entrada).")]
        public string? V_ADDRESS_WORK { get; set; }

        

        /// <summary>
        /// update tabla Contacto_Emergencia
        /// </summary>

        //public int I_ID_CONTACT_EMERGENCIA { get; set; }
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El nombre del parentesco no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_NAME_RELATIONSHIP { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$", ErrorMessage = "El parentesco no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_RELATIONSHIP { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El celular del parentesco no es válido (letras, caracteres especiales o espacios).")]
        public string? V_MOVIL_PHONE_RELATIONSHIP { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El teléfono del parentesco no es válido (letras, caracteres especiales o espacios).")]
        public string? V_PHONE_RELATIONSHIP { get; set; }

        /// <summary>
        /// update tabla contacto
        /// </summary>
        //public int I_ID_CONTACT { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El teléfono no es válido (letras, caracteres especiales o espacios).")]
        public string? V_PHONE { get; set; }

        [RequiredNull(ErrorMessage = "El celular de la persona es requerida.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El celular no es válido (letras, caracteres especiales o espacios).")]
        public string? V_MOVIL_PHONE { get; set; }

        [RegularExpression(@"^(?:[^\s].*)?$", ErrorMessage = "El correo eletrónico de la persona no es válida (espacios al inicio o al final de la data de entrada).")]
        public string? V_EMAIL { get; set; }
    }
}
