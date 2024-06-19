using FluentValidation;

namespace HRA.Application.UseCases.Persona_.Commands.UpdatePersona
{
    public class PersonaValidate : AbstractValidator<UpdatePersonaVM>
    {
        public PersonaValidate()
        {

            RuleFor(v => v.V_UBIGEO_ID)
                .Matches(@"^[0-9]*$").WithMessage("El ID de Ubigeo no es válido (letras, caracteres o espacios).");

            RuleFor(v => v.V_SEX_ID)
                .Matches(@"^[0-9]*$").WithMessage("El ID de Ubigeo no es válido (letras, caracteres o espacios).");

            RuleFor(v => v.V_FIRST_NAME)
                .Matches(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$").WithMessage("El nombre de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 25).WithMessage("Ingrese como maximo de 25 caracteres.");

            RuleFor(v => v.V_SECOND_NAME)
                .Matches(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$").WithMessage("El segundo nombre de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 25).WithMessage("Ingrese como maximo de 25 caracteres.");

            RuleFor(v => v.V_PATERNAL_LAST_NAME)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El apellido paterno de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 50).WithMessage("Ingrese como maximo de 50 caracteres.");

            RuleFor(v => v.V_MOTHER_LAST_NAME)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El apellido materno de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 50).WithMessage("Ingrese como maximo de 50 caracteres.");

            RuleFor(v => v.D_BIRTH_DATE)
                .Matches(@"^(0[1-9]|1[0-9]|2[0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$|^$").WithMessage("La fecha de nacimiento no es valido (más de un espacio, fecha incorrecta, espacios al inicio o al final de la data de entrada).");

            RuleFor(v => v.V_ADDRESS_HOME)                
                .Matches("^(?:[^\\s].*)?$").WithMessage("La dirección de domicilio de la persona no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 250).WithMessage("Ingrese como maximo de 250 caracteres.");

            RuleFor(v => v.V_ADDRESS_WORK)
                .Matches("^(?:[^\\s].*)?$").WithMessage("La dirección del trabajo de la persona no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 250).WithMessage("Ingrese como maximo de 250 caracteres.");

            // insert tabla documento_persona        
            RuleFor(v => v.V_NUMBER_DOCUMENT)
                .Matches(@"^[0-9]{8,15}$|^$").WithMessage("El número de documento no es válido (letras, caracteres especiales, espacios o debe contener solo 8 a 15 dígitos).")
                .Length(0, 15).WithMessage("Ingrese como maximo de 8 a 15 dígitos.");

            //La validación de la tabla contacto_emergencia

            RuleFor(v => v.V_NAME_RELATIONSHIP)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("Los nombres del parentesco de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 200).WithMessage("Ingrese como maximo de 200 caracteres.");

            RuleFor(v => v.V_RELATIONSHIP)
                .Matches(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$").WithMessage("El parentesco de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 200).WithMessage("Ingrese como maximo de 200 caracteres.");

            RuleFor(v => v.V_MOVIL_PHONE_RELATIONSHIP)
                .Matches(@"^[0-9]*$").WithMessage("El celular del parentesco no es válido (letras, caracteres especiales o espacios).")
                .Length(0, 9).WithMessage("Ingrese como máximo de 9 caracteres.");

            RuleFor(v => v.V_PHONE_RELATIONSHIP)
                .Matches(@"^[0-9]*$").WithMessage("El telefono del parentesco no es válido (letras, caracteres especiales o espacios).")
                .Length(0, 20).WithMessage("Ingrese como máximo de 20 caracteres.");

            //La validación de la tabla contacto
            RuleFor(v => v.V_PHONE)
                .Matches(@"^[0-9]*$").WithMessage("El telefono no es válido (letras, caracteres especiales o espacios).")
                .Length(0, 20).WithMessage("Ingrese como máximo de 20 caracteres.");

            RuleFor(v => v.V_MOVIL_PHONE)
                .Matches(@"^[0-9]*$").WithMessage("El celular no es válido (letras, caracteres especiales o espacios).")
                .Length(0, 9).WithMessage("Ingrese como maximo de 9 caracteres.");

            RuleFor(v => v.V_EMAIL)
                .Matches("^(?:[^\\s].*)?$").WithMessage("El correo elctrónico de la persona no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 50).WithMessage("Ingrese como máximo de 50 caracteres.");
        }
    }
}
