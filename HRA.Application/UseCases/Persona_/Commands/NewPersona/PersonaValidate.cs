using FluentValidation;

namespace HRA.Application.UseCases.Persona_.Commands.NewPersona
{
    public class PersonaValidate : AbstractValidator<NewPersonaVM>
    {
        public PersonaValidate()
        {
            RuleFor(v => v.I_UBIGEO_ID)
                .NotEmpty().WithMessage("La llave foránea de Ubigeo es requerido.")
                .Matches(@"^[0-9]+$").WithMessage("El ID de la llave foranes no es válido (letras, caracteres especiales o espacios).");

            RuleFor(v => v.I_SEX_ID)
                .NotEmpty().WithMessage("La llave foránea de Sexo es requerido.")
                .Matches(@"^[0-9]+$").WithMessage("El ID de la llave foranes no es válido (letras, caracteres especiales o espacios).");

            RuleFor(v => v.V_FIRST_NAME)
                .NotEmpty().WithMessage("El nombre de la persona es requerido.")
                .Matches(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]+$").WithMessage("El nombre de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(1, 25).WithMessage("Ingrese como maximo de 25 caracteres.");

            RuleFor(v => v.V_SECOND_NAME)
                .Matches(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$").WithMessage("El segundo nombre de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 25).WithMessage("Ingrese como maximo de 25 caracteres.");
            
            RuleFor(v => v.V_PATERNAL_LAST_NAME)
                .NotEmpty().WithMessage("El apellido paterno de la persona es requerido.")
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El apellido paterno de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(1, 50).WithMessage("Ingrese como maximo de 50 caracteres.");
            
            RuleFor(v => v.V_MOTHER_LAST_NAME)
                .NotEmpty().WithMessage("El apellido materno de la persona es requerido.")
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El apellido materno de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(1, 50).WithMessage("Ingrese como maximo de 50 caracteres.");

            RuleFor(v => v.D_BIRTH_DATE)
                .Matches(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}").WithMessage("La fecha de nacimiento no es valido (más de un espacio, fecha incorrecta, espacios al inicio o al final de la data de entrada).");

            RuleFor(v => v.V_ADDRESS_HOME)
                .NotEmpty().WithMessage("La dirección de domicilio de la persona es requerido.")
                .Matches("^[^\\s].*[^\\s]$").WithMessage("La dirección de domicilio de la persona no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(1, 250).WithMessage("Ingrese como maximo de 250 caracteres.");

            RuleFor(v => v.V_ADDRESS_WORK)
                .Matches("^(?:[^\\s].*)?").WithMessage("La dirección del trabajo de la persona no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 250).WithMessage("Ingrese como maximo de 250 caracteres.");

            // insert tabla documento_persona        
            RuleFor(v=> v.V_NUMBER_DOCUMENT)
                .NotEmpty().WithMessage("El número de documento de la persona es requerida.")
                .Matches(@"^[0-9]{8,15}$").WithMessage("El número de documento no es válido (letras, caracteres especiales, espacios o debe contener solo 8 a 15 digitos).")
                .Length(1, 15).WithMessage("Ingrese como maximo de 15 dígitos.");


            //La validación de la tabla contacto_emergencia

            RuleFor(v => v.V_NAME_RELATIONSHIP)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("Los nombres del parentesco de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 200).WithMessage("Ingrese como maximo de 200 caracteres.");

            RuleFor(v => v.V_RELATIONSHIP)
                .Matches(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$").WithMessage("El parentesco de la persona no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 200).WithMessage("Ingrese como maximo de 200 caracteres.");

            RuleFor(v => v.V_MOVIL_PHONE_RELATIONSHIP)
                .Matches(@"^[0-9]*$").WithMessage("El celular de parentesco no es válido (letras, caracteres especiales o espacios).")
                .Length(0, 20).WithMessage("Ingrese como máximo de 20 caracteres.");

            RuleFor(v => v.V_PHONE_RELATIONSHIP)
                .Matches(@"^[0-9]*$").WithMessage("El telefonodel parentesco no es válido (letras, caracteres especiales o espacios).")
                .Length(0, 20).WithMessage("Ingrese como máximo de 20 caracteres.");


            //La validación de la tabla contacto
            RuleFor(v => v.V_PHONE)
                .Matches(@"^[0-9]*$").WithMessage("El telefono no es válido (letras, caracteres especiales o espacios).")
                .Length(0, 20).WithMessage("Ingrese como máximo de 20 caracteres.");

            RuleFor(v => v.V_MOVIL_PHONE)
                .NotEmpty().WithMessage("El celular de la persona es requerida.")
                .Matches(@"^[0-9]+$").WithMessage("El celular no es válido (letras, caracteres especiales o espacios).")
                .Length(1, 9).WithMessage("Ingrese como maximo de 9 caracteres.");

            RuleFor(v => v.V_EMAIL)
                .Matches("^(?:[^\\s].*)?").WithMessage("El correo elctrónico de la persona no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 50).WithMessage("Ingrese como máximo de 50 caracteres.");

        }
    }
}
