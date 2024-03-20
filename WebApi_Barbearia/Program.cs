
using AutoMapper;
using Dominio.Dtos.Agendamento;
using Dominio.Dtos.Cliente;
using Dominio.Dtos.Servico;
using Entidades.Models;
using Identity;
using Infraestrutura.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Barbearia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Conex�o Banco 
            var StringConnection = builder.Configuration.GetConnectionString("ConnectionNotbook");
            builder.Services.AddDbContext<ContextBase>(op => op.UseSqlServer(StringConnection));
            builder.Services.AddDbContext<ContextBaseIdentity>(op => op.UseSqlServer(StringConnection));

            //Conexão Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ContextBaseIdentity>().AddDefaultTokenProviders();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteRequest, Cliente>();
                cfg.CreateMap<Cliente, ClienteResponse>().ReverseMap();
                cfg.CreateMap<ServicoRequest, Servico>(); 
                cfg.CreateMap<ServicoResponse, Servico>().ReverseMap();
                cfg.CreateMap<AgendamentoRequest, Agendamento>().ForMember(x => x.Servicos, opt => opt.MapFrom(x => x.ServicoId));
                cfg.CreateMap<Agendamento, AgendamentoResponse>().ForMember(x => x.Cliente, opt => opt.MapFrom(x => x.Cliente.Nome))
                                                                 .ForMember(x => x.Servicos , opt => opt.MapFrom(x => x.Servicos));


            });

            IMapper mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
