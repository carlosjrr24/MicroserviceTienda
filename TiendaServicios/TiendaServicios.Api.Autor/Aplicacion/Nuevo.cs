﻿using FluentValidation;
using MediatR;
using System.Security.Cryptography.X509Certificates;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest {


            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set;}

        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x=> x.Apellido).NotEmpty(); 

            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            public readonly ContextoAutor _contexto;

            public Manejador (ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //throw new NotImplementedException();

                var autorLibro = new AutorLibro
                {
                    Nombre= request.Nombre,
                    Apellido= request.Apellido,
                    FechaNacimiento= request.FechaNacimiento,
                    AutorLibroGuid = Guid.NewGuid().ToString()

                };

                _contexto.AutorLibros.Add(autorLibro);
                var valor = await _contexto.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el Autor del libro");

            }
        }



    }
}
