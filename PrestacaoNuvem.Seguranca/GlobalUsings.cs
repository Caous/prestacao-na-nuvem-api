//Package Nuggets
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Options;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using AutoMapper;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Principal;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Mvc;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using FluentValidation;


//PrestacaoNuvem
global using PrestacaoNuvem.Seguranca.Infrastructure.Configurations.Security;
global using PrestacaoNuvem.Seguranca.Mappers;
global using PrestacaoNuvem.Seguranca.Domain.Model;
global using PrestacaoNuvem.Seguranca.Infrastructure.Context;
global using PrestacaoNuvem.Seguranca.Dto;
global using PrestacaoNuvem.Seguranca.Infrastructure.Configurations.Repositories.Interfaces;
global using PrestacaoNuvem.Seguranca.Infrastructure.Repositories.Services;
global using PrestacaoNuvem.Seguranca.Infrastructure.Configurations.ContextConfiguration;
global using PrestacaoNuvem.Seguranca.Infrastructure.Configurations.DependecyInjectionConfig;
global using PrestacaoNuvem.Seguranca.Infrastructure.Configurations.SwaggerConfig;
global using PrestacaoNuvem.Seguranca.Infrastructure.Constants;
