
using AutoMapper;
using Dominio.Dtos;
using Entidades.Models;
using Infraestrutura.Configuration;
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

            //Conexão Banco 
            var StringConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ContextBase>(op => op.UseSqlServer(StringConnection));

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteRequest, Cliente>();
                cfg.CreateMap<Cliente, ClienteResponse>().ReverseMap();
                
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
