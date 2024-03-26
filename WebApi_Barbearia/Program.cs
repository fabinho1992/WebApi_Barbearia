
using AutoMapper;
using Dominio.Dtos.Agendamento;
using Dominio.Dtos.Cliente;
using Dominio.Dtos.Servico;
using Entidades.Models;
using Identity;
using Identity.Model;
using Infraestrutura.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi_Barbearia.Controllers;
using WebApi_Barbearia.Services;

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
            //Configurando Swagger
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Barbearia", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
            });

            //Conex�o Banco 
            var StringConnection = builder.Configuration.GetConnectionString("ConnectionPc");
            builder.Services.AddDbContext<ContextBase>(op => op.UseSqlServer(StringConnection));
            builder.Services.AddDbContext<ContextBaseIdentity>(op => op.UseSqlServer(StringConnection));


            //Conexão Identity
            builder.Services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<ContextBaseIdentity>().AddDefaultTokenProviders();

            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<UsuarioService>();
            

            //TokenJwt
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(option =>
               option.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,

                   ValidIssuer = builder.Configuration["Configuration: Issuer"],
                   ValidAudience = builder.Configuration["Configuration: Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt: KeyJwt"]))
               });

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
